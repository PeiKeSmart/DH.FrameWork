using NewLife;
using NewLife.Caching;

using XCode.Membership;

namespace DH.Services.Customers
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public partial class CustomerService : ICustomerService
    {

        private readonly ICache _cache;

        public CustomerService(ICache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// 获取客户角色标识符
        /// </summary>
        /// <param name="customer">用户</param>
        /// <returns>
        /// 任务结果包含客户角色标识符
        /// </returns>
        public virtual int[] GetCustomerRoleIds(User customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            var model = customer.RoleIds.SplitAsInt();

            return model;
        }

    }
}
