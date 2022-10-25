namespace DH.Core.Domain.Seo
{
    /// <summary>
    /// 表示页面标题SEO调整
    /// </summary>
    public enum PageTitleSeoAdjustment
    {
        /// <summary>
        /// 页面名称位于站点名称之后
        /// </summary>
        PagenameAfterStorename = 0,

        /// <summary>
        /// 站点名称位于页面名称之后
        /// </summary>
        StorenameAfterPagename = 10
    }
}
