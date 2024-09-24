using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

using DH;

using JetBrains.Annotations;

using NewLife;

using Pek;

namespace System;

/// <summary>
/// 字符串(<see cref="string"/>) 扩展
/// </summary>
public static partial class DHStringExtensions
{
    /// <summary>
    /// 将字符串中的行尾转换为<see cref="Environment.NewLine"/>。
    /// </summary>
    public static string NormalizeLineEndings(this string str)
    {
        return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
    }

    /// <summary>
    /// 获取字符串中第n个字符的索引。
    /// </summary>
    /// <param name="str">要搜索的源字符串</param>
    /// <param name="c">要在<paramref name="str"/>中搜索的字符</param>
    /// <param name="n">发生次数</param>
    public static int NthIndexOf(this string str, char c, int n)
    {
        Check.NotNull(str, nameof(str));

        var count = 0;
        for (var i = 0; i < str.Length; i++)
        {
            if (str[i] != c)
            {
                continue;
            }

            if ((++count) == n)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 从给定字符串的末尾移除第一个出现的给定后缀。
    /// </summary>
    /// <param name="str">字符串。</param>
    /// <param name="postFixes">一个或多个后缀。</param>
    /// <returns>修改的字符串或相同的字符串（如果它没有任何给定的后缀）</returns>
    [ContractAnnotation("null <= str:null")]
    public static string RemovePostFix(this string str, params string[] postFixes)
    {
        return str.RemovePostFix(StringComparison.Ordinal, postFixes);
    }

    /// <summary>
    /// 从给定字符串的末尾移除第一个出现的给定后缀。
    /// </summary>
    /// <param name="str">字符串。</param>
    /// <param name="comparisonType">字符串比较类型</param>
    /// <param name="postFixes">一个或多个后缀。</param>
    /// <returns>修改的字符串或相同的字符串（如果它没有任何给定的后缀）</returns>
    [ContractAnnotation("null <= str:null")]
    public static string RemovePostFix(this string str, StringComparison comparisonType, params string[] postFixes)
    {
        if (str.IsNullOrEmpty())
        {
            return str;
        }

        if (postFixes.IsNullOrEmpty())
        {
            return str;
        }

        foreach (var postFix in postFixes)
        {
            if (str.EndsWith(postFix, comparisonType))
            {
                return str.Left(str.Length - postFix.Length);
            }
        }

        return str;
    }

    /// <summary>
    /// 从给定字符串的开头移除第一个出现的给定前缀。
    /// </summary>
    /// <param name="str">字符串。</param>
    /// <param name="preFixes">一个或多个前缀。</param>
    /// <returns>修改的字符串或相同的字符串（如果它没有任何给定的前缀）</returns>
    [ContractAnnotation("null <= str:null")]
    public static string RemovePreFix(this string str, params string[] preFixes)
    {
        return str.RemovePreFix(StringComparison.Ordinal, preFixes);
    }

    /// <summary>
    /// 从给定字符串的开头移除第一个出现的给定前缀。
    /// </summary>
    /// <param name="str">字符串。</param>
    /// <param name="comparisonType">字符串比较类型</param>
    /// <param name="preFixes">一个或多个前缀。</param>
    /// <returns>修改的字符串或相同的字符串（如果它没有任何给定的前缀）</returns>
    [ContractAnnotation("null <= str:null")]
    public static string RemovePreFix(this string str, StringComparison comparisonType, params string[] preFixes)
    {
        if (str.IsNullOrEmpty())
        {
            return str;
        }

        if (preFixes.IsNullOrEmpty())
        {
            return str;
        }

        foreach (var preFix in preFixes)
        {
            if (str.StartsWith(preFix, comparisonType))
            {
                return str.Right(str.Length - preFix.Length);
            }
        }

        return str;
    }

    public static string ReplaceFirst(this string str, string search, string replace, StringComparison comparisonType = StringComparison.Ordinal)
    {
        Check.NotNull(str, nameof(str));

        var pos = str.IndexOf(search, comparisonType);
        if (pos < 0)
        {
            return str;
        }

        return str.Substring(0, pos) + replace + str.Substring(pos + search.Length);
    }

    /// <summary>
    /// 使用字符串。Split方法通过给定的分隔符拆分给定的字符串。
    /// </summary>
    public static string[] Split(this string str, string separator)
    {
        return str.Split(new[] { separator }, StringSplitOptions.None);
    }

    /// <summary>
    /// 使用字符串。Split方法通过给定的分隔符拆分给定的字符串。
    /// </summary>
    public static string[] Split(this string str, string separator, StringSplitOptions options)
    {
        return str.Split(new[] { separator }, options);
    }

    /// <summary>
    /// 使用字符串。Split方法按<see cref="Environment.NewLine"/>.
    /// </summary>
    public static string[] SplitToLines(this string str)
    {
        return str.Split(Environment.NewLine);
    }

    /// <summary>
    /// 使用string.Split方法将给定字符串拆分为<see cref="Environment.NewLine"/>.
    /// </summary>
    public static string[] SplitToLines(this string str, StringSplitOptions options)
    {
        return str.Split(Environment.NewLine, options);
    }

    /// <summary>
    /// 将PascalCase字符串转换为camelCase字符串。
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="useCurrentCulture">将true设置为使用当前区域性。否则，将使用不变区域性。</param>
    /// <param name="handleAbbreviations">如果要将“XYZ”转换为“XYZ”，请将true设置为 true。</param>
    /// <returns>camelCase字符串格式</returns>
    [ContractAnnotation("null <= str:null")]
    public static string ToCamelCase(this string str, bool useCurrentCulture = false, bool handleAbbreviations = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        if (str.Length == 1)
        {
            return useCurrentCulture ? str.ToLower() : str.ToLowerInvariant();
        }

        if (handleAbbreviations && IsAllUpperCase(str))
        {
            return useCurrentCulture ? str.ToLower() : str.ToLowerInvariant();
        }

        return (useCurrentCulture ? char.ToLower(str[0]) : char.ToLowerInvariant(str[0])) + str.Substring(1);
    }

    /// <summary>
    /// 将给定的PascalCase/camelCase字符串转换为句子(通过按空格拆分单词)。
    /// 示例："ThisIsSampleSentence"转换为"This is a sample sentence".
    /// </summary>
    /// <param name="str">要转换的字符串.</param>
    /// <param name="useCurrentCulture">将true设置为使用当前区域性。否则，将使用不变区域性。</param>
    [ContractAnnotation("null <= str:null")]
    public static string ToSentenceCase(this string str, bool useCurrentCulture = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        return useCurrentCulture
            ? Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]))
            : Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLowerInvariant(m.Value[1]));
    }

    /// <summary>
    /// 将给定的PascalCase/camelCase字符串转换为kebab大小写。
    /// </summary>
    /// <param name="str">要转换的字符串。</param>
    /// <param name="useCurrentCulture">将true设置为使用当前区域性。否则，将使用不变区域性。</param>
    [ContractAnnotation("null <= str:null")]
    public static string ToKebabCase(this string str, bool useCurrentCulture = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        str = str.ToCamelCase();

        return useCurrentCulture
            ? Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + "-" + char.ToLower(m.Value[1]))
            : Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + "-" + char.ToLowerInvariant(m.Value[1]));
    }

    /// <summary>
    /// 将给定的PascalCase/camelCase字符串转换为snake大小写。
    /// 示例："ThisIsSampleSentence"转换为"this_is_a_sample_sentence".
    /// https://github.com/npgsql/npgsql/blob/dev/src/Npgsql/NameTranslation/NpgsqlSnakeCaseNameTranslator.cs#L51
    /// </summary>
    /// <param name="str">要转换的字符串。</param>
    /// <returns></returns>
    public static string ToSnakeCase(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        var builder = new StringBuilder(str.Length + Math.Min(2, str.Length / 5));
        var previousCategory = default(UnicodeCategory?);

        for (var currentIndex = 0; currentIndex < str.Length; currentIndex++)
        {
            var currentChar = str[currentIndex];
            if (currentChar == '_')
            {
                builder.Append('_');
                previousCategory = null;
                continue;
            }

            var currentCategory = char.GetUnicodeCategory(currentChar);
            switch (currentCategory)
            {
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.TitlecaseLetter:
                    if (previousCategory == UnicodeCategory.SpaceSeparator ||
                        previousCategory == UnicodeCategory.LowercaseLetter ||
                        previousCategory != UnicodeCategory.DecimalDigitNumber &&
                        previousCategory != null &&
                        currentIndex > 0 &&
                        currentIndex + 1 < str.Length &&
                        char.IsLower(str[currentIndex + 1]))
                    {
                        builder.Append('_');
                    }

                    currentChar = char.ToLower(currentChar);
                    break;

                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    if (previousCategory == UnicodeCategory.SpaceSeparator)
                    {
                        builder.Append('_');
                    }
                    break;

                default:
                    if (previousCategory != null)
                    {
                        previousCategory = UnicodeCategory.SpaceSeparator;
                    }
                    continue;
            }

            builder.Append(currentChar);
            previousCategory = currentCategory;
        }

        return builder.ToString();
    }

    /// <summary>
    /// 将字符串转换为枚举值。
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="value">要转换的字符串值</param>
    /// <returns>返回枚举对象</returns>
    public static T ToEnum<T>(this string value)
        where T : struct
    {
        Check.NotNull(value, nameof(value));
        return (T)Enum.Parse(typeof(T), value);
    }

    /// <summary>
    /// 将字符串转换为枚举值。
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="value">要转换的字符串值</param>
    /// <param name="ignoreCase">忽略大小写</param>
    /// <returns>返回枚举对象</returns>
    public static T ToEnum<T>(this string value, bool ignoreCase)
        where T : struct
    {
        Check.NotNull(value, nameof(value));
        return (T)Enum.Parse(typeof(T), value, ignoreCase);
    }

    public static string ToMd5(this string str)
    {
        using (var md5 = MD5.Create())
        {
            var inputBytes = Encoding.UTF8.GetBytes(str);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var hashByte in hashBytes)
            {
                sb.Append(hashByte.ToString("X2"));
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// 将camelCase字符串转换为PascalCase字符串。
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="useCurrentCulture">将true设置为使用当前区域性。否则，将使用不变区域性。</param>
    /// <returns>字符串的PascalCase</returns>
    [ContractAnnotation("null <= str:null")]
    public static string ToPascalCase(this string str, bool useCurrentCulture = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        if (str.Length == 1)
        {
            return useCurrentCulture ? str.ToUpper() : str.ToUpperInvariant();
        }

        return (useCurrentCulture ? char.ToUpper(str[0]) : char.ToUpperInvariant(str[0])) + str.Substring(1);
    }

    /// <summary>
    /// 如果超过最大长度，则从字符串的开头获取字符串的子字符串。
    /// </summary>
    [ContractAnnotation("null <= str:null")]
    public static string Truncate(this string str, int maxLength)
    {
        if (str == null)
        {
            return null;
        }

        if (str.Length <= maxLength)
        {
            return str;
        }

        return str.Left(maxLength);
    }

    /// <summary>
    /// 如果字符串超过最大长度，则从字符串的Ending获取字符串的子字符串。
    /// </summary>
    [ContractAnnotation("null <= str:null")]
    public static string TruncateFromBeginning(this string str, int maxLength)
    {
        if (str == null)
        {
            return null;
        }

        if (str.Length <= maxLength)
        {
            return str;
        }

        return str.Right(maxLength);
    }

    /// <summary>
    /// 如果超过最大长度，则从字符串的开头获取字符串的子字符串。
    /// 如果字符串被截断，它会在字符串末尾添加一个“…”后缀。
    /// 返回的字符串不能长于maxLength。
    /// </summary>
    /// <exception cref="ArgumentNullException">如果<paramref name="str"/>为空，则引发</exception>
    public static string TruncateWithPostfix(this string str, int maxLength)
    {
        return TruncateWithPostfix(str, maxLength, "...");
    }

    /// <summary>
    /// 如果超过最大长度，则从字符串的开头获取字符串的子字符串。
    /// 如果字符串被截断，它会将给定的<paramref name="postfix"/>添加到字符串的末尾。
    /// 返回的字符串不能长于maxLength。
    /// </summary>
    /// <exception cref="ArgumentNullException">如果<paramref name="str"/>为空，则引发</exception>
    [ContractAnnotation("null <= str:null")]
    public static string TruncateWithPostfix(this string str, int maxLength, string postfix)
    {
        if (str == null)
        {
            return null;
        }

        if (str == string.Empty || maxLength == 0)
        {
            return string.Empty;
        }

        if (str.Length <= maxLength)
        {
            return str;
        }

        if (maxLength <= postfix.Length)
        {
            return postfix.Left(maxLength);
        }

        return str.Left(maxLength - postfix.Length) + postfix;
    }

    /// <summary>
    /// 使用<see cref="Encoding.UTF8"/>编码将给定字符串转换为字节数组。
    /// </summary>
    public static byte[] GetBytes(this string str)
    {
        return str.GetBytes(Encoding.UTF8);
    }

    /// <summary>
    /// 使用给定的<paramref name="encoding"/>将给定字符串转换为字节数组
    /// </summary>
    public static byte[] GetBytes([NotNull] this string str, [NotNull] Encoding encoding)
    {
        Check.NotNull(str, nameof(str));
        Check.NotNull(encoding, nameof(encoding));

        return encoding.GetBytes(str);
    }

    private static bool IsAllUpperCase(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (Char.IsLetter(input[i]) && !Char.IsUpper(input[i]))
            {
                return false;
            }
        }

        return true;
    }
}
