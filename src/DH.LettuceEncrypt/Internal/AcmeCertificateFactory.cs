﻿using Certes;
using Certes.Acme;
using Certes.Acme.Resource;

using LettuceEncrypt.Accounts;
using LettuceEncrypt.Acme;
using LettuceEncrypt.Internal.PfxBuilder;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NewLife.Log;

using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LettuceEncrypt.Internal;

internal class AcmeCertificateFactory
{
    private readonly AcmeClientFactory _acmeClientFactory;
    private readonly TermsOfServiceChecker _tosChecker;
    private readonly IOptions<LettuceEncryptOptions> _options;
    private readonly IHttpChallengeResponseStore _challengeStore;
    private readonly IAccountStore _accountRepository;
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly TlsAlpnChallengeResponder _tlsAlpnChallengeResponder;
    private readonly IDnsChallengeProvider _dnsChallengeProvider;
    private readonly ICertificateAuthorityConfiguration _certificateAuthority;
    private readonly IPfxBuilderFactory _pfxBuilderFactory;
    private readonly TaskCompletionSource<object?> _appStarted = new();
    private AcmeClient? _client;
    private IKey? _acmeAccountKey;

    public AcmeCertificateFactory(
        AcmeClientFactory acmeClientFactory,
        TermsOfServiceChecker tosChecker,
        IOptions<LettuceEncryptOptions> options,
        IHttpChallengeResponseStore challengeStore,
        ILogger<AcmeCertificateFactory> logger,
        IHostApplicationLifetime appLifetime,
        TlsAlpnChallengeResponder tlsAlpnChallengeResponder,
        ICertificateAuthorityConfiguration certificateAuthority,
        IDnsChallengeProvider dnsChallengeProvider,
        IPfxBuilderFactory pfxBuilderFactory,
        IAccountStore? accountRepository = null)
    {
        _acmeClientFactory = acmeClientFactory;
        _tosChecker = tosChecker;
        _options = options;
        _challengeStore = challengeStore;
        _logger = logger;
        _appLifetime = appLifetime;
        _tlsAlpnChallengeResponder = tlsAlpnChallengeResponder;
        _dnsChallengeProvider = dnsChallengeProvider;
        _certificateAuthority = certificateAuthority;
        _pfxBuilderFactory = pfxBuilderFactory;

        appLifetime.ApplicationStarted.Register(() => _appStarted.TrySetResult(null));
        if (appLifetime.ApplicationStarted.IsCancellationRequested)
        {
            _appStarted.TrySetResult(null);
        }

        _accountRepository = accountRepository ?? new FileSystemAccountStore(logger, certificateAuthority);
    }

    public async Task<AccountModel> GetOrCreateAccountAsync(CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAccountAsync(cancellationToken);

        _acmeAccountKey = account != null
            ? KeyFactory.FromDer(account.PrivateKey)
            : KeyFactory.NewKey(Certes.KeyAlgorithm.ES256);

        _client = _acmeClientFactory.Create(_acmeAccountKey);

        if (account != null && await ExistingAccountIsValidAsync())
        {
            return account;
        }

        return await CreateAccount(cancellationToken);
    }

    private async Task<AccountModel> CreateAccount(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (_client == null || _acmeAccountKey == null)
        {
            throw new InvalidOperationException();
        }

        var tosUri = await _client.GetTermsOfServiceAsync();

        _tosChecker.EnsureTermsAreAccepted(tosUri);

        var options = _options.Value;
        _logger.LogInformation("Creating new account for {email}", options.EmailAddress);
        var accountId = await _client.CreateAccountAsync(options.EmailAddress);

        var accountModel = new AccountModel
        {
            Id = accountId,
            EmailAddresses = new[] { options.EmailAddress },
            PrivateKey = _acmeAccountKey.ToDer(),
        };

        await _accountRepository.SaveAccountAsync(accountModel, cancellationToken);

        return accountModel;
    }

    private async Task<bool> ExistingAccountIsValidAsync()
    {
        if (_client == null)
        {
            throw new InvalidOperationException();
        }

        // double checks the account is still valid
        Account existingAccount;
        try
        {
            existingAccount = await _client.GetAccountAsync();
        }
        catch (AcmeRequestException exception)
        {
            _logger.LogWarning(
                "An account key was found, but could not be matched to a valid account. Validation error: {acmeError}",
                exception.Error);
            return false;
        }

        if (existingAccount.Status != AccountStatus.Valid)
        {
            _logger.LogWarning(
                "An account key was found, but the account is no longer valid. Account status: {status}." +
                "A new account will be registered.",
                existingAccount.Status);
            return false;
        }

        _logger.LogInformation("Using existing account for {contact}", existingAccount.Contact);

        if (existingAccount.TermsOfServiceAgreed != true)
        {
            var tosUri = await _client.GetTermsOfServiceAsync();
            _tosChecker.EnsureTermsAreAccepted(tosUri);
            await _client.AgreeToTermsOfServiceAsync();
        }

        return true;
    }

    public async Task<(X509Certificate2, IKey, CertificateChain)> CreateCertificateAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (_client == null)
        {
            throw new InvalidOperationException();
        }

        IOrderContext? orderContext = null;
        var orders = await _client.GetOrdersAsync();
        if (orders.Any())
        {
            var expectedDomains = new HashSet<string>(_options.Value.DomainNames);
            foreach (var order in orders)
            {
                var orderDetails = await _client.GetOrderDetailsAsync(order);
                if (orderDetails.Status != OrderStatus.Pending)
                {
                    continue;
                }

                var orderDomains = orderDetails
                    .Identifiers
                    .Where(i => i.Type == IdentifierType.Dns)
                    .Select(s => s.Value);

                if (expectedDomains.SetEquals(orderDomains))
                {
                    XTrace.Log.Debug("找到证书的现有订单");
                    orderContext = order;
                    break;
                }
            }
        }

        if (orderContext == null)
        {
            XTrace.Log.Debug("为证书创建新订单");
            orderContext = await _client.CreateOrderAsync(_options.Value.DomainNames);
        }

        cancellationToken.ThrowIfCancellationRequested();
        var authorizations = await _client.GetOrderAuthorizations(orderContext);

        cancellationToken.ThrowIfCancellationRequested();
        await Task.WhenAll(BeginValidateAllAuthorizations(authorizations, cancellationToken));

        cancellationToken.ThrowIfCancellationRequested();
        return await CompleteCertificateRequestAsync(orderContext, cancellationToken);
    }

    private IEnumerable<Task> BeginValidateAllAuthorizations(IEnumerable<IAuthorizationContext> authorizations,
        CancellationToken cancellationToken)
    {
        foreach (var authorization in authorizations)
        {
            yield return ValidateDomainOwnershipAsync(authorization, cancellationToken);
        }
    }

    private async Task ValidateDomainOwnershipAsync(IAuthorizationContext authorizationContext,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (_client == null)
        {
            throw new InvalidOperationException();
        }

        var authorization = await _client.GetAuthorizationAsync(authorizationContext);
        var domainName = authorization.Identifier.Value;

        if (authorization.Status == AuthorizationStatus.Valid)
        {
            // Short circuit if authorization is already complete
            return;
        }

        _logger.LogDebug("请求授权为{domainName}创建证书", domainName);

        cancellationToken.ThrowIfCancellationRequested();

        var validators = new List<DomainOwnershipValidator>();

        if (_tlsAlpnChallengeResponder.IsEnabled)
        {
            validators.Add(new TlsAlpn01DomainValidator(
                _tlsAlpnChallengeResponder, _appLifetime, _client, _logger, domainName));
        }

        if (_options.Value.AllowedChallengeTypes.HasFlag(ChallengeType.Http01))
        {
            validators.Add(new Http01DomainValidator(
                _challengeStore, _appLifetime, _client, _logger, domainName, _options.Value));
        }

        if (_options.Value.AllowedChallengeTypes.HasFlag(ChallengeType.Dns01))
        {
            validators.Add(new Dns01DomainValidator(
                _dnsChallengeProvider, _appLifetime, _client, _logger, domainName));
        }

        if (validators.Count == 0)
        {
            var challengeTypes = string.Join(", ", Enum.GetNames(typeof(ChallengeType)));
            throw new InvalidOperationException(
                "找不到验证域所有权的方法。" +
                "确保至少配置了其中一种挑战类型：" + challengeTypes);
        }

        foreach (var validator in validators)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                await validator.ValidateOwnershipAsync(authorizationContext, cancellationToken);
                // 如果验证失败，则会引发上面的方法。如果没有出现异常，我们假设验证已成功完成。
                return;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "使用{validatorType}进行验证失败，出现错误: {error}",
                    validator.GetType().Name, ex.Message);
            }
        }

        throw new InvalidOperationException($"无法验证域名的所有权 '{domainName}'");
    }

    private async Task<(X509Certificate2, IKey, CertificateChain)> CompleteCertificateRequestAsync(IOrderContext order,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (_client == null)
        {
            throw new InvalidOperationException();
        }

        var commonName = _options.Value.DomainNames[0];
        XTrace.Log.Debug($"正在为{commonName}创建证书");

        var csrInfo = new CsrInfo
        {
            CommonName = commonName,
        };
        var privateKey = KeyFactory.NewKey((Certes.KeyAlgorithm)_options.Value.KeyAlgorithm);
        var acmeCert = await _client.GetCertificateAsync(csrInfo, privateKey, order);

        _logger.LogAcmeAction("NewCertificate");

        var pfxBuilder = CreatePfxBuilder(acmeCert, privateKey);
        var pfx = pfxBuilder.Build("HTTPS Cert - " + _options.Value.DomainNames, string.Empty);
        return (new X509Certificate2(pfx, string.Empty, X509KeyStorageFlags.Exportable), privateKey, acmeCert);
    }

    internal IPfxBuilder CreatePfxBuilder(CertificateChain certificateChain, IKey certKey)
    {
        var pfxBuilder = _pfxBuilderFactory.FromChain(certificateChain, certKey);

        XTrace.Log.Debug($"在生成pfx证书文件之前，向证书添加{_options.Value.AdditionalIssuers.Length + _certificateAuthority.IssuerCertificates.Length}个其他颁发者");

        foreach (var issuer in _options.Value.AdditionalIssuers.Concat(_certificateAuthority.IssuerCertificates))
        {
            pfxBuilder.AddIssuer(Encoding.UTF8.GetBytes(issuer));
        }

        return pfxBuilder;
    }
}
