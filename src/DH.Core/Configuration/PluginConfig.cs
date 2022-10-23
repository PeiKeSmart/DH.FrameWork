namespace DH.Core.Configuration
{
    /// <summary>
    /// 表示插件配置参数
    /// </summary>
    public partial class PluginConfig : IConfig
    {
        /// <summary>
        /// 获取或设置一个值，该值指示是否绕过某些安全检查将程序集从上下文加载到加载中。
        /// </summary>
        public bool UseUnsafeLoadAssembly { get; set; } = true;
    }
}
