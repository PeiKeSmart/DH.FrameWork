namespace DH.Web.Framework.Models
{
    /// <summary>
    /// 表示本地化的区域设置模型
    /// </summary>
    public partial interface ILocalizedLocaleModel
    {
        /// <summary>
        /// 获取或设置语言标识符
        /// </summary>
        int LanguageId { get; set; }
    }
}
