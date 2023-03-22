using DH.Services.Plugins;

namespace DH.Services.Cms
{
    /// <summary>
    /// 提供用于创建小部件的接口
    /// </summary>
    public partial interface IWidgetPlugin : IPlugin
    {
        /// <summary>
        /// 获取一个值，该值指示是否在管理区域中的小部件列表页上隐藏此插件
        /// </summary>
        bool HideInWidgetList { get; }

        /// <summary>
        /// 获取应呈现此窗口小部件的小部件区域
        /// </summary>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含小组件区域
        /// </returns>
        Task<IList<string>> GetWidgetZonesAsync();

        /// <summary>
        /// 获取用于显示小部件的视图组件的类型
        /// </summary>
        /// <param name="widgetZone">小组件区域的名称</param>
        /// <returns>查看组件类型</returns>
        Type GetWidgetViewComponent(string widgetZone);
    }
}
