using Certes;
using Certes.Acme;
using Certes.Acme.Resource;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LettuceEncrypt.Internal;

internal class AcmeClient
{
    private readonly AcmeContext _context;
    private readonly ILogger<AcmeClient> _logger;
    private readonly IOptions<LettuceEncryptOptions> _options;
    private readonly IKey _acmeAccountKey;
    private IAccountContext? _accountContext;

    public AcmeClient(ILogger<AcmeClient> logger, IOptions<LettuceEncryptOptions> options, Uri directoryUri, IKey acmeAccountKey)
    {
        _logger = logger;
        _options = options;
        _acmeAccountKey = acmeAccountKey;
        _logger.LogInformation("Using certificate authority {directoryUri}", directoryUri);
        _context = new AcmeContext(directoryUri, acmeAccountKey);
    }

    public async Task<Account> GetAccountAsync()
    {
        _logger.LogAcmeAction("FetchAccount");
        _accountContext = await _context.Account().ConfigureAwait(false);
        _logger.LogAcmeAction("FetchAccountDetails", _accountContext);
        return await _accountContext.Resource().ConfigureAwait(false);
    }

    public IKey? GetAccountKey()
    {
        _logger.LogAcmeAction("GetAccountKey");
        return _acmeAccountKey;
    }

    public async Task<int> CreateAccountAsync(string emailAddress)
    {
        _logger.LogAcmeAction("NewAccount");
        var eabCredentials = _options.Value.EabCredentials;
        _accountContext = await _context.NewAccount(emailAddress, termsOfServiceAgreed: true, eabKeyId: eabCredentials.EabKeyId, eabKey: eabCredentials.EabKey, eabKeyAlg: eabCredentials.EabKeyAlg).ConfigureAwait(false);

        return int.TryParse(_accountContext.Location.Segments.Last(), out var accountId)
            ? accountId
            : 0;
    }

    public async Task<Uri> GetTermsOfServiceAsync()
    {
        _logger.LogAcmeAction("FetchTOS");
        return await _context.TermsOfService().ConfigureAwait(false);
    }

    public async Task AgreeToTermsOfServiceAsync()
    {
        if (_accountContext == null)
        {
            throw MissingAccountContext();
        }
        _logger.LogAcmeAction("UpdateTOS");
        await _accountContext.Update(agreeTermsOfService: true).ConfigureAwait(false);
    }

    public async Task<IEnumerable<IOrderContext>> GetOrdersAsync()
    {
        if (_accountContext == null)
        {
            throw MissingAccountContext();
        }

        _logger.LogAcmeAction("FetchOrderList");
        var orderListContext = await _accountContext.Orders().ConfigureAwait(false);

        if (orderListContext == null)
        {
            return Enumerable.Empty<IOrderContext>();
        }
        _logger.LogAcmeAction("FetchOrderDetails", orderListContext);
        return await orderListContext.Orders().ConfigureAwait(false);
    }

    public async Task<IOrderContext> CreateOrderAsync(string[] domainNames)
    {
        _logger.LogAcmeAction("NewOrder");
        return await _context.NewOrder(domainNames).ConfigureAwait(false);
    }

    public async Task<Order> GetOrderDetailsAsync(IOrderContext order)
    {
        _logger.LogAcmeAction("FetchOrderDetails", order);
        return await order.Resource().ConfigureAwait(false);
    }

    public async Task<IEnumerable<IAuthorizationContext>> GetOrderAuthorizations(IOrderContext orderContext)
    {
        _logger.LogAcmeAction("FetchAuthorizations", orderContext);
        return await orderContext.Authorizations().ConfigureAwait(false);
    }

    public async Task<Authorization> GetAuthorizationAsync(IAuthorizationContext authorizationContext)
    {
        _logger.LogAcmeAction("FetchAuthorizationDetails", authorizationContext);
        return await authorizationContext.Resource().ConfigureAwait(false);
    }

    public async Task<IChallengeContext> CreateChallengeAsync(IAuthorizationContext authorizationContext, string challengeType)
    {
        _logger.LogAcmeAction("CreateChallenge", authorizationContext);
        return await authorizationContext.Challenge(challengeType).ConfigureAwait(false);
    }

    public async Task<Challenge> ValidateChallengeAsync(IChallengeContext httpChallenge)
    {
        _logger.LogAcmeAction("ValidateChallenge", httpChallenge);
        return await httpChallenge.Validate().ConfigureAwait(false);
    }

    public async Task<CertificateChain> GetCertificateAsync(CsrInfo csrInfo, IKey privateKey, IOrderContext order)
    {
        _logger.LogAcmeAction("GenerateCertificate", order);
        return await order.Generate(csrInfo, privateKey).ConfigureAwait(false);
    }

    private static Exception MissingAccountContext() => new InvalidOperationException("Account wasn't initialized yet");
}
