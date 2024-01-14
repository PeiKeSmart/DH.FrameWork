using DH.Cookies;
using DH.Core.Infrastructure;
using DH.Helpers;
using DH.Security;

using Microsoft.Extensions.Options;

namespace DH.Sms.FengHuo;

/// <summary>
/// 短信服务
/// </summary>
public class SmsService : ISmsService
{
    /// <summary>
    /// 短信配置选项
    /// </summary>
    private readonly FengHuoSms _options;

    /// <summary>
    /// 初始化短信服务
    /// </summary>
    /// <param name="options">短信配置选项</param>
    public SmsService(IOptionsMonitor<FengHuoSms> options)
    {
        _options = options.CurrentValue;
    }

    /// <summary>
    /// 初始化短信服务
    /// </summary>
    /// <param name="UserName">短信平台账号</param>
    /// <param name="PassWord">短信平台密码</param>
    /// <param name="PassKey">短信签名</param>
    public SmsService(String UserName, String PassWord, String PassKey)
    {
        _options = new FengHuoSms()
        {
            AccessKeyId = UserName,
            AccessKeySecret = PassWord,
            passKey = PassKey
        };
    }

    /// <summary>
    /// 获取YYYYMMDDHHMISS格式当前时间
    /// </summary>
    /// <returns></returns>
    private string GetSeed()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmss");
    }

    /// <summary>
    /// 获取加密数据
    /// </summary>
    /// <param name="seed">时间</param>
    /// <returns></returns>
    private string GetToken(string seed)
    {
        return Encrypt.Sha1("account=" + _options.AccessKeyId + "&ts=" + seed + "&secret=" + _options.AccessKeySecret).ToLower();
    }

    /// <summary>
    /// 发送短信
    /// </summary>
    /// <param name="mobile">手机号,可批量，用逗号分隔开，上限为1000个</param>
    /// <param name="content">内容</param>
    public async Task<SmsResult> SendAsync(string mobile, string content)
    {
        var seed = GetSeed();
        var token = GetToken(seed);
        var sendaction = _options.Url + "send";

        var result = await DHWeb.Client().Post(sendaction)
            .Data("account", _options.AccessKeyId)
            .Data("token", token)
            .Data("ts", seed)
            .Data("mobiles", mobile)
            .Data("content", content.UrlEncode())
            .Data("ext", "")
            .ResultAsync();
        if (result.Contains("提交成功"))
        {
            return new SmsResult(true, result);
        }
        else
        {
            return new SmsResult(false, result);
        }
    }

    /// <summary>
    /// 发送模板短信
    /// </summary>
    /// <param name="mobiles">手机号,可批量，用逗号分隔开，上限为1000个</param>
    /// <param name="templateId">对应的模板ID</param>
    /// <param name="paramValues">对应的参数</param>
    /// <returns></returns>
    public async Task<SmsResult> SendTemplateParamd(string mobiles, String templateId, String[] paramValues)
    {
        var seed = GetSeed();
        var token = GetToken(seed);
        var sendaction = _options.Url + "sendTemplateParamd";

        var _cookie = EngineContext.Current.Resolve<ICookie>();
        var Sid = _cookie.GetValue<Int64>(DHSetting.Current.SidName);

        var irequest = DHWeb.Client().Post(sendaction)
            .Data("account", _options.AccessKeyId)
            .Data("token", token)
            .Data("ts", seed)
            .Data("templateId", templateId)
            .Data("mobiles", mobiles)
            .Data("ref", Sid)
            .Data("ext", "");

        for(var i = 0; i < paramValues.Length; i++)
        {
            irequest.Data($"param{i + 1}", paramValues[i]);
        }

        var result = await irequest.ResultAsync();
        if (result.Contains("提交成功"))
        {
            return new SmsResult(true, result);
        }
        else
        {
            return new SmsResult(false, result);
        }
    }
}
