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
/// �⽫����ACME״̬������״̬������֤�����ɺ�����
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
        XTrace.WriteLine($"��������AcmeCertificateLoader");

        if (!_server.GetType().Name.StartsWith(nameof(KestrelServer)))
        {
            var serverType = _server.GetType().FullName;
            XTrace.Log.Warn(
                "LettuceEncryptֻ����Kestrelһ��ʹ�ã���{serverType}�������ϲ���֧�֡���������֤�����á�",
                serverType);
            return;
        }

        if (_config.GetValue<bool>("UseIISIntegration"))
        {
            XTrace.Log.Warn(
                "LettuceEncrypt��������IIS���йܵ�Ӧ�ó���IIS������̬HTTPS֤��󶨡�" +
                "��������֤�����á�");
            return;
        }

        // �ں�̨����֤��
        if (!LettuceEncryptDomainNamesWereConfigured())
        {
            XTrace.Log.Info("δ��������");
            return;
        }

        using var acmeStateMachineScope = _serviceScopeFactory.CreateScope();

        try
        {
            IAcmeState state = acmeStateMachineScope.ServiceProvider.GetRequiredService<ServerStartupState>();

            while (!stoppingToken.IsCancellationRequested)
            {
                XTrace.WriteLine($"ACME״̬ת�����ƶ���{state.GetType().Name}");
                state = await state.MoveNextAsync(stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            XTrace.Log.Debug("������״̬��ȡ���������˳�������");
        }
        catch (AggregateException ex) when (ex.InnerException != null)
        {
            XTrace.WriteLine("ACME״̬������δ����Ĵ���");
            XTrace.WriteException(ex.InnerException);
        }
        catch (Exception ex)
        {
            XTrace.WriteLine("ACME״̬������δ����Ĵ���");
            XTrace.WriteException(ex);
        }
    }

    private bool LettuceEncryptDomainNamesWereConfigured()
        => _options.Value.DomainNames
            .Any(w => !string.Equals("localhost", w, StringComparison.OrdinalIgnoreCase));
}
