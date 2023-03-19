using NewLife.Serialization;

using Newtonsoft.Json.Linq;

namespace DH.Helpers;

/// <summary>
/// Json辅助扩展操作
/// </summary>
public static class JsonExtensions
{
    #region ToObject(将Json字符串转换为对象)

    /// <summary>
    /// 将Json字符串转换为对象
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="json">Json字符串</param>
    /// <returns></returns>
    public static T ToObject<T>(this string json)
    {
        return JsonHelper.ToJsonEntity<T>(json);
    }

    /// <summary>
    /// 将Json字符串转换为对像
    /// </summary>
    /// <param name="json">Json字符串</param>
    /// <returns></returns>
    public static object ToObject(this string json)
    {
        return DHJsonHelper.ToObject(json);
    }

    #endregion

    #region ToJson(将对象转换为Json字符串)

    /// <summary>
    /// 将对象转换为Json字符串
    /// </summary>
    /// <param name="target">目标对象</param>
    /// <param name="isConvertToSingleQuotes">是否将双引号转换成单引号</param>
    /// <param name="camelCase">是否驼峰式命名</param>
    /// <param name="indented">是否缩进</param>
    /// <param name="nullValue">是否写空值，默认为true</param>
    /// <returns></returns>
    public static string ToDHJson(this object target, bool isConvertToSingleQuotes = false, bool camelCase = false, bool indented = false, bool nullValue = true)
    {
        return DHJsonHelper.ToJson(target, isConvertToSingleQuotes, camelCase, indented, nullValue);
    }

    #endregion

    #region ToJObject(将Json字符串转换为Linq对象)

    /// <summary>
    /// 将Json字符串转换为Linq对象
    /// </summary>
    /// <param name="json">Json字符串</param>
    /// <returns></returns>
    public static JObject ToJObject(this string json)
    {
        return DHJsonHelper.ToJObject(json);
    }

    #endregion

    #region IsJson(判断字符串是否为Json格式)

    /// <summary>
    /// 判断字符串是否为Json格式。为效率考虑，仅做了开始和结束字符的验证
    /// </summary>
    /// <param name="json">json字符串</param>
    public static bool IsJson(this string json)
    {
        return DHJsonHelper.IsJson(json);
    }

    #endregion
}