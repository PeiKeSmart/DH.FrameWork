using DH;

using JetBrains.Annotations;

namespace System;

/// <summary>
/// 集合的扩展方法。
/// </summary>
public static class DHCollectionExtensions
{
    /// <summary>
    /// 检查任何给定的集合对象为空或没有项。
    /// </summary>
    [ContractAnnotation("source:null => true")]
    public static bool IsNullOrEmpty<T>([CanBeNull] this ICollection<T> source)
    {
        return source == null || source.Count <= 0;
    }

    /// <summary>
    /// 从集合中删除满足给定<paramref name="predicate"/>的所有项。
    /// </summary>
    /// <typeparam name="T">集合中项目的类型</typeparam>
    /// <param name="source">集合</param>
    /// <param name="predicate">删除项目的条件</param>
    /// <returns>删除的项目列表</returns>
    public static IList<T> RemoveAll<T>([NotNull] this ICollection<T> source, Func<T, bool> predicate)
    {
        var items = source.Where(predicate).ToList();

        foreach (var item in items)
        {
            source.Remove(item);
        }

        return items;
    }

    /// <summary>
    /// 从集合中删除所有项。
    /// </summary>
    /// <typeparam name="T">集合中项目的类型</typeparam>
    /// <param name="source">集合</param>
    /// <param name="items">要从列表中删除的项目</param>
    public static void RemoveAll<T>([NotNull] this ICollection<T> source, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            source.Remove(item);
        }
    }
}
