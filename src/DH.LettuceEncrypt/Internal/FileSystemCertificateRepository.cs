using Certes;
using Certes.Acme;

using NewLife.Log;

using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LettuceEncrypt.Internal;

internal class FileSystemCertificateRepository : ICertificateRepository, ICertificateSource
{
    private readonly DirectoryInfo _certDir;

    public FileSystemCertificateRepository(DirectoryInfo directory, string? pfxPassword)
    {
        RootDir = directory;
        PfxPassword = pfxPassword;
        _certDir = directory.CreateSubdirectory("certs");
    }

    public DirectoryInfo RootDir { get; }
    public string? PfxPassword { get; }

    public Task<IEnumerable<X509Certificate2>> GetCertificatesAsync(CancellationToken cancellationToken)
    {
        XTrace.WriteLine($"进来了吗？FileSystemCertificateRepository");

        var certs = new List<X509Certificate2>();
        foreach (var file in _certDir.GetFiles("*.pfx"))
        {
            var cert = new X509Certificate2(
                fileName: file.FullName,
                password: PfxPassword);
            certs.Add(cert);
        }

        return Task.FromResult(certs.AsEnumerable());
    }

    public Task SaveAsync(X509Certificate2 certificate, IKey privateKey, CertificateChain acmeCert, CancellationToken cancellationToken)
    {
        _certDir.Create();

        var tmpFile = Path.GetTempFileName();
        File.WriteAllBytes(
            tmpFile,
            certificate.Export(X509ContentType.Pfx, PfxPassword));

        var fileName = certificate.Thumbprint + ".pfx";
        var output = Path.Combine(_certDir.FullName, fileName);

        // File.Move 是大多数操作系统上的原子操作。首先通过写入临时文件然后移动它，它避免了与读的潜在竞争条件。

        File.Move(tmpFile, output);

        privateKey.ToPem();
        fileName = certificate.Thumbprint + ".privatekey.pem";
        output = Path.Combine(_certDir.FullName, fileName);
        output.AsFile().WriteBytes(Encoding.UTF8.GetBytes(privateKey.ToPem()));

        fileName = certificate.Thumbprint + ".pem";
        output = Path.Combine(_certDir.FullName, fileName);
        output.AsFile().WriteBytes(Encoding.UTF8.GetBytes(acmeCert.ToPem()));

        return Task.CompletedTask;
    }
}
