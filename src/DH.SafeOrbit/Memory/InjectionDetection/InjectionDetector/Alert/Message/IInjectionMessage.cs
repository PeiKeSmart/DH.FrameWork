using System;

namespace DG.SafeOrbit.Memory.Injection
{
    public interface IInjectionMessage : IEquatable<IInjectionMessage>
    {
        InjectionType InjectionType { get; }
        object InjectedObject { get; }
        DateTimeOffset InjectionDetectionTime { get; }
    }
}