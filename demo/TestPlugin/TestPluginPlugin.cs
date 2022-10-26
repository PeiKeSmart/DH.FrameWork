using DH.Services.Cms;
using DH.Services.Plugins;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TestPlugin.Components;

namespace TestPlugin
{
    /// <summary>
    /// 插件
    /// </summary>
    public class TestPluginPlugin : BasePlugin, IWidgetPlugin
    {
        /// <summary>
        /// 获取用于显示小部件的视图组件的名称
        /// </summary>
        /// <param name="widgetZone">小部件区域的名称</param>
        /// <returns>组件视图名称</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "WidgetsNivoSlider";
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string>
            {

            });
        }

        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(WidgetsNivoSliderViewComponent);
        }

        /// <summary>
        /// 获取一个值，该值指示是否在管理区域的小部件列表页面上隐藏此插件。
        /// </summary>
        public bool HideInWidgetList => false;
    }
}
