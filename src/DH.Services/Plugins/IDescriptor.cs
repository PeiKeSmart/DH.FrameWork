namespace DH.Services.Plugins
{
    /// <summary>
    /// 表示应用程序扩展（插件或主题）的描述符
    /// </summary>
    public partial interface IDescriptor
    {
        /// <summary>
        /// 获取或设置系统名称
        /// </summary>
        string SystemName { get; set; }

        /// <summary>
        /// 获取或设置友好名称
        /// </summary>
        string FriendlyName { get; set; }
    }
}
