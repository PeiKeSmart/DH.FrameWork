using System.Text;

namespace DH.Helpers;

/// <summary>
/// 类型转换 操作
/// </summary>
public static partial class Conv
{
    #region ToByte(转换为byte)

    /// <summary>
    /// 转换为8位整型
    /// </summary>
    /// <param name="input">输入值</param>
    public static byte ToDGByte(this object input) => ToDGByte(input, default);

    /// <summary>
    /// 转换为8位整型
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="defaultValue">默认值</param>
    public static byte ToDGByte(this object input, byte defaultValue) => ToDGByteOrNull(input) ?? defaultValue;

    /// <summary>
    /// 转换为8位可空整型
    /// </summary>
    /// <param name="input">输入值</param>
    public static byte? ToDGByteOrNull(this object input)
    {
        var success = byte.TryParse(input.SafeString(), out var result);
        if (success)
            return result;
        try
        {
            var temp = ToDGDoubleOrNull(input, 0);
            if (temp == null)
                return null;
            return Convert.ToByte(temp);
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region ToBytes(转换为bytes)

    /// <summary>
    /// 转换为字节数组
    /// </summary>
    /// <param name="input">输入值</param>        
    public static byte[] ToDGBytes(this string input) => ToDGBytes(input, Encoding.UTF8);

    /// <summary>
    /// 转换为字节数组
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="encoding">字符编码</param>
    public static byte[] ToDGBytes(this string input, Encoding encoding) => string.IsNullOrWhiteSpace(input) ? new byte[] { } : encoding.GetBytes(input);

    #endregion

    #region ToChar(转换为char)

    /// <summary>
    /// 转换为字符
    /// </summary>
    /// <param name="input">输入值</param>
    public static char ToDGChar(this object input) => ToDGChar(input, default);

    /// <summary>
    /// 转换为字符
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="defaultValue">默认值</param>
    public static char ToDGChar(this object input, char defaultValue) => ToDGCharOrNull(input) ?? defaultValue;

    /// <summary>
    /// 转换为可空字符
    /// </summary>
    /// <param name="input">输入值</param>
    public static char? ToDGCharOrNull(this object input)
    {
        var success = char.TryParse(input.SafeString(), out var result);
        if (success)
            return result;
        return null;
    }

    #endregion

    #region ToShort(转换为short)

    /// <summary>
    /// 转换为16位整型
    /// </summary>
    /// <param name="input">输入值</param>
    public static short ToDGShort(this object input) => ToDGShort(input, default);

    /// <summary>
    /// 转换为16位整型
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="defaultValue">默认值</param>
    public static short ToDGShort(this object input, short defaultValue) => ToDGShortOrNull(input) ?? defaultValue;

    /// <summary>
    /// 转换为16位可空整型
    /// </summary>
    /// <param name="input">输入值</param>
    public static short? ToDGShortOrNull(this object input)
    {
        var success = short.TryParse(input.SafeString(), out var result);
        if (success)
            return result;
        try
        {
            var temp = ToDGDoubleOrNull(input, 0);
            if (temp == null)
                return null;
            return System.Convert.ToInt16(temp);
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region ToInt(转换为int)

    /// <summary>
    /// 转换为32位整型
    /// </summary>
    /// <param name="input">输入值</param>
    public static int ToDGInt(this object input) => ToDGInt(input, default);

    /// <summary>
    /// 转换为32位整型
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="defaultValue">默认值</param>
    public static int ToDGInt(this object input, int defaultValue) => ToDGIntOrNull(input) ?? defaultValue;

    /// <summary>
    /// 转换为32位可空整型
    /// </summary>
    /// <param name="input">输入值</param>
    public static int? ToDGIntOrNull(this object input)
    {
        var success = int.TryParse(input.SafeString(), out var result);
        if (success)
            return result;
        try
        {
            var temp = ToDGDoubleOrNull(input, 0);
            if (temp == null)
                return null;
            return System.Convert.ToInt32(temp);
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region ToLong(转换为long)

    /// <summary>
    /// 转换为64位整型
    /// </summary>
    /// <param name="input">输入值</param>
    public static long ToDGLong(this object input) => ToDGLong(input, default);

    /// <summary>
    /// 转换为64位整型
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="defaultValue">默认值</param>
    public static long ToDGLong(this object input, long defaultValue) => ToDGLongOrNull(input) ?? defaultValue;

    /// <summary>
    /// 转换为64位可空整型
    /// </summary>
    /// <param name="input">输入值</param>
    public static long? ToDGLongOrNull(this object input)
    {
        var success = long.TryParse(input.SafeString(), out var result);
        if (success)
            return result;
        try
        {
            var temp = ToDGDecimalOrNull(input, 0);
            if (temp == null)
                return null;
            return System.Convert.ToInt64(temp);
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region ToFloat(转换为float)

    /// <summary>
    /// 转换为32位浮点型，并按指定小数位舍入
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="digits">小数位数</param>
    public static float ToDGFloat(this object input, int? digits = null) => ToDGFloat(input, default, digits);

    /// <summary>
    /// 转换为32位浮点型，并按指定小数位舍入
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="defaultValue">默认值</param>
    /// <param name="digits">小数位数</param>
    public static float ToDGFloat(this object input, float defaultValue, int? digits = null) => ToDGFloatOrNull(input, digits) ?? defaultValue;

    /// <summary>
    /// 转换为32位可空浮点型，并按指定小数位舍入
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="digits">小数位数</param>
    public static float? ToDGFloatOrNull(this object input, int? digits = null)
    {
        var success = float.TryParse(input.SafeString(), out var result);
        if (!success)
            return null;
        if (digits == null)
            return result;
        return (float)Math.Round(result, digits.Value);
    }

    #endregion

    #region ToDouble(转换为double)

    /// <summary>
    /// 转换为64位浮点型，并按指定小数位舍入，温馨提示：4舍6入5成双
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="digits">小数位数</param>
    public static double ToDGDouble(this object input, int? digits = null) => ToDGDouble(input, default, digits);

    /// <summary>
    /// 转换为64位浮点型，并按指定小数位舍入，温馨提示：4舍6入5成双
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="defaultValue">默认值</param>
    /// <param name="digits">小数位数</param>
    public static double ToDGDouble(this object input, double defaultValue, int? digits = null) => ToDGDoubleOrNull(input, digits) ?? defaultValue;

    /// <summary>
    /// 转换为64位可空浮点型，并按指定小数位舍入，温馨提示：4舍6入5成双
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="digits">小数位数</param>
    public static double? ToDGDoubleOrNull(this object input, int? digits = null)
    {
        var success = double.TryParse(input.SafeString(), out var result);
        if (!success)
            return null;
        return digits == null ? result : Math.Round(result, digits.Value);
    }

    #endregion

    #region ToDecimal(转换为decimal)

    /// <summary>
    /// 转换为128位浮点型，并按指定小数位舍入，温馨提示：4舍6入5成双
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="digits">小数位数</param>
    public static decimal ToDGDecimal(this object input, int? digits = null) => ToDGDecimal(input, default, digits);

    /// <summary>
    /// 转换为128位浮点型，并按指定小数位舍入，温馨提示：4舍6入5成双
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="defaultValue">默认值</param>
    /// <param name="digits">小数位数</param>
    public static decimal ToDGDecimal(this object input, decimal defaultValue, int? digits = null) => ToDGDecimalOrNull(input, digits) ?? defaultValue;

    /// <summary>
    /// 转换为128位可空浮点型，并按指定小数位舍入，温馨提示：4舍6入5成双
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="digits">小数位数</param>
    public static decimal? ToDGDecimalOrNull(this object input, int? digits = null)
    {
        var success = decimal.TryParse(input.SafeString(), out var result);
        if (!success)
            return null;
        return digits == null ? result : Math.Round(result, digits.Value);
    }

    #endregion

    #region ToBool(转换为bool)

    /// <summary>
    /// 转换为布尔值
    /// </summary>
    /// <param name="input">输入值</param>
    public static bool ToDGBool(this object input) => ToDGBool(input, default);

    /// <summary>
    /// 转换为布尔值
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="defaultValue">默认值</param>
    public static bool ToDGBool(this object input, bool defaultValue) => ToDGBoolOrNull(input) ?? defaultValue;

    /// <summary>
    /// 转换为可空布尔值
    /// </summary>
    /// <param name="input">输入值</param>
    public static bool? ToDGBoolOrNull(this object input)
    {
        bool? value = GetBool(input);
        if (value != null)
            return value.Value;
        return bool.TryParse(input.SafeString(), out var result) ? (bool?)result : null;
    }

    /// <summary>
    /// 获取布尔值
    /// </summary>
    /// <param name="input">输入值</param>
    private static bool? GetBool(object input)
    {
        switch (input.SafeString().ToLower())
        {
            case "0":
            case "否":
            case "不":
            case "no":
            case "fail":
                return false;
            case "1":
            case "是":
            case "ok":
            case "yes":
                return true;
            default:
                return null;
        }
    }

    #endregion

    #region ToDate(转换为DateTime)

    /// <summary>
    /// 转换为日期
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="defaultValue">默认值</param>
    public static DateTime ToDGDate(this object input, DateTime defaultValue = default) => ToDGDateOrNull(input) ?? DateTime.MinValue;

    /// <summary>
    /// 转换为可空日期
    /// </summary>
    /// <param name="input">输入值</param>
    /// <param name="defaultValue">默认值</param>
    public static DateTime? ToDGDateOrNull(this object input, DateTime? defaultValue = null)
    {
        if (input == null)
            return defaultValue;
        return DateTime.TryParse(input.SafeString(), out var result) ? result : defaultValue;
    }

    #endregion

    #region ToGuid(转换为Guid)

    /// <summary>
    /// 转换为Guid
    /// </summary>
    /// <param name="input">输入值</param>
    public static Guid ToDGGuid(this object input) => ToDGGuidOrNull(input) ?? Guid.Empty;

    /// <summary>
    /// 转换为可空Guid
    /// </summary>
    /// <param name="input">输入值</param>
    public static Guid? ToDGGuidOrNull(this object input) => Guid.TryParse(input.SafeString(), out var result) ? (Guid?)result : null;

    /// <summary>
    /// 转换为Guid集合
    /// </summary>
    /// <param name="input">输入值，以逗号分隔的Guid集合字符串，范例：83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
    public static List<Guid> ToDGGuidList(this string input) => ToDGList<Guid>(input);

    #endregion

    #region ToList(泛型集合转换)

    /// <summary>
    /// 泛型集合转换
    /// </summary>
    /// <typeparam name="T">目标元素类型</typeparam>
    /// <param name="input">输入值，以逗号分隔的元素集合字符串，范例：83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
    public static List<T> ToDGList<T>(this string input)
    {
        var result = new List<T>();
        if (string.IsNullOrWhiteSpace(input))
            return result;
        var array = input.Split(',');
        result.AddRange(from each in array where !string.IsNullOrWhiteSpace(each) select To<T>(each));
        return result;
    }

    #endregion

    #region ToEnum(转换为枚举)

    /// <summary>
    /// 转换为枚举
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="input">输入值</param>
    public static T ToDGEnum<T>(this object input) where T : struct => ToDGEnum<T>(input, default);

    /// <summary>
    /// 转换为枚举
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="input">输入值</param>
    /// <param name="defaultValue">默认值</param>
    public static T ToDGEnum<T>(this object input, T defaultValue) where T : struct => ToDGEnumOrNull<T>(input) ?? defaultValue;

    /// <summary>
    /// 转换为可空枚举
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="input">输入值</param>
    public static T? ToDGEnumOrNull<T>(this object input) where T : struct
    {
        var success = System.Enum.TryParse(input.SafeString(), true, out T result);
        if (success)
            return result;
        return null;
    }

    #endregion

    #region To(通用泛型转换)

    /// <summary>
    /// 通用泛型转换
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="input">输入值</param>
    public static T To<T>(this object input)
    {
        if (input == null)
            return default;
        if (input is string && string.IsNullOrWhiteSpace(input.ToString()))
            return default;

        var type = Common.GetType<T>();
        var typeName = type.Name.ToLower();
        try
        {
            if (typeName == "string")
                return (T)(object)input.ToString();
            if (typeName == "guid")
                return (T)(object)new Guid(input.ToString());
            if (type.IsEnum)
                return Enum.Parse<T>(input);
            if (input is IConvertible)
                return (T)System.Convert.ChangeType(input, type);
            return (T)input;
        }
        catch
        {
            return default;
        }
    }

    #endregion

    #region 转换对象为字符串
    /// <summary>
    /// 转换对象为字符串
    /// </summary>
    /// <param name="Value">对象</param>
    /// <returns>转换后的字符串</returns>
    public static string ObjToString(this object Value)
    {
        if (Value.ObjIsNull())
        {
            return string.Empty;
        }
        return Value.ToString();
    }

    /// <summary>
    /// 转换对象为字符串
    /// </summary>
    /// <param name="Value">对象</param>
    /// <param name="DefaultValue">默认值</param>
    /// <param name="Trim">是否去除空格</param>
    /// <returns>转换后的字符串</returns>
    public static string ObjToString(this object Value, string DefaultValue, bool Trim)
    {
        if (Value.ObjIsNull())
        {
            return ObjToString(DefaultValue);
        }
        if (Trim)
        {
            return Value.ToString().Trim();
        }
        return Value.ToString();
    }

    #endregion

    /// <summary>
    /// 将ip地址转换成long类型
    /// </summary>
    /// <param name="ip">ip</param>
    /// <returns></returns>
    public static long ConvertIPToLong(this string ip)
    {
        if (!ip.Contains("."))
        {
            ip = "127.0.0.1";
        }
        if (!ip.IsIPAddress())
        {
            ip = "127.0.0.1";
        }
        string[] ips = ip.Split('.');
        long number = 16777216L * long.Parse(ips[0]) + 65536L * long.Parse(ips[1]) + 256 * long.Parse(ips[2]) + long.Parse(ips[3]);
        return number;
    }
}
