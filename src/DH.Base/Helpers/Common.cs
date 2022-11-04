namespace DH.Helpers;

/// <summary>
/// 常用公共操作
/// </summary>
public static partial class Common
{
    #region GetType(获取类型)

    /// <summary>
    /// 获取类型
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public static Type GetType<T>() => GetType(typeof(T));

    /// <summary>
    /// 获取类型
    /// </summary>
    /// <param name="type">类型</param>
    public static Type GetType(Type type) => Nullable.GetUnderlyingType(type) ?? type;

    #endregion
}