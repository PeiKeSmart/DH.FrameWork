using LettuceEncrypt.Accounts;
using LettuceEncrypt.Acme;

using Microsoft.Extensions.Logging;

using System.Text.Json;

namespace LettuceEncrypt.Internal;

internal class FileSystemAccountStore : IAccountStore
{
    private readonly DirectoryInfo _accountDir;
    private readonly ILogger _logger;

    public FileSystemAccountStore(
        ILogger logger,
        ICertificateAuthorityConfiguration certificateAuthority)
        : this(new DirectoryInfo(AppContext.BaseDirectory), logger, certificateAuthority)
    {
    }

    public FileSystemAccountStore(
        DirectoryInfo rootDirectory,
        ILogger logger,
        ICertificateAuthorityConfiguration certificateAuthority)
    {
        _logger = logger;

        var topAccountDir = rootDirectory.CreateSubdirectory("accounts");
        var directoryUri = certificateAuthority.AcmeDirectoryUri;
        var subPath = Path.Combine(directoryUri.Authority, directoryUri.LocalPath.Substring(1));
        _accountDir = topAccountDir.CreateSubdirectory(subPath);
    }

    public async Task<AccountModel?> GetAccountAsync(CancellationToken cancellationToken)
    {
        _logger.LogTrace("正在{path}中查找帐户信息", _accountDir.FullName);

        foreach (var jsonFile in _accountDir.GetFiles("*.json"))
        {
            _logger.LogTrace("正在分析帐户信息的{path}", jsonFile);

            var accountModel = await Deserialize(jsonFile, cancellationToken);
            if (accountModel != null)
            {
                _logger.LogDebug("已从{path}加载帐户信息", _accountDir.FullName);
                return accountModel;
            }
        }

        _logger.LogDebug("在{path}中找不到帐户信息", _accountDir.FullName);
        return default;
    }

    private static async Task<AccountModel?> Deserialize(FileInfo jsonFile, CancellationToken cancellationToken)
    {
        using var fileStream = jsonFile.OpenRead();
        var deserializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        return await JsonSerializer.DeserializeAsync<AccountModel>(fileStream, deserializeOptions,
            cancellationToken);
    }

    public async Task SaveAccountAsync(AccountModel account, CancellationToken cancellationToken)
    {
        _accountDir.Create();

        var jsonFile = new FileInfo(Path.Combine(_accountDir.FullName, $"{account.Id}.json"));
        _logger.LogTrace("正在将帐户信息保存到{path}", jsonFile.FullName);

        using var writeStream = jsonFile.OpenWrite();
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        await JsonSerializer.SerializeAsync(writeStream, account, serializerOptions, cancellationToken);

        _logger.LogDebug("已将帐户信息保存到{path}", jsonFile.FullName);
    }
}
