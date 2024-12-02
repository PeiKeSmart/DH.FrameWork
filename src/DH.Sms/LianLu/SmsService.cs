using DH.Security;

using Microsoft.Extensions.Options;

using Pek.Timing;

namespace DH.Sms.LianLu;

/// <summary>
/// 短信服务
/// </summary>
public class SmsService : ISmsService
{
    /// <summary>
    /// 短信配置选项
    /// </summary>
    private readonly LianLuSms _options;

    /// <summary>
    /// 初始化短信服务
    /// </summary>
    /// <param name="options">短信配置选项</param>
    public SmsService(IOptionsMonitor<LianLuSms> options)
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
        _options = new LianLuSms()
        {
            AccessKeyId = UserName,
            AccessKeySecret = PassWord,
            passKey = PassKey
        };
    }

    /// <summary>
    /// 发送短信
    /// </summary>
    /// <param name="mobiles">手机号,可批量，用逗号分隔开，上限为1000个</param>
    /// <param name="content">内容</param>
    public async Task<SmsResult> SendAsync(string mobiles, string content)
    {
        var sendaction = _options.Url + "/api/sms/send";

        var ts = UnixTime.ToTimestamp();
        var sign = EncryptHelper.GetMD5($"{_options.AccessKeyId}{ts}{_options.AccessKeySecret}").ToLower();

        var result = await Pek.Helpers.DHWeb.Client().Post(sendaction)
            .Data("userid", _options.AccessKeyId)
            .Data("ts", ts)
            .Data("sign", sign)
            .Data("mobile", mobiles)
            //.Data("msgcontent", content.UrlEncode())
            .Data("msgcontent", content)
            .Data("extnum", "")
            .Data("time", "")
            .Data("messageid", "")
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
}
