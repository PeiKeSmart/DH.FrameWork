using DH.Entity;
using DH.Helpers;
using DH.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using NewLife;

namespace DH.Services.MVC.Filters;

/// <summary>
/// 接口校验过滤器,如果正式环境Setting.Current.Debug为false则检验
/// </summary>
public class ApiNewSignatureAttribute : ActionFilterAttribute {
    /// <summary>
    /// 自定义检验数据
    /// </summary>
    public String CheckName { get; set; }

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
        var result = new DResult();

        var headers = context.HttpContext.Request.Headers;

        var Token = DHSetting.Current.ServerToken;  // 获取与客户端协定的密钥

        if (!headers.ContainsKey("Signature") || !headers.ContainsKey("TimeStamp") || !headers.ContainsKey("Nonce"))
        {
            result.msg = LocaleStringResource.GetResource("传入的检验值不能为空");
            context.Result = new JsonResult(result);
            return;
        }

        Signature = headers["Signature"];
        TimeStamp = headers["TimeStamp"];
        Nonce = headers["Nonce"];

        if (Signature.IsNullOrEmpty() || TimeStamp.IsNullOrEmpty() || Nonce.IsNullOrEmpty())
        {
            result.msg = LocaleStringResource.GetResource("传入的检验值不能为空");
            context.Result = new JsonResult(result);
            return;
        }

        if (CheckSignature.Check(Signature, TimeStamp.ToLong(), Nonce, Token, out String sign) != 1)
        {
            result.msg = LocaleStringResource.GetResource("校验失败");
            result.data = new { Signature = Signature, TimeStamp = TimeStamp, Nonce = Nonce, Token = Token, Sign = sign };
            context.Result = new JsonResult(result);
            return;
        }

        base.OnActionExecuting(context);
    }
}