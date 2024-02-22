using SKIT.FlurlHttpClient;

namespace DH.FlurlHttpClient.KeRuYun.Api {
    /// <summary>
    /// 客如云开放 API 响应的基类。
    /// </summary>
    public abstract class KeRuYunApiResponse : CommonResponseBase, ICommonResponse {

        /// <summary>
        /// 获取客如云 API 返回的业务处理错误代码。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("code")]
        [System.Text.Json.Serialization.JsonPropertyName("code")]
        public virtual Int32 Code { get; set; }

        /// <summary>
        /// 获取一个值，该值指示调用银豹 API 是否成功（即 HTTP 状态码为 200、且 status 值为 success）。
        /// </summary>
        /// <returns></returns>
        public override bool IsSuccessful()
        {
            return GetRawStatus() == 200 && Code == 0;
        }
    }
}
