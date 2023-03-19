using NewLife;
using NewLife.Log;

namespace DH.Helpers;

/// <summary>
/// 格式化 操作
/// </summary>
public static partial class Format {
    /// <summary>
    /// 加密手机号码
    /// </summary>
    /// <param name="phone">手机号码</param>
    public static string EncryptPhoneOfChina(string phone) =>
        string.IsNullOrWhiteSpace(phone)
            ? string.Empty
            : $"{phone.Substring(0, 3)}******{phone.Substring(phone.Length - 2, 2)}";

    /// <summary>
    /// 加密车牌号
    /// </summary>
    /// <param name="plateNumber">车牌号</param>
    public static string EncryptPlateNumberOfChina(string plateNumber) =>
        string.IsNullOrWhiteSpace(plateNumber)
            ? string.Empty
            : $"{plateNumber.Substring(0, 2)}***{plateNumber.Substring(plateNumber.Length - 2, 2)}";

    /// <summary>
    /// 加密汽车VIN
    /// </summary>
    /// <param name="vinCode">汽车VIN</param>
    public static string EncryptVinCode(string vinCode) =>
        string.IsNullOrWhiteSpace(vinCode)
            ? string.Empty
            : $"{vinCode.Substring(0, 3)}***********{vinCode.Substring(vinCode.Length - 3, 3)}";

    /// <summary>
    /// 格式化金额
    /// </summary>
    /// <param name="money">金额</param>
    /// <param name="isEncrypt">是否加密。默认：false</param>
    public static string FormatMoney(decimal money, bool isEncrypt = false) => isEncrypt ? "***" : $"{money:N2}";

    /// <summary>
    /// 将传入的字符串中间部分字符替换成特殊字符
    /// </summary>
    /// <param name="value">需要替换的字符串</param>
    /// <param name="startLen">前保留长度</param>
    /// <param name="endLen">尾保留长度</param>
    /// <param name="specialChar">特殊字符</param>
    /// <returns>被特殊字符替换的字符串</returns>
    public static string ReplaceWithSpecialChar(string value, int startLen = 2, int endLen = 2, char specialChar = '*')
    {
        try
        {
            if (value.IsNullOrWhiteSpace())
            {
                return String.Empty;
            }
            int lenth = value.Length - startLen - endLen;

            if (lenth <= 0)
            {
                return value;
            }

            string replaceStr = value.Substring(startLen, lenth);

            string specialStr = string.Empty;

            for (int i = 0; i < replaceStr.Length; i++)
            {
                specialStr += specialChar;
            }

            value = value.Replace(replaceStr, specialStr);
        }
        catch (Exception ex)
        {
            XTrace.WriteException(ex);
        }

        return value;
    }
}