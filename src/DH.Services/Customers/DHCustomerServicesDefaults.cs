using DH.Core.Caching;

namespace DH.Services.Customers
{
    /// <summary>
    /// 表示与客户服务相关的默认值
    /// </summary>
    public static partial class DHCustomerServicesDefaults
    {

        /// <summary>
        /// 获取用于缓存的键
        /// </summary>
        /// <remarks>
        /// {0} : 用户识别符
        /// {1} : show hidden
        /// </remarks>
        public static CacheKey CustomerRoleIdsCacheKey => new("DH.customer.customerrole.ids.{0}-{1}", CustomerCustomerRolesPrefix);

        /// <summary>
        /// 获取要清除缓存的键模式
        /// </summary>
        public static string CustomerCustomerRolesPrefix => "DH.customer.customerrole.";

    }
}
