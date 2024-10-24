using DH.Entity;
using DH.Helpers;

using Microsoft.AspNetCore.Mvc.Filters;

using NewLife;

using Pek.Models;

namespace DH.AspNetCore.MVC.Filters;

/// <summary>
/// 接口校验过滤器,IsCheckApiSignature为true则检验
/// Signature:通信加密签名
/// TimeStamp:时间戳
/// Nonce:随机数
/// </summary>
public class ApiSignatureAttribute : ActionFilterAttribute {
    /// <summary>
    /// 检验类型：0为每个检验值只能被使用一次，1为指定时间内可一直使用
    /// </summary>
    public Int32 CheckType { get; set; }

    /// <summary>
    /// 检验类型为1时生效，值为指定时间，单位为秒。
    /// </summary>
    public Int32 CacheTime { get; set; }

    /// <summary>
    /// 是否检验时间戳
    /// </summary>
    public Boolean CheckTimeStamp { get; set; } = true;

    /// <summary>
    /// 通信加密签名
    /// </summary>
    private String Signature;

    /// <summary>
    /// 时间戳
    /// </summary>
    private String TimeStamp;

    /// <summary>
    /// 随机数
    /// </summary>
    private String Nonce;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (DHSetting.Current.IsCheckApiSignature)
        {
            var result = new DGResult();

            var headers = context.HttpContext.Request.Headers;

            var Token = DHSetting.Current.ServerToken;  // 获取与客户端协定的密钥

            if (!headers.ContainsKey("Signature"))
            {
                result.Message = LocaleStringResource.GetResource("传入的Signature不能为空");
                context.Result = result;
                return;
            }

            if (!headers.ContainsKey("TimeStamp"))
            {
                result.Message = LocaleStringResource.GetResource("传入的TimeStamp不能为空");
                context.Result = result;
                return;
            }

            if (!headers.ContainsKey("Nonce"))
            {
                result.Message = LocaleStringResource.GetResource("传入的Nonce不能为空");
                context.Result = result;
                return;
            }

            Signature = headers["Signature"];
            TimeStamp = headers["TimeStamp"];
            Nonce = headers["Nonce"];

            if (Signature.IsNullOrEmpty() || TimeStamp.IsNullOrEmpty() || Nonce.IsNullOrEmpty())
            {
                result.Message = LocaleStringResource.GetResource("传入的检验值不能为空");
                context.Result = result;
                return;
            }

            var m = DH.Helpers.CheckSignature.Check(Signature, TimeStamp.ToLong(), Nonce, Token, CheckType, CacheTime, CheckTimeStamp, out var sign);

            if (m != 1)
            {
                if (m == 2 || m == 3)
                {
                    result.Message = LocaleStringResource.GetResource("时间戳有误,请检查并同步时间");
                }
                else if (m == 4)
                {
                    result.Message = LocaleStringResource.GetResource("签名已使用");
                }
                else
                {
                    result.Message = LocaleStringResource.GetResource("校验失败");
                }
                //result.Data = new { Signature = Signature, TimeStamp = TimeStamp, Nonce = Nonce, Token = Token, Sign = sign };
                context.Result = result;
                return;
            }
        }

        base.OnActionExecuting(context);
    }
}