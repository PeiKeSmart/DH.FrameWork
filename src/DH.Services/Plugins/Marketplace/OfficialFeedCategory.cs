namespace DH.Services.Plugins.Marketplace
{
    /// <summary>
    /// Category for the official marketplace
    /// </summary>
    public partial class OfficialFeedCategory
    {
        /// <summary>
        /// 标识符
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父类别标识符
        /// </summary>
        public int ParentCategoryId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
