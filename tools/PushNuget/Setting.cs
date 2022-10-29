using NewLife.Xml;

using System.ComponentModel;

namespace PushNuget
{
    /// <summary>
    /// Nuget上传设置
    /// </summary>
    [DisplayName("Nuget上传设置")]
    [XmlConfigFile(@"Config\Nuget.config", 15_000)]
    public class Setting : XmlConfig<Setting>
    {
        /// <summary>
        /// Nuget密钥
        /// </summary>
        [Description("Nuget密钥")]
        public string Key { get; set; } = "";

        /// <summary>
        /// Nuget地址。可用自己搭建的Nuget平台
        /// </summary>
        [Description("Nuget地址")]
        public string Source { get; set; } = "https://api.nuget.org/v3/index.json";
    }
}
