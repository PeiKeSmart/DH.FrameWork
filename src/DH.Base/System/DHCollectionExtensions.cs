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
    /// 如果项不在集合中，则将其添加到集合中。
    /// </summary>
    /// <param name="source">集合</param>
    /// <param name="item">要检查和添加的项目</param>
    /// <typeparam name="T">集合中项目的类型</typeparam>
    /// <returns>Returns True if added, returns False if not.</returns>
    public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, T item)
    {
        Check.NotNull(source, nameof(source));

        if (source.Contains(item))
        {
            return false;
        }

        source.Add(item);
        return true;
    }

    /// <summary>
    /// 将集合中不存在的项添加到集合中。
    /// </summary>
    /// <param name="source">集合</param>
    /// <param name="items">要检查和添加的项目</param>
    /// <typeparam name="T">集合中项目的类型</typeparam>
    /// <returns>返回添加的项目。</returns>
    public static IEnumerable<T> AddIfNotContains<T>([NotNull] this ICollection<T> source, IEnumerable<T> items)
    {
        Check.NotNull(source, nameof(source));

        var addedItems = new List<T>();

        foreach (var item in items)
        {
            if (source.Contains(item))
            {
                continue;
            }

            source.Add(item);
            addedItems.Add(item);
        }

        return addedItems;
    }

    /// <summary>
    /// 根据给定的<paramref name="predicate"/>将项目添加到集合中（如果该项目不在集合中）。
    /// </summary>
    /// <param name="source">集合</param>
    /// <param name="predicate">决定项目是否已在集合中的条件</param>
    /// <param name="itemFactory">返回项目的工厂</param>
    /// <typeparam name="T">集合中项目的类型</typeparam>
    /// <returns>如果添加则返回True，如果未添加则返回False。</returns>
    public static bool AddIfNotContains<T>([NotNull] this ICollection<T> source, [NotNull] Func<T, bool> predicate, [NotNull] Func<T> itemFactory)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(predicate, nameof(predicate));
        Check.NotNull(itemFactory, nameof(itemFactory));

        if (source.Any(predicate))
        {
            return false;
        }

        source.Add(itemFactory());
        return true;
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
