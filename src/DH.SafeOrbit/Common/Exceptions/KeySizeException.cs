﻿using DG.SafeOrbit.Exceptions.SerializableException;

using System.Runtime.Serialization;
using System.Security.Permissions;

namespace DG.SafeOrbit.Exceptions
{
    /// <summary>
    ///     This type of exception is thrown if the key size was given wrong to complete the cryptologic operation.
    /// </summary>
    [Serializable]
    public class KeySizeException : SafeOrbitException
    {
        public KeySizeException(int actual, int minSize, int maxSize) : base(
            $"The length of the key parameter for the cryptographic function must be between {minSize} bits ({minSize / 8} bytes) and {maxSize} bits (({maxSize / 8} bytes) but it was {actual * 8} bits ({actual} bytes)")
        {
            MinSize = minSize;
            MaxSize = maxSize;
        }

        public int MinSize { get; set; }
        public int MaxSize { get; set; }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public KeySizeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected override void ConfigureSerialize(ISerializationContext serializationContext)
        {
            serializationContext.Add(() => MinSize);
            serializationContext.Add(() => MaxSize);
            base.ConfigureSerialize(serializationContext);
        }
    }
}