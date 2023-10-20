using Certes;
using Certes.Acme;

namespace LettuceEncrypt.Internal.PfxBuilder;

internal interface IPfxBuilderFactory
{
    IPfxBuilder FromChain(CertificateChain certificateChain, IKey certKey);
}
