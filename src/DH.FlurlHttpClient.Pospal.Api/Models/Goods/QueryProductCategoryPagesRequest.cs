namespace DG.FlurlHttpClient.Pospal.Api.Models
{
    /// <summary>
    /// <para>表示 [POST] /pospal-api2/openapi/v1/productOpenApi/queryProductCategoryPages 接口的请求。</para>
    /// </summary>
    public class QueryProductCategoryPagesRequest : PospalApiRequest
    {
        public static class Types
        {
            public class Parameter
            {
                /// <summary>
                /// 获取或设置 分页参数类型。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("parameterType")]
                [System.Text.Json.Serialization.JsonPropertyName("parameterType")]
                public String? ParameterType { get; set; }

                /// <summary>
                /// 获取或设置 分页参数值。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("parameterValue")]
                [System.Text.Json.Serialization.JsonPropertyName("parameterValue")]
                public String? ParameterValue { get; set; }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty("appId")]
        [System.Text.Json.Serialization.JsonPropertyName("appId")]
        public override string? AppId { get; set; }

        /// <summary>
        /// 获取或设置参数信息。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("postBackParameter")]
        [System.Text.Json.Serialization.JsonPropertyName("postBackParameter")]
        public Types.Parameter? PostBackParameter { get; set; }
    }
}
