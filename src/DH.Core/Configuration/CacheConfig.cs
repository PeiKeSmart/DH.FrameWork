namespace DH.Core.Configuration
{
    /// <summary>
    /// 表示缓存配置参数
    /// </summary>
    public partial class CacheConfig : IConfig
    {
        /// <summary>
        /// 获取或设置默认缓存时间（分钟）
        /// </summary>
        public int DefaultCacheTime { get; private set; } = 60;

        /// <summary>
        /// 获取或设置短期缓存时间（分钟）
        /// </summary>
        public int ShortTermCacheTime { get; private set; } = 3;

        /// <summary>
        /// 获取或设置绑定文件缓存时间（分钟）
        /// </summary>
        public int BundledFilesCacheTime { get; private set; } = 120;
    }
}
