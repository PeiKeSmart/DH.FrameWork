﻿namespace DH.Extension;

/// <summary>
/// 字节值(<see cref="Byte"/>) 扩展
/// </summary>
public static class ByteExtensions
{
    #region Max(获取两个数中最大值)

    /// <summary>
    /// 获取两个数中最大值
    /// </summary>
    /// <param name="value1">值1</param>
    /// <param name="value2">值2</param>
    /// <returns></returns>
    public static byte Max(this byte value1, byte value2)
    {
        return Math.Max(value1, value2);
    }

    #endregion

    #region Min(获取两个数中最小值)

    /// <summary>
    /// 获取两个数中最小值
    /// </summary>
    /// <param name="value1">值1</param>
    /// <param name="value2">值2</param>
    /// <returns></returns>
    public static byte Min(this byte value1, byte value2)
    {
        return Math.Min(value1, value2);
    }

    #endregion

}