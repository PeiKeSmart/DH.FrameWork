using DH.Entity;

namespace DH.Core
{
    /// <summary>
    /// 表示工作上下文
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        UserDetail GetCurrentCustomer();

        /// <summary>
        /// 获取当前用户工作语言
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task<Language> GetWorkingLanguageAsync();

    }
}
