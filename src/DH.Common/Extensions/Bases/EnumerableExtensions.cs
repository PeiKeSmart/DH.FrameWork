﻿using System.Linq.Expressions;

namespace DH.Extensions;

public static partial class EnumerableExtensions {
    /// <summary>
    /// 添加符合条件的多个元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="predicate"></param>
    /// <param name="values"></param>
    public static void AddRangeIf<T>(this ICollection<T> @this, Func<T, bool> predicate, params T[] values)
    {
        foreach (var obj in values)
        {
            if (predicate(obj))
            {
                @this.Add(obj);
            }
        }
    }

    /// <summary>
    /// 添加不重复的元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="values"></param>
    public static void AddRangeIfNotContains<T>(this ICollection<T> @this, params T[] values)
    {
        foreach (T obj in values)
        {
            if (!@this.Contains(obj))
            {
                @this.Add(obj);
            }
        }
    }

    /// <summary>
    /// 移除符合条件的元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="where"></param>
    public static void RemoveWhere<T>(this ICollection<T> @this, Func<T, bool> @where)
    {
        foreach (var obj in @this.Where(where).ToList())
        {
            @this.Remove(obj);
        }
    }

    /// <summary>
    /// 在元素之后添加元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="condition">条件</param>
    /// <param name="value">值</param>
    public static void InsertAfter<T>(this IList<T> list, Func<T, bool> condition, T value)
    {
        foreach (var item in list.Select((item, index) => new { item, index }).Where(p => condition(p.item)).OrderByDescending(p => p.index))
        {
            if (item.index + 1 == list.Count)
            {
                list.Add(value);
            }
            else
            {
                list.Insert(item.index + 1, value);
            }
        }
    }

    /// <summary>
    /// 在元素之后添加元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="index">索引位置</param>
    /// <param name="value">值</param>
    public static void InsertAfter<T>(this IList<T> list, int index, T value)
    {
        foreach (var item in list.Select((v, i) => new { Value = v, Index = i }).Where(p => p.Index == index).OrderByDescending(p => p.Index))
        {
            if (item.Index + 1 == list.Count)
            {
                list.Add(value);
            }
            else
            {
                list.Insert(item.Index + 1, value);
            }
        }
    }

    /// <summary>
    /// 转HashSet
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static HashSet<TResult> ToHashSet<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
    {
        var set = new HashSet<TResult>();
        set.UnionWith(source.Select(selector));
        return set;
    }

    /// <summary>
    /// 异步foreach
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="maxParallelCount">最大并行数</param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task ForeachAsync<T>(this IEnumerable<T> source, Func<T, Task> action, int maxParallelCount, CancellationToken cancellationToken = default)
    {
        var list = new List<Task>();
        foreach (var item in source)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            list.Add(action(item));
            if (list.Count >= maxParallelCount)
            {
                await Task.WhenAll(list);
                list.Clear();
            }
        }

        await Task.WhenAll(list);
    }

    /// <summary>
    /// 异步foreach
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task ForeachAsync<T>(this IEnumerable<T> source, Func<T, Task> action, CancellationToken cancellationToken = default)
    {
        return ForeachAsync(source, action, source.Count(), cancellationToken);
    }

    /// <summary>
    /// 异步Select
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static Task<TResult[]> SelectAsync<T, TResult>(this IEnumerable<T> source, Func<T, Task<TResult>> selector)
    {
        return Task.WhenAll(source.Select(selector));
    }

    /// <summary>
    /// 异步Select
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static Task<TResult[]> SelectAsync<T, TResult>(this IEnumerable<T> source, Func<T, int, Task<TResult>> selector)
    {
        return Task.WhenAll(source.Select(selector));
    }

    /// <summary>
    /// 异步For
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="maxParallelCount">最大并行数</param>
    /// <param name="cancellationToken">取消口令</param>
    /// <returns></returns>
    public static async Task ForAsync<T>(this IEnumerable<T> source, Func<T, int, Task> selector, int maxParallelCount, CancellationToken cancellationToken = default)
    {
        var list = new List<Task>();
        int index = 0;
        foreach (var item in source)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            list.Add(selector(item, index++));
            if (list.Count >= maxParallelCount)
            {
                await Task.WhenAll(list);
                list.Clear();
            }
        }

        await Task.WhenAll(list);
    }

    /// <summary>
    /// 异步For
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="cancellationToken">取消口令</param>
    /// <returns></returns>
    public static Task ForAsync<T>(this IEnumerable<T> source, Func<T, int, Task> selector, CancellationToken cancellationToken = default)
    {
        return ForAsync(source, selector, source.Count(), cancellationToken);
    }

    /// <summary>
    /// 取最大值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static TResult MaxOrDefault<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector) => source.Select(selector).DefaultIfEmpty().Max();

    /// <summary>
    /// 取最大值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static TResult MaxOrDefault<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, TResult defaultValue) => source.Select(selector).DefaultIfEmpty(defaultValue).Max();

    /// <summary>
    /// 取最大值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TSource MaxOrDefault<TSource>(this IQueryable<TSource> source) => source.DefaultIfEmpty().Max();

    /// <summary>
    /// 取最大值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static TSource MaxOrDefault<TSource>(this IQueryable<TSource> source, TSource defaultValue) => source.DefaultIfEmpty(defaultValue).Max();

    /// <summary>
    /// 取最大值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, TResult defaultValue) => source.Select(selector).DefaultIfEmpty(defaultValue).Max();

    /// <summary>
    /// 取最大值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector) => source.Select(selector).DefaultIfEmpty().Max();

    /// <summary>
    /// 取最大值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> source) => source.DefaultIfEmpty().Max();

    /// <summary>
    /// 取最大值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue) => source.DefaultIfEmpty(defaultValue).Max();

    /// <summary>
    /// 取最小值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static TResult MinOrDefault<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector) => source.Select(selector).DefaultIfEmpty().Min();

    /// <summary>
    /// 取最小值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static TResult MinOrDefault<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, TResult defaultValue) => source.Select(selector).DefaultIfEmpty(defaultValue).Min();

    /// <summary>
    /// 取最小值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TSource MinOrDefault<TSource>(this IQueryable<TSource> source) => source.DefaultIfEmpty().Min();

    /// <summary>
    /// 取最小值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static TSource MinOrDefault<TSource>(this IQueryable<TSource> source, TSource defaultValue) => source.DefaultIfEmpty(defaultValue).Min();

    /// <summary>
    /// 取最小值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector) => source.Select(selector).DefaultIfEmpty().Min();

    /// <summary>
    /// 取最小值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, TResult defaultValue) => source.Select(selector).DefaultIfEmpty(defaultValue).Min();

    /// <summary>
    /// 取最小值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source) => source.DefaultIfEmpty().Min();

    /// <summary>
    /// 取最小值
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue) => source.DefaultIfEmpty(defaultValue).Min();

    /// <summary>
    /// 标准差
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <returns></returns>
    public static TResult StandardDeviation<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector) where TResult : IConvertible
    {
        return StandardDeviation(source.Select(t => selector(t).ConvertTo<double>())).ConvertTo<TResult>();
    }

    /// <summary>
    /// 标准差
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static T StandardDeviation<T>(this IEnumerable<T> source) where T : IConvertible
    {
        return StandardDeviation(source.Select(t => t.ConvertTo<double>())).ConvertTo<T>();
    }

    /// <summary>
    /// 标准差
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static double StandardDeviation(this IEnumerable<double> source)
    {
        double result = 0;
        int count = source.Count();
        if (count > 1)
        {
            double avg = source.Average();
            double sum = source.Sum(d => (d - avg) * (d - avg));
            result = Math.Sqrt(sum / count);
        }

        return result;
    }
}