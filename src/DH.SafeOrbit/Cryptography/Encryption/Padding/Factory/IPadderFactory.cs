using DG.SafeOrbit.Cryptography.Encryption.Padding.Padders;

namespace DG.SafeOrbit.Cryptography.Encryption.Padding.Factory
{
    public interface IPadderFactory
    {
        IPadder GetPadder(PaddingMode paddingMode);
    }
}