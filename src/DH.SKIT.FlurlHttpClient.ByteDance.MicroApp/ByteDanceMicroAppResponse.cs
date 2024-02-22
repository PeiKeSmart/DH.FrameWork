using System.Collections.Generic;

namespace SKIT.FlurlHttpClient.ByteDance.MicroApp
{
    /// <summary>
    /// 字节小程序 API 响应的基类。
    /// </summary>
    public abstract class ByteDanceMicroAppResponse : CommonResponseBase, ICommonResponse
    {
        /// <summary>
        /// 获取字节小程序 API 返回的详细错误号。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("errcode")]
        [System.Text.Json.Serialization.JsonPropertyName("errcode")]
        public virtual long ErrorCode { get; set; }

        /// <summary>
        /// 获取字节小程序 API 返回的错误描述。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("errmsg")]
        [System.Text.Json.Serialization.JsonPropertyName("errmsg")]
        public virtual string? ErrorMessage { get; set; }

        /// <summary>
        /// 获取字节小程序 API 返回的日志 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("log_id")]
        [System.Text.Json.Serialization.JsonPropertyName("log_id")]
        public virtual string? LogId { get; set; }

        /// <summary>
        /// 获取一个值，该值指示调用字节小程序 API 是否成功（即 HTTP 状态码为 200、且 "errcode" 值为 0）。
        /// </summary>
        /// <returns></returns>
        public override bool IsSuccessful()
        {
            return GetRawStatus() == 200 && ErrorCode == 0;
        }
    }
}
