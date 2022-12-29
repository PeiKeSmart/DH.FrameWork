using Murmur;

using NewLife;

using System.Text;

namespace DH.Helpers;

/// <summary>
/// 生成短惟一码
/// </summary>
public class ShortUniqueCode
{
    public static string CreateCode(int Id)
    {
        string code = "";
        string source_string = "2YU9IP1ASDFG8QWERTHJ7KLZX4CV5B3ONM6"; //自定义35进制  
        while (Id > 0)
        {
            int mod = Id % 35;
            Id = (Id - mod) / 35;
            code = source_string.ToCharArray()[mod] + code;
        }
        return code.PadRight(6, '0'); //不足6位补0
    }

    public static int Decode(string code)
    {
        code = new string((from s in code where s != '0' select s).ToArray());
        int num = 0;
        string source_string = "2YU9IP1ASDFG8QWERTHJ7KLZX4CV5B3ONM6";
        for (int i = 0; i < code.ToCharArray().Length; i++)
        {
            for (int j = 0; j < source_string.ToCharArray().Length; j++)
            {
                if (code.ToCharArray()[i] == source_string.ToCharArray()[j])
                {
                    num += j * Convert.ToInt32(Math.Pow(35, code.ToCharArray().Length - i - 1));
                }
            }
        }
        return num;
    }

    public static string[] ShortUrl(string url)
    {
        //可以自定义生成MD5加密字符传前的混合KEY
        string key = "GiciskyNet";
        //要使用生成URL的字符
        string[] chars = new string[]
        {
                "a", "b", "c", "d", "e", "f", "g", "h",
                "i", "j", "k", "l", "m", "n", "o", "p",
                "q", "r", "s", "t", "u", "v", "w", "x",
                "y", "z", "0", "1", "2", "3", "4", "5",
                "6", "7", "8", "9", "A", "B", "C", "D",
                "E", "F", "G", "H", "I", "J", "K", "L",
                "M", "N", "O", "P", "Q", "R", "S", "T",
                "U", "V", "W", "X", "Y", "Z"
        };
        //对传入网址进行MD5加密
        string hex = (key + url).MD5();
        string[] resUrl = new string[4];
        for (int i = 0; i < 4; i++)
        {
            //把加密字符按照8位一组16进制与0x3FFFFFFF进行位与运算
            int hexint = 0x3FFFFFFF & Convert.ToInt32("0x" + hex.Substring(i * 8, 8), 16);
            string outChars = string.Empty;
            for (int j = 0; j < 6; j++)
            {
                //把得到的值与0x0000003D进行位与运算，取得字符数组chars索引
                int index = 0x0000003D & hexint;
                //把取得的字符相加
                outChars += chars[index];
                //每次循环按位右移5位
                hexint = hexint >> 5;
            }
            //把字符串存入对应索引的输出数组
            resUrl[i] = outChars;
        }
        return resUrl;
    }

    private static readonly string chars = "aAbBcCdDeEfFgGhHiIjJkKlLmMnNoOpPqQrRsStTuUvVwWxXyYzZ0123456789";

    /// <summary>
    /// 转为62进制
    /// </summary>
    /// <param name="bytes">字节数组</param>
    /// <returns></returns>
    public static string ConvertTo62(byte[] bytes)
    {
        var id = BitConverter.ToUInt32(bytes, 0);
        var list = new List<char>();
        while (id > 0)
        {
            var item = (int)(id % 62);
            list.Add(chars[item]);
            id /= 62;
        }
        list.Reverse();
        return new string(list.ToArray());
    }

    /// <summary>
    /// 计算指定字符串的哈希值
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static byte[] GetMurmurHashBytes(string str)
    {
        var hash = MurmurHash.Create32();

        var bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(str));
        return bytes;
    }

    /// <summary>
    /// 获取下一个短网址Id
    /// </summary>
    /// <param name="url"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    public static string GetNextCode(string url, long salt)
    {
        var hashurl = url + salt;
        var bytes = GetMurmurHashBytes(hashurl);
        var code = ConvertTo62(bytes);
        return code;
    }

}