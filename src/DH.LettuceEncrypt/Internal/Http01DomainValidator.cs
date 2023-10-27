using Certes.Acme;
using Certes.Acme.Resource;

using FluentFTP;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NewLife;
using NewLife.Log;

using System.Net;
using System.Text;

namespace LettuceEncrypt.Internal;

internal class Http01DomainValidator : DomainOwnershipValidator {
    private readonly IHttpChallengeResponseStore _challengeStore;
    private readonly LettuceEncryptOptions _options;

    public Http01DomainValidator(
        IHttpChallengeResponseStore challengeStore,
        IHostApplicationLifetime appLifetime,
        AcmeClient client, ILogger logger, string domainName, LettuceEncryptOptions options)
        : base(appLifetime, client, logger, domainName)
    {
        _challengeStore = challengeStore;
        _options = options;
    }

    public override async Task ValidateOwnershipAsync(IAuthorizationContext authzContext, CancellationToken cancellationToken)
    {
        await PrepareHttpChallengeResponseAsync(authzContext, cancellationToken);
        await WaitForChallengeResultAsync(authzContext, cancellationToken);
    }

    private async Task PrepareHttpChallengeResponseAsync(
        IAuthorizationContext authorizationContext,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (_client == null)
        {
            throw new InvalidOperationException();
        }

        var httpChallenge = await _client.CreateChallengeAsync(authorizationContext, ChallengeTypes.Http01);
        if (httpChallenge == null)
        {
            var ex = new InvalidOperationException(
                $"未收到挑战类型{ChallengeTypes.Http01}的挑战信息");
            XTrace.WriteException(ex);

            throw ex;
        }

        var keyAuth = httpChallenge.KeyAuthz;
        XTrace.WriteLine($"获取到的密钥授权字符串：{keyAuth}_Token:{httpChallenge.Token}");

        if (!_options.FtpDomain.IsNullOrWhiteSpace() && !_options.FtpUser.IsNullOrWhiteSpace() && !_options.FtpPassWord.IsNullOrWhiteSpace())
        {
            FtpClient client = new(_options.FtpDomain)
            {
                Credentials = new NetworkCredential(_options.FtpUser, _options.FtpPassWord)
            };
            client.Connect();

            if (client.IsConnected)
            {
                if (!client.DirectoryExists("/.well-known/acme-challenge"))
                {
                    client.CreateDirectory("/.well-known/acme-challenge");
                }

                client.UploadBytes(Encoding.UTF8.GetBytes(keyAuth), $"/.well-known/acme-challenge/{httpChallenge.Token}");
            }
            else
            {
                XTrace.WriteLine($"Ftp登录失败");
            }
        }
        else
        {
            XTrace.WriteLine($"未配置Ftp信息");
        }

        _challengeStore.AddChallengeResponse(httpChallenge.Token, keyAuth);

        XTrace.WriteLine("正在等待服务器开始接受HTTP请求");
        await _appStarted.Task;

        XTrace.WriteLine("正在请求服务器验证HTTP质询");
        await _client.ValidateChallengeAsync(httpChallenge);
    }
}
