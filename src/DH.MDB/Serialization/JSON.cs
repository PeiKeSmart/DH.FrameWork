namespace DH.Serialization;

/// <summary>
/// 封装了 json 反序列化中的常见操作：将 json 字符串反序列化为对象、对象列表、字典等。
/// 序列化工具见 JsonString
/// </summary>
public class JSON
{

    public static List<T> ConvertList<T>(List<Object> list)
    {

        if (list == null) return null;

        List<T> results = new List<T>();
        foreach (Object obj in list)
        {

            if (obj == null) continue;

            T item;

            if (typeof(T) == typeof(DateTime))
            {
                item = (T)((Object)cvt.ToTime(obj, DateTime.MinValue));
            }
            else if (typeof(T) == typeof(long))
            {
                long x;
                long.TryParse(obj.ToString(), out x);
                item = (T)((Object)x);
            }
            else if (typeof(T) == typeof(double))
            {
                item = (T)((Object)cvt.ToDouble(obj.ToString()));
            }
            else
            {
                item = (T)obj;
            }

            results.Add(item);
        }

        return results;
    }

}
