namespace DH.Serialization;

/// <summary>
/// json 反序列化工具
/// </summary>
public class JsonParser
{

    /// <summary>
    /// 解析字符串，返回对象。
    /// 根据 json 的不同，可能返回整数(int)、布尔类型(bool)、字符串(string)、一般对象(Dictionary&lt;string, object&gt;)、数组(List&lt;object&gt;)等不同类型
    /// </summary>
    /// <param name="src"></param>
    /// <returns>根据 json 的不同，可能返回整数(int)、布尔类型(bool)、字符串(string)、一般对象(Dictionary&lt;string, object&gt;)、数组(List&lt;object&gt;)等不同类型</returns>
    public static Object Parse(String src)
    {

        if (strUtil.IsNullOrEmpty(src)) return null;

        return new InitJsonParser(new CharSource(src)).getResult();
    }

}
