using Microsoft.Extensions.Hosting;

using NewLife.Log;

using System.Security.Cryptography.X509Certificates;

namespace LettuceEncrypt.Internal;

internal class StartupCertificateLoader : IHostedService
{
    private readonly IEnumerable<ICertificateSource> _certSources;
    private readonly CertificateSelector _selector;

    public StartupCertificateLoader(
        IEnumerable<ICertificateSource> certSources,
        CertificateSelector selector)
    {
        _certSources = certSources;
        _selector = selector;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var allCerts = new List<X509Certificate2>();

        XTrace.WriteLine($"进来了吗？StartupCertificateLoader");

        foreach (var certSource in _certSources)
        {
            var certs = await certSource.GetCertificatesAsync(cancellationToken);
            allCerts.AddRange(certs);
        }

        // 请先添加较新的证书。这样可以避免对旧证书进行潜在的不必要的证书验证
        foreach (var cert in allCerts.OrderByDescending(c => c.NotAfter))
        {
            _selector.Add(cert);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
