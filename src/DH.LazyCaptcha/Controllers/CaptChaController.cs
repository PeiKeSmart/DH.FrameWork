using DH.AspNetCore.Attributes;
using DH.Core.Infrastructure;
using DH.RateLimter;

using Microsoft.AspNetCore.Mvc;

namespace DH.LazyCaptcha.Controllers;

/// <summary>
/// 验证码控制器
/// </summary>
[HiddenApi]
public partial class CaptChaController : Controller
{
    /// <summary>
    /// 验证码，适用于跨平台。
    /// </summary>
    /// <returns></returns>
    [RateValve(Policy = Policy.Ip, Limit = 30, Duration = 60)]
    public IActionResult GetCheckCode()
    {
        var Captcha = EngineContext.Current.Resolve<ICaptcha>();

        var info = Captcha.Generate();
        var stream = new MemoryStream(info.Bytes);
        return File(stream, "image/gif");
    }
}
