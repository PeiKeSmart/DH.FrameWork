using DH.Core.Webs;

using Microsoft.Extensions.Options;

using Pek.Helpers;
using Pek.Security;
using Pek.Webs;

namespace DH.Sms.FengHuo;

/// <summary>
/// 短信服务
/// </summary>
public class SmsService : ISmsService
{
    /// <summary>
    /// 短信配置选项
    /// </summary>
    private FengHuoSms _options;

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
    private static String GetSeed() => DateTime.Now.ToString("yyyyMMddHHmmss");

    /// <summary>
    /// 获取加密数据
    /// </summary>
    /// <param name="seed">时间</param>
    /// <returns></returns>
    private String GetToken(String seed) => Encrypt.Sha1("account=" + _options.AccessKeyId + "&ts=" + seed + "&secret=" + _options.AccessKeySecret).ToLower();

    /// <summary>
    /// 发送短信
    /// </summary>
    /// <param name="mobile">手机号,可批量，用逗号分隔开，上限为1000个</param>
    /// <param name="content">内容</param>
    public async Task<SmsResult> SendAsync(String mobile, String content)
    {
        var seed = GetSeed();
        var token = GetToken(seed);
        var sendaction = _options.Url + "send";

        var response = await DHWeb.Client().Post(sendaction)
            .Data("account", _options.AccessKeyId)
            .Data("token", token)
            .Data("ts", seed)
            .Data("mobiles", mobile)
            .Data("content", content.UrlEncode())
            .Data("ext", "")
            .GetResponseAsync().ConfigureAwait(false);

        if (!response.IsSuccess)
        {
            var errorMsg = $"HTTP请求失败: {response.StatusCode}, 响应: {response.GetDataOrDefault("N/A")}";
            return new SmsResult(false, errorMsg);
        }

        var result = response.GetDataOrDefault(String.Empty);
        if (result.Contains("提交成功"))
        {
            return new SmsResult(true, result);
        }
        else
        {
            return new SmsResult(false, $"短信发送失败: {result}");
        }
    }

    /// <summary>
    /// 发送模板短信
    /// </summary>
    /// <param name="AccessKeyId">AccessId</param>
    /// <param name="AccessKeySecret">AccessSecret</param>
    /// <param name="passKey">短信签名</param>
    /// <param name="mobiles">手机号,可批量，用逗号分隔开，上限为1000个</param>
    /// <param name="templateId">对应的模板ID</param>
    /// <param name="paramValues">对应的参数</param>
    /// <returns></returns>
    public async Task<SmsResult> SendTemplateParamd(String AccessKeyId, String AccessKeySecret, String passKey, String mobiles, String templateId, String[] paramValues) 
    {
        _options = new FengHuoSms()
        {
            AccessKeyId = AccessKeyId,
            AccessKeySecret = AccessKeySecret,
            passKey = passKey
        };

        var seed = GetSeed();
        var token = GetToken(seed);
        var sendaction = _options.Url + "sendTemplateParamd";

        var Sid = DHWebHelper.FillDeviceId(Pek.Webs.HttpContext.Current);

        var irequest = Pek.Helpers.DHWeb.Client().Post(sendaction)
            .Data("account", _options.AccessKeyId)
            .Data("token", token)
            .Data("ts", seed)
            .Data("templateId", templateId)
            .Data("mobiles", mobiles)
            .Data("ref", Sid)
            .Data("ext", "");

        for (var i = 0; i < paramValues.Length; i++)
        {
            irequest.Data($"param{i + 1}", paramValues[i]);
        }

        var response = await irequest.GetResponseAsync().ConfigureAwait(false);

        if (!response.IsSuccess)
        {
            var errorMsg = $"HTTP请求失败: {response.StatusCode}, 响应: {response.GetDataOrDefault("N/A")}";
            return new SmsResult(false, errorMsg);
        }

        var result = response.GetDataOrDefault(String.Empty);
        if (result.Contains("提交成功"))
        {
            return new SmsResult(true, result);
        }
        else
        {
            return new SmsResult(false, $"模板短信发送失败: {result}");
        }
    }
}
