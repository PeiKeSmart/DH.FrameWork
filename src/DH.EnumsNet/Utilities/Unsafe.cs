using System.Diagnostics.CodeAnalysis;
using System.Security;

namespace System.Runtime.CompilerServices;
internal static class UnsafeUtility {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SecuritySafeCritical]
    public static int SizeOf<T>() => Unsafe.SizeOf<T>();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SecuritySafeCritical]
    public static ref TTo As<TFrom, TTo>(ref TFrom source) => ref Unsafe.As<TFrom, TTo>(ref source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SecuritySafeCritical]
    [return: NotNullIfNotNull("value")]
    public static T? As<T>(object? value) where T : class => Unsafe.As<T>(value);
}