﻿using Certes;
using Certes.Acme;

using Microsoft.Extensions.Options;

using NewLife;
using NewLife.Log;
using NewLife.Serialization;

using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LettuceEncrypt.Internal;

internal class FileSystemCertificateRepository : ICertificateRepository, ICertificateSource
{
    private readonly DirectoryInfo _certDir;
    private readonly IOptions<LettuceEncryptOptions> _options;

    public FileSystemCertificateRepository(IOptions<LettuceEncryptOptions> options, DirectoryInfo directory, string? pfxPassword)
    {
        _options = options;
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
        fileName = "privkey.pem";
        output = Path.Combine(_certDir.FullName, fileName);
        var privkey = output.AsFile().WriteBytes(Encoding.UTF8.GetBytes(privateKey.ToPem()));

        fileName = "fullchain.pem";
        output = Path.Combine(_certDir.FullName, fileName);
        var fullchain = output.AsFile().WriteBytes(Encoding.UTF8.GetBytes(acmeCert.ToPem()));

        if (!_options.Value.PemCertDeployPath.IsNullOrWhiteSpace())
        {
            XTrace.WriteLine($"部署cert目录下的Pem证书");

            var savePath = $"{_options.Value.PemCertDeployPath.CombinePath("privkey.pem")}".AsFile();
            savePath.FullName.EnsureDirectory();
            privkey.CopyTo(savePath.FullName, true);

            savePath = $"{_options.Value.PemCertDeployPath.CombinePath("fullchain.pem")}".AsFile();
            fullchain.CopyTo(savePath.FullName, true);
        }

        if (!_options.Value.PemSslDeployPath.IsNullOrWhiteSpace())
        {
            XTrace.WriteLine($"部署ssl目录下的Pem证书");

            var savePath = $"{_options.Value.PemSslDeployPath.CombinePath("privkey.pem")}".AsFile();
            savePath.FullName.EnsureDirectory();
            privkey.CopyTo(savePath.FullName, true);

            savePath = $"{_options.Value.PemSslDeployPath.CombinePath("fullchain.pem")}".AsFile();
            fullchain.CopyTo(savePath.FullName, true);

            var subjects = _options.Value.DomainNames.Where(e => e.Contains("www."));
            var data = new { issuer = "R3", notAfter = DateTime.Now.AddDays(90).ToShortDateString(), notBefore = DateTime.Now.ToShortDateString(), dns = _options.Value.DomainNames, subject = subjects.Any() ? subjects.FirstOrDefault() : _options.Value.DomainNames.FirstOrDefault(), endtime = 89};

            savePath = $"{_options.Value.PemSslDeployPath.CombinePath("info.json")}".AsFile();
            savePath.WriteBytes(data.ToJson().GetBytes());
        }

        return Task.CompletedTask;
    }
}
