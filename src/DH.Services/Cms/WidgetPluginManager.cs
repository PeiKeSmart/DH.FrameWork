﻿using DH.Domain.Cms;
using DH.Services.Customers;
using DH.Services.Plugins;

using XCode.Membership;

namespace DH.Services.Cms;

/// <summary>
/// 表示小部件插件管理器实现
/// </summary>
public partial class WidgetPluginManager : PluginManager<IWidgetPlugin>, IWidgetPluginManager {
    #region Fields

    protected readonly WidgetSettings _widgetSettings;

    #endregion

    #region Ctor

    public WidgetPluginManager(ICustomerService customerService,
        IPluginService pluginService,
        WidgetSettings widgetSettings) : base(customerService, pluginService)
    {
        _widgetSettings = widgetSettings;
    }

    #endregion

    #region Methods

    /// <summary>
    /// 加载活动小部件
    /// </summary>
    /// <param name="customer">按客户筛选;传递 null 以加载所有插件</param>
    /// <param name="storeId">按商店过滤;传递 0 以加载所有插件</param>
    /// <param name="widgetZone">小部件区域;传递 null 以加载所有插件</param>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含活动小组件的列表
    /// </returns>
    public virtual async Task<IList<IWidgetPlugin>> LoadActivePluginsAsync(User customer = null, int storeId = 0, string widgetZone = null)
    {
        var widgets = await LoadActivePluginsAsync(_widgetSettings.ActiveWidgetSystemNames, customer, storeId);

        // 按小部件区域筛选
        if (!string.IsNullOrEmpty(widgetZone))
            widgets = await widgets.WhereAwait(async widget =>
                (await widget.GetWidgetZonesAsync()).Contains(widgetZone, StringComparer.InvariantCultureIgnoreCase)).ToListAsync();

        return widgets;
    }

    /// <summary>
    /// 检查传递的小部件是否处于活动状态
    /// </summary>
    /// <param name="widget">要检查的小部件</param>
    /// <returns>结果</returns>
    public virtual bool IsPluginActive(IWidgetPlugin widget)
    {
        return IsPluginActive(widget, _widgetSettings.ActiveWidgetSystemNames);
    }

    /// <summary>
    /// 检查传递的系统名称的小部件是否处于活动状态
    /// </summary>
    /// <param name="systemName">要检查的小部件的系统名称</param>
    /// <param name="customer">按客户筛选;传递 null 以加载所有插件</param>
    /// <param name="storeId">按商店过滤;传递 0 以加载所有插件</param>
    /// <returns>
    /// 表示异步操作的任务
    /// 任务结果包含结果
    /// </returns>
    public virtual async Task<bool> IsPluginActiveAsync(string systemName, User customer = null, int storeId = 0)
    {
        var widget = await LoadPluginBySystemNameAsync(systemName, customer, storeId);

        return IsPluginActive(widget);
    }

    #endregion
}