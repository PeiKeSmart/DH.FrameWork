namespace SKIT.FlurlHttpClient.ByteDance.TikTokGlobalShop.Models
{
    /// <summary>
    /// <para>表示 [POST] /fulfillment/{version}/orders/{order_id}/shipping_info/update 接口的请求。</para>
    /// </summary>
    public class FulfillmentUpdateOrderShippingInfoRequest : TikTokShopRequest
    {
        /// <summary>
        /// 获取或设置订单 ID。
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string OrderId { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置物流单号。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("tracking_number")]
        [System.Text.Json.Serialization.JsonPropertyName("tracking_number")]
        public string TrackingNumber { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置运输服务商 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("shipping_provider_id")]
        [System.Text.Json.Serialization.JsonPropertyName("shipping_provider_id")]
        public string ShippingProviderId { get; set; } = string.Empty;
    }
}
