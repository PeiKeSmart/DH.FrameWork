using Newtonsoft.Json;

namespace DH.Services.Plugins
{
    /// <summary>
    /// 表示插件描述符的基本信息
    /// </summary>
    public partial class PluginDescriptorBaseInfo : IComparable<PluginDescriptorBaseInfo>
    {
        /// <summary>
        /// 获取或设置插件系统名称
        /// </summary>
        [JsonProperty(PropertyName = "SystemName")]
        public virtual string SystemName { get; set; }

        /// <summary>
        /// 获取或设置版本
        /// </summary>
        [JsonProperty(PropertyName = "Version")]
        public virtual string Version { get; set; }

        /// <summary>
        /// 将此实例与指定的PluginDescriptorBaseInfo对象进行比较
        /// </summary>
        /// <param name="other">要与此实例比较的PluginDescriptorBaseInfo</param>
        /// <returns>一个整数，指示此实例是在指定参数的排序顺序中的前面、后面还是出现在同一位置</returns>
        public int CompareTo(PluginDescriptorBaseInfo other)
        {
            return string.Compare(SystemName, other.SystemName, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// 确定此实例和另一个指定的PluginDescriptor对象是否具有相同的SystemName
        /// </summary>
        /// <param name="value">要与此实例比较的PluginDescriptor</param>
        /// <returns>如果value参数的SystemName与此实例的SystemName相同，则为True；否则，为false</returns>
        public override bool Equals(object value)
        {
            return SystemName?.Equals((value as PluginDescriptorBaseInfo)?.SystemName) ?? false;
        }

        /// <summary>
        /// 返回此插件描述符的哈希代码
        /// </summary>
        /// <returns>32位有符号整数哈希码</returns>
        public override int GetHashCode()
        {
            return SystemName.GetHashCode();
        }

        /// <summary>
        /// 获取插件描述符的基本信息的副本
        /// </summary>
        [JsonIgnore]
        public virtual PluginDescriptorBaseInfo GetBaseInfoCopy =>
            new()
            { SystemName = SystemName, Version = Version };
    }
}
