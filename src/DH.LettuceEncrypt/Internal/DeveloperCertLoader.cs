using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NewLife.Log;

using System.Security.Cryptography.X509Certificates;

namespace LettuceEncrypt.Internal;

/// <summary>
/// ����ASP.NET������Ա֤��
/// /// </summary>
internal class DeveloperCertLoader : ICertificateSource
{
    // see https://github.com/aspnet/Common/blob/61320f4ecc1a7b60e76ca8fe05cd86c98778f92c/shared/Microsoft.AspNetCore.Certificates.Generation.Sources/CertificateManager.cs#L19-L20
    // This is the unique OID for the developer cert generated by VS and the .NET Core CLI
    private const string AspNetHttpsOid = "1.3.6.1.4.1.311.84.1.1";
    private const string AspNetHttpsOidFriendlyName = "ASP.NET Core HTTPS����֤��";
    private readonly IHostEnvironment _environment;
    private readonly ILogger<DeveloperCertLoader> _logger;

    public DeveloperCertLoader(
        IHostEnvironment environment,
        ILogger<DeveloperCertLoader> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    public Task<IEnumerable<X509Certificate2>> GetCertificatesAsync(CancellationToken cancellationToken)
    {
        if (!_environment.IsDevelopment())
        {
            return Task.FromResult(Enumerable.Empty<X509Certificate2>());
        }

        var certs = FindDeveloperCert();

        return Task.FromResult(certs);
    }

    private IEnumerable<X509Certificate2> FindDeveloperCert()
    {
        using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        store.Open(OpenFlags.ReadOnly);
        var certs = store.Certificates.Find(X509FindType.FindByExtension, AspNetHttpsOid, validOnly: false);
        if (certs.Count == 0)
        {
            XTrace.Log.Debug("�Ҳ���" + AspNetHttpsOidFriendlyName);
        }
        else
        {
            XTrace.Log.Debug("ʹ��" + AspNetHttpsOidFriendlyName + " ����'localhost'����");

            foreach (var cert in certs)
            {
                yield return cert;
            }
        }
    }
}