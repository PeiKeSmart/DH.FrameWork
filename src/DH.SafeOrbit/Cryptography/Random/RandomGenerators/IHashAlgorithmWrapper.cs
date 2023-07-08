using System;

namespace DG.SafeOrbit.Cryptography.Random.RandomGenerators
{
    internal interface IHashAlgorithmWrapper : IDisposable
    {
        int HashSizeInBits { get; }
        byte[] ComputeHash(byte[] data);
    }
}