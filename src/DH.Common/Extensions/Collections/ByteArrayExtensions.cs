using System.Text;

namespace DH.Extensions;

/// <summary>
/// 字节数组(<see cref="Byte"/>[]) 扩展
/// </summary>
public static class ByteArrayExtensions
{
    #region ToString(将byte[]转换成字符串)

    /// <summary>
    /// 将byte[]转换成字符串，默认字符编码：<see cref="Encoding.UTF8"/>
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="encoding">字符编码</param>
    public static string ToString(this byte[] value, Encoding encoding)
    {
        encoding = (encoding ?? Encoding.UTF8);
        return encoding.GetString(value);
    }

    #endregion

    #region ToHexString(将byte[]转换成16进制字符串表示形式)

    /// <summary>
    /// 将byte[]转换成16进制字符串表示形式
    /// </summary>
    /// <param name="value">值</param>
    public static string ToHexString(this byte[] value)
    {
        var sb = new StringBuilder();
        foreach (var b in value)
            sb.AppendFormat(" {0}", b.ToString("X2").PadLeft(2, '0'));
        return sb.Length > 0 ? sb.ToString().Substring(1) : sb.ToString();
    }

    #endregion

    #region ToInt(将byte[]转换成int)

    /// <summary>
    /// 将byte[]转换成int
    /// </summary>
    /// <param name="value">值</param>
    public static int ToInt(this byte[] value)
    {
        // 如果传入的字节数组长度小于4,则返回0
        if (value.Length < 4)
            return 0;
        int num = 0;
        // 如果传入的字节数组长度大于4,需要进行处理
        if (value.Length >= 4)
        {
            // 创建一个临时缓冲区
            byte[] tempBuffer = new byte[4];
            // 将传入的字节数组的前4个字节复制到临时缓冲区
            Buffer.BlockCopy(value, 0, tempBuffer, 0, 4);
            // 将临时缓冲区的值转换成整数，并赋给num
            num = BitConverter.ToInt32(tempBuffer, 0);
        }

        return num;
    }

    #endregion

    #region ToLong(将byte[]转换成long)

    /// <summary>
    /// 将byte[]转换成long
    /// </summary>
    /// <param name="value">值</param>
    public static long ToLong(this byte[] value)
    {
        // 如果传入的字节数组长度小于8,则返回0
        if (value.Length < 8)
            return 0;
        long num = 0;
        if (value.Length >= 8)
        {
            // 创建一个临时缓冲区
            byte[] tempBuffer = new byte[8];
            // 将传入的字节数组的前8个字节复制到临时缓冲区
            Buffer.BlockCopy(value, 0, tempBuffer, 0, 8);
            // 将临时缓冲区的值转换成证书，并赋给num
            num = BitConverter.ToInt64(tempBuffer, 0);
        }
        return num;
    }

    #endregion

    #region ToBase64String(将byte[]转换成Base64字符串)

    /// <summary>
    /// 将byte[]转换成Base64字符串
    /// </summary>
    /// <param name="value">值</param>
    public static string ToBase64String(this byte[] value) => Convert.ToBase64String(value);

    #endregion

    #region ToMemoryStream(将byte[]转换成内存流)

    /// <summary>
    /// 将byte[]转换成内存流
    /// </summary>
    /// <param name="value">值</param>
    public static MemoryStream ToMemoryStream(this byte[] value) => new MemoryStream(value);

    #endregion

    #region Copy(复制一份二维数组的副本)

    /// <summary>
    /// 复制一份二维数组的副本
    /// </summary>
    /// <param name="bytes">二维数组</param>
    public static byte[,] Copy(this byte[,] bytes)
    {
        int width = bytes.GetLength(0), height = bytes.GetLength(1);
        byte[,] newBytes = new byte[width, height];
        Array.Copy(bytes, newBytes, bytes.Length);
        return newBytes;
    }

    #endregion

    /// <summary>
    /// 翻转byte数组
    /// </summary>
    /// <param name="bytes"></param>
    public static void ReverseBytes(this byte[] bytes)
    {
        byte tmp;
        int len = bytes.Length;

        for (int i = 0; i < len / 2; i++)
        {
            tmp = bytes[len - 1 - i];
            bytes[len - 1 - i] = bytes[i];
            bytes[i] = tmp;
        }
    }

    /// <summary>
    /// 规定转换起始位置和长度
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="start"></param>
    /// <param name="len"></param>
    public static void ReverseBytes(this byte[] bytes, int start, int len)
    {
        int end = start + len - 1;
        byte tmp;
        int i = 0;
        for (int index = start; index < start + len / 2; index++, i++)
        {
            tmp = bytes[end - i];
            bytes[end - i] = bytes[index];
            bytes[index] = tmp;
        }
    }

    /// <summary>
    /// 翻转字节顺序 (16-bit)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static UInt16 ReverseBytes(this UInt16 value)
    {
        return (UInt16)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
    }

    /// <summary>
    /// 翻转字节顺序 (32-bit)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static UInt32 ReverseBytes(this UInt32 value)
    {
        return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
               (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
    }

    /// <summary>
    /// 翻转字节顺序 (64-bit)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static UInt64 ReverseBytes(this UInt64 value)
    {
        return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
               (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8 |
               (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 |
               (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
    }

}