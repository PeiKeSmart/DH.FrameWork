using DH.Core.Configuration;

namespace DH.Core.Domain.Security
{
    /// <summary>
    /// 代理设置
    /// </summary>
    public partial class ProxySettings : ISettings
    {
        /// <summary>
        /// 获取或设置一个值，该值指示是否应使用代理连接
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取或设置代理服务器的地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 获取或设置代理服务器的端口
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 获取或设置代理连接的用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 获取或设置代理连接的密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否为本地地址绕过代理服务器
        /// </summary>
        public bool BypassOnLocal { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示处理程序是否随请求发送Authorization标头
        /// </summary>
        public bool PreAuthenticate { get; set; }
    }
}
