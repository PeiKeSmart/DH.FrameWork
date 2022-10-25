using DH.Services.Plugins;

using Newtonsoft.Json;

namespace DH.Services.Themes
{
    /// <summary>
    /// 表示主题描述符
    /// </summary>
    public partial class ThemeDescriptor : IDescriptor
    {
        /// <summary>
        /// 获取或设置主题系统名称
        /// </summary>
        [JsonProperty(PropertyName = "SystemName")]
        public string SystemName { get; set; }

        /// <summary>
        /// 获取或设置主题友好名称
        /// </summary>
        [JsonProperty(PropertyName = "FriendlyName")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// 获取或设置主题友好名称
        /// </summary>
        [JsonProperty(PropertyName = "SupportRTL")]
        public bool SupportRtl { get; set; }

        /// <summary>
        /// 获取或设置主题预览图像的路径
        /// </summary>
        [JsonProperty(PropertyName = "PreviewImageUrl")]
        public string PreviewImageUrl { get; set; }

        /// <summary>
        /// 获取或设置主题的预览文本
        /// </summary>
        [JsonProperty(PropertyName = "PreviewText")]
        public string PreviewText { get; set; }
    }
}
