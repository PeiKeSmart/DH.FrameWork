using DH.Core.Configuration;

namespace DH.Core.Domain.Customers
{
    /// <summary>
    /// 外部身份验证设置
    /// </summary>
    public partial class ExternalAuthenticationSettings : ISettings
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExternalAuthenticationSettings()
        {
            ActiveAuthenticationMethodSystemNames = new List<string>();
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是否需要电子邮件验证。
        /// 在大多数情况下，我们可以跳过Facebook或任何其他第三方外部认证插件的电子邮件验证。我想我们可以信任Facebook进行验证。
        /// </summary>
        public bool RequireEmailValidation { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否需要记录身份验证过程中的错误
        /// </summary>
        public bool LogErrors { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否允许用户删除外部身份验证关联
        /// </summary>
        public bool AllowCustomersToRemoveAssociations { get; set; }

        /// <summary>
        /// 获取或设置活动身份验证方法的系统名称
        /// </summary>
        public List<string> ActiveAuthenticationMethodSystemNames { get; set; }
    }
}
