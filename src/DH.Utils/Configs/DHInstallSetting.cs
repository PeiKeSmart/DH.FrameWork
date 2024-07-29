using NewLife.Configuration;

using System.ComponentModel;

namespace DH.Configs;

/// <summary>安装设置</summary>
[DisplayName("安装设置")]
[Config("DHInstallSetting")]
public class DHInstallSetting : Config<DHInstallSetting> {
    /// <summary>系统初始化控制参数</summary>
    [Description("系统初始化控制参数,系统是否为运行端第一次运行,true：是，false：否")]
    public Boolean LocalFirst { get; set; } = true;
}
