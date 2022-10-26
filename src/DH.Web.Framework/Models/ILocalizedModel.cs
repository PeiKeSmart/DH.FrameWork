namespace DH.Web.Framework.Models
{
    /// <summary>
    /// 表示本地化模型
    /// </summary>
    public partial interface ILocalizedModel
    {
    }

    /// <summary>
    /// 表示通用本地化模型
    /// </summary>
    /// <typeparam name="TLocalizedModel">本地化模型类型</typeparam>
    public partial interface ILocalizedModel<TLocalizedModel> : ILocalizedModel
    {
        /// <summary>
        /// 获取或设置本地化区域设置模型
        /// </summary>
        IList<TLocalizedModel> Locales { get; set; }
    }
}
