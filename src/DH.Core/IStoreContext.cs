using DH.Entity;

namespace DH.Core
{
    /// <summary>
    /// 系统上下文
    /// </summary>
    public interface IStoreContext
    {
        /// <summary>
        /// 获取当前系统信息
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task<Store> GetCurrentStoreAsync();

        /// <summary>
        /// 获取当前系统信息
        /// </summary>
        Store GetCurrentStore();

        /// <summary>
        /// 获取活动系统范围配置
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task<int> GetActiveStoreScopeConfigurationAsync();
    }
}
