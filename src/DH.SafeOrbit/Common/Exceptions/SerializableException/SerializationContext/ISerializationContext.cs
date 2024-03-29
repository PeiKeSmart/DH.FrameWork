using System.Linq.Expressions;

namespace DG.SafeOrbit.Exceptions.SerializableException
{
    /// <summary>
    ///     Abstracts a class that represents the context of a serialization
    /// </summary>
    /// <seealso cref="SerializationContext" />
    /// <seealso cref="SerializableExceptionBase" />
    public interface ISerializationContext
    {
        ICollection<ISerializationPropertyInfo> PropertyInfos { get; }
        void Add<T>(Expression<Func<T>> property);
    }
}