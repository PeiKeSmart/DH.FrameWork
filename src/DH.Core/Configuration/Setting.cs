using NewLife.Xml;
using System.ComponentModel;

namespace DH.Core.Configuration
{
    /// <summary>基类设置</summary>
    [DisplayName("基类设置")]
    [XmlConfigFile("Config/DHSetting.config", 10000)]
    public class DHSetting : XmlConfig<DHSetting>
    {
        /// <summary>是否启用调试。默认true</summary>
        [Description("调试")]
        [Category("通用")]
        public Boolean Debug { get; set; } = true;

        /// <summary>系统初始化控制参数</summary>
        [Description("系统初始化控制参数,系统是否安装,true：已安装，false：未安装")]
        public Boolean IsInstalled { get; set; } = false;

    }
}
