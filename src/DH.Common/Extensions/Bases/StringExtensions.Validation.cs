﻿using DH.Extensions;

using Pek;

namespace DH.Extension;

/// <summary>
/// 字符串(<see cref="string"/>) 扩展 - 验证
/// </summary>
public static partial class StringExtensions
{
    #region IsImageFile(是否图片文件)

    /// <summary>
    /// 判断指定路径是否图片文件
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns>结果</returns>
    public static bool IsImageFile(this string fileName)
    {
        if (!File.Exists(fileName))
        {
            return false;
        }

        byte[] filedata = File.ReadAllBytes(fileName);
        if (filedata.Length == 0)
        {
            return false;
        }
        ushort code = BitConverter.ToUInt16(filedata, 0);
        switch (code)
        {
            case 0x4D42://bmp
            case 0xD8FF://jpg
            case 0x4947://gif
            case 0x5089://png
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// 判断指定文件字节流是否图片文件
    /// </summary>
    /// <param name="fileData">字节流</param>
    /// <returns></returns>
    public static Boolean IsImageFile(this Byte[] fileData)
    {
        if (fileData == null || fileData.Length == 0)
        {
            return false;
        }

        if (!CheckPictureSafe(fileData))
        {
            return false;
        }

        ushort code = BitConverter.ToUInt16(fileData, 0);
        switch (code)
        {
            case 0x4D42://bmp
            case 0xD8FF://jpg
            case 0x4947://gif
            case 0x5089://png
                return true;
            default:
                return false;
        }
    }

    /// <summary>
    /// 检测上传图片是否安全函数
    /// </summary>
    /// <param name="fileData"></param>
    /// <returns></returns>
    public static Boolean CheckPictureSafe(Byte[] fileData)
    {
        var str_Temp = fileData.GetString().Replace("'", "''");

        var DangerString = "iframe|.getfolder|.createfolder|.deletefolder|.createdirectory|.deletedirectory|.saveas|wscript.shell|script.encode|server.|.createobject|execute|activexobject|language=|include|filesystemobject|shell.application";
        var sArray = DangerString.Split('|');

        foreach (var i in sArray)
        {
            if (str_Temp.IndexOf(i) > -1)
            {
                return false;
            }
        }

        return true;
    }

    #endregion

    #region IsLike(通配符比较)

    /// <summary>
    /// 任何模式通配符比较
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="patterns">模式</param>
    /// <returns></returns>
    public static bool IsLikeAny(this string value, params string[] patterns)
    {
        return patterns.Any(value.IsLike);
    }

    /// <summary>
    /// 通配符比较
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="pattern">模式</param>
    /// <returns></returns>
    public static bool IsLike(this string value, string pattern)
    {
        if (value == pattern)
        {
            return true;
        }
        if (pattern[0] == '*' && pattern.Length > 1)
        {
            return value.Where((t, index) => value.Substring(index).IsLike(pattern.Substring(1))).Any();
        }

        if (pattern[0] == '*')
        {
            return true;
        }

        if (pattern[0] == value[0])
        {
            return value.Substring(1).IsLike(pattern.Substring(1));
        }
        return false;
    }

    #endregion

    #region IsItemInEnum(判断数据是否在给定的枚举定义中)

    /// <summary>
    /// 判断数据是否在给定的枚举定义中
    /// </summary>
    /// <typeparam name="TEnum">泛型枚举</typeparam>
    /// <param name="value">匹配的枚举</param>
    /// <returns>匿名方法条件</returns>
    public static Func<bool> IsItemInEnum<TEnum>(this string value) where TEnum : struct
    {
        return () => string.IsNullOrEmpty(value) || !Enum.IsDefined(typeof(TEnum), value);
    }

    #endregion

    #region IsRangeLength(判断字符串长度是否在指定范围内)

    /// <summary>
    /// 判断字符串长度是否在指定范围内
    /// </summary>
    /// <param name="source">源字符串</param>
    /// <param name="minLength">最小长度</param>
    /// <param name="maxLength">最大长度</param>
    /// <returns></returns>
    public static bool IsRangeLength(this string source, int minLength, int maxLength)
    {
        return source.Length >= minLength && source.Length <= maxLength;
    }

    #endregion

    #region EqualsAny(确定字符串是否与所提供的值相等)

    /// <summary>
    /// 确定字符串是否与所提供的值相等
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="comparisonType">区域性比较</param>
    /// <param name="values">提供的值</param>
    /// <returns></returns>
    public static bool EqualsAny(this string value, StringComparison comparisonType, params string[] values)
    {
        return values.Any(v => value.Equals(v, comparisonType));
    }

    #endregion

    #region EquivalentTo(字符串是否全等)

    /// <summary>
    /// 确定两个指定的字符串具有相同的值，参数指定区域性、大小写及比较所选用的规则
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="whateverCaseString">比较字符串</param>
    /// <param name="comparison">区域性</param>
    /// <returns></returns>
    public static bool EquivalentTo(this string value, string whateverCaseString, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase)
    {
        return string.Equals(value, whateverCaseString, comparison);
    }

    #endregion

    #region Contains(确定输入字符串是否包含指定字符串)

    /// <summary>
    /// 确定输入字符串是否包含指定字符串
    /// </summary>
    /// <param name="inputValue">输入字符串</param>
    /// <param name="comparisonValue">包含字符串</param>
    /// <param name="comparisonType">区域</param>
    /// <returns></returns>
    public static bool Contains(this string inputValue, string comparisonValue, StringComparison comparisonType)
    {
        return (inputValue.IndexOf(comparisonValue, comparisonType) != -1);
    }

    /// <summary>
    /// 确定输入字符串是否包含指定字符串，且字符串不为空
    /// </summary>
    /// <param name="inputValue">输入字符串</param>
    /// <param name="comparisonValue">指定字符串</param>
    /// <returns></returns>
    public static bool ContainsEquivalenceTo(this string inputValue, string comparisonValue)
    {
        return BothStringsAreEmpty(inputValue, comparisonValue) ||
               StringContainsEquivalence(inputValue, comparisonValue);
    }

    /// <summary>
    /// 两个字符串是否均为空
    /// </summary>
    /// <param name="inputValue">字符串1</param>
    /// <param name="comparisonValue">字符串2</param>
    /// <returns></returns>
    private static bool BothStringsAreEmpty(string inputValue, string comparisonValue)
    {
        return (inputValue.IsEmpty() && comparisonValue.IsEmpty());
    }

    /// <summary>
    /// 确定输入字符串是否包含指定字符串，且两个字符串不为空
    /// </summary>
    /// <param name="inputValue">输入字符串</param>
    /// <param name="comparisonValue">指定字符串</param>
    /// <returns></returns>
    private static bool StringContainsEquivalence(string inputValue, string comparisonValue)
    {
        return ((!inputValue.IsEmpty()) && inputValue.Contains(comparisonValue, StringComparison.InvariantCultureIgnoreCase));
    }

    /// <summary>
    /// 确定字符串是否包含所提供的值
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="values">提供的值</param>
    /// <returns></returns>
    public static bool ContainsAny(this string value, params string[] values)
    {
        return value.ContainsAny(StringComparison.CurrentCulture, values);
    }

    /// <summary>
    /// 确定字符串是否包含所提供的值
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="comparisonType">区域性比较</param>
    /// <param name="values">提供的值</param>
    /// <returns></returns>
    public static bool ContainsAny(this string value, StringComparison comparisonType, params string[] values)
    {
        return values.Any(v => value.IndexOf(v, comparisonType) > -1);
    }

    /// <summary>
    /// 确定字符串是否包含所有提供的值
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="values">提供的值</param>
    /// <returns></returns>
    public static bool ContainsAll(this string value, params string[] values)
    {
        return value.ContainsAll(StringComparison.CurrentCulture, values);
    }

    /// <summary>
    /// 确定字符串是否包含所有提供的值
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="comparisonType">区域性比较</param>
    /// <param name="values">提供的值</param>
    /// <returns></returns>
    public static bool ContainsAll(this string value, StringComparison comparisonType, params string[] values)
    {
        return values.All(v => value.IndexOf(v, comparisonType) > -1);
    }

    #endregion
}