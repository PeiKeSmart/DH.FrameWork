using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

using System.Text;

namespace DH.AspNetCore.MVC.Formatter;

/// <summary>
/// body 支持 plain text
/// </summary>
/// <remarks>https://mp.weixin.qq.com/s/M1AGn6V_mpnewrr-QLnpoA</remarks>
public sealed class PlainTextInputFormatter : TextInputFormatter {
    public PlainTextInputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/plain"));

        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        using var reader = context.ReaderFactory(context.HttpContext.Request.Body, encoding);
        var rawContent = await reader.ReadToEndAsync();
        return await InputFormatterResult.SuccessAsync(rawContent);
    }
}