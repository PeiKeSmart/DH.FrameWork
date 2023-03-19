﻿using System.Diagnostics;

namespace DH.Extend;

[DebuggerStepThrough]
public static class ExtIEnumerable {
    public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> fun)
    {
        foreach (T item in source)
        {
            fun(item);
        }
        return source;
    }
    public static List<TResult> ToList<T, TResult>(this IEnumerable<T> source, Func<T, TResult> fun)
    {
        List<TResult> result = new List<TResult>();
        source.Each(m => result.Add(fun(m)));
        return result;
    }

}