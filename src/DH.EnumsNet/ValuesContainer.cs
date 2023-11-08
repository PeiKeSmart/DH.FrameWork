using EnumsNET.Numerics;
using EnumsNET.Utilities;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace EnumsNET;
internal interface IValuesContainer {
    IReadOnlyList<object> GetNonGenericContainer();
}

internal sealed class ValuesContainer<TEnum, TUnderlying, TUnderlyingOperations> : IReadOnlyList<TEnum>, IValuesContainer
    where TEnum : struct, Enum
    where TUnderlying : struct, IComparable<TUnderlying>, IEquatable<TUnderlying>
#if ICONVERTIBLE
    , IConvertible
#endif
    where TUnderlyingOperations : struct, IUnderlyingOperations<TUnderlying> {
    private readonly IEnumerable<EnumMemberInternal<TUnderlying, TUnderlyingOperations>> _members;
    private TEnum[] _valuesArray;
    private IReadOnlyList<object> _nonGenericValuesContainer;

    public int Count { get; }

    public TEnum this[int index] => (_valuesArray ??= ArrayHelper.ToArray(this, Count))[index];

    public ValuesContainer(IEnumerable<EnumMemberInternal<TUnderlying, TUnderlyingOperations>> members, int count, bool cached)
    {
        Debug.Assert(count == members.Count());
        _members = members;
        Count = count;
        if (cached)
        {
            _valuesArray = ArrayHelper.ToArray(this, count);
        }
    }

    public IEnumerator<TEnum> GetEnumerator() => _valuesArray != null ? ((IEnumerable<TEnum>)_valuesArray).GetEnumerator() : Enumerate();

    private IEnumerator<TEnum> Enumerate()
    {
        foreach (var member in _members)
        {
            var v = member.Value;
            yield return UnsafeUtility.As<TUnderlying, TEnum>(ref v);
        }
    }

    public IReadOnlyList<object> GetNonGenericContainer()
    {
        var nonGenericValuesContainer = _nonGenericValuesContainer;
        return nonGenericValuesContainer ?? Interlocked.CompareExchange(ref _nonGenericValuesContainer, (nonGenericValuesContainer = new NonGenericValuesContainer<TEnum, TUnderlying, TUnderlyingOperations>(this)), null) ?? nonGenericValuesContainer;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

internal sealed class NonGenericValuesContainer<TEnum, TUnderlying, TUnderlyingOperations> : IReadOnlyList<object>
    where TEnum : struct, Enum
    where TUnderlying : struct, IComparable<TUnderlying>, IEquatable<TUnderlying>
#if ICONVERTIBLE
    , IConvertible
#endif
    where TUnderlyingOperations : struct, IUnderlyingOperations<TUnderlying> {
    private readonly ValuesContainer<TEnum, TUnderlying, TUnderlyingOperations> _container;

    public object this[int index] => _container[index];

    public int Count => _container.Count;

    public NonGenericValuesContainer(ValuesContainer<TEnum, TUnderlying, TUnderlyingOperations> container)
    {
        _container = container;
    }

    public IEnumerator<object> GetEnumerator()
    {
        foreach (var value in _container)
        {
            yield return value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}