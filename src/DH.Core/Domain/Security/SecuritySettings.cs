using DH.Core.Configuration;

namespace DH.Core.Domain.Security
{
    /// <summary>
    /// 安全设置
    /// </summary>
    public partial class SecuritySettings : ISettings
    {
        /// <summary>
        /// 获取或设置加密密钥
        /// </summary>
        public string EncryptionKey { get; set; }

        /// <summary>
        /// 获取或设置管理区域允许的IP地址列表
        /// </summary>
        public List<string> AdminAreaAllowedIpAddresses { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否在注册页上启用蜜罐
        /// </summary>
        public bool HoneypotEnabled { get; set; }

        /// <summary>
        /// 获取或设置蜜罐输入名称
        /// </summary>
        public string HoneypotInputName { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示头中是否允许非ASCII字符
        /// </summary>
        public bool AllowNonAsciiCharactersInHeaders { get; set; }
    }
}
