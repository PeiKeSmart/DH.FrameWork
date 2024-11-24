using System.Text;

using Pek.Security;

namespace DH.Security;

public class CheckSignature
{
    public static readonly string Token = "hlktech_test";

    public static String Create(long timestamp, string nonce, string token)
    {
        token ??= Token;
        var array = new string[] { timestamp.ToString(), nonce, token };
        Array.Sort(array);  //升序
        var text = string.Join("", array); //在指定 String 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串
                                              //text = DESEncrypt.Encrypt(text, 0);
        text = Encrypt.GetSha1(text);
        return text;
    }

    /// <summary>
    /// 生成接口校验值。参考伊戈尔项目SaasController/GetGateWayAndDevices
    /// </summary>
    /// <param name="dic"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static String CreateSign(IDictionary<String, String> dic, string token)
    {
        token ??= Token;

        var array = dic.Keys.ToArray();
        Array.Sort(array);  //升序
        var build = new StringBuilder();
        foreach (var item in array)
        {
            build.Append($"{item}={dic[item]}&");
        }
        var result = build.ToString().Trim('&');
        result += $"&key={token}";
        result = Encrypt.Md5Upper(result);

        return result;
    }

}