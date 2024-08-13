using System.ComponentModel;

using NewLife.Configuration;

using XCode.Configuration;

namespace DH.Domain.Cms;

/// <summary>小部件设置</summary>
[DisplayName("小部件设置")]
//[XmlConfigFile("Config/DHSetting.config", 10000)]
[Config("WidgetSettings")]
public class WidgetSettings : Config<WidgetSettings> {
    #region 静态
    /// <summary>指向数据库参数字典表</summary>
    static WidgetSettings() => Provider = new DbConfigProvider { UserId = 0, Category = "Widget" };
    #endregion

    /// <summary>
    /// 获取或设置活动小部件的系统名称
    /// </summary>
    public List<String> ActiveWidgetSystemNames { get; set; }
}