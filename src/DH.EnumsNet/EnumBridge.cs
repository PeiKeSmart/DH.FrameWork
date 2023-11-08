using EnumsNET.Numerics;

using System.Runtime.CompilerServices;

namespace EnumsNET;
internal interface IEnumBridge {
    EnumComparer CreateEnumComparer(EnumCache enumCache);
    EnumMember CreateEnumMember(EnumMemberInternal member);
    IReadOnlyList<EnumMember> CreateMembersContainer(IEnumerable<EnumMemberInternal> members, int count, bool cached);
}

internal interface IEnumBridge<TUnderlying, TUnderlyingOperations> : IEnumBridge
    where TUnderlying : struct, IComparable<TUnderlying>, IEquatable<TUnderlying>
#if ICONVERTIBLE
    , IConvertible
#endif
    where TUnderlyingOperations : struct, IUnderlyingOperations<TUnderlying> {
    bool CustomValidate(object customValidator, TUnderlying value);
    TUnderlying? IsEnum(object value);
    object ToObjectUnchecked(TUnderlying value);
    IValuesContainer CreateValuesContainer(IEnumerable<EnumMemberInternal<TUnderlying, TUnderlyingOperations>> members, int count, bool cached);
}

// Acts as a bridge in the reverse from the underlying type to the enum type
// through the use of the implemented interface IEnumBridge<TUnderlying, TUnderlyingOperations>.
internal sealed class EnumBridge<TEnum, TUnderlying, TUnderlyingOperations> : IEnumBridge<TUnderlying, TUnderlyingOperations>
    where TEnum : struct, Enum
    where TUnderlying : struct, IComparable<TUnderlying>, IEquatable<TUnderlying>
#if ICONVERTIBLE
    , IConvertible
#endif
    where TUnderlyingOperations : struct, IUnderlyingOperations<TUnderlying> {
    public object ToObjectUnchecked(TUnderlying value) => UnsafeUtility.As<TUnderlying, TEnum>(ref value);

    public bool CustomValidate(object customValidator, TUnderlying value) => UnsafeUtility.As<IEnumValidatorAttribute<TEnum>>(customValidator).IsValid(UnsafeUtility.As<TUnderlying, TEnum>(ref value));

    public EnumComparer CreateEnumComparer(EnumCache enumCache) => new EnumComparer<TEnum>(enumCache);

    public EnumMember CreateEnumMember(EnumMemberInternal member) => new EnumMember<TEnum>(member);

    public TUnderlying? IsEnum(object value) => value is TEnum e ? UnsafeUtility.As<TEnum, TUnderlying>(ref e) : (TUnderlying?)null;

    public IValuesContainer CreateValuesContainer(IEnumerable<EnumMemberInternal<TUnderlying, TUnderlyingOperations>> members, int count, bool cached) => new ValuesContainer<TEnum, TUnderlying, TUnderlyingOperations>(members, count, cached);

    public IReadOnlyList<EnumMember> CreateMembersContainer(IEnumerable<EnumMemberInternal> members, int count, bool cached) => new MembersContainer<TEnum>(members, count, cached);
}