using JetBrains.Annotations;

namespace DH.Infrastructure;

public interface IObjectAccessor<out T>
{
    [CanBeNull]
    T Value { get; }
}