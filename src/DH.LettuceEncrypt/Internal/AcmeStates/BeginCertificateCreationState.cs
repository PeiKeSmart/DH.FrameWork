using Certes;
using Certes.Acme;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NewLife.Log;

using System.Security.Cryptography.X509Certificates;

namespace LettuceEncrypt.Internal.AcmeStates;

internal class BeginCertificateCreationState : AcmeState
{
    private readonly ILogger<ServerStartupState> _logger;
    private readonly IOptions<LettuceEncryptOptions> _options;
    private readonly AcmeCertificateFactory _acmeCertificateFactory;
    private readonly CertificateSelector _selector;
    private readonly IEnumerable<ICertificateRepository> _certificateRepositories;

    public BeginCertificateCreationState(
        AcmeStateMachineContext context, ILogger<ServerStartupState> logger,
        IOptions<LettuceEncryptOptions> options, AcmeCertificateFactory acmeCertificateFactory,
        CertificateSelector selector, IEnumerable<ICertificateRepository> certificateRepositories)
        : base(context)
    {
        _logger = logger;
        _options = options;
        _acmeCertificateFactory = acmeCertificateFactory;
        _selector = selector;
        _certificateRepositories = certificateRepositories;
    }

    public override async Task<IAcmeState> MoveNextAsync(CancellationToken cancellationToken)
    {
        XTrace.WriteLine($"��������BeginCertificateCreationState");

        var domainNames = _options.Value.DomainNames;

        try
        {
            var account = await _acmeCertificateFactory.GetOrCreateAccountAsync(cancellationToken);
            XTrace.Log.Info($"ʹ���ʻ� {account.Id}");

            XTrace.Log.Info($"Ϊ{string.Join(",", domainNames)}����֤��");

            var cert = await _acmeCertificateFactory.CreateCertificateAsync(cancellationToken);

            XTrace.Log.Info($"������֤�� {cert.Item1.Subject} ({cert.Item1.Thumbprint})");

            await SaveCertificateAsync(cert.Item1, cert.Item2, cert.Item3, cancellationToken);
        }
        catch (Exception ex)
        {
            XTrace.WriteLine($"�޷��Զ�Ϊ{domainNames}����֤��");
            XTrace.WriteException(ex);
            throw;
        }

        return MoveTo<CheckForRenewalState>();
    }

    private async Task SaveCertificateAsync(X509Certificate2 cert, IKey privateKey, CertificateChain acmeCert, CancellationToken cancellationToken)
    {
        _selector.Add(cert);

        var saveTasks = new List<Task>
        {
            Task.Delay(TimeSpan.FromMinutes(5), cancellationToken)
        };

        var errors = new List<Exception>();
        foreach (var repo in _certificateRepositories)
        {
            try
            {
                saveTasks.Add(repo.SaveAsync(cert, privateKey, acmeCert, cancellationToken));
            }
            catch (Exception ex)
            {
                // ͬ��������ܻ�����ʧ��
                errors.Add(ex);
            }
        }

        await Task.WhenAll(saveTasks);

        if (errors.Count > 0)
        {
            throw new AggregateException("�޷���֤�鱣�浽�洢��", errors);
        }
    }
}
