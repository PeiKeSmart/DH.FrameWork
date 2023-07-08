﻿using DG.SafeOrbit.Extensions;

using System.Security.Cryptography;

namespace DG.SafeOrbit.Cryptography.Hashers
{
    public class Sha512Hasher : ISafeHasher
    {
        internal static byte[] SaltBytes =
        {
            0x1e, 0x82, 0xf5, 0x55, 0xa9, 0xbe, 0x70, 0xf7, 0x97, 0xff, 0xd1, 0x98, 0xea, 0xed, 0x81, 0x13, 0xd9, 0xa5,
            0x18, 0x83,
            0xd9, 0x2, 0xee, 0xf8, 0x32, 0x4e, 0x87, 0x0, 0xba, 0xeb, 0x1, 0xe0, 0x86, 0xc2, 0x63, 0x78, 0xc9, 0xe3,
            0x82, 0x30,
            0xa0, 0xe5, 0xb0, 0xb, 0x23, 0x7f, 0xa6, 0xde, 0x2e, 0x17, 0x7b, 0xda, 0x83, 0xd8, 0xbc, 0xff, 0xd1, 0xcf,
            0x72, 0x36,
            0x8d, 0x6c, 0x59, 0x5d, 0xc2, 0x1e, 0x58, 0x4d, 0xb8, 0x5b, 0x4c, 0xcf, 0x7e, 0x44, 0x7, 0x95, 0x9b, 0x62,
            0xc6, 0x1d,
            0xcb, 0x2, 0x8f, 0x11, 0x1f, 0x61, 0x37, 0xb7, 0xd9, 0xb4, 0x34, 0x2e, 0x1c, 0x67, 0x6e, 0x27, 0xbb, 0xd2,
            0x64, 0xbf
        };

        private readonly HashAlgorithm _algorithm;

        public Sha512Hasher() : this(SHA512.Create())
        {
        }

        /// <exception cref="ArgumentNullException"><paramref name="algorithm" /> is <see langword="null" />.</exception>
        internal Sha512Hasher(HashAlgorithm algorithm)
        {
            _algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
        }

        public byte[] Compute(byte[] input)
        {
            var data = input.Combine(SaltBytes);
            var result = _algorithm.ComputeHash(data);
            Array.Clear(data, 0, data.Length);
            return result;
        }
    }
}