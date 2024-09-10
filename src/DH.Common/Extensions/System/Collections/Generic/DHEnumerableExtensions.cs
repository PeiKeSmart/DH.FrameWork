namespace System.Collections.Generic;

/// <summary> 
/// <see cref="IEnumerable{T}"/>的扩展方法。
/// </summary>
public static class DHEnumerableExtensions
{
    /// <summary>
    /// 串联类型为System的构造<see cref="IEnumerable{T}"/>集合的成员。字符串，在每个成员之间使用指定的分隔符。
    /// 这是string.Join(...)的快捷方式。
    /// </summary>
    /// <param name="source">包含要连接的字符串的集合。</param>
    /// <param name="separator">用作分隔符的字符串。只有当值有多个元素时，返回的字符串中才会包含分隔符。</param>
    /// <returns>由分隔符字符串分隔的值的成员组成的字符串。如果值没有成员，则该方法返回System.String.Empty。</returns>
    public static string JoinAsString(this IEnumerable<string> source, string separator)
    {
        return string.Join(separator, source);
    }

    /// <summary>
    /// 串联集合的成员，在每个成员之间使用指定的分隔符。
    /// 这是string.Join(...)的快捷方式。
    /// </summary>
    /// <param name="source">包含要连接的对象的集合。</param>
    /// <param name="separator">用作分隔符的字符串。只有当值有多个元素时，返回的字符串中才会包含分隔符。</param>
    /// <typeparam name="T">值成员的类型。</typeparam>
    /// <returns>由分隔符字符串分隔的值的成员组成的字符串。如果值没有成员，则该方法返回System.String.Empty。</returns>
    public static string JoinAsString<T>(this IEnumerable<T> source, string separator)
    {
        return string.Join(separator, source);
    }

    /// <summary>
    /// 如果给定条件为true，则通过给定谓词过滤<see cref="IEnumerable{T}"/>。
    /// </summary>
    /// <param name="source">可枚举以应用筛选</param>
    /// <param name="condition">布尔值</param>
    /// <param name="predicate">谓词以筛选可枚举</param>
    /// <returns>基于<paramref name="condition"/>筛选或未筛选可枚举</returns>
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
    {
        return condition
            ? source.Where(predicate)
            : source;
    }

    /// <summary>
    /// 如果给定条件为true，则通过给定谓词过滤<see cref="IEnumerable{T}"/>。
    /// </summary>
    /// <param name="source">可枚举以应用筛选</param>
    /// <param name="condition">布尔值</param>
    /// <param name="predicate">谓词以筛选可枚举</param>
    /// <returns>基于<paramref name="condition"/>筛选或未筛选可枚举</returns>
    public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
    {
        return condition
            ? source.Where(predicate)
            : source;
    }
}
