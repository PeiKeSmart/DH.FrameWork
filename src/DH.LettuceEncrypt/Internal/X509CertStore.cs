using Certes;
using Certes.Acme;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NewLife.Log;

using System.Security.Cryptography.X509Certificates;

namespace LettuceEncrypt.Internal;

internal class X509CertStore : ICertificateSource, ICertificateRepository, IDisposable {
    private readonly X509Store _store;
    private readonly IOptions<LettuceEncryptOptions> _options;
    private readonly ILogger<X509CertStore> _logger;

    public bool AllowInvalidCerts { get; set; }

    public X509CertStore(IOptions<LettuceEncryptOptions> options, ILogger<X509CertStore> logger)
    {
        _store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        _store.Open(OpenFlags.ReadWrite);
        _options = options;
        _logger = logger;
    }

    public Task<IEnumerable<X509Certificate2>> GetCertificatesAsync(CancellationToken cancellationToken)
    {
        XTrace.WriteLine($"��������X509CertStore:");

        var domainNames = new HashSet<string>(_options.Value.DomainNames);
        var result = new List<X509Certificate2>();

        var certs = _store.Certificates.Find(X509FindType.FindByTimeValid,
            DateTime.Now,
            validOnly: !AllowInvalidCerts);

        foreach (var cert in certs)
        {
            if (!cert.HasPrivateKey)
            {
                continue;
            }

            foreach (var dnsName in X509CertificateHelpers.GetAllDnsNames(cert))
            {
                if (domainNames.Contains(dnsName))
                {
                    result.Add(cert);
                    break;
                }
            }
        }

        return Task.FromResult(result.AsEnumerable());
    }

    public Task SaveAsync(X509Certificate2 certificate, IKey privateKey, CertificateChain acmeCert, CancellationToken cancellationToken)
    {
        try
        {
            _store.Add(certificate);
        }
        catch (Exception ex)
        {
            _logger.LogError(0, ex, "�޷���֤�鱣�浽�洢");
            throw;
        }

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _store.Close();
    }
}
