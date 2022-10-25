namespace DH.Web.Framework.Themes
{
    /// <summary>
    /// 表示主题上下文
    /// </summary>
    public partial interface IThemeContext
    {
        /// <summary>
        /// 获取当前主题系统名称
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task<string> GetWorkingThemeNameAsync();

        /// <summary>
        /// 设置当前主题系统名称
        /// </summary>
        void SetWorkingThemeNameAsync(string workingThemeName);
    }
}
