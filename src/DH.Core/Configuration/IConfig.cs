using System.Text.Json.Serialization;

namespace DH.Core.Configuration
{
    /// <summary>
    /// 表示应用程序设置中的配置
    /// </summary>
    public partial interface IConfig
    {
        /// <summary>
        /// 获取要加载配置的节名称
        /// </summary>
        [JsonIgnore]
        string Name => GetType().Name;

        /// <summary>
        /// 获取配置顺序
        /// </summary>
        /// <returns>Order</returns>
        public int GetOrder() => 1;
    }
}
