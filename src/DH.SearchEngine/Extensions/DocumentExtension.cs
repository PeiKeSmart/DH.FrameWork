using Lucene.Net.Documents;

using Newtonsoft.Json;

using System.ComponentModel;
using System.Globalization;

namespace DH.SearchEngine.Extensions;

public static class DocumentExtension
{
    /// <summary>
    /// 获取文档的值
    /// </summary>
    /// <param name="doc">Lucene文档</param>
    /// <param name="key">键</param>
    /// <param name="t">类型</param>
    /// <returns></returns>
    public static object Get(this Document doc, string key, Type t)
    {
        string value = doc.Get(key);
        return t switch
        {
            _ when t.IsAssignableFrom(typeof(string)) => value,
            _ when t.IsValueType => ConvertTo(value, t),
            _ => JsonConvert.DeserializeObject(value, t)
        };
    }

    /// <summary>
    /// 类型直转
    /// </summary>
    /// <param name="value"></param>
    /// <param name="type">目标类型</param>
    /// <returns></returns>
    private static object ConvertTo(string value, Type type)
    {
        if (value == null)
        {
            return default;
        }

        if (value.GetType() == type)
        {
            return value;
        }

        if (type.IsEnum)
        {
            return Enum.Parse(type, value.ToString(CultureInfo.InvariantCulture));
        }

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            var underlyingType = Nullable.GetUnderlyingType(type);
            return underlyingType!.IsEnum ? Enum.Parse(underlyingType, value.ToString(CultureInfo.CurrentCulture)) : Convert.ChangeType(value, underlyingType);
        }

        var converter = TypeDescriptor.GetConverter(value);
        if (converter != null)
        {
            if (converter.CanConvertTo(type))
            {
                return converter.ConvertTo(value, type);
            }
        }

        converter = TypeDescriptor.GetConverter(type);
        if (converter != null)
        {
            if (converter.CanConvertFrom(value.GetType()))
            {
                return converter.ConvertFrom(value);
            }
        }

        return Convert.ChangeType(value, type);
    }

    /// <summary>
    /// 获取文档的值
    /// </summary>
    /// <param name="doc">Lucene文档</param>
    /// <param name="key">键</param>
    /// <returns></returns>
    public static object Get<T>(this Document doc, string key)
    {
        string value = doc.Get(key);
        switch (typeof(T))
        {
            case Type _ when typeof(T).IsAssignableFrom(typeof(int)):
                return int.Parse(value);
            case Type _ when typeof(T).IsAssignableFrom(typeof(DateTime)):
                return DateTime.Parse(value);
            case Type _ when typeof(T).IsAssignableFrom(typeof(double)):
                return double.Parse(value);
            case Type _ when typeof(T).IsAssignableFrom(typeof(long)):
                return long.Parse(value);
            case Type _ when typeof(T).IsAssignableFrom(typeof(decimal)):
                return decimal.Parse(value);
            case Type _ when typeof(T).IsAssignableFrom(typeof(char)):
                return char.Parse(value);
            case Type _ when typeof(T).IsAssignableFrom(typeof(byte)):
                return byte.Parse(value);
            case Type _ when typeof(T).IsAssignableFrom(typeof(float)):
                return float.Parse(value);
            case Type _ when typeof(T).IsAssignableFrom(typeof(bool)):
                return bool.Parse(value);
            case Type _ when typeof(T).BaseType == typeof(Enum):
                return Enum.Parse(typeof(T), value);
            case Type _ when typeof(T).IsAssignableFrom(typeof(string)):
                return value;
            default:
                return Convert.ChangeType(value, typeof(T));
        }
    }
}