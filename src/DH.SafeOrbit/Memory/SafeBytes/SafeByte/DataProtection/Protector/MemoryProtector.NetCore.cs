﻿using System;
using System.Threading.Tasks;
using DG.SafeOrbit.Cryptography.Encryption;
using DG.SafeOrbit.Cryptography.Encryption.Padding;
using DG.SafeOrbit.Cryptography.Random;

namespace DG.SafeOrbit.Memory.SafeBytesServices.DataProtection.Protector
{
    /// <summary>
    ///     .NET Core implementation of <see cref="IByteArrayProtector" />.
    /// </summary>
    /// <seealso cref="IByteArrayProtector" />
    public partial class MemoryProtector : IByteArrayProtector
    {
        private static readonly IFastEncryptor Encryptor = new BlowfishEncryptor(BlowfishCipherMode.Ecb);
        private readonly IFastEncryptor _encryptor;
        private readonly byte[] _key;

        public MemoryProtector() : this(Encryptor, FastRandom.StaticInstance)
        {
        }

        /// <exception cref="ArgumentNullException"><paramref name="encryptor" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="random" /> is <see langword="null" />.</exception>
        internal MemoryProtector(IFastEncryptor encryptor, ICryptoRandom random)
        {
            _encryptor = encryptor ?? throw new ArgumentNullException(nameof(encryptor));
            _key = random.GetBytes(Encryptor.MinKeySizeInBits);
            _encryptor.Padding = PaddingMode.None;
        }

        /// <inheritdoc />
        public int BlockSizeInBytes => _encryptor.BlockSizeInBits / 8;

        /// <inheritdoc />
        /// <inheritdoc cref="EnsureParameter" />
        public async Task ProtectAsync(byte[] userData)
        {
            this.EnsureParameter(userData);
            var encryptedBytes = await _encryptor.EncryptAsync(userData, _key)
                .ConfigureAwait(false);
            SetBytesToByteArray(
                source: encryptedBytes,
                target: ref userData);
        }

        /// <inheritdoc />
        /// <inheritdoc cref="EnsureParameter" />
        public async Task UnprotectAsync(byte[] encryptedData)
        {
            this.EnsureParameter(encryptedData);
            var decryptedBytes = await _encryptor.DecryptAsync(encryptedData, _key)
                .ConfigureAwait(false);
            SetBytesToByteArray(
                source: decryptedBytes,
                target: ref encryptedData);
        }

        private static void SetBytesToByteArray(ref byte[] target, byte[] source)
        {
            if (target.Length != source.Length)
                throw new ArgumentException(
                    $"Length of the {nameof(target)} must be equal to the length of {nameof(source)}. {nameof(target)} length was {target.Length}, {nameof(source)} length is {source.Length}");
            for (var i = 0; i < source.Length; i++)
                target[i] = source[i];
        }
    }
}