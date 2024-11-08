﻿namespace SKIT.FlurlHttpClient.ByteDance.DouyinShop.Models
{
    /// <summary>
    /// <para>表示 [POST] /spu/addShopSpu 接口的响应。</para>
    /// </summary>
    public class SPUAddShopSPUResponse : DouyinShopResponse<SPUAddShopSPUResponse.Types.Data>
    {
        public static class Types
        {
            public class Data
            {
                /// <summary>
                /// 获取或设置 SPU ID。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("spu_id")]
                [System.Text.Json.Serialization.JsonPropertyName("spu_id")]
                [System.Text.Json.Serialization.JsonNumberHandling(System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString | System.Text.Json.Serialization.JsonNumberHandling.WriteAsString)]
                public long SPUId { get; set; }
            }
        }
    }
}
