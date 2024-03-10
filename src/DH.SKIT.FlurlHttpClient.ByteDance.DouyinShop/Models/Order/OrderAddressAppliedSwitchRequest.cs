namespace SKIT.FlurlHttpClient.ByteDance.DouyinShop.Models
{
    /// <summary>
    /// <para>表示 [POST] /order/AddressAppliedSwitch 接口的请求。</para>
    /// </summary>
    public class OrderAddressAppliedSwitchRequest : DouyinShopRequest
    {
        /// <summary>
        /// 获取或设置订单 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("is_allowed")]
        [System.Text.Json.Serialization.JsonPropertyName("is_allowed")]
        public bool IsAllowed { get; set; }
    }
}
