using System.Collections.Concurrent;
using System.Dynamic;

namespace System.Collections.Generic;

/// <summary>
/// 字典的扩展方法。
/// </summary>
public static class DHDictionaryExtensions
{
    /// <summary>
    /// 如果字典中存在值，则使用此方法尝试获取该值。
    /// </summary>
    /// <typeparam name="T">值的类型</typeparam>
    /// <param name="dictionary">集合对象</param>
    /// <param name="key">键</param>
    /// <param name="value">键的值(如果键不存在，则为默认值)</param>
    /// <returns>如果字典中确实存在键，则为True</returns>
    internal static bool TryGetValue<T>(this IDictionary<string, object> dictionary, string key, out T value)
    {
        object valueObj;
        if (dictionary.TryGetValue(key, out valueObj) && valueObj is T)
        {
            value = (T)valueObj;
            return true;
        }

        value = default;
        return false;
    }

    /// <summary>
    /// 从具有给定键的字典中获取值。如果找不到，则返回默认值。
    /// </summary>
    /// <param name="dictionary">要检查和获取的字典</param>
    /// <param name="key">查找值的键</param>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    /// <returns>如果找不到就用默认值。</returns>
    public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
    {
        TValue obj;
        return dictionary.TryGetValue(key, out obj) ? obj : default;
    }

    /// <summary>
    /// 从具有给定键的字典中获取值。如果找不到，则返回默认值。
    /// </summary>
    /// <param name="dictionary">要检查和获取的字典</param>
    /// <param name="key">查找值的键</param>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    /// <returns>如果找不到就用默认值。</returns>
    public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
    {
        return dictionary.TryGetValue(key, out var obj) ? obj : default;
    }

    /// <summary>
    /// 从具有给定键的字典中获取值。如果找不到，则返回默认值。
    /// </summary>
    /// <param name="dictionary">要检查和获取的字典</param>
    /// <param name="key">查找值的键</param>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    /// <returns>如果找不到就用默认值。</returns>
    public static TValue GetOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
    {
        return dictionary.TryGetValue(key, out var obj) ? obj : default;
    }

    /// <summary>
    /// 从具有给定键的字典中获取值。如果找不到，则返回默认值。
    /// </summary>
    /// <param name="dictionary">要检查和获取的字典</param>
    /// <param name="key">查找值的键</param>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    /// <returns>如果找不到就用默认值。</returns>
    public static TValue GetOrDefault<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key)
    {
        return dictionary.TryGetValue(key, out var obj) ? obj : default;
    }

    /// <summary>
    /// 从具有给定键的字典中获取值。如果找不到，则返回默认值。
    /// </summary>
    /// <param name="dictionary">要检查和获取的字典</param>
    /// <param name="key">查找值的键</param>
    /// <param name="factory">如果在字典中找不到，则用于创建值的工厂方法</param>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    /// <returns>如果找不到就用默认值。</returns>
    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> factory)
    {
        TValue obj;
        if (dictionary.TryGetValue(key, out obj))
        {
            return obj;
        }

        return dictionary[key] = factory(key);
    }

    /// <summary>
    /// 从具有给定键的字典中获取值。如果找不到，则返回默认值。
    /// </summary>
    /// <param name="dictionary">要检查和获取的词典</param>
    /// <param name="key">查找值的键</param>
    /// <param name="factory">如果在字典中找不到，则用于创建值的工厂方法</param>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    /// <returns>如果找不到就用默认值。</returns>
    public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)
    {
        return dictionary.GetOrAdd(key, k => factory());
    }

    /// <summary>
    /// 从具有给定键的并发字典中获取值。如果找不到，则返回默认值。
    /// </summary>
    /// <param name="dictionary">要检查并获取的并发字典</param>
    /// <param name="key">查找值的键</param>
    /// <param name="factory">如果在字典中找不到，则用于创建值的工厂方法</param>
    /// <typeparam name="TKey">键的类型</typeparam>
    /// <typeparam name="TValue">值的类型</typeparam>
    /// <returns>如果找不到就用默认值。</returns>
    public static TValue GetOrAdd<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)
    {
        return dictionary.GetOrAdd(key, k => factory());
    }

    /// <summary>
    /// 将字典转换为动态对象，以便在运行时添加和删除
    /// </summary>
    /// <param name="dictionary">集合对象</param>
    /// <returns>如果值正确，则返回表示对象的ExpandoObject</returns>
    public static dynamic ConvertToDynamicObject(this Dictionary<string, object> dictionary)
    {
        var expandoObject = new ExpandoObject();
        var expendObjectCollection = (ICollection<KeyValuePair<string, object>>)expandoObject;

        foreach (var keyValuePair in dictionary)
        {
            expendObjectCollection.Add(keyValuePair);
        }

        return expandoObject;
    }
}
