using DG.SafeOrbit.Exceptions.SerializableException;
using DG.SafeOrbit.Memory;
using DG.SafeOrbit.Memory.Injection;

using System.Runtime.Serialization;
using System.Security.Permissions;

namespace DG.SafeOrbit.Exceptions
{
    /// <summary>
    ///     An exception to throw when memory injection is detected.
    /// </summary>
    /// <seealso cref="SafeOrbitException" />
    /// <seealso cref="SerializableExceptionBase" />
    [Serializable]
    public class MemoryInjectionException : SafeOrbitException
    {
        public MemoryInjectionException(IInjectionMessage message)
            : base(message.ToString())
        {
            InjectionType = message.InjectionType;
            InjectedObject = message.InjectedObject;
            DetectionTime = message.InjectionDetectionTime;
        }

        public MemoryInjectionException(string message, Exception inner) : base(message, inner)
        {
        }

        public MemoryInjectionException(InjectionType injectionType, string message, Exception inner)
            : base(message, inner)
        {
            InjectionType = injectionType;
        }

        public MemoryInjectionException(Exception inner) : base(inner)
        {
        }

        public MemoryInjectionException(Exception inner, InjectionType injectionType) : base(inner)
        {
            InjectionType = injectionType;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public MemoryInjectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InjectionType InjectionType { get; set; }
        public object InjectedObject { get; set; }
        public DateTimeOffset DetectionTime { get; set; }

        protected override void ConfigureSerialize(ISerializationContext serializationContext)
        {
            serializationContext.Add(() => InjectionType);
            serializationContext.Add(() => InjectedObject);
            serializationContext.Add(() => DetectionTime);
            base.ConfigureSerialize(serializationContext);
        }
    }
}