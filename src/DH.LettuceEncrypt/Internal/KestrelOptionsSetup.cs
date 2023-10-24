using McMaster.AspNetCore.Kestrel.Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;

namespace LettuceEncrypt.Internal;

internal class KestrelOptionsSetup : IConfigureOptions<KestrelServerOptions>
{
    private readonly IServerCertificateSelector _certificateSelector;
    private readonly TlsAlpnChallengeResponder _tlsAlpnChallengeResponder;

    public KestrelOptionsSetup(IServerCertificateSelector certificateSelector, TlsAlpnChallengeResponder tlsAlpnChallengeResponder)
    {
        _certificateSelector = certificateSelector ?? throw new ArgumentNullException(nameof(certificateSelector));
        _tlsAlpnChallengeResponder = tlsAlpnChallengeResponder ?? throw new ArgumentNullException(nameof(tlsAlpnChallengeResponder));
    }

    public void Configure(KestrelServerOptions options)
    {
        options.ConfigureHttpsDefaults(o => o.UseLettuceEncrypt(_certificateSelector, _tlsAlpnChallengeResponder));
    }
}
