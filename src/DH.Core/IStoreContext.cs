using DH.Entity;

namespace DH.Core
{
    /// <summary>
    /// 站点上下文
    /// </summary>
    public interface IStoreContext
    {
        /// <summary>
        /// 获取当前站点信息
        /// </summary>
        Store GetCurrentStore();
    }
}
