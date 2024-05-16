﻿namespace SKIT.FlurlHttpClient.ByteDance.DouyinShop.Models
{
    /// <summary>
    /// <para>表示 [POST] /product/setOnline 接口的请求。</para>
    /// </summary>
    public class ProductSetOnlineRequest : DouyinShopRequest
    {
        /// <summary>
        /// 获取或设置商品 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("product_id")]
        [System.Text.Json.Serialization.JsonPropertyName("product_id")]
        [System.Text.Json.Serialization.JsonNumberHandling(System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString | System.Text.Json.Serialization.JsonNumberHandling.WriteAsString)]
        public long ProductId { get; set; }
    }
}
