using DH.Core.Domain.Localization;
using DH.Core.Infrastructure;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace DH.Services.Localization;

/// <summary>
/// 通过URL确定请求的区域性信息
/// </summary>
public partial class DHSeoUrlCultureProvider : RequestCultureProvider
{
    /// <summary>
    /// 实现提供程序以确定给定请求的区域性
    /// </summary>
    /// <param name="httpContext">请求的httpContext</param>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含ProviderCultureResult
    /// </returns>
    public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
    {
        var localizationSettings = LocalizationSettings.Current;

        if (!localizationSettings.SeoFriendlyUrlsForLanguagesEnabled)
            return await NullProviderCultureResult;

        // 已启用本地化URL，因此请尝试从请求的页面URL获取语言
        var (isLocalized, language) = httpContext.Request.Path.Value.IsLocalizedUrlAsync(httpContext.Request.PathBase, false);
        if (!isLocalized || language is null)
            return await NullProviderCultureResult;

        return new ProviderCultureResult(language.LanguageCulture);
    }
}