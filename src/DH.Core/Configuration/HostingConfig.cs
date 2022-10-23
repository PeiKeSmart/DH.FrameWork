namespace DH.Core.Configuration
{
    /// <summary>
    /// 表示宿主配置参数
    /// </summary>
    public partial class HostingConfig : IConfig
    {
        /// <summary>
        /// 获取或设置一个值，该值指示是否使用代理服务器和负载平衡器
        /// </summary>
        public bool UseProxy { get; private set; }

        /// <summary>
        /// 获取或设置用于检索原始方案（HTTP/HTTPS）的值的标头
        /// </summary>
        public string ForwardedProtoHeaderName { get; private set; } = string.Empty;

        /// <summary>
        /// 获取或设置用于检索原始客户端IP的标头
        /// </summary>
        public string ForwardedForHeaderName { get; private set; } = string.Empty;

        /// <summary>
        /// 获取或设置已知代理的地址以接受来自
        /// </summary>
        public string KnownProxies { get; private set; } = string.Empty;
    }
}
