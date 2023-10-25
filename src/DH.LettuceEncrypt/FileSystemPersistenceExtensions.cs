using LettuceEncrypt.Accounts;
using LettuceEncrypt.Acme;
using LettuceEncrypt.Internal;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LettuceEncrypt;

/// <summary>
/// 用于配置证书持久性的扩展
/// </summary>
public static class FileSystemStorageExtensions
{
    /// <summary>
    /// 将证书和帐户数据保存到目录中。
    /// 证书以 .pfx （PKCS #12） 格式存储在<paramref name="directory"/> 的子目录中。
    /// 帐户密钥信息以 JSON 格式存储在 <paramref name="directory"/> 的不同子目录中。
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="directory">用于存储信息的根目录。信息可能存储在子目录中。</param>
    /// <param name="pfxPassword">对于无密码的 .pfx 文件，设置为 null 或空。</param>
    /// <returns></returns>
    public static ILettuceEncryptServiceBuilder PersistDataToDirectory(
        this ILettuceEncryptServiceBuilder builder,
        DirectoryInfo directory,
        string? pfxPassword)
    {
        ArgumentNullException.ThrowIfNull(builder);

        ArgumentNullException.ThrowIfNull(directory);

        var otherFileSystemRepoServices = builder
            .Services
            .Where(d => d.ServiceType == typeof(ICertificateRepository)
            && d.ImplementationInstance != null
            && d.ImplementationInstance.GetType() == typeof(FileSystemCertificateRepository));

        foreach (var serviceDescriptor in otherFileSystemRepoServices)
        {
            var otherRepo = (FileSystemCertificateRepository)serviceDescriptor.ImplementationInstance!;
            if (otherRepo.RootDir.Equals(directory))
            {
                if (otherRepo.PfxPassword != pfxPassword)
                {
                    throw new ArgumentException($"已为 {directory} 配置了另一个文件系统存储库，但密码不同。");
                }
                return builder;
            }
        }

        var lettuceEncryptOptions = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<LettuceEncryptOptions>>();

        var implementationInstance = new FileSystemCertificateRepository(lettuceEncryptOptions, directory, pfxPassword);
        builder.Services
            .AddSingleton<ICertificateRepository>(implementationInstance)
            .AddSingleton<ICertificateSource>(implementationInstance);

        builder.Services.TryAddSingleton<IAccountStore>(services => new FileSystemAccountStore(directory,
                services.GetRequiredService<ILogger<FileSystemAccountStore>>(),
                services.GetRequiredService<ICertificateAuthorityConfiguration>()));

        return builder;
    }
}
