using DH.Core.Configuration;

namespace DH.Domain.Cms;

/// <summary>
/// 小部件设置
/// </summary>
public partial class WidgetSettings : ISettings {
    public WidgetSettings()
    {
        ActiveWidgetSystemNames = new List<string>();
    }

    /// <summary>
    /// 获取或设置活动小部件的系统名称
    /// </summary>
    public List<string> ActiveWidgetSystemNames { get; set; }
}