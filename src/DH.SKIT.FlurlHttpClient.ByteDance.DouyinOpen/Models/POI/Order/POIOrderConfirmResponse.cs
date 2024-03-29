﻿namespace SKIT.FlurlHttpClient.ByteDance.DouyinOpen.Models
{
    /// <summary>
    /// <para>表示 [POST] /poi/order/confirm 接口的响应。</para>
    /// </summary>
    public class POIOrderConfirmResponse : DouyinOpenResponse<POIOrderConfirmResponse.Types.Data>
    {
        public static class Types
        {
            public class Data : DouyinOpenResponseData
            {
                /// <summary>
                /// 获取或设置订单外部 ID。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("order_ext_id")]
                [System.Text.Json.Serialization.JsonPropertyName("order_ext_id")]
                public string OrderExternalId { get; set; } = default!;

                /// <summary>
                /// 获取或设置券码 ID 列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("code_list")]
                [System.Text.Json.Serialization.JsonPropertyName("code_list")]
                public string[] CodeList { get; set; } = default!;
            }
        }
    }
}
