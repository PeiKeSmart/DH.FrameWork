using System.Collections.Concurrent;

namespace DH;

/// <summary>
/// 取线程内唯一对象
/// </summary>
public static class CallContext
{
    static ConcurrentDictionary<String, AsyncLocal<Object>> state = new ConcurrentDictionary<String, AsyncLocal<Object>>();

    public static void SetData(String name, Object data) =>
        state.GetOrAdd(name, _ => new AsyncLocal<Object>()).Value = data;

    public static object GetData(String name) =>
        state.TryGetValue(name, out AsyncLocal<Object> data) ? data.Value : null;
}

/// <summary>
/// 取线程内唯一对象
/// </summary>
/// <typeparam name="T"></typeparam>
public static class CallContext<T>
{
    static ConcurrentDictionary<string, AsyncLocal<T>> state = new ConcurrentDictionary<string, AsyncLocal<T>>();

    public static void SetData(string name, T data) => state.GetOrAdd(name, _ => new AsyncLocal<T>()).Value = data;

    public static T GetData(string name) => state.TryGetValue(name, out AsyncLocal<T> data) ? data.Value : default;
}