using NewLife.Xml;
using System;
using System.ComponentModel;

namespace DH.WebHook
{
    /// <summary>WebHooks设置</summary>
    [DisplayName("WebHooks设置")]
    [XmlConfigFile("Config/WebHookSetting.config", 10000)]
    public class WebHookSetting : XmlConfig<WebHookSetting>
    {
        /// <summary>钉钉Host</summary>
        [Description("钉钉Host")]
        public String DingTalkHost { get; set; }

        /// <summary>钉钉机器人接口地址</summary>
        [Description("钉钉机器人接口地址")]
        public String DingTalkSendUrl { get; set; }

        /// <summary>企业微信机器人密钥</summary>
        [Description("企业微信机器人密钥")]
        public String WeChatWorkAppId { get; set; }
    }
}
