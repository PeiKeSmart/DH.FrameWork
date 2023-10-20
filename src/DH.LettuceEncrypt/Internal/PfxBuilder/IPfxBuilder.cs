namespace LettuceEncrypt.Internal.PfxBuilder;

internal interface IPfxBuilder
{
    void AddIssuer(byte[] certificate);

    byte[] Build(string friendlyName, string password);
}
