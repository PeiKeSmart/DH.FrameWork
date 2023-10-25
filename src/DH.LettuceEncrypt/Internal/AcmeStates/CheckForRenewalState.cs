using LettuceEncrypt.Internal.IO;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NewLife.Log;

namespace LettuceEncrypt.Internal.AcmeStates;

internal class CheckForRenewalState : AcmeState
{
    private readonly ILogger<CheckForRenewalState> _logger;
    private readonly IOptions<LettuceEncryptOptions> _options;
    private readonly CertificateSelector _selector;
    private readonly IClock _clock;

    public CheckForRenewalState(
        AcmeStateMachineContext context,
        ILogger<CheckForRenewalState> logger,
        IOptions<LettuceEncryptOptions> options,
        CertificateSelector selector,
        IClock clock) : base(context)
    {
        _logger = logger;
        _options = options;
        _selector = selector;
        _clock = clock;
    }

    public override async Task<IAcmeState> MoveNextAsync(CancellationToken cancellationToken)
    {
        XTrace.WriteLine($"��������CheckForRenewalState");

        while (!cancellationToken.IsCancellationRequested)
        {
            var checkPeriod = _options.Value.RenewalCheckPeriod;
            var daysInAdvance = _options.Value.RenewDaysInAdvance;
            if (!checkPeriod.HasValue || !daysInAdvance.HasValue)
            {
                XTrace.Log.Info($"δ�����Զ�֤������������ֹͣ{nameof(AcmeCertificateLoader)}");
                return MoveTo<TerminalState>();
            }

            var domainNames = _options.Value.DomainNames;
            if (_logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Debug))
            {
                XTrace.Log.Debug($"���ڼ��{string.Join(", ", domainNames)}��֤������");
            }

            foreach (var domainName in domainNames)
            {
                if (!_selector.TryGet(domainName, out var cert)
                    || cert == null
                    || cert.NotAfter <= _clock.Now.DateTime + daysInAdvance.Value)
                {
                    return MoveTo<BeginCertificateCreationState>();
                }
                XTrace.WriteLine($"��ȡ֤���ʱ�䣺{domainName}_{cert.NotAfter}_{_clock.Now.DateTime}_{daysInAdvance.Value}");
            }

            await Task.Delay(checkPeriod.Value, cancellationToken);
        }

        return MoveTo<TerminalState>();
    }
}
