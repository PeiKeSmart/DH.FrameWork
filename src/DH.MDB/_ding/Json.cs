using DH.Serialization;

namespace DH;

/// <summary>
/// 提供 json 序列化和反序列化的功能
/// </summary>
public class Jsons
{
    /// <summary>
    /// 将 json 字符串反序列化为对象列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public static List<T> DeserializeList<T>(String jsonString)
    {
        return TypedDeserializeHelper.DeserializeList<T>(jsonString);
    }

    /// <summary>
    /// 将 json 字符串反序列化为原始的 JsonObject 类型
    /// </summary>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public static JsonObject ParseJson(String jsonString)
    {
        if (strUtil.IsNullOrEmpty(jsonString)) return new JsonObject();
        return JsonParser.Parse(jsonString) as JsonObject;
    }

    /// <summary>
    /// 将 json 字符串解析为 json 原始数据类型的列表，比如 ["abc", 88, {name:"aa", gender:"male"}]
    /// </summary>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public static List<Object> ParseList(String jsonString)
    {
        if (strUtil.IsNullOrEmpty(jsonString)) return new List<Object>();
        return JsonParser.Parse(jsonString) as List<Object>;
    }

    /// <summary>
    /// 将 json 字符串反序列化为 原始的json强类型(int,string,JsonObject等) 的数据列表。
    /// 当列表内数据类型相同时使用，比如 ["abc", "www", "qqqxyz", "uuu"]
    /// </summary>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public static List<T> ParseList<T>(String jsonString)
    {
        if (strUtil.IsNullOrEmpty(jsonString)) return new List<T>();
        return DeserializeList<T>(jsonString);
    }

}
