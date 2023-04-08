using System.Text;

namespace DH.Helpers;

/// <summary>
/// 随机数操作管理类
/// </summary>
public static class Randoms
{
    /// <summary>
    /// 大小写+数字
    /// </summary>
    /// <param name="lens"></param>
    /// <returns></returns>
    public static string RandomString(int lens)
    {
        char[] chArray = new char[] {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q',
        'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
        'H', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
        'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
     };
        int length = chArray.Length;
        string str = "";
        Random rnd = new Random();
        for (int i = 0; i < lens; i++)
        {
            str += chArray[rnd.Next(length)];
        }
        return str;
    }

    /// <summary>
    /// 大写+数字
    /// </summary>
    /// <param name="lens"></param>
    /// <returns></returns>
    public static string RandomStr(int lens)
    {
        char[] chArray = new char[] {
        'A', 'B', 'C', 'D', 'E', 'F', 'G',
        'H', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
        'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
     };
        int length = chArray.Length;
        string str = "";
        Random rnd = new Random();
        for (int i = 0; i < lens; i++)
        {
            str += chArray[rnd.Next(length)];
        }
        return str;
    }

    #region 根据用户指定的字符串和指定长度生成随机的字符串
    /// <summary>
    /// 根据用户指定的字符串和指定长度生成随机的字符串
    /// </summary>
    /// <param name="pwdchars"></param>
    /// <param name="pwdlen"></param>
    /// <returns></returns>
    public static string MakeRandomString(this string pwdchars, int pwdlen)
    {
        StringBuilder builder = new StringBuilder();
        Random rnd = new Random();
        for (int i = 0; i < pwdlen; i++)
        {
            int num = rnd.Next(pwdchars.Length);
            builder.Append(pwdchars[num]);
        }
        return builder.ToString();
    }
    #endregion

    #region 生成0-9随机数
    /// <summary>
    /// 生成0-9随机数
    /// </summary>
    /// <param name="VcodeNum">生成长度</param>
    /// <returns></returns>
    public static string RndNum(int VcodeNum)
    {
        StringBuilder sb = new StringBuilder(VcodeNum);
        Random rnd = new Random();
        for (int i = 1; i < VcodeNum + 1; i++)
        {
            int t = rnd.Next(9);
            sb.AppendFormat("{0}", t);
        }
        return sb.ToString();

    }
    #endregion

    #region 得到根据时间的随机文件名
    /// <summary>
    /// 得到根据时间的随机文件名
    /// </summary>
    /// <returns></returns>
    public static string MakeFileRndName()
    {
        return (DateTime.Now.ToString("yyyyMMddHHmmss") + MakeRandomString("0123456789", 4));
    }
    #endregion

    #region 获取时间编号
    public static string GetNO()
    {
        return DateTime.Now.ToString("yyyyMMddhhmmss");
    }
    #endregion

    #region 以日期为标准获得一个绝对的名称
    /// <summary>
    /// 以日期为标准获得一个绝对的名称
    /// </summary>
    /// <returns></returns>
    public static string MakeName()
    {
        return DateTime.Now.ToString("yyMMddHHmmss");
    }
    #endregion

    #region 得到年月的文件夹
    /// <summary>
    /// 得到年月的文件夹
    /// </summary>
    /// <returns></returns>
    public static string MakeFolderName()
    {
        return DateTime.Now.ToString("yyyyMM");
    }
    #endregion

    #region 获取一个由26个小写字母组成的指定长度的随即字符串
    /// <summary>
    /// 获取一个由26个小写字母组成的指定长度的随即字符串
    /// </summary>
    /// <param name="intLong">指定长度</param>
    /// <returns></returns>
    public static string RandomSTR(int intLong)
    {
        string str = "";
        string[] strArray = new string[] {
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
     };
        Random random = new Random();
        for (int i = 0; i < intLong; i++)
        {
            str += strArray[random.Next(26)];
        }
        return str;
    }
    #endregion

}