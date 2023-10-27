using McMaster.AspNetCore.Kestrel.Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;

namespace LettuceEncrypt.Internal;

internal class KestrelOptionsSetup : IConfigureOptions<KestrelServerOptions>
{
    private readonly IServerCertificateSelector _certificateSelector;
    private readonly TlsAlpnChallengeResponder _tlsAlpnChallengeResponder;
    private readonly IOptions<LettuceEncryptOptions> _options;

    public KestrelOptionsSetup(IServerCertificateSelector certificateSelector, TlsAlpnChallengeResponder tlsAlpnChallengeResponder, IOptions<LettuceEncryptOptions> options)
    {
        _certificateSelector = certificateSelector ?? throw new ArgumentNullException(nameof(certificateSelector));
        _tlsAlpnChallengeResponder = tlsAlpnChallengeResponder ?? throw new ArgumentNullException(nameof(tlsAlpnChallengeResponder));
        _options = options;
    }

    public void Configure(KestrelServerOptions options)
    {
        if (!_options.Value.IsLocalHost)
        {
            options.ConfigureHttpsDefaults(o => o.UseLettuceEncrypt(_certificateSelector, _tlsAlpnChallengeResponder));
        }
    }
}
