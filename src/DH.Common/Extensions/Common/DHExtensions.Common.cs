using System.ComponentModel;
using System.Text.RegularExpressions;

using Pek;

namespace DH;

/// <summary>
/// 系统扩展 - 公共扩展
/// </summary>
public static partial class DHExtensions
{
    #region SafeValue(安全获取值)

    /// <summary>
    /// 安全获取值，当值为null时，不会抛出异常
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="value">可空值</param>
    public static T SafeValue<T>(this T? value) where T : struct => value ?? default;

    #endregion

    #region Value(获取枚举值)

    /// <summary>
    /// 获取枚举值
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static int Value(this Enum instance) => Pek.Helpers.Enum.GetValue(instance.GetType(), instance);

    /// <summary>
    /// 获取枚举值
    /// </summary>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <param name="instance">枚举实例</param>
    public static TResult Value<TResult>(this Enum instance) => Pek.Helpers.Conv.To<TResult>(instance.Value());

    #endregion

    #region Description(获取枚举描述)

    /// <summary>
    /// 获取枚举描述，使用<see cref="DescriptionAttribute"/>特性设置描述
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static string Description(this Enum instance) => Helpers.Enum.GetDescription(instance.GetType(), instance);

    #endregion

    #region GetMatch(获取匹配项)

    /// <summary>
    /// 在指定的输入字符串中搜索指定的正则表达式的第一个匹配项
    /// </summary>
    /// <param name="value">要搜索匹配项的字符串</param>
    /// <param name="pattern">要匹配的正则表达式模式</param>
    /// <returns>一个对象，包含有关匹配项的信息</returns>
    public static string GetMatch(this string value, string pattern)
    {
        if (value.IsEmpty())
            return string.Empty;
        return Regex.Match(value, pattern).Value;
    }

    /// <summary>
    /// 在指定的输入字符串中搜索指定的正则表达式的所有匹配项的字符串集合
    /// </summary>
    /// <param name="value">要搜索匹配项的字符串</param>
    /// <param name="pattern">要匹配的正则表达式模式</param>
    /// <returns> 一个集合，包含有关匹配项的字符串值</returns>
    public static IEnumerable<string> GetMatchingValues(this string value, string pattern)
    {
        if (value.IsEmpty())
            return new string[] { };
        return value.GetMatchingValues(pattern, RegexOptions.None);
    }

    /// <summary>
    /// 使用正则表达式来确定一个给定的正则表达式模式的所有匹配的字符串返回的枚举
    /// </summary>
    /// <param name="value">输入字符串</param>
    /// <param name="pattern">正则表达式</param>
    /// <param name="options">比较规则</param>
    /// <returns>匹配字符串的枚举</returns>
    public static IEnumerable<string> GetMatchingValues(this string value, string pattern, RegexOptions options) =>
        from Match match in value.GetMatches(pattern, options) where match.Success select match.Value;

    /// <summary>
    /// 使用正则表达式来确定指定的正则表达式模式的所有匹配项
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="pattern">正则表达式</param>
    /// <param name="options">比较规则</param>
    public static MatchCollection GetMatches(this string value, string pattern, RegexOptions options) => Regex.Matches(value, pattern, options);

    #endregion
}