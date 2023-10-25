using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NewLife;
using NewLife.Log;

namespace LettuceEncrypt.Internal.AcmeStates;

internal class ServerStartupState : SyncAcmeState
{
    private readonly IOptions<LettuceEncryptOptions> _options;
    private readonly CertificateSelector _selector;
    private readonly ILogger<ServerStartupState> _logger;

    public ServerStartupState(
        AcmeStateMachineContext context,
        IOptions<LettuceEncryptOptions> options,
        CertificateSelector selector,
        ILogger<ServerStartupState> logger) :
        base(context)
    {
        _options = options;
        _selector = selector;
        _logger = logger;
    }

    public override IAcmeState MoveNext()
    {
        var domainNames = _options.Value.DomainNames;
        var hasCertForAllDomains = domainNames.All(_selector.HasCertForDomain);
        if (hasCertForAllDomains)
        {
            XTrace.Log.Debug($"已找到{domainNames.Join()}的证书。");
            return MoveTo<CheckForRenewalState>();
        }

        return MoveTo<BeginCertificateCreationState>();
    }
}
