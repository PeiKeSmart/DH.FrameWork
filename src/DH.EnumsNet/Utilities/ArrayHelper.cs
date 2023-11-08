using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace EnumsNET.Utilities;
internal static class ArrayHelper {
#if !ARRAY_EMPTY
        private static class Cache<T>
        {
            public static readonly T[] Empty = new T[0];
        }
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Empty<T>() =>
#if ARRAY_EMPTY
        Array.Empty<T>();
#else
            Cache<T>.Empty;
#endif

    public static T[] ToArray<T>(IEnumerable<T> items, int count)
    {
        var a = new T[count];
        var i = 0;
        foreach (var item in items)
        {
            a[i++] = item;
        }
        Debug.Assert(i == count);
        return a;
    }
}