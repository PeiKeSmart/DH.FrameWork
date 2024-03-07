namespace DH.Helpers;

/// <summary>
/// 小写的名称 - 值
/// </summary>
public class NameValueL<T> {
    /// <summary>
    /// 名称
    /// </summary>
    public String? name { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public T? value { get; set; }
}

/// <summary>
/// 大写的名称 - 值
/// </summary>
public class NameValueU<T> {
    /// <summary>
    /// 名称
    /// </summary>
    public String? Name { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public T? Value { get; set; }

    /// <summary>
    /// 实例化
    /// </summary>
    public NameValueU() { }

    /// <summary>
    /// 实例化
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public NameValueU(String name, T value)
    {
        Name = name;
        Value = value;
    }
}

/// <summary>
/// 名称 - 值
/// </summary>
public class NameValue : NameValue<String> {
    /// <summary>
    /// 初始化一个<see cref="NameValue"/>类型的实例
    /// </summary>
    public NameValue() { }

    /// <summary>
    /// 初始化一个<see cref="NameValue"/>类型的实例
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="value">值</param>
    public NameValue(String name, String value)
    {
        Name = name;
        Value = value;
    }
}

/// <summary>
/// 名称 - 值
/// </summary>
/// <typeparam name="T">类型</typeparam>
public class NameValue<T> {
    /// <summary>
    /// 初始化一个<see cref="NameValue{T}"/>类型的实例
    /// </summary>
    public NameValue() { }

    /// <summary>
    /// 初始化一个<see cref="NameValue{T}"/>类型的实例
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="value">值</param>
    public NameValue(String name, T value)
    {
        Name = name;
        Value = value;
    }

    /// <summary>
    /// 名称
    /// </summary>
    public String? Name { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public T? Value { get; set; }
}