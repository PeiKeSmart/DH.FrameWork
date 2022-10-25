namespace DH.Services.Themes
{
    /// <summary>
    /// 表示主题提供程序
    /// </summary>
    public partial interface IThemeProvider
    {
        /// <summary>
        /// Get theme descriptor from the description text
        /// </summary>
        /// <param name="text">Description text</param>
        /// <returns>Theme descriptor</returns>
        ThemeDescriptor GetThemeDescriptorFromText(string text);

        /// <summary>
        /// 获取所有主题
        /// </summary>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含主题描述符的列表
        /// </returns>
        Task<IList<ThemeDescriptor>> GetThemesAsync();

        /// <summary>
        /// Get a theme by the system name
        /// </summary>
        /// <param name="systemName">Theme system name</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the me descriptor
        /// </returns>
        Task<ThemeDescriptor> GetThemeBySystemNameAsync(string systemName);

        /// <summary>
        /// Check whether the theme with specified system name exists
        /// </summary>
        /// <param name="systemName">Theme system name</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the rue if the theme exists; otherwise false
        /// </returns>
        Task<bool> ThemeExistsAsync(string systemName);
    }
}
