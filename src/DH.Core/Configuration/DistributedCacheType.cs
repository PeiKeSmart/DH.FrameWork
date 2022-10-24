using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace DH.Core.Configuration
{
    /// <summary>
    /// 表示分布式缓存类型枚举
    /// </summary>
    public enum DistributedCacheType
    {
        [EnumMember(Value = "memory")]
        Memory,
        [EnumMember(Value = "sqlserver")]
        SqlServer,
        [EnumMember(Value = "redis")]
        Redis
    }
}
