namespace SKIT.FlurlHttpClient.Baidu.SmartApp.SDK.ThirdParty.Models
{
    /// <summary>
    /// <para>表示 [POST] /rest/2.0/smartapp/subchain/delete 接口的请求。</para>
    /// </summary>
    public class RestSubchainDeleteRequest : BaiduSmartAppThirdPartyRequest
    {
        /// <summary>
        /// 获取或设置子链 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("subchain_id")]
        [System.Text.Json.Serialization.JsonPropertyName("subchain_id")]
        public long SubchainId { get; set; }
    }
}
