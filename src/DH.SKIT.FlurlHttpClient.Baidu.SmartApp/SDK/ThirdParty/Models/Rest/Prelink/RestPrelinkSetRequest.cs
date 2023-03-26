using System.Collections.Generic;

namespace SKIT.FlurlHttpClient.Baidu.SmartApp.SDK.ThirdParty.Models
{
    /// <summary>
    /// <para>表示 [POST] /rest/2.0/smartapp/prelink/set 接口的请求。</para>
    /// </summary>
    public class RestPrelinkSetRequest : BaiduSmartAppThirdPartyRequest
    {
        /// <summary>
        /// 获取或设置 Prelink 链接列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("prelink_url")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.TextualStringListWithCommaSplitConverter))]
        [System.Text.Json.Serialization.JsonPropertyName("prelink_url")]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Converters.TextualStringListWithCommaSplitConverter))]
        public IList<string> PrelinkUrlList { get; set; } = new List<string>();
    }
}
