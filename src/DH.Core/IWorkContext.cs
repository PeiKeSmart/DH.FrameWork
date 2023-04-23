using DH.Entity;

namespace DH.Core {
    /// <summary>
    /// 表示工作上下文
    /// </summary>
    public interface IWorkContext {
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        UserDetail CurrentCustomer { get; }

        /// <summary>
        /// 获取当前用户工作语言
        /// </summary>
        /// <returns></returns>
        Language WorkingLanguage { get; set; }

        /// <summary>
        /// 指示我们是否在管理区域中
        /// </summary>
        Boolean IsAdmin { get; set; }
    }
}
