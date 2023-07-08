using DG.SafeOrbit.Cryptography.Encryption;
using DG.SafeOrbit.Cryptography.Encryption.Kdf;
using DG.SafeOrbit.Cryptography.Encryption.Padding.Factory;
using DG.SafeOrbit.Cryptography.Encryption.Padding.Padders;
using DG.SafeOrbit.Cryptography.Hashers;
using DG.SafeOrbit.Cryptography.Random;
using DG.SafeOrbit.Memory;
using DG.SafeOrbit.Memory.InjectionServices.Stampers.Serialization;
using DG.SafeOrbit.Memory.SafeBytesServices;
using DG.SafeOrbit.Memory.SafeBytesServices.Collection;
using DG.SafeOrbit.Memory.SafeBytesServices.DataProtection;
using DG.SafeOrbit.Memory.SafeBytesServices.DataProtection.Protector;
using DG.SafeOrbit.Memory.SafeBytesServices.Factory;
using DG.SafeOrbit.Memory.SafeBytesServices.Id;
using DG.SafeOrbit.Memory.SafeStringServices;
using DG.SafeOrbit.Memory.SafeStringServices.Text;

namespace DG.SafeOrbit.Library
{
    public static class FactoryBootstrapper
    {
        public static void Bootstrap(ISafeContainer safeContainer)
        {
            // Converters
            safeContainer.Register(() => safeContainer, LifeTime.Singleton);
            // Hash
            safeContainer.Register<IFastHasher, Murmur32>(() => Murmur32.StaticInstance, LifeTime.Singleton);
            safeContainer.Register<ISafeHasher, Sha512Hasher>(LifeTime.Singleton);
            // Encryption
            safeContainer.Register<IKeyDerivationFunction, Pbkdf2KeyDeriver>();
            safeContainer.Register<IPadderFactory, PadderFactory>(LifeTime.Singleton);
            safeContainer.Register<IPkcs7Padder, Pkcs7Padder>(LifeTime.Singleton);
            safeContainer.Register<IFactory<IPkcs7Padder>, SafeContainerWrapper<IPkcs7Padder>>(LifeTime.Singleton);
            safeContainer.Register<IFastEncryptor, BlowfishEncryptor>(LifeTime.Transient);
            safeContainer.Register<ISafeEncryptor, AesEncryptor>();
            // Memory
            safeContainer.Register<ISerializer, Serializer>(LifeTime.Singleton);
            // SafeObject
            safeContainer.Register(() => SafeObjectFactory.StaticInstance, LifeTime.Singleton);
            // SafeBytes
            safeContainer.Register<ISafeBytes, SafeBytes>();
            safeContainer.Register<ISafeByte, SafeByte>();
            safeContainer.Register<ISafeByteFactory, MemoryCachedSafeByteFactory>(LifeTime.Singleton);
            safeContainer.Register<IByteArrayProtector, MemoryProtector>(LifeTime.Transient);
            safeContainer.Register<IMemoryProtectedBytes, MemoryProtectedBytes>(LifeTime.Transient);
            safeContainer.Register<IByteIdListSerializer<int>, ByteIdListSerializer>(LifeTime.Singleton);
            safeContainer.Register<IByteIdGenerator, HashedByteIdGenerator>(LifeTime.Singleton);
            safeContainer.Register<ISafeByteCollection, EncryptedSafeByteCollection>();
            safeContainer.Register<IFactory<ISafeBytes>, SafeContainerWrapper<ISafeBytes>>();
            safeContainer.Register<IFactory<ISafeByte>, SafeContainerWrapper<ISafeByte>>(LifeTime.Singleton);
            safeContainer.Register<IFactory<ISafeByteCollection>, SafeContainerWrapper<ISafeByteCollection>>();
            // SafeString
            safeContainer.Register<ISafeString, SafeString>();
            safeContainer.Register<IFactory<ISafeString>, SafeContainerWrapper<ISafeString>>();
            safeContainer.Register<ISafeStringToStringMarshaler, SafeStringToStringMarshaler>();
            safeContainer.Register<IFactory<ISafeStringToStringMarshaler>, SafeContainerWrapper<ISafeStringToStringMarshaler>>();
            // Random
            safeContainer.Register<IFastRandom, FastRandom>(LifeTime.Singleton);
            safeContainer.Register<ISafeRandom, SafeRandom>(LifeTime.Singleton);
            // Text
            safeContainer.Register<ITextService, TextService>();
        }
    }
}