using DH.Core.Configuration;

namespace DH.Core.Domain.Catalog
{
    /// <summary>
    /// 目录设置
    /// </summary>
    public partial class CatalogSettings : ISettings
    {

        /// <summary>
        /// 获取或设置一个值，该值指示是否忽略ACL规则（侧面）。启用后，它可以显著提高性能。
        /// </summary>
        public bool IgnoreAcl { get; set; }


        /// <summary>
        /// 获取或设置一个值，该值指示是否在目录页上显示所有图片
        /// </summary>
        public bool DisplayAllPicturesOnCatalogPages { get; set; }

    }
}
