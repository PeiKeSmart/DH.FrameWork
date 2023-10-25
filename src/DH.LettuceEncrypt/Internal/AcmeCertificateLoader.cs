using LettuceEncrypt.Internal.AcmeStates;

using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NewLife.Log;

namespace LettuceEncrypt.Internal;

/// <summary>
/// 这将启动ACME状态机，该状态机处理证书生成和续订
/// </summary>
internal class AcmeCertificateLoader : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IOptions<LettuceEncryptOptions> _options;
    private readonly ILogger _logger;

    private readonly IServer _server;
    private readonly IConfiguration _config;

    public AcmeCertificateLoader(
        IServiceScopeFactory serviceScopeFactory,
        IOptions<LettuceEncryptOptions> options,
        ILogger<AcmeCertificateLoader> logger,
        IServer server,
        IConfiguration config)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _options = options;
        _logger = logger;
        _server = server;
        _config = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        XTrace.WriteLine($"进来了吗？AcmeCertificateLoader");

        if (!_server.GetType().Name.StartsWith(nameof(KestrelServer)))
        {
            var serverType = _server.GetType().FullName;
            XTrace.Log.Warn(
                "LettuceEncrypt只能与Kestrel一起使用，在{serverType}服务器上不受支持。正在跳过证书设置。",
                serverType);
            return;
        }

        if (_config.GetValue<bool>("UseIISIntegration"))
        {
            XTrace.Log.Warn(
                "LettuceEncrypt不适用于IIS中托管的应用程序。IIS不允许动态HTTPS证书绑定。" +
                "正在跳过证书设置。");
            return;
        }

        // 在后台加载证书
        if (!LettuceEncryptDomainNamesWereConfigured())
        {
            XTrace.Log.Info("未配置域名");
            return;
        }

        using var acmeStateMachineScope = _serviceScopeFactory.CreateScope();

        try
        {
            IAcmeState state = acmeStateMachineScope.ServiceProvider.GetRequiredService<ServerStartupState>();

            while (!stoppingToken.IsCancellationRequested)
            {
                XTrace.WriteLine($"ACME状态转换：移动到{state.GetType().Name}");
                state = await state.MoveNextAsync(stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            XTrace.Log.Debug("已请求状态机取消。正在退出。。。");
        }
        catch (AggregateException ex) when (ex.InnerException != null)
        {
            XTrace.WriteLine("ACME状态机遇到未处理的错误");
            XTrace.WriteException(ex.InnerException);
        }
        catch (Exception ex)
        {
            XTrace.WriteLine("ACME状态机遇到未处理的错误");
            XTrace.WriteException(ex);
        }
    }

    private bool LettuceEncryptDomainNamesWereConfigured()
        => _options.Value.DomainNames
            .Any(w => !string.Equals("localhost", w, StringComparison.OrdinalIgnoreCase));
}
