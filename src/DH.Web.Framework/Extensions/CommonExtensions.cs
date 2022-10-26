using Humanizer;
using Humanizer.Localisation;

using Microsoft.AspNetCore.Mvc.Rendering;

using System.Globalization;

namespace DH.Web.Framework.Extensions
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class CommonExtensions
    {
        /// <summary>
        /// 返回一个值，该值指示是否无法进行实际选择
        /// </summary>
        /// <param name="items">项目</param>
        /// <param name="ignoreZeroValue">一个值，指示是否应忽略值为“0”的项目</param>
        /// <returns>一个值，指示是否不可能进行真正的选择</returns>
        public static bool SelectionIsNotPossible(this IList<SelectListItem> items, bool ignoreZeroValue = true)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            // 我们忽略值为“0”的项目？通常是“全选”、“等等
            return items.Count(x => !ignoreZeroValue || !x.Value.ToString().Equals("0")) < 2;
        }

        /// <summary>
        /// DateTime的相对格式（例如2小时前、一个月前）
        /// </summary>
        /// <param name="source">源（UTC格式）</param>
        /// <param name="languageCode">语言文化代码</param>
        /// <returns>格式化的日期和时间字符串</returns>
        public static string RelativeFormat(this DateTime source, string languageCode = "en-US")
        {
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - source.Ticks);
            var delta = ts.TotalSeconds;

            CultureInfo culture;
            try
            {
                culture = new CultureInfo(languageCode);
            }
            catch (CultureNotFoundException)
            {
                culture = new CultureInfo("en-US");
            }
            return TimeSpan.FromSeconds(delta).Humanize(precision: 1, culture: culture, maxUnit: TimeUnit.Year);
        }
    }
}
