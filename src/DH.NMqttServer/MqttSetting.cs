using NewLife.Configuration;
using NewLife.Xml;

using System.ComponentModel;

namespace DH.NMqttServer;

[Config("MqttServer")]
public class MqttSetting : XmlConfig<MqttSetting>
{
    /// <summary>调试开关。默认 false</summary>
    [Description("调试开关。默认 false")]
    public Boolean Debug { get; set; }

    /// <summary>端口。默认 1883</summary>
    [Description("端口。默认 1883")]
    public Int32 Port { get; set; } = 1883;
}