using DH.Services.Plugins;

namespace DH.Services.Cms
{
    /// <summary>
    /// 提供用于创建小部件的接口
    /// </summary>
    public partial interface IWidgetPlugin : IPlugin
    {
        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        bool HideInWidgetList { get; }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the widget zones
        /// </returns>
        Task<IList<string>> GetWidgetZonesAsync();

        /// <summary>
        /// Gets a type of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component type</returns>
        Type GetWidgetViewComponent(string widgetZone);
    }
}
