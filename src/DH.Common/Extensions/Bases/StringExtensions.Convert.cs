using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace DH.Extension;

/// <summary>
/// 字符串(<see cref="string"/>) 扩展 - 转换
/// </summary>
public static partial class StringExtensions
{
    #region ToBytes(转换成Byte[])

    /// <summary>
    /// 将字符串转为byte[]数组，默认编码为<see cref="Encoding.UTF8"/>
    /// </summary>
    /// <param name="value">字符串</param>
    /// <param name="encoding">编码格式</param>
    /// <returns>byte[]数组</returns>
    public static byte[] ToBytes(this string value, Encoding encoding = null)
    {
        encoding = (encoding ?? Encoding.UTF8);
        return encoding.GetBytes(value);
    }

    #endregion

    #region ToXDocument(转换成XDocument)

    /// <summary>
    /// 字符串转为XDocument（Linq to Xml Dom）
    /// </summary>
    /// <param name="xml">xml字符串</param>
    /// <returns></returns>
    public static XDocument ToXDocument(this string xml)
    {
        return XDocument.Parse(xml);
    }

    #endregion

    #region ToXElement(转换成XElement)

    /// <summary>
    /// 字符串转为XElement对象（Linq to Xml XElement）
    /// </summary>
    /// <param name="xml">xml字符串</param>
    /// <returns></returns>
    public static XElement ToXElement(this string xml)
    {
        return XElement.Parse(xml);
    }

    #endregion

    #region ToXmlDocument(转换成XmlDocument)

    /// <summary>
    /// 字符串转为XmlDocument对象（Xml Dom）
    /// </summary>
    /// <param name="xml">xml字符串</param>
    /// <returns></returns>
    public static XmlDocument ToXmlDocument(this string xml)
    {
        var documnet = new XmlDocument();
        documnet.LoadXml(xml);
        return documnet;
    }

    #endregion

    #region ToXPath(转换成XPath)

    /// <summary>
    /// 字符串转为XmlPathDom对象（Xml XPath Dom）
    /// </summary>
    /// <param name="xml">xml字符串</param>
    /// <returns></returns>
    public static XPathNavigator ToXPath(this string xml)
    {
        var documnet = new XPathDocument(new StringReader(xml));
        return documnet.CreateNavigator();
    }

    #endregion

    #region HexStringToBytes(16进制字符串转换成字节数组)

    /// <summary>
    /// 16进制字符串转换为字节数组
    /// </summary>
    /// <param name="value">16进制字符串</param>
    /// <returns>字节数组</returns>
    public static byte[] HexStringToBytes(this string value)
    {
        value = value.Replace(" ", "");
        int maxByte = value.Length / 2 - 1;
        var bytes = new byte[maxByte + 1];
        for (int i = 0; i <= maxByte; i++)
        {
            bytes[i] = byte.Parse(value.Substring(2 * i, 2), NumberStyles.AllowHexSpecifier);
        }
        return bytes;
    }

    #endregion

    #region ToUnicodeString(转换成Unicode字符串)

    /// <summary>
    /// 转换成Unicode字符串
    /// </summary>
    /// <param name="source">源字符串</param>
    /// <returns>Unicode编码后的字符串</returns>
    public static string ToUnicodeString(this string source)
    {
        var bytes = Encoding.Unicode.GetBytes(source);
        var stringBuilder = new StringBuilder();
        for (var i = 0; i < bytes.Length; i += 2)
        {
            stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
        }
        return stringBuilder.ToString();
    }

    #endregion

    #region ToUnicodeString(Unicode转字符串)
    /// <summary>
    /// Unicode转字符串
    /// </summary>
    /// <param name="source">经过Unicode编码的字符串</param>
    /// <returns>正常字符串</returns>
    public static string UnicodeToString(this string source)
    {
        return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(source, x => Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)).ToString());
    }
    #endregion

    #region ToSecureString(转换成安全字符串)

    /// <summary>
    /// 将字符串转为安全字符串
    /// </summary>
    /// <param name="value">普通字符串</param>
    /// <param name="markReadOnly">是否只读</param>
    /// <returns>安全字符串</returns>
    public static SecureString ToSecureString(this string value, bool markReadOnly = true)
    {
        if (value.IsEmpty())
        {
            return null;
        }
        SecureString temp = new SecureString();
        foreach (char c in value)
        {
            temp.AppendChar(c);
        }
        if (markReadOnly)
        {
            temp.MakeReadOnly();
        }
        return temp;
    }

    #endregion

    #region ToUnSecureString(转换成普通字符串)

    /// <summary>
    /// 将安全字符串转为普通字符串
    /// </summary>
    /// <param name="value">安全字符串</param>
    /// <returns>普通字符串</returns>
    public static string ToUnSecureString(this SecureString value)
    {
        if (ReferenceEquals(value, null))
        {
            return null;
        }
        IntPtr unmanagedString = IntPtr.Zero;
        try
        {
            unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(value);
            return Marshal.PtrToStringUni(unmanagedString);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
        }
    }

    #endregion

    #region ToSbcCase(转换成全角)

    /// <summary>
    /// 将字符串转换成全角字符串(SBC Case)
    /// </summary>
    /// <param name="input">任意字符串</param>
    /// <returns></returns>
    public static string ToSbcCase(this string input)
    {
        char[] c = input.ToCharArray();
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] == 32)
            {
                c[i] = (char)12288;
                continue;
            }
            if (c[i] < 127)
            {
                c[i] = (char)(c[i] + 65248);
            }
        }
        return new string(c);
    }

    #endregion

    #region ToDbcCase(转换成半角)

    /// <summary>
    /// 将字符串转换成半角字符串(DBC Case)
    /// </summary>
    /// <param name="input">任意字符串</param>
    /// <returns></returns>
    public static string ToDbcCase(this string input)
    {
        char[] c = input.ToCharArray();
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] == 12288)
            {
                c[i] = (char)32;
                continue;
            }
            if (c[i] > 35280 && c[i] < 65375)
            {
                c[i] = (char)(c[i] - 65248);
            }
        }
        return new string(c);
    }

    #endregion

    #region ToDateTime(时间戳转换成时间)

    /// <summary>
    /// 将时间戳转换成时间
    /// </summary>
    /// <param name="timeStamp">时间戳格式字符串</param>
    /// <returns></returns>
    public static DateTime StampToDateTime(this string timeStamp)
    {
        if (timeStamp.Length > 10)
        {
            timeStamp = timeStamp.Substring(0, 10);
        }
#pragma warning disable CS0618 // '“TimeZone”已过时:“System.TimeZone has been deprecated.  Please investigate the use of System.TimeZoneInfo instead.”
        DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
#pragma warning restore CS0618 // '“TimeZone”已过时:“System.TimeZone has been deprecated.  Please investigate the use of System.TimeZoneInfo instead.”
        long lIime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lIime);
        return dateTimeStart.Add(toNow);
    }

    public static DateTime StampToDateTime(this long timeStamp)
    {
#pragma warning disable CS0618 // '“TimeZone”已过时:“System.TimeZone has been deprecated.  Please investigate the use of System.TimeZoneInfo instead.”
        var dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
#pragma warning restore CS0618 // '“TimeZone”已过时:“System.TimeZone has been deprecated.  Please investigate the use of System.TimeZoneInfo instead.”
        TimeSpan toNow = new TimeSpan(timeStamp);
        return dtStart.Add(toNow);
    }

    #endregion
}