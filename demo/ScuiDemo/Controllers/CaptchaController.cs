using DH.Helpers;
using DH.Web.Framework;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NewLife.Caching;

using ScuiDemo.Common.Utils;

namespace ScuiDemo.Controllers;

public class CaptchaController: ApiControllerBaseX
{
    [HttpGet("{identify}"), AllowAnonymous]
    public async Task<IActionResult> Get(string identify)
    {
        if (string.IsNullOrEmpty(identify))
        {
            identify = DHWeb.IP;
        }

        var code = await CaptchaUtils.GenerateRandomCaptchaAsync(4);
        Cache.Default.Set<String>(KeyUtils.CAPTCHACODE + identify, code, 5 * 60);
        var captcha = await CaptchaUtils.GenerateCaptchaImageAsync(code);
        return File(captcha.ms.ToArray(), "image/gif");
    }
}
