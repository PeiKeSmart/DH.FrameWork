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
        Language GetWorkingLanguage();

        /// <summary>
        /// 指示我们是否在管理区域中
        /// </summary>
        Boolean IsAdmin { get; set; }
    }
}
