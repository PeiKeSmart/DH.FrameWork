using System.Text;

namespace DH.QRCode.Codec.Util;

public class SystemUtils {
    public static int ReadInput(Stream sourceStream, sbyte[] target, int start, int count)
    {
        if (target.Length == 0)
        {
            return 0;
        }
        byte[] array = new byte[target.Length];
        int num = sourceStream.Read(array, start, count);
        if (num == 0)
        {
            return -1;
        }
        for (int i = start; i < start + num; i++)
        {
            target[i] = (sbyte)array[i];
        }
        return num;
    }

    public static int ReadInput(TextReader sourceTextReader, short[] target, int start, int count)
    {
        if (target.Length == 0)
        {
            return 0;
        }
        char[] array = new char[target.Length];
        int num = sourceTextReader.Read(array, start, count);
        if (num == 0)
        {
            return -1;
        }
        for (int i = start; i < start + num; i++)
        {
            target[i] = (short)array[i];
        }
        return num;
    }

    public static void WriteStackTrace(Exception throwable, TextWriter stream)
    {
        stream.Write(throwable.StackTrace);
        stream.Flush();
    }

    public static int URShift(int number, int bits)
    {
        if (number >= 0)
        {
            return number >> bits;
        }
        return (number >> bits) + (2 << ~bits);
    }

    public static int URShift(int number, long bits)
    {
        return URShift(number, (int)bits);
    }

    public static long URShift(long number, int bits)
    {
        if (number >= 0)
        {
            return number >> bits;
        }
        return (number >> bits) + (2L << ~bits);
    }

    public static long URShift(long number, long bits)
    {
        return URShift(number, (int)bits);
    }

    public static byte[] ToByteArray(sbyte[] sbyteArray)
    {
        byte[] array = null;
        if (sbyteArray != null)
        {
            array = new byte[sbyteArray.Length];
            for (int i = 0; i < sbyteArray.Length; i++)
            {
                array[i] = (byte)sbyteArray[i];
            }
        }
        return array;
    }

    public static byte[] ToByteArray(string sourceString)
    {
        return Encoding.UTF8.GetBytes(sourceString);
    }

    public static byte[] ToByteArray(object[] tempObjectArray)
    {
        byte[] array = null;
        if (tempObjectArray != null)
        {
            array = new byte[tempObjectArray.Length];
            for (int i = 0; i < tempObjectArray.Length; i++)
            {
                array[i] = (byte)tempObjectArray[i];
            }
        }
        return array;
    }

    public static sbyte[] ToSByteArray(byte[] byteArray)
    {
        sbyte[] array = null;
        if (byteArray != null)
        {
            array = new sbyte[byteArray.Length];
            for (int i = 0; i < byteArray.Length; i++)
            {
                array[i] = (sbyte)byteArray[i];
            }
        }
        return array;
    }

    public static char[] ToCharArray(sbyte[] sByteArray)
    {
        return Encoding.UTF8.GetChars(ToByteArray(sByteArray));
    }

    public static char[] ToCharArray(byte[] byteArray)
    {
        return Encoding.UTF8.GetChars(byteArray);
    }
}