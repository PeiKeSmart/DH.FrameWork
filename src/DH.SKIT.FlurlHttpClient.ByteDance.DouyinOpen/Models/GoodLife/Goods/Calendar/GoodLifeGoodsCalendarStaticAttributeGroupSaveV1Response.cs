namespace SKIT.FlurlHttpClient.ByteDance.DouyinOpen.Models
{
    /// <summary>
    /// <para>表示 [POST] /goodlife/v1/goods/calendar_static_attr_group/save 接口的响应。</para>
    /// </summary>
    public class GoodLifeGoodsCalendarStaticAttributeGroupSaveV1Response : DouyinOpenResponse<GoodLifeGoodsCalendarStaticAttributeGroupSaveV1Response.Types.Data>
    {
        public static class Types
        {
            public class BaseResponse
            {
                /// <summary>
                /// 获取或设置业务状态码。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("StatusCode")]
                [System.Text.Json.Serialization.JsonPropertyName("StatusCode")]
                [System.Text.Json.Serialization.JsonNumberHandling(System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString)]
                public int StatusCode { get; set; }

                /// <summary>
                /// 获取或设置业务状态描述。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("StatusMessage")]
                [System.Text.Json.Serialization.JsonPropertyName("StatusMessage")]
                public string? StatusMessage { get; set; }
            }

            public class Data : DouyinOpenResposneData
            {
            }
        }

        /// <summary>
        /// 获取或设置基础响应。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("BaseResp")]
        [System.Text.Json.Serialization.JsonPropertyName("BaseResp")]
        public Types.BaseResponse BaseResponse { get; set; } = default!;

        public override bool IsSuccessful()
        {
            return base.IsSuccessful() && BaseResponse?.StatusCode == 0;
        }
    }
}