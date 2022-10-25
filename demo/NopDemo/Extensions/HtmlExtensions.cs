using DH.Core.Infrastructure;
using DH.Services.Themes;
using DH.Web.Framework.Themes;

using Microsoft.AspNetCore.Mvc.Rendering;

using System.Globalization;

namespace NopDemo.Extensions
{
    public static class HtmlExtensions
    {


        /// <summary>
        /// 返回一个值，指示工作语言和主题是否支持RTL（从右到左）
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="themeName">Theme name</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the value
        /// </returns>
        public static async Task<bool> ShouldUseRtlThemeAsync(this IHtmlHelper html, string themeName = null)
        {
            if (!CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
                return false;

            // 确保活动主题也支持它
            themeName ??= await EngineContext.Current.Resolve<IThemeContext>().GetWorkingThemeNameAsync();
            var theme = await EngineContext.Current.Resolve<IThemeProvider>().GetThemeBySystemNameAsync(themeName);

            return theme?.SupportRtl ?? false;
        }
    }
}
