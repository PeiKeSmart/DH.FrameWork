using Certes;
using Certes.Acme;

namespace LettuceEncrypt.Internal.PfxBuilder;

internal sealed class PfxBuilderFactory : IPfxBuilderFactory
{
    public IPfxBuilder FromChain(CertificateChain certificateChain, IKey certKey)
        => new PfxBuilderWrapper(certificateChain.ToPfx(certKey));
}
