using XCode.Membership;

namespace DH.Services.Customers
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public partial interface ICustomerService
    {
        /// <summary>
        /// 获取客户角色标识符
        /// </summary>
        /// <param name="customer">用户</param>
        /// <returns>
        /// 任务结果包含客户角色标识符
        /// </returns>
        int[] GetCustomerRoleIds(User customer);
    }
}
