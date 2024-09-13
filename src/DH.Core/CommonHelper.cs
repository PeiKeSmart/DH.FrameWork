using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

using DH.Exceptions;

using Pek.Infrastructure;

namespace DH.Core;

/// <summary>
/// 表示公共帮助程序
/// </summary>
public partial class CommonHelper
{
    #region 字段

    //we use EmailValidator from FluentValidation. So let's keep them sync - https://github.com/JeremySkinner/FluentValidation/blob/master/src/FluentValidation/Validators/EmailValidator.cs
    private const string EMAIL_EXPRESSION = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";

    private static readonly Regex _emailRegex;

    #endregion

    #region 初始化

    static CommonHelper()
    {
        _emailRegex = new Regex(EMAIL_EXPRESSION, RegexOptions.IgnoreCase);
    }

    #endregion

    #region Methods

    /// <summary>
    /// 确保订户发送电子邮件或投掷。
    /// </summary>
    /// <param name="email">电子邮件。</param>
    /// <returns></returns>
    public static string EnsureSubscriberEmailOrThrow(string email)
    {
        var output = EnsureNotNull(email);
        output = output.Trim();
        output = EnsureMaximumLength(output, 255);

        if (!IsValidEmail(output))
        {
            throw new DHException("电子邮件无效。");
        }

        return output;
    }

    /// <summary>
    /// 验证字符串是否为有效的电子邮件格式
    /// </summary>
    /// <param name="email">电子邮件验证</param>
    /// <returns>如果字符串是有效的电子邮件地址，则为true；如果不是，则为false</returns>
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        email = email.Trim();

        return _emailRegex?.IsMatch(email) == true;
    }

    /// <summary>
    /// 验证字符串是否为有效的IP地址
    /// </summary>
    /// <param name="ipAddress">IP地址</param>
    /// <returns>如果字符串是有效的IpAddress，则为true；如果不是，则为false</returns>
    public static bool IsValidIpAddress(string ipAddress)
    {
        return IPAddress.TryParse(ipAddress, out var _);
    }

    /// <summary>
    /// 生成随机数字代码
    /// </summary>
    /// <param name="length">长度</param>
    /// <returns>返回字符串</returns>
    public static string GenerateRandomDigitCode(int length)
    {
        using var random = new SecureRandomNumberGenerator();
        var str = string.Empty;
        for (var i = 0; i < length; i++)
            str = string.Concat(str, random.Next(10).ToString());
        return str;
    }

    /// <summary>
    /// 返回指定范围内的随机整数
    /// </summary>
    /// <param name="min">最小数量</param>
    /// <param name="max">最大数量</param>
    /// <returns>Result</returns>
    public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
    {
        using var random = new SecureRandomNumberGenerator();
        return random.Next(min, max);
    }

    /// <summary>
    /// 确保字符串不超过允许的最大长度
    /// </summary>
    /// <param name="str">输入字符串</param>
    /// <param name="maxLength">最大长度</param>
    /// <param name="postfix">如果原始字符串被缩短，则要添加到末尾的字符串</param>
    /// <returns>如果字符串长度正常，则输入字符串；否则，截断输入字符串</returns>
    public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        if (str.Length <= maxLength)
            return str;

        var pLen = postfix?.Length ?? 0;

        var result = str[0..(maxLength - pLen)];
        if (!string.IsNullOrEmpty(postfix))
        {
            result += postfix;
        }

        return result;
    }

    /// <summary>
    /// 确保字符串仅包含数值
    /// </summary>
    /// <param name="str">输入字符串</param>
    /// <returns>仅包含数值的输入字符串，如果输入为null/空，则为空字符串</returns>
    public static string EnsureNumericOnly(string str)
    {
        return string.IsNullOrEmpty(str) ? string.Empty : new string(str.Where(char.IsDigit).ToArray());
    }

    /// <summary>
    /// 确保字符串不为空
    /// </summary>
    /// <param name="str">输入字符串</param>
    /// <returns>返回字符串</returns>
    public static string EnsureNotNull(string str)
    {
        return str ?? string.Empty;
    }

    /// <summary>
    /// 指示是否指定的字符串是null或空字符串
    /// </summary>
    /// <param name="stringsToValidate">要验证的字符串数组</param>
    /// <returns>返回布尔值</returns>
    public static bool AreNullOrEmpty(params string[] stringsToValidate)
    {
        return stringsToValidate.Any(string.IsNullOrEmpty);
    }

    /// <summary>
    /// 比较两个数组
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="a1">数组1</param>
    /// <param name="a2">数组2</param>
    /// <returns>返回布尔值</returns>
    public static bool ArraysEqual<T>(T[] a1, T[] a2)
    {
        //also see Enumerable.SequenceEqual(a1, a2);
        if (ReferenceEquals(a1, a2))
            return true;

        if (a1 == null || a2 == null)
            return false;

        if (a1.Length != a2.Length)
            return false;

        var comparer = EqualityComparer<T>.Default;
        return !a1.Where((t, i) => !comparer.Equals(t, a2[i])).Any();
    }

    /// <summary>
    /// 将对象的特性设置为值。
    /// </summary>
    /// <param name="instance">要设置其属性的对象。</param>
    /// <param name="propertyName">要设置的属性的名称。</param>
    /// <param name="value">要将属性设置为的值。</param>
    public static void SetProperty(object instance, string propertyName, object value)
    {
        if (instance == null)
            throw new ArgumentNullException(nameof(instance));
        if (propertyName == null)
            throw new ArgumentNullException(nameof(propertyName));

        var instanceType = instance.GetType();
        var pi = instanceType.GetProperty(propertyName);
        if (pi == null)
            throw new DHException("在类型'{1}'的实例上找不到属性'{0}'", propertyName, instanceType);
        if (!pi.CanWrite)
            throw new DHException("类型'{1}'的实例上的属性'{0}'没有setter", propertyName, instanceType);
        if (value != null && !value.GetType().IsAssignableFrom(pi.PropertyType))
            value = To(value, pi.PropertyType);
        pi.SetValue(instance, value, Array.Empty<object>());
    }

    /// <summary>
    /// 将值转换为目标类型。
    /// </summary>
    /// <param name="value">要转换的值。</param>
    /// <param name="destinationType">要将值转换为的类型。</param>
    /// <returns>转换后的值。</returns>
    public static object To(object value, Type destinationType)
    {
        return To(value, destinationType, CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// 将值转换为目标类型。
    /// </summary>
    /// <param name="value">要转换的值。</param>
    /// <param name="destinationType">要将值转换为的类型。</param>
    /// <param name="culture">文化</param>
    /// <returns>转换后的值。</returns>
    public static object To(object value, Type destinationType, CultureInfo culture)
    {
        if (value == null)
            return null;

        var sourceType = value.GetType();

        var destinationConverter = TypeDescriptor.GetConverter(destinationType);
        if (destinationConverter.CanConvertFrom(value.GetType()))
            return destinationConverter.ConvertFrom(null, culture, value);

        var sourceConverter = TypeDescriptor.GetConverter(sourceType);
        if (sourceConverter.CanConvertTo(destinationType))
            return sourceConverter.ConvertTo(null, culture, value, destinationType);

        if (destinationType.IsEnum && value is int)
            return Enum.ToObject(destinationType, (int)value);

        if (!destinationType.IsInstanceOfType(value))
            return Convert.ChangeType(value, destinationType, culture);

        return value;
    }

    /// <summary>
    /// 将值转换为目标类型。
    /// </summary>
    /// <param name="value">要转换的值。</param>
    /// <typeparam name="T">要将值转换为的类型。</typeparam>
    /// <returns>转换后的值。</returns>
    public static T To<T>(object value)
    {
        //return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        return (T)To(value, typeof(T));
    }

    /// <summary>
    /// 转换前端枚举
    /// </summary>
    /// <param name="str">输入字符串</param>
    /// <returns>转换的字符串</returns>
    public static string ConvertEnum(string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;
        var result = string.Empty;
        foreach (var c in str)
            if (c.ToString() != c.ToString().ToLowerInvariant())
                result += " " + c.ToString();
            else
                result += c.ToString();

        // 确保没有空格（例如，当第一个字母为大写时）
        result = result.TrimStart();
        return result;
    }

    /// <summary>
    /// 以年为单位获得差异
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public static int GetDifferenceInYears(DateTime startDate, DateTime endDate)
    {
        //来源：http://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-in-c

        //这假设你正在寻找西方的年龄观念，而不是使用东亚的计算方法。
        var age = endDate.Year - startDate.Year;
        if (startDate > endDate.AddYears(-age))
            age--;
        return age;
    }

    /// <summary>
    /// 使用当前线程区域性的约定将DateTime设置为指定的年、月和日
    /// </summary>
    /// <param name="year">年</param>
    /// <param name="month">月</param>
    /// <param name="day">日</param>
    /// <returns>可为空的实例</returns>
    public static DateTime? ParseDate(int? year, int? month, int? day)
    {
        if (!year.HasValue || !month.HasValue || !day.HasValue)
            return null;

        DateTime? date = null;
        try
        {
            date = new DateTime(year.Value, month.Value, day.Value, CultureInfo.CurrentCulture.Calendar);
        }
        catch { }
        return date;
    }

    /// <summary>
    /// Get private fields property value
    /// </summary>
    /// <param name="target">Target object</param>
    /// <param name="fieldName">Field name</param>
    /// <returns>Value</returns>
    public static object GetPrivateFieldValue(object target, string fieldName)
    {
        if (target == null)
        {
            throw new ArgumentNullException(nameof(target), "The assignment target cannot be null.");
        }

        if (string.IsNullOrEmpty(fieldName))
        {
            throw new ArgumentException("fieldName", "The field name cannot be null or empty.");
        }

        var t = target.GetType();
        FieldInfo fi = null;

        while (t != null)
        {
            fi = t.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);

            if (fi != null) break;

            t = t.BaseType;
        }

        if (fi == null)
        {
            throw new Exception($"Field '{fieldName}' not found in type hierarchy.");
        }

        return fi.GetValue(target);
    }

    #endregion

    #region Properties

    /// <summary>
    /// 获取或设置默认文件提供程序
    /// </summary>
    public static IDHFileProvider DefaultFileProvider { get; set; }

    #endregion
}
