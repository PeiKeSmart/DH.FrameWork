using Microsoft.AspNetCore.Html;

namespace DH.Web.Framework.Localization
{
    /// <summary>
    /// 本地化字符串
    /// </summary>
    public partial class LocalizedString : HtmlString
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="localized">本地化值</param>
        public LocalizedString(string localized) : base(localized)
        {
            Text = localized;
        }

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; }
    }
}
