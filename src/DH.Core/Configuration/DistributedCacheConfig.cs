using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DH.Core.Configuration
{
    /// <summary>
    /// 表示分布式缓存配置参数
    /// </summary>
    public partial class DistributedCacheConfig : IConfig
    {
        /// <summary>
        /// 获取或设置分布式缓存类型
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public DistributedCacheType DistributedCacheType { get; private set; } = DistributedCacheType.Redis;

        /// <summary>
        /// 获取或设置一个值，该值指示是否应使用分布式缓存
        /// </summary>
        public bool Enabled { get; private set; } = false;

        /// <summary>
        /// 获取或设置连接字符串。启用分布式缓存时使用
        /// </summary>
        public string ConnectionString { get; private set; } = "127.0.0.1:6379,ssl=False";

        /// <summary>
        /// 获取或设置架构名称。在启用分布式缓存且DistributedCacheType属性设置为SqlServer时使用
        /// </summary>
        public string SchemaName { get; private set; } = "dbo";

        /// <summary>
        /// 获取或设置表名称。在启用分布式缓存且DistributedCacheType属性设置为SqlServer时使用
        /// </summary>
        public string TableName { get; private set; } = "DistributedCache";
    }
}
