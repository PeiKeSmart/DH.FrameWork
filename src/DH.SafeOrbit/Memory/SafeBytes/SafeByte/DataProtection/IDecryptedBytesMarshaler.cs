using System;

namespace DG.SafeOrbit.Memory.SafeBytesServices.DataProtection
{
    public interface IDecryptedBytesMarshaler : IDisposable
    {
        byte[] PlainBytes { get; }
    }
}