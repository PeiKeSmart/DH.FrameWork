using DH.Helpers;

using Pek.Helpers;

using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace DH;

/// <summary>
/// 系统扩展 - 验证
/// </summary>
public static partial class DHExtensions
{
    #region IsNull(是否为空)

    /// <summary>
    /// 判断目标对象是否为空
    /// </summary>
    /// <param name="target">目标对象</param>
    public static bool IsNull(this object target) => target.IsNull<object>();

    /// <summary>
    /// 判断目标对象是否为空
    /// </summary>
    /// <typeparam name="T">目标对象类型</typeparam>
    /// <param name="target">目标对象</param>
    public static bool IsNull<T>(this T target) => ReferenceEquals(target, null);

    #endregion

    #region NotEmpty(是否非空)

    /// <summary>
    /// 判断 字符串 是否非空
    /// </summary>
    /// <param name="value">数据</param>
    public static bool NotEmpty(this string value) => !string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// 判断 Guid 是否非空
    /// </summary>
    /// <param name="value">数据</param>
    public static bool NotEmpty(this Guid value) => value != Guid.Empty;

    /// <summary>
    /// 判断 Guid? 是否非空
    /// </summary>
    /// <param name="value">数据</param>
    public static bool NotEmpty(this Guid? value) => value != null && value != Guid.Empty;

    /// <summary>
    /// 判断 StringBuilder 是否为空
    /// </summary>
    /// <param name="sb">数据</param>
    public static bool NotEmpty(this StringBuilder sb) => sb != null && sb.Length != 0 && sb.ToString().NotEmpty();

    /// <summary>
    /// 判断 迭代集合 是否非空
    /// </summary>
    /// <typeparam name="T">泛型对象</typeparam>
    /// <param name="enumerable">数据</param>
    public static bool NotEmpty<T>(this IEnumerable<T> enumerable)
    {
        if (enumerable == null)
            return false;
        if (enumerable.Any())
            return true;
        return false;
    }

    #endregion

    #region 检测是否有Sql危险字符

    /// <summary>
    /// 检测是否有Sql危险字符
    /// </summary>
    /// <param name="str">要判断字符串</param>
    /// <returns>判断结果</returns>
    public static bool IsSafeSqlString(this string str)
    {
        return !Pek.DHExtensions.QuickValidate(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
    }

    #endregion 检测是否有Sql危险字符

    

    

    #region 判断对象是否为布尔值

    /// <summary>
    /// 判断对象是否为布尔值
    /// </summary>
    /// <param name="Value">对象</param>
    /// <returns>bool 是为 true ，否则 false</returns>
    public static bool IsBool(this object Value)
    {
        string[] array = new string[] { "true", "false", "yes", "no", "1", "0" };
        return (Array.IndexOf(array, Value.ObjToString().ToLower()) >= 0);
    }

    #endregion 判断对象是否为布尔值

    #region 数字判断

    /// <summary>
    /// 判断对象是否为整型数值
    /// </summary>
    /// <param name="Value">要判断的对象</param>
    /// <returns>是否为正整数</returns>
    /// <remarks>只能匹配正整数和0</remarks>
    public static bool IsInt(this object Value)
    {
        return Pek.DHExtensions.QuickValidate(Value, "[0-9]*$");
    }

    /// <summary>
    /// 验证是否为正整数
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsInt(this string str)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(str, @"^[0-9]*$");
    }

    /// <summary>
    /// 判断一个字符串是否为Int
    /// </summary>
    /// <param name="_value">要判断的对象</param>
    /// <returns>是否为正负整数型</returns>
    public static bool IsInt1(this string _value)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(-){0,1}\d+$");
        if (regex.Match(_value).Success)
        {
            if ((long.Parse(_value) > 0x7fffffffL) || (long.Parse(_value) < -2147483648L))
            {
                return false;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// 判断对象是否为Int32类型的数字,包含小数。
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static bool IsNumeric(this string expression)
    {
        if (!Pek.DHExtensions.StrIsNullOrEmpty(expression))
        {
            string str = expression;
            if (str.Length > 0 && str.Length <= 11 && System.Text.RegularExpressions.Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
            {
                if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 判断对象是否为Int32类型的数字
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static bool IsNumeric(this object expression)
    {
        if (!Pek.DHExtensions.ObjIsNull(expression))
            return IsNumeric(expression.ToString());

        return false;
    }

    /// <summary>
    /// 是否是浮点数
    /// </summary>
    /// <param name="_value">输入字符串</param>
    /// <returns></returns>
    public static bool IsDecimal(this string _value)
    {
        return Pek.DHExtensions.QuickValidate(_value, @"^[0-9]+[.]?[0-9]+$");
    }

    /// <summary>
    /// 是否是浮点数 可带正负号
    /// </summary>
    /// <param name="_value">输入字符串</param>
    /// <returns></returns>
    public static bool IsDecimalSign(this string _value)
    {
        return Pek.DHExtensions.QuickValidate(_value, @"^[+-]?[0-9]+[.]?[0-9]+$");  //等价于^[+-]?\d+[.]?\d+$
    }

    /// <summary>
    /// 判断对象是否为浮点型数值
    /// </summary>
    /// <param name="Value">要判断的对象</param>
    /// <returns>bool 是为 true ，否则 false</returns>
    public static bool IsFloat(this object Value)
    {
        return Pek.DHExtensions.QuickValidate(Value, "^(-?[0-9]*[.]*[0-9]*)$");
    }

    /// <summary>
    /// 是否为Double类型
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static bool IsDouble(this object expression)
    {
        if (!Pek.DHExtensions.ObjIsNull(expression))
            return Pek.DHExtensions.QuickValidate(expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");

        return false;
    }

    #endregion 数字判断

    

    #region 邮件地址

    /// <summary>
    /// 检测是否符合email格式
    /// </summary>
    /// <param name="strEmail">要判断的email字符串</param>
    /// <returns>判断结果</returns>
    public static bool IsEmail(this string strEmail)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(strEmail, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }

    #endregion 邮件地址

    #region 检测是否符合email格式

    /// <summary>
    /// 检测是否符合email格式
    /// </summary>
    /// <param name="strEmail">要判断的email字符串</param>
    /// <returns>判断结果</returns>
    public static bool IsValidEmail(this string strEmail)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
    }

    public static bool IsValidDoEmail(this string strEmail)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }

    #endregion 检测是否符合email格式

    #region 字符串是否可以转化为日期

    /// <summary>
    /// 检查一个字符串是否可以转化为日期，一般用于验证用户输入日期的合法性。
    /// </summary>
    /// <param name="_value">需验证的字符串。</param>
    /// <returns>是否可以转化为日期的bool值。</returns>
    public static bool IsDateTime(this string _value)
    {
        DateTime dTime;
        if (!DateTime.TryParse(_value, out dTime))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 判断是否是时间格式
    /// </summary>
    /// <returns></returns>
    public static bool IsTime(this string timeval)
    {
        return Pek.DHExtensions.QuickValidate(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
    }

    /// <summary>
    /// 判断字符串是否是yy-mm-dd字符串
    /// </summary>
    /// <param name="str">待判断字符串</param>
    /// <returns>判断结果</returns>
    public static bool IsDateString(this string str)
    {
        return Pek.DHExtensions.QuickValidate(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
    }

    #endregion 字符串是否可以转化为日期

    #region IsZeroOrMinus(是否为0或负数)

    /// <summary>
    /// 判断 short 是否为0或负数
    /// </summary>
    /// <param name="value">数据</param>
    public static bool IsZeroOrMinus(this short value) => value <= 0;

    /// <summary>
    /// 判断 int 是否为0或负数
    /// </summary>
    /// <param name="value">数据</param>
    public static bool IsZeroOrMinus(this int value) => value <= 0;

    /// <summary>
    /// 判断 long 是否为0或负数
    /// </summary>
    /// <param name="value">数据</param>
    public static bool IsZeroOrMinus(this long value) => value <= 0;

    /// <summary>
    /// 判断 float 是否为0或负数
    /// </summary>
    /// <param name="value">数据</param>
    public static bool IsZeroOrMinus(this float value) => value <= 0;

    /// <summary>
    /// 判断 double 是否为0或负数
    /// </summary>
    /// <param name="value">数据</param>
    public static bool IsZeroOrMinus(this double value) => value <= 0;

    /// <summary>
    /// 判断 decimal 是否为0或负数
    /// </summary>
    /// <param name="value">数据</param>
    public static bool IsZeroOrMinus(this decimal value) => value <= 0;

    #endregion

    #region IsPercentage(是否为百分数)

    /// <summary>
    /// 判断 float 是否为百分数
    /// </summary>
    /// <param name="value">数据</param>
    public static bool IsPercentage(this float value) => value > 0 && value <= 1;

    /// <summary>
    /// 判断 double 是否为百分数
    /// </summary>
    /// <param name="value">数据</param>
    public static bool IsPercentage(this double value) => value > 0 && value <= 1;

    /// <summary>
    /// 判断 decimal 是否为百分数
    /// </summary>
    /// <param name="value">数据</param>
    public static bool IsPercentage(this decimal value) => value > 0 && value <= 1;

    #endregion

    #region IsZeroOrPercentage(是否为0或百分数)

    /// <summary>
    /// 判断 float 是否为0或百分数
    /// </summary>
    /// <param name="value">数据</param>
    public static bool IsZeroOrPercentage(this float value) => value.IsPercentage() || value.Equals(0f);

    /// <summary>
    /// 判断 double 是否为0或百分数
    /// </summary>
    /// <param name="value">数据</param>
    public static bool IsZeroOrPercentage(this double value) => value.IsPercentage() || value.Equals(0d);

    /// <summary>
    /// 判断 decimal 是否为0或百分数
    /// </summary>
    /// <param name="value">数据</param>
    public static bool IsZeroOrPercentage(this decimal value) => value.IsPercentage() || value.Equals(0m);

    #endregion
}