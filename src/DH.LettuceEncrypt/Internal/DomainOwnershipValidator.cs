using Certes.Acme;
using Certes.Acme.Resource;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LettuceEncrypt.Internal;

internal abstract class DomainOwnershipValidator
{
    protected readonly AcmeClient _client;
    protected readonly ILogger _logger;
    protected readonly string _domainName;
    protected readonly TaskCompletionSource<object?> _appStarted = new();

    protected DomainOwnershipValidator(IHostApplicationLifetime appLifetime, AcmeClient client, ILogger logger, string domainName)
    {
        _client = client;
        _logger = logger;
        _domainName = domainName;

        appLifetime.ApplicationStarted.Register(() => _appStarted.TrySetResult(null));
        if (appLifetime.ApplicationStarted.IsCancellationRequested)
        {
            _appStarted.TrySetResult(null);
        }
    }

    public abstract Task ValidateOwnershipAsync(IAuthorizationContext authzContext, CancellationToken cancellationToken);

    protected async Task WaitForChallengeResultAsync(IAuthorizationContext authorizationContext, CancellationToken cancellationToken)
    {
        var retries = 60;
        var delay = TimeSpan.FromSeconds(2);

        while (retries > 0)
        {
            retries--;

            cancellationToken.ThrowIfCancellationRequested();

            var authorization = await _client.GetAuthorizationAsync(authorizationContext);

            _logger.LogAcmeAction("GetAuthorization");

            switch (authorization.Status)
            {
                case AuthorizationStatus.Valid:
                    return;
                case AuthorizationStatus.Pending:
                    await Task.Delay(delay, cancellationToken);
                    continue;
                case AuthorizationStatus.Invalid:
                    throw InvalidAuthorizationError(authorization);
                case AuthorizationStatus.Revoked:
                    throw new InvalidOperationException(
                        $"验证域名'{_domainName}'的授权已被吊销。");
                case AuthorizationStatus.Expired:
                    throw new InvalidOperationException(
                        $"验证域名'{_domainName}'的授权已过期。");
                case AuthorizationStatus.Deactivated:
                default:
                    throw new ArgumentOutOfRangeException("authorization",
                        "验证域所有权时来自服务器的意外响应。");
            }
        }

        throw new TimeoutException("等待域所有权验证时超时。");
    }

    private Exception InvalidAuthorizationError(Authorization authorization)
    {
        var reason = "unknown";
        var domainName = authorization.Identifier.Value;
        try
        {
            var errors = authorization.Challenges.Where(a => a.Error != null).Select(a => a.Error)
                .Select(error => $"{error.Type}: {error.Detail}, Code = {error.Status}");
            reason = string.Join("; ", errors);
        }
        catch
        {
            _logger.LogTrace("无法确定验证失败的原因。响应：{resp}", authorization);
        }

        _logger.LogError("未能验证域名'{domainName}'的所有权。原因：{reason}", domainName,
            reason);

        return new InvalidOperationException($"无法验证域名'{domainName}'的所有权");
    }
}
