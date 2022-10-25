using DH.Entity;

using Microsoft.AspNetCore.Http;

using System.Net;

namespace DH.Services.Localization
{
    /// <summary>
    /// 表示本地化URL的扩展
    /// </summary>
    public static class LocalizedUrlExtensions
    {
        /// <summary>
        /// 获取一个值，该值指示URL是否本地化（包含SEO代码）
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="pathBase">应用程序路径库</param>
        /// <param name="isRawPath">指示传递的URL是否为原始URL的值</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含rue，如果传递的URL包含SEO代码；否则为假。如果URL已本地化，则其SEO代码位于URL中的语言
        /// </returns>
        public static (bool IsLocalized, Language Language) IsLocalizedUrlAsync(this string url, PathString pathBase, bool isRawPath)
        {
            if (string.IsNullOrEmpty(url))
                return (false, null);

            // 从原始URL中删除应用程序路径
            if (isRawPath)
                url = url.RemoveApplicationPathFromRawUrl(pathBase);

            // 获取传递的URL的第一段
            var firstSegment = url.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? string.Empty;
            if (string.IsNullOrEmpty(firstSegment))
                return (false, null);

            // 假设第一段是语言代码，并尝试获取语言
            var language = Language.GetAllLanguages()
                .FirstOrDefault(urlLanguage => urlLanguage.UniqueSeoCode.Equals(firstSegment, StringComparison.InvariantCultureIgnoreCase));

            // 如果语言存在并且发布的传递URL已本地化
            return (language?.Status ?? false, language);
        }

        /// <summary>
        /// 从原始URL中删除应用程序路径
        /// </summary>
        /// <param name="rawUrl">原始URL</param>
        /// <param name="pathBase">应用程序路径库</param>
        /// <returns>结果</returns>
        public static string RemoveApplicationPathFromRawUrl(this string rawUrl, PathString pathBase)
        {
            new PathString(rawUrl).StartsWithSegments(pathBase, out var result);
            return WebUtility.UrlDecode(result);
        }

        /// <summary>
        /// 从URL中删除语言SEO代码
        /// </summary>
        /// <param name="url">原始url</param>
        /// <param name="pathBase">应用程序路径库</param>
        /// <param name="isRawPath">一个值，指示传递的URL是否为原始URL</param>
        /// <returns>没有语言SEO代码的URL</returns>
        public static string RemoveLanguageSeoCodeFromUrl(this string url, PathString pathBase, bool isRawPath)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            // 从原始URL中删除应用程序路径
            if (isRawPath)
                url = url.RemoveApplicationPathFromRawUrl(pathBase);

            // 获取结果URL
            url = url.TrimStart('/');
            var result = url.Contains('/') ? url[(url.IndexOf('/'))..] : string.Empty;

            // 并将应用程序路径添加回原始URL
            if (isRawPath)
                result = pathBase + result;

            return result;
        }

        /// <summary>
        /// 将语言SEO代码添加到URL
        /// </summary>
        /// <param name="url">原始URL</param>
        /// <param name="pathBase">应用程序路径库</param>
        /// <param name="isRawPath">指示传递的URL是否为原始URL的值</param>
        /// <param name="language">语言</param>
        /// <returns>结果</returns>
        public static string AddLanguageSeoCodeToUrl(this string url, PathString pathBase, bool isRawPath, Language language)
        {
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            // 不需要null验证
            //if (string.IsNullOrEmpty(url))
            //    return url;

            // 从原始URL中删除应用程序路径
            if (isRawPath && !string.IsNullOrEmpty(url))
                url = url.RemoveApplicationPathFromRawUrl(pathBase);

            // 添加语言代码
            url = $"/{language.UniqueSeoCode}{url}";

            // 并将应用程序路径添加回原始URL
            if (isRawPath)
                url = pathBase + url;

            return url;
        }
    }
}
