namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.Models
{
    /// <summary>
    /// <para>表示 [POST] /apps/v1/capacity/update_simple_qr_bind 接口的请求。</para>
    /// </summary>
    public class AppsCapacityUpdateSimpleQrcodeBindV1Request : ByteDanceMicroAppRequest
    {
        /// <summary>
        /// 获取或设置更新前的链接地址。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("before_qr_url")]
        [System.Text.Json.Serialization.JsonPropertyName("before_qr_url")]
        public string OldUrl { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置链接地址。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("qr_url")]
        [System.Text.Json.Serialization.JsonPropertyName("qr_url")]
        public string NewUrl { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置小程序页面路径。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("load_path")]
        [System.Text.Json.Serialization.JsonPropertyName("load_path")]
        public string PagePath { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置测试范围。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("stage")]
        [System.Text.Json.Serialization.JsonPropertyName("stage")]
        public string Stage { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置是否独占该链接作为前缀。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("exclusive_qr_url_prefix")]
        [System.Text.Json.Serialization.JsonPropertyName("exclusive_qr_url_prefix")]
        public bool IsPrefixExclusive { get; set; }
    }
}
