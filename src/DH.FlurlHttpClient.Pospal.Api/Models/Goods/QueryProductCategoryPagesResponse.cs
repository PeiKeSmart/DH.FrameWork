using static DG.FlurlHttpClient.Pospal.Api.Models.QueryProductCategoryPagesResponse.Types.Data.Types;

namespace DG.FlurlHttpClient.Pospal.Api.Models
{
    /// <summary>
    /// <para>表示 [POST] /pospal-api2/openapi/v1/productOpenApi/queryProductCategoryPages 接口的响应。</para>
    /// </summary>
    public class QueryProductCategoryPagesResponse : PospalApiResponse
    {
        public static class Types
        {
            public class Data
            {
                public static class Types
                {
                    public class PostBackParameter
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

                    public class Result
                    {
                        /// <summary>
                        /// 获取或设置 当前商品分类唯一标识。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("uid")]
                        [System.Text.Json.Serialization.JsonPropertyName("uid")]
                        public Int64? UId { get; set; }

                        /// <summary>
                        /// 获取或设置 当前商品分类的父分类的唯一标识。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("parentUid")]
                        [System.Text.Json.Serialization.JsonPropertyName("parentUid")]
                        public Int64? ParentUid { get; set; }

                        /// <summary>
                        /// 获取或设置 商品分类名称。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("name")]
                        [System.Text.Json.Serialization.JsonPropertyName("name")]
                        public String? Name { get; set; }
                    }
                }

                /// <summary>
                /// <inheritdoc/>
                /// </summary>
                [Newtonsoft.Json.JsonProperty("result")]
                [System.Text.Json.Serialization.JsonPropertyName("result")]
                public Types.Result[]? Result { get; set; }

                /// <summary>
                /// <inheritdoc/>
                /// </summary>
                [Newtonsoft.Json.JsonProperty("postBackParameter")]
                [System.Text.Json.Serialization.JsonPropertyName("postBackParameter")]
                public Types.PostBackParameter? PostBackParameter { get; set; }

                /// <summary>
                /// <inheritdoc/>
                /// </summary>
                [Newtonsoft.Json.JsonProperty("pageSize")]
                [System.Text.Json.Serialization.JsonPropertyName("pageSize")]
                public Int32 PageSize { get; set; }
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty("data")]
        [System.Text.Json.Serialization.JsonPropertyName("data")]
        public Types.Data? Data { get; set; }

    }
}
