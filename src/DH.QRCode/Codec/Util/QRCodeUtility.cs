using System.Text;

namespace DH.QRCode.Codec.Util;

public class QRCodeUtility {
    public static int sqrt(int val)
    {
        int num = 0;
        int num2 = 32768;
        int num3 = 15;
        do
        {
            int num4;
            if (val >= (num4 = (num << 1) + num2 << num3--))
            {
                num += num2;
                val -= num4;
            }
        }
        while ((num2 >>= 1) > 0);
        return num;
    }

    public static bool IsUniCode(string value)
    {
        byte[] characters = AsciiStringToByteArray(value);
        byte[] characters2 = UnicodeStringToByteArray(value);
        string a = FromASCIIByteArray(characters);
        string b = FromUnicodeByteArray(characters2);
        if (a != b)
        {
            return true;
        }
        return false;
    }

    public static bool IsUnicode(byte[] byteData)
    {
        string str = FromASCIIByteArray(byteData);
        string str2 = FromUnicodeByteArray(byteData);
        byte[] array = AsciiStringToByteArray(str);
        byte[] array2 = UnicodeStringToByteArray(str2);
        if (array[0] != array2[0])
        {
            return true;
        }
        return false;
    }

    public static string FromASCIIByteArray(byte[] characters)
    {
        ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
        return aSCIIEncoding.GetString(characters);
    }

    public static string FromUnicodeByteArray(byte[] characters)
    {
        Encoding encoding = Encoding.GetEncoding("gb2312");
        return encoding.GetString(characters);
    }

    public static byte[] AsciiStringToByteArray(string str)
    {
        ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
        return aSCIIEncoding.GetBytes(str);
    }

    public static byte[] UnicodeStringToByteArray(string str)
    {
        Encoding encoding = Encoding.GetEncoding("gb2312");
        return encoding.GetBytes(str);
    }
}