using System.Text;

namespace DH.Helpers;

public class HexHelper {

    #region Hex string and Byte[] transform
    /// <summary>
    /// 字节数据转化成16进制表示的字符串 ->
    /// Byte data into a string of 16 binary representations
    /// </summary>
    /// <param name="InBytes">字节数组</param>
    /// <returns>返回的字符串</returns>
    /// <exception cref="NullReferenceException"></exception>
    /// <example>
    /// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ByteToHexStringExample1" title="ByteToHexString示例" />
    /// </example>
    public static string ByteToHexString(byte[] InBytes)
    {
        return ByteToHexString(InBytes, (char)0);
    }

    /// <summary>
    /// 字节数据转化成16进制表示的字符串 ->
    /// Byte data into a string of 16 binary representations
    /// </summary>
    /// <param name="InBytes">字节数组</param>
    /// <param name="segment">分割符</param>
    /// <returns>返回的字符串</returns>
    /// <exception cref="NullReferenceException"></exception>
    /// <example>
    /// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ByteToHexStringExample2" title="ByteToHexString示例" />
    /// </example>
    public static string ByteToHexString(byte[] InBytes, char segment)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte InByte in InBytes)
        {
            if (segment == 0) sb.Append(string.Format("{0:X2}", InByte));
            else sb.Append(string.Format("{0:X2}{1}", InByte, segment));
        }

        if (segment != 0 && sb.Length > 1 && sb[sb.Length - 1] == segment)
        {
            sb.Remove(sb.Length - 1, 1);
        }
        return sb.ToString();
    }

    /// <summary>
    /// 字符串数据转化成16进制表示的字符串 ->
    /// String data into a string of 16 binary representations
    /// </summary>
    /// <param name="InString">输入的字符串数据</param>
    /// <returns>返回的字符串</returns>
    /// <exception cref="NullReferenceException"></exception>
    public static string ByteToHexString(string InString)
    {
        return ByteToHexString(Encoding.Unicode.GetBytes(InString));
    }

    public static byte[] HexStringToBytes(String hexString)
    {
        if (hexString == null || hexString.Equals(""))
        {
            return null;
        }
        hexString = hexString.ToUpper();
        int length = hexString.Length / 2;
        char[] hexChars = hexString.ToCharArray();
        byte[] d = new byte[length];
        for (int i = 0; i < length; i++)
        {
            int pos = i * 2;
            d[i] = (byte)(charToByte(hexChars[pos]) << 4 | charToByte(hexChars[pos + 1]));
        }
        return d;
    }

    private static byte charToByte(char c)
    {
        return (byte)"0123456789ABCDEF".IndexOf(c);
    }

    #endregion

}