using DH.Core.Infrastructure;
using DH.Security;
using DH.Timing;

using NewLife.Caching;

namespace DH.Helpers;

/// <summary>
/// 接口安全检验
/// </summary>
public partial class CheckSignature {
    public static readonly string Token = "hlktech_test";

    /// <summary>
    /// 检查通讯密钥
    /// </summary>
    /// <param name="signature">通信加密签名</param>
    /// <param name="timestamp">时间戳</param>
    /// <param name="nonce">随机字符串</param>
    /// <param name="token">通讯密钥</param>
    /// <param name="retusnsignature">检验值</param>
    /// <returns>0为校验值不一致，1为正常，2为大于当前时间戳范围，3为小于当前时间戳范围，4为被使用过</returns>
    public static Int32 Check(string signature, long timestamp, string nonce, string token, out string retusnsignature)
    {
        retusnsignature = "";

        var time = UnixTime.ToDateTime(timestamp);
        if (time > DateTime.Now.AddSeconds(DHSetting.Current.SignatureExpire)) return 2;
        if (time < DateTime.Now.AddSeconds(-DHSetting.Current.SignatureExpire)) return 3;

        var _cache = EngineContext.Current.Resolve<ICache>();
        if (_cache.ContainsKey(signature))
        {
            return 4;
        }
        else
        {
            _cache.Set(signature, "", TimeSpan.FromSeconds(DHSetting.Current.SignatureExpire));
        }

        token ??= Token;
        string[] array = new string[] { timestamp.ToString(), nonce, token };
        Array.Sort(array);  //升序
        string text = string.Join("", array); //在指定 String 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串
                                              //text = DESEncrypt.Encrypt(text, 0);
        text = EncryptHelper.GetSha1(text);
        retusnsignature = text;
        return signature == retusnsignature ? 1 : 0;
    }

    /// <summary>
    /// 检查通讯密钥
    /// </summary>
    /// <param name="signature">通信加密签名</param>
    /// <param name="timestamp">时间戳</param>
    /// <param name="nonce">随机字符串</param>
    /// <param name="token">通讯密钥</param>
    /// <param name="retusnsignature">检验值</param>
    /// <param name="CacheTime">检验类型为1时生效，值为指定时间，单位为秒。</param>
    /// <param name="CheckType">检验类型：0为每个检验值只能被使用一次，1为指定时间内可一直使用</param>
    /// <returns>0为校验值不一致，1为正常，2为大于当前时间戳范围，3为小于当前时间戳范围，4为被使用过</returns>
    public static Int32 Check(string signature, long timestamp, string nonce, string token, Int32 CheckType, Int32 CacheTime, out string retusnsignature)
    {
        retusnsignature = "";

        var cacheTime = CacheTime;
        if (CheckType == 0)
        {
            cacheTime = DHSetting.Current.SignatureExpire;
        }
        else
        {
            if (cacheTime == 0)
            {
                cacheTime = DHSetting.Current.SignatureExpire;
            }
        }

        var time = UnixTime.ToDateTime(timestamp);
        if (time > DateTime.Now.AddSeconds(cacheTime)) return 2;
        if (time < DateTime.Now.AddSeconds(-cacheTime)) return 3;

        if (CheckType == 0)
        {
            var _cache = EngineContext.Current.Resolve<ICache>();
            if (_cache.ContainsKey(signature))
            {
                return 4;
            }
            else
            {
                _cache.Set(signature, "", TimeSpan.FromSeconds(DHSetting.Current.SignatureExpire));
            }
        }

        token ??= Token;
        string[] array = new string[] { timestamp.ToString(), nonce, token };
        Array.Sort(array);  //升序
        string text = string.Join("", array); //在指定 String 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串
                                              //text = DESEncrypt.Encrypt(text, 0);
        text = EncryptHelper.GetSha1(text);
        retusnsignature = text;
        return signature == retusnsignature ? 1 : 0;
    }

    /// <summary>
    /// 检查通讯密钥
    /// </summary>
    /// <param name="dic">传入数据</param>
    /// <param name="token">通讯密钥</param>
    /// <param name="retusnsignature">检验值</param>
    /// <returns></returns>
    public static bool CheckSign(IDictionary<String, String> dic, string token, out string retusnsignature)
    {
        retusnsignature = "";

        if (dic == null) return false;
        if (!dic.ContainsKey("timeStamp")) return false;
        if (!dic.ContainsKey("sign")) return false;

        var timestamp = dic["timeStamp"];
        var signature = dic["sign"];

        var time = UnixTime.ToDateTime(timestamp.ToDGLong());
        if (time > DateTime.Now.AddSeconds(DHSetting.Current.SignatureExpire)) return false;
        if (time < DateTime.Now.AddSeconds(-DHSetting.Current.SignatureExpire)) return false;

        var _cache = EngineContext.Current.Resolve<ICache>();
        if (_cache.ContainsKey(signature))
        {
            return false;
        }
        else
        {
            _cache.Set(signature, "", TimeSpan.FromSeconds(DHSetting.Current.SignatureExpire));
        }

        dic.Remove("sign");

        return signature == DH.Security.CheckSignature.CreateSign(dic, token);
    }
}