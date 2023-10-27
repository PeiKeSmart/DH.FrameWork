using LettuceEncrypt.Internal;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// 辅助方法
/// </summary>
internal static class LettuceEncryptApplicationBuilderExtensions
{
    /// <summary>
    /// 添加了用于验证域所有权的中间件
    /// </summary>
    /// <param name="app">应用程序生成器</param>
    /// <returns>应用程序生成器</returns>
    public static IApplicationBuilder UseHttpChallengeResponseMiddleware(this IApplicationBuilder app)
    {
        app.Map("/.well-known/acme-challenge", mapped =>
        {
            mapped.UseMiddleware<HttpChallengeResponseMiddleware>();
        });
        return app;
    }
}
