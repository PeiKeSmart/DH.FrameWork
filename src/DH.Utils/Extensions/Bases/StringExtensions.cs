using DH.Extensions;

using NewLife;

using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace DH.Extension;

/// <summary>
/// 字符串(<see cref="string"/>) 扩展
/// </summary>
public static partial class StringExtensions
{
    #region Remove(移除字符串)
    /// <summary>
    /// 从当前字符串中移除任何指定的字符
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="removeChar">需要移除的字符</param>
    /// <returns></returns>
    public static string Remove(this string value, params char[] removeChar)
    {
        var result = value;
        if (!string.IsNullOrEmpty(result) && removeChar != null)
        {
            Array.ForEach(removeChar, c => result = result.Remove(c.ToString()));
        }
        return result;
    }

    /// <summary>
    /// 从当前字符串中移除任何指定的字符串
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="strings">需要移除的字符串</param>
    /// <returns></returns>
    public static string Remove(this string value, params string[] strings)
    {
        return strings.Aggregate(value, (current, c) => current.Replace(c, string.Empty));
    }

    /// <summary>
    /// 从当前字符串中移除指定索引的字符串
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="index">索引</param>
    /// <param name="isLeft">是否左侧</param>
    /// <returns></returns>
    public static string Remove(this string value, int index, bool isLeft = true)
    {
        if (value.Length <= index)
        {
            return "";
        }
        if (isLeft)
        {
            return value.Substring(index);
        }
        return value.Substring(0, value.Length - index);
    }

    /// <summary>
    /// 移除当前字符串中的所有特殊字符
    /// </summary>
    /// <param name="value">输入字符串</param>
    /// <returns>调整后的字符串</returns>
    public static string RemoveAllSpecialCharacters(this string value)
    {
        StringBuilder sb = new StringBuilder(value.Length);
        foreach (var c in value.Where(Char.IsLetterOrDigit))
        {
            sb.Append(c);
        }
        return sb.ToString();
    }

    /// <summary>
    /// 去除字符串末尾指定的符号
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="defaultChar">需要去除的符号，默认：,</param>
    /// <returns></returns>
    public static string RemoveEnd(this string value, string defaultChar = ",")
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        if (string.IsNullOrWhiteSpace(defaultChar))
        {
            return value.SafeString();
        }

        if (value.ToLower().EndsWith(defaultChar.ToLower()))
        {
            return value.Remove(value.Length - defaultChar.Length, defaultChar.Length);
        }
        return value;
    }

    /// <summary>
    /// 指定清除标签的内容
    /// </summary>
    /// <param name="str">内容</param>
    /// <param name="tag">标签</param>
    /// <param name="options">选项</param>
    /// <returns></returns>
    public static string Remove(this string str, string tag, RegexOptions options = RegexOptions.None)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return string.Empty;
        }
        return tag.IsEmpty() ? str : Regex.Replace(str, tag, "", options);
    }
    #endregion

    #region ReverseString(反转字符串)
    /// <summary>
    /// 反转字符串
    /// </summary>
    /// <param name="value">要反转的字符串</param>
    /// <returns>反转后的字符串</returns>
    public static string ReverseString(this string value)
    {
        value.CheckNotNull("value");
        return new string(value.Reverse().ToArray());
    }
    #endregion

    #region Split(字符串分割成数组)
    /// <summary>
    /// 以指定字符串作为分隔符将指定字符串分隔成数组
    /// </summary>
    /// <param name="value">要分割的字符串</param>
    /// <param name="strSplit">字符串类型的分隔符</param>
    /// <param name="removeEmptyEntries">是否移除数据中元素为空字符串的项</param>
    /// <returns>分割后的数据</returns>
    public static string[] Split(this string value, string strSplit, bool removeEmptyEntries = false)
    {
        return value.Split(new[] { strSplit },
            removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
    }

    /// <summary>
    /// 分割字符串
    /// </summary>
    /// <param name="sourceStr">源字符串</param>
    /// <param name="splitStr">分隔字符串</param>
    /// <returns></returns>
    public static string[] SplitString(this string sourceStr, string splitStr)
    {
        if (string.IsNullOrEmpty(sourceStr) || string.IsNullOrEmpty(splitStr))
            return new string[0] { };

        if (sourceStr.IndexOf(splitStr) == -1)
            return new string[] { sourceStr };

        if (splitStr.Length == 1)
            return sourceStr.Split(splitStr[0]);
        else
            return Regex.Split(sourceStr, Regex.Escape(splitStr), RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// 分割字符串
    /// </summary>
    /// <param name="sourceStr">源字符串</param>
    /// <returns></returns>
    public static string[] SplitString(this string sourceStr)
    {
        return SplitString(sourceStr, ",");
    }
    #endregion

    #region 截取字符串

    /// <summary>
    /// 截取字符串
    /// </summary>
    /// <param name="sourceStr">源字符串</param>
    /// <param name="startIndex">开始位置的索引</param>
    /// <param name="length">子字符串的长度</param>
    /// <returns></returns>
    public static string SubString(this string sourceStr, int startIndex, int length)
    {
        if (!string.IsNullOrEmpty(sourceStr))
        {
            if (sourceStr.Length >= (startIndex + length))
                return sourceStr.Substring(startIndex, length);
            else
                return sourceStr.Substring(startIndex);
        }

        return "";
    }

    /// <summary>
    /// 截取字符串
    /// </summary>
    /// <param name="sourceStr">源字符串</param>
    /// <param name="length">子字符串的长度</param>
    /// <returns></returns>
    public static string SubString(this string sourceStr, int length)
    {
        return SubString(sourceStr, 0, length);
    }

    #endregion

    #region GetTextLength(获取字符串长度)
    /// <summary>
    /// 获取字符串长度，支持汉字，每个汉字长度为2个字节
    /// </summary>
    /// <param name="value">参数字符串</param>
    /// <returns>当前字符串的长度，每个汉字长度为2个字节</returns>
    public static int GetTextLength(this string value)
    {
        ASCIIEncoding ascii = new ASCIIEncoding();
        int tempLen = 0;
        byte[] bytes = ascii.GetBytes(value);
        foreach (byte b in bytes)
        {
            if (b == 63)
            {
                tempLen += 2;
            }
            else
            {
                tempLen += 1;
            }
        }
        return tempLen;
    }
    #endregion

    #region TrimToMaxLength(切割字符串)
    /// <summary>
    /// 切割字符串，指定最大长度
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="maxLength">指定最大长度</param>
    /// <returns></returns>
    public static string TrimToMaxLength(this string value, int maxLength)
    {
        return (value == null || value.Length <= maxLength ? value : value.Substring(0, maxLength));
    }

    /// <summary>
    /// 切割字符串，并指定最大长度和添加后缀
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="maxLength">指定最大长度</param>
    /// <param name="suffix">后缀</param>
    /// <returns></returns>
    public static string TrimToMaxLength(this string value, int maxLength, string suffix)
    {
        return (value == null || value.Length <= maxLength ? value : string.Concat(value.Substring(0, maxLength), suffix));
    }
    #endregion

    #region Truncate(截断字符串)
    /// <summary>
    /// 截断字符串，是否添加圆点
    /// </summary>
    /// <param name="value">字符串</param>
    /// <param name="length">截断长度</param>
    /// <param name="userElipse">是否使用圆点</param>
    /// <returns></returns>
    public static string Truncate(this string value, int length, bool userElipse = false)
    {
        int e = userElipse ? 3 : 0;
        if (length - e <= 0)
        {
            throw new InvalidOperationException($"Length must be greater than {e}.");
        }
        if (value.IsEmpty() || value.Length <= length)
        {
            return value;
        }
        return value.Substring(0, length - e) + new string('.', e);
    }
    #endregion

    #region PadBoth(指定字符串长度)
    /// <summary>
    /// 指定字符串长度，如果字符串长度大于指定的字符串长度，则截断字符串，若字符串长度小于指定字符串长度，则填充字符到指定字符串长度
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="width">指定字符串长度</param>
    /// <param name="padChar">填充字符</param>
    /// <param name="truncate">是否截断</param>
    /// <returns></returns>
    public static string PadBoth(this string value, int width, char padChar, bool truncate = false)
    {
        int diff = width - value.Length;
        if (diff == 0 || diff < 0 && !(truncate))
        {
            return value;
        }
        else if (diff < 0)
        {
            return value.Substring(0, width);
        }
        else
        {
            return value.PadLeft(width - diff / 2, padChar).PadRight(width, padChar);
        }
    }
    #endregion

    #region Ensure(确保字符串包含指定字符串)
    /// <summary>
    /// 确保字符串包含指定前缀
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="prefix">前缀</param>
    /// <returns></returns>
    public static string EnsureStartsWith(this string value, string prefix)
    {
        return value.StartsWith(prefix) ? value : string.Concat(prefix, value);
    }
    /// <summary>
    /// 确保字符串包含指定后缀
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="suffix">后缀</param>
    /// <returns></returns>
    public static string EnsureEndWith(this string value, string suffix)
    {
        return value.EndsWith(suffix) ? value : string.Concat(value, suffix);
    }
    #endregion

    #region Repeat(重复指定字符串)
    /// <summary>
    /// 重复指定字符串，根据指定重复次数
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="repeatCount">重复次数</param>
    /// <returns>重复字符串</returns>
    public static string Repeat(this string value, int repeatCount)
    {
        if (value.Length == 1)
        {
            return new string(value[0], repeatCount);
        }
        StringBuilder sb = new StringBuilder(repeatCount * value.Length);
        while (repeatCount-- > 0)
        {
            sb.Append(value);
        }
        return sb.ToString();
    }
    #endregion

    #region ExtractNumber(提取字符串中所有数字)
    /// <summary>
    /// 提取指定字符串中所有数字
    /// </summary>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static string ExtractNumber(this string value)
    {
        return
            value.Where(Char.IsDigit).Aggregate(new StringBuilder(value.Length), (sb, c) => sb.Append(c)).ToString();
    }
    #endregion

    #region ConcatWith(连接字符串)
    /// <summary>
    /// 连接两个字符串
    /// </summary>
    /// <param name="value">目标字符串</param>
    /// <param name="values">源字符串</param>
    /// <returns>连接后的字符串</returns>
    public static string ConcatWith(this string value, params string[] values)
    {
        return string.Concat(value, string.Concat(values));
    }
    #endregion

    #region Join(连接元素)
    /// <summary>
    /// 连接字符串数组的所有元素，根据指定分隔符
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="value">值</param>
    /// <param name="separator">分隔符</param>
    /// <param name="obj">对象数组</param>
    /// <returns></returns>
    public static string Join<T>(this string value, string separator, T[] obj)
    {
        if (obj == null || obj.Length == 0)
        {
            return value;
        }
        if (separator == null)
        {
            separator = string.Empty;
        }
        Converter<T, string> converter = o => o.ToString();
        StringBuilder sb = new StringBuilder();
        sb.Append(value);
        sb.Append(separator);
        sb.Append(string.Join(separator, Array.ConvertAll(obj, converter)));
        return sb.ToString();
    }

    /// <summary>
    /// 将字符串数组连接为字符串，如果值不为null或System.String.Empty，则将字符串数组连接
    /// </summary>
    /// <param name="values">字符串数组</param>
    /// <param name="separator">分隔符</param>
    /// <returns>字符串</returns>
    public static string JoinNotNullOrEmpty(this string[] values, string separator)
    {
        var items = values.Where(s => !string.IsNullOrEmpty(s)).ToList();
        return string.Join(separator, items.ToArray());
    }
    #endregion

    #region Get(获取范围字符串)
    /// <summary>
    /// 获取指定字符串参数之前的字符串
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="x">指定字符串参数</param>
    /// <returns></returns>
    public static string GetBefore(this string value, string x)
    {
        var xPos = value.IndexOf(x, StringComparison.Ordinal);
        return xPos == -1 ? string.Empty : value.Substring(0, xPos);
    }

    /// <summary>
    /// 获取指定字符串参数之间的字符串
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="x">指定左侧字符串参数</param>
    /// <param name="y">指定右侧字符串参数</param>
    /// <returns></returns>
    public static string GetBetween(this string value, string x, string y)
    {
        var xPos = value.IndexOf(x, StringComparison.Ordinal);
        var yPos = value.LastIndexOf(y, StringComparison.Ordinal);
        if (xPos == -1 || yPos == -1)
        {
            return string.Empty;
        }
        var startIndex = xPos + x.Length;
        return startIndex >= yPos ? string.Empty : value.Substring(startIndex, yPos - startIndex).Trim();
    }

    /// <summary>
    /// 获取指定字符串参数之后的字符串
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="x">指定字符串参数</param>
    /// <returns></returns>
    public static string GetAfter(this string value, string x)
    {
        var xPos = value.IndexOf(x, StringComparison.Ordinal);
        if (xPos == -1)
        {
            return string.Empty;
        }
        var startIndex = xPos + x.Length;
        return startIndex >= value.Length ? string.Empty : value.Substring(startIndex).Trim();
    }

    #region 取左、中、右
    /// <summary>
    /// 取左边的字符
    /// </summary>
    /// <param name="sSource">字符串</param>
    /// <param name="iLength">要取长度</param>
    /// <returns></returns>
    public static string Left(this string sSource, int iLength)
    {
        if (!sSource.IsNullOrEmpty())
        {
            if (iLength > sSource.Length)
            {
                return sSource;
            }
            else
            {
                return sSource.Substring(0, iLength);
            }
        }
        return string.Empty;
    }

    /// <summary>
    /// 取右边的字符
    /// </summary>
    /// <param name="sSource">字符串</param>
    /// <param name="iLength">要取长度</param>
    /// <returns></returns>
    public static String Right(this string sSource, int iLength)
    {
        if (!sSource.IsNullOrEmpty())
        {
            if (iLength > sSource.Length)
            {
                return sSource;
            }
            else
            {
                return sSource.Substring(sSource.Length - iLength, iLength);
            }
        }
        return string.Empty;
    }

    /// <summary>
    /// 取中间的字符。
    /// </summary>
    /// <param name="sSource">字符串</param>
    /// <param name="iStart">开始长度</param>
    /// <param name="iLength">结束长度</param>
    /// <returns></returns>
    public static String Mid(this string sSource, int iStart, int iLength)
    {
        if (!sSource.IsNullOrEmpty())
        {
            int iStartPoint = iStart > sSource.Length ? sSource.Length : iStart;
            return sSource.Substring(iStartPoint, iStartPoint + iLength > sSource.Length ? sSource.Length - iStartPoint : iLength);
        }
        return string.Empty;
    }

    #endregion

    /// <summary>
    /// 获取字符串指定索引部分
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="index">指定索引</param>
    /// <returns></returns>
    public static string SubstringFrom(this string value, int index)
    {
        return index < 0 && index < value.Length ? value : value.Substring(index, value.Length - index);
    }
    #endregion

    #region 截取字符长度
    /// <summary>
    /// 截取清空Html字符串
    /// </summary>
    /// <param name="inputString">字符</param>
    /// <param name="len">长度</param>
    /// <returns></returns>
    public static string CutString(this string inputString, int len)
    {
        if (string.IsNullOrEmpty(inputString))
            return "";
        inputString = DropHTML(inputString);
        ASCIIEncoding ascii = new ASCIIEncoding();
        int tempLen = 0;
        string tempString = "";
        byte[] s = ascii.GetBytes(inputString);
        for (int i = 0; i < s.Length; i++)
        {
            if ((int)s[i] == 63)
            {
                tempLen += 2;
            }
            else
            {
                tempLen += 1;
            }

            try
            {
                tempString += inputString.Substring(i, 1);
            }
            catch
            {
                break;
            }

            if (tempLen > len)
                break;
        }
        //如果截过则加上半个省略号 
        byte[] mybyte = Encoding.UTF8.GetBytes(inputString);
        if (mybyte.Length > len)
            tempString += "…";
        return tempString;
    }
    #endregion

    #region TXT代码转换成HTML格式
    /// <summary>
    /// 字符串字符处理
    /// </summary>
    /// <param name="Input">等待处理的字符串</param>
    /// <returns>处理后的字符串</returns>
    public static String ToHtml(string Input)
    {
        StringBuilder sb = new StringBuilder(Input);
        sb.Replace("'", "&apos;");
        sb.Replace("&", "&amp;");
        sb.Replace("<", "&lt;");
        sb.Replace(">", "&gt;");
        sb.Replace("\r\n", "<br />");
        sb.Replace("\n", "<br />");
        sb.Replace("\t", " ");
        //sb.Replace(" ", "&nbsp;");
        return sb.ToString();
    }
    #endregion

    #region 清除HTML标记
    public static string DropHTML(string Htmlstring)
    {
        if (string.IsNullOrEmpty(Htmlstring)) return "";
        //删除脚本  
        Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
        //删除HTML  
        Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

        Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
        Htmlstring.Replace("<", "");
        Htmlstring.Replace(">", "");
        Htmlstring.Replace("\r\n", "");
        Htmlstring.Replace("&emsp;", "");
        Htmlstring = WebUtility.HtmlEncode(Htmlstring).Trim();
        return Htmlstring;
    }
    #endregion

    /// <summary>
    /// 按文本内容长度截取HTML字符串(支持截取带HTML代码样式的字符串)
    /// </summary>
    /// <param name="html">将要截取的字符串参数</param>
    /// <param name="len">截取的字节长度</param>
    /// <param name="endString">字符串末尾补上的字符串</param>
    /// <param name="IsHtml">是否Html</param>
    /// <returns>返回截取后的字符串</returns>
    public static string HTMLSubstring(this string html, int len, string endString, Boolean IsHtml = true)
    {
        if (!IsHtml)
        {
            html = WebUtility.HtmlDecode(html);
        }

        if (string.IsNullOrEmpty(html) || html.Length <= len) return html;
        MatchCollection mcentiry, mchtmlTag;
        ArrayList inputHTMLTag = new ArrayList();
        string r = "", tmpValue;
        int rWordCount = 0, i = 0;
        Regex rxSingle = new("^<(br|hr|img|input|param|meta|link)", RegexOptions.Compiled | RegexOptions.IgnoreCase)//是否单标签正则
            , rxEndTag = new("</[^>]+>", RegexOptions.Compiled)//是否结束标签正则
            , rxTagName = new("<([a-z]+)[^>]*>", RegexOptions.Compiled | RegexOptions.IgnoreCase)//获取标签名正则
            , rxHtmlTag = new("<[^>]+>", RegexOptions.Compiled)//html标签正则
            , rxEntity = new("&[a-z]{1,9};", RegexOptions.Compiled | RegexOptions.IgnoreCase)//实体正则
            , rxEntityReverse = new("§", RegexOptions.Compiled)//反向替换实体正则
            ;
        html = html.Replace("§", "§");//替换字符§为他的实体“§”，以便进行下一步替换
        mcentiry = rxEntity.Matches(html);//收集实体对象到匹配数组中
        html = rxEntity.Replace(html, "§");//替换实体为特殊字符§，这样好控制一个实体占用一个字符
        mchtmlTag = rxHtmlTag.Matches(html);//收集html标签到匹配数组中
        html = rxHtmlTag.Replace(html, "__HTMLTag__");//替换为特殊标签
        string[] arrWord = html.Split(new string[] { "__HTMLTag__" }, StringSplitOptions.None);//通过特殊标签进行拆分
        int wordNum = arrWord.Length;
        //获取指定内容长度及HTML标签
        for (; i < wordNum; i++)
        {
            if (rWordCount + arrWord[i].Length >= len) r += arrWord[i].Substring(0, len - rWordCount) + endString;
            else r += arrWord[i];
            rWordCount += arrWord[i].Length;//计算已经获取到的字符长度
            if (rWordCount >= len) break;
            //搜集已经添加的非单标签，以便封闭HTML标签对
            if (i < wordNum - 1)
            {
                tmpValue = mchtmlTag[i].Value;
                if (!rxSingle.IsMatch(tmpValue))
                { //不是单标签
                    if (rxEndTag.IsMatch(tmpValue) && inputHTMLTag.Count > 0) inputHTMLTag.RemoveAt(inputHTMLTag.Count - 1);
                    else inputHTMLTag.Add(tmpValue);
                }
                r += tmpValue;
            }

        }
        //替换回实体
        for (i = 0; i < mcentiry.Count; i++) r = rxEntityReverse.Replace(r, mcentiry[i].Value, 1);
        //封闭标签
        for (i = inputHTMLTag.Count - 1; i >= 0; i--) r += "</" + rxTagName.Match(inputHTMLTag[i].ToString()).Groups[1].Value + ">";
        return r;
    }

    /// <summary>
    /// 提取摘要，是否清除HTML代码
    /// </summary>
    /// <param name="content"></param>
    /// <param name="length"></param>
    /// <param name="StripHTML"></param>
    /// <returns></returns>
    public static string GetContentSummary(this string content, int length, bool StripHTML)
    {
        if (string.IsNullOrEmpty(content) || length == 0)
            return "";
        if (StripHTML)
        {
            Regex re = new Regex("<[^>]*>");
            content = re.Replace(content, "");
            content = content.Replace("　", "").Replace(" ", "");
            if (content.Length <= length)
                return content;
            else
                return content.Substring(0, length) + "……";
        }
        else
        {
            if (content.Length <= length)
                return content;

            int pos = 0, npos = 0, size = 0;
            bool firststop = false, notr = false, noli = false;
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                if (pos >= content.Length)
                    break;
                string cur = content.Substring(pos, 1);
                if (cur == "<")
                {
                    string next = content.Substring(pos + 1, 3).ToLower();
                    if (next.IndexOf("p") == 0 && next.IndexOf("pre") != 0)
                    {
                        npos = content.IndexOf(">", pos) + 1;
                    }
                    else if (next.IndexOf("/p") == 0 && next.IndexOf("/pr") != 0)
                    {
                        npos = content.IndexOf(">", pos) + 1;
                        if (size < length)
                            sb.Append("<br/>");
                    }
                    else if (next.IndexOf("br") == 0)
                    {
                        npos = content.IndexOf(">", pos) + 1;
                        if (size < length)
                            sb.Append("<br/>");
                    }
                    else if (next.IndexOf("img") == 0)
                    {
                        npos = content.IndexOf(">", pos) + 1;
                        if (size < length)
                        {
                            sb.Append(content.Substring(pos, npos - pos));
                            size += npos - pos + 1;
                        }
                    }
                    else if (next.IndexOf("li") == 0 || next.IndexOf("/li") == 0)
                    {
                        npos = content.IndexOf(">", pos) + 1;
                        if (size < length)
                        {
                            sb.Append(content.Substring(pos, npos - pos));
                        }
                        else
                        {
                            if (!noli && next.IndexOf("/li") == 0)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                                noli = true;
                            }
                        }
                    }
                    else if (next.IndexOf("tr") == 0 || next.IndexOf("/tr") == 0)
                    {
                        npos = content.IndexOf(">", pos) + 1;
                        if (size < length)
                        {
                            sb.Append(content.Substring(pos, npos - pos));
                        }
                        else
                        {
                            if (!notr && next.IndexOf("/tr") == 0)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                                notr = true;
                            }
                        }
                    }
                    else if (next.IndexOf("td") == 0 || next.IndexOf("/td") == 0)
                    {
                        npos = content.IndexOf(">", pos) + 1;
                        if (size < length)
                        {
                            sb.Append(content.Substring(pos, npos - pos));
                        }
                        else
                        {
                            if (!notr)
                            {
                                sb.Append(content.Substring(pos, npos - pos));
                            }
                        }
                    }
                    else
                    {
                        npos = content.IndexOf(">", pos) + 1;
                        sb.Append(content.Substring(pos, npos - pos));
                    }
                    if (npos <= pos)
                        npos = pos + 1;
                    pos = npos;
                }
                else
                {
                    if (size < length)
                    {
                        sb.Append(cur);
                        size++;
                    }
                    else
                    {
                        if (!firststop)
                        {
                            sb.Append("……");
                            firststop = true;
                        }
                    }
                    pos++;
                }

            }
            return sb.ToString();
        }
    }

    #region WordCase(单词大小写)
    /// <summary>
    /// 首字母大写
    /// </summary>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static string ToUpperFirstLetter(this string value)
    {
        return ToFirstLetter(value);
    }

    /// <summary>
    /// 首字母小写
    /// </summary>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static string ToLowerFirstLetter(this string value)
    {
        return ToFirstLetter(value, false);
    }

    /// <summary>
    /// 首字母大小写
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="isUpper">是否大写</param>
    /// <returns></returns>
    private static string ToFirstLetter(string value, bool isUpper = true)
    {
        if (value.IsEmpty())
        {
            return string.Empty;
        }
        char[] valueChars = value.ToCharArray();
        if (isUpper)
        {
            valueChars[0] = char.ToUpper(valueChars[0]);
        }
        else
        {
            valueChars[0] = char.ToLower(valueChars[0]);
        }
        return new string(valueChars);
    }

    /// <summary>
    /// 将指定字符串转为词首字母大写
    /// </summary>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static string ToTitleCase(this string value)
    {
        return value.ToTitleCase(CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// 将指定字符串转为词首字母大写
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="culture">区域性信息</param>
    /// <returns></returns>
    public static string ToTitleCase(this string value, CultureInfo culture)
    {
        return culture.TextInfo.ToTitleCase(value);
    }

    /// <summary>
    /// 将单词的单数形式转为复数形式
    /// </summary>
    /// <param name="singular">单数形式的单词</param>
    /// <returns>复数形式的单词</returns>
    public static string ToPlural(this string singular)
    {
        //多个单词的形式 B A：适用于第一单词只有（A）的复数形式
        int index = singular.LastIndexOf(" of ", StringComparison.Ordinal);
        if (index > 0)
        {
            return (singular.Substring(0, index)) + singular.Remove(0, index).ToPlural();
        }
        //单数形式单词规则
        //-es为后缀结束规则
        if (singular.EndsWith("sh") || singular.EndsWith("ch") || singular.EndsWith("us") || singular.EndsWith("ss"))
        {
            return singular + "es";
        }
        //-ies为后缀结束规则
        if (singular.EndsWith("y"))
        {
            return singular.Remove(singular.Length - 1, 1) + "ies";
        }
        //-oes为后缀结束规则
        if (singular.EndsWith("o"))
        {
            return singular.Remove(singular.Length - 1, 1) + "oes";
        }
        //-s为后缀结束规则
        return singular + "s";
    }
    #endregion

    #region ReplaceAll(替换字符串指定的所有值)
    /// <summary>
    /// 替换字符串中指定的所有值
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="oldValues">需要替换的值</param>
    /// <param name="replacePredicate">替换谓词</param>
    /// <example>
    /// <code>
    ///         var str = "White Red Blue Green Yellow Black Gray";
    ///         var achromaticColors = new[] {"White", "Black", "Gray"};
    ///         str = str.ReplaceAll(achromaticColors, v => "[" + v + "]");
    ///         // str == "[White] Red Blue Green Yellow [Black] [Gray]"
    /// </code>
    /// </example>
    /// <returns></returns>
    public static string ReplaceAll(this string value, IEnumerable<string> oldValues,
        Func<string, string> replacePredicate)
    {
        StringBuilder sb = new StringBuilder(value);
        foreach (var oldValue in oldValues)
        {
            var newValue = replacePredicate(oldValue);
            sb.Replace(oldValue, newValue);
        }
        return sb.ToString();
    }
    /// <summary>
    /// 替换字符串中指定的所有值
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="oldValues">需要替换的值</param>
    /// <param name="newValue">新值</param>
    /// <example>
    /// 	<code>
    ///         var str = "White Red Blue Green Yellow Black Gray";
    ///         var achromaticColors = new[] {"White", "Black", "Gray"};
    ///         str = str.ReplaceAll(achromaticColors, "[AchromaticColor]");
    ///         // str == "[AchromaticColor] Red Blue Green Yellow [AchromaticColor] [AchromaticColor]"
    /// 	</code>
    /// </example>
    /// <returns></returns>
    public static string ReplaceAll(this string value, IEnumerable<string> oldValues, string newValue)
    {
        StringBuilder sb = new StringBuilder(value);
        foreach (var oldValue in oldValues)
        {
            sb.Replace(oldValue, newValue);
        }
        return sb.ToString();
    }
    /// <summary>
    /// 替换字符串中指定的所有值
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="oldValues">需要替换的值</param>
    /// <param name="newValues">新的值</param>
    /// <example>
    /// 	<code>
    ///         var str = "White Red Blue Green Yellow Black Gray";
    ///         var achromaticColors = new[] {"White", "Black", "Gray"};
    ///         var exquisiteColors = new[] {"FloralWhite", "Bistre", "DavyGrey"};
    ///         str = str.ReplaceAll(achromaticColors, exquisiteColors);
    ///         // str == "FloralWhite Red Blue Green Yellow Bistre DavyGrey"
    /// 	</code>
    /// </example>
    /// <returns></returns>
    public static string ReplaceAll(this string value, IEnumerable<string> oldValues, IEnumerable<string> newValues)
    {
        StringBuilder sb = new StringBuilder(value);
        var newValueEnum = newValues.GetEnumerator();
        foreach (var oldValue in oldValues)
        {
            if (!newValueEnum.MoveNext())
            {
                throw new ArgumentOutOfRangeException("newValues", "newValues sequence is shorter than oldValues sequence");
            }
            sb.Replace(oldValue, newValueEnum.Current);
        }
        if (newValueEnum.MoveNext())
        {
            throw new ArgumentOutOfRangeException("newValues", "newValues sequence is longer than oldValues sequence");
        }
        return sb.ToString();
    }
    #endregion

    #region ParseCommandlineParams(解析命令行参数)
    /// <summary>
    /// 解析命令行参数
    /// </summary>
    /// <param name="value">值</param>
    /// <returns>一个命令行参数字符串字典对象</returns>
    public static StringDictionary ParseCommandlineParams(this string[] value)
    {
        var parameters = new StringDictionary();
        var spliter = new Regex(@"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        var remover = new Regex(@"^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        string parameter = null;
        // Valid parameters forms:
        // {-,/,--}param{ ,=,:}((",')value(",'))
        // Examples: -param1 value1 --param2 /param3:"Test-:-work" /param4=happy -param5 '--=nice=--'
        foreach (string txt in value)
        {
            // Look for new parameters (-,/ or --) and a possible enclosed value (=,:)
            string[] parts = spliter.Split(txt, 3);
            switch (parts.Length)
            {
                // Found a value (for the last parameter found (space separator))
                case 1:
                    if (parameter != null)
                    {
                        if (!parameters.ContainsKey(parameter))
                        {
                            parts[0] = remover.Replace(parts[0], "$1");
                            parameters.Add(parameter, parts[0]);
                        }
                        parameter = null;
                    }
                    // else Error: no parameter waiting for a value (skipped)
                    break;
                // Found just a parameter
                case 2:
                    // The last parameter is still waiting. With no value, set it to true.
                    if (parameter != null)
                    {
                        if (!parameters.ContainsKey(parameter)) parameters.Add(parameter, "true");
                    }
                    parameter = parts[1];
                    break;
                // Parameter with enclosed value
                case 3:
                    // The last parameter is still waiting. With no value, set it to true.
                    if (parameter != null)
                    {
                        if (!parameters.ContainsKey(parameter)) parameters.Add(parameter, "true");
                    }
                    parameter = parts[1];
                    // Remove possible enclosing characters (",')
                    if (!parameters.ContainsKey(parameter))
                    {
                        parts[2] = remover.Replace(parts[2], "$1");
                        parameters.Add(parameter, parts[2]);
                    }
                    parameter = null;
                    break;
            }
        }
        // In case a parameter is still waiting
        if (parameter != null)
        {
            if (!parameters.ContainsKey(parameter))
            {
                parameters.Add(parameter, "true");
            }
        }
        return parameters;
    }
    #endregion

    #region ParseStringToEnum(解析字符串到枚举项)
    /// <summary>
    /// 如果存在该枚举，解析字符串到字符串枚举项，否则返回默认枚举
    /// </summary>
    /// <typeparam name="TEnum">泛型枚举</typeparam>
    /// <param name="value">需转换为枚举的字符串</param>
    /// <param name="ignorecase">是否区分大小写</param>
    /// <returns>枚举项</returns>
    /// <example>
    /// 	<code>
    /// 		public enum EnumTwo {  None, One,}
    /// 		object[] items = new object[] { "One".ParseStringToEnum《EnumTwo》(), "Two".ParseStringToEnum《EnumTwo》() };
    /// 	</code>
    /// </example>
    public static TEnum ParseStringToEnum<TEnum>(this string value, bool ignorecase = default(bool))
        where TEnum : struct
    {
        return value.IsItemInEnum<TEnum>()()
            ? default(TEnum)
            : (TEnum)Enum.Parse(typeof(TEnum), value, ignorecase);
    }
    #endregion

    #region EncodeEmailAddress(编码电子邮件地址)
    /// <summary>
    /// 将电子邮件地址进行编码，以便于链接仍然有效
    /// </summary>
    /// <param name="emailAddress">邮箱地址</param>
    /// <returns>编码后的邮箱地址</returns>
    public static string EncodeEmailAddress(this string emailAddress)
    {
        string tempHtmlEncode = emailAddress;
        for (int i = tempHtmlEncode.Length; i >= 1; i--)
        {
            int acode = Convert.ToInt32(tempHtmlEncode[i - 1]);
            string repl;
            switch (acode)
            {
                case 32:
                    repl = " ";
                    break;
                case 34:
                    repl = "\"";
                    break;
                case 38:
                    repl = "&";
                    break;
                case 60:
                    repl = "<";
                    break;
                case 62:
                    repl = ">";
                    break;
                default:
                    if (acode >= 32 && acode <= 127)
                    {
                        repl = "&#" + Convert.ToString(acode) + ";";
                    }
                    else
                    {
                        repl = "&#" + Convert.ToString(acode) + ";";
                    }
                    break;
            }
            if (repl.Length > 0)
            {
                tempHtmlEncode = tempHtmlEncode.Substring(0, i - 1) +
                                 repl + tempHtmlEncode.Substring(i);
            }
        }
        return tempHtmlEncode;
    }
    #endregion

    #region RepairZero(补足位数)
    /// <summary>
    /// 补足位数，指定字符串的固定长度，如果字符串小于固定长度，则在字符串的前面补足零，可设置的固定长度最大为9位
    /// </summary>
    /// <param name="text">原始字符串</param>
    /// <param name="limitedLength">字符串的固定长度</param>
    /// <returns></returns>
    public static string RepairZero(this string text, int limitedLength)
    {
        return text.PadLeft(limitedLength, '0');
    }
    #endregion

    #region ReplaceFirst(替换字符串-首匹配)

    /// <summary>
    /// 替换字符串-首匹配
    /// </summary>
    /// <param name="this">当前值</param>
    /// <param name="oldValue">旧值</param>
    /// <param name="newValue">新值</param>
    /// <returns></returns>
    public static string ReplaceFirst(this string @this, string oldValue, string newValue)
    {
        var startIndex = @this.IndexOf(oldValue, StringComparison.Ordinal);
        if (startIndex == -1)
        {
            return @this;
        }

        return @this.Remove(startIndex, oldValue.Length).Insert(startIndex, newValue);
    }

    /// <summary>
    /// 替换字符串-首匹配
    /// </summary>
    /// <param name="this">当前值</param>
    /// <param name="number">替换数</param>
    /// <param name="oldValue">旧值</param>
    /// <param name="newValue">新值</param>
    /// <returns></returns>
    public static string ReplaceFirst(this string @this, int number, string oldValue, string newValue)
    {
        List<string> list = @this.Split(oldValue).ToList();
        var old = number + 1;
        IEnumerable<string> listStart = list.Take(old);
        IEnumerable<string> listEnd = list.Skip(old);

        return string.Join(newValue, listStart)
               + (listEnd.Any() ? oldValue : "")
               + string.Join(oldValue, listEnd);
    }

    #endregion

    #region ReplaceLast(替换字符串-尾匹配)

    /// <summary>
    /// 替换字符串-尾匹配
    /// </summary>
    /// <param name="this">当前值</param>
    /// <param name="oldValue">旧值</param>
    /// <param name="newValue">新值</param>
    /// <returns></returns>
    public static string ReplaceLast(this string @this, string oldValue, string newValue)
    {
        var startIndex = @this.LastIndexOf(oldValue, StringComparison.Ordinal);
        if (startIndex == -1)
        {
            return @this;
        }

        return @this.Remove(startIndex, oldValue.Length).Insert(startIndex, newValue);
    }

    /// <summary>
    /// 替换字符串-尾匹配
    /// </summary>
    /// <param name="this">当前值</param>
    /// <param name="number">替换数</param>
    /// <param name="oldValue">旧值</param>
    /// <param name="newValue">新值</param>
    /// <returns></returns>
    public static string ReplaceLast(this string @this, int number, string oldValue, string newValue)
    {
        List<string> list = @this.Split(oldValue).ToList();
        var old = Math.Max(0, list.Count - number - 1);
        IEnumerable<string> listStart = list.Take(old);
        IEnumerable<string> listEnd = list.Skip(old);

        return string.Join(oldValue, listStart)
               + (old > 0 ? oldValue : "")
               + string.Join(newValue, listEnd);
    }

    #endregion
}