using System.Collections.Generic;

namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.SDK.RoleApi
{
    /// <summary>
    /// 字节小程序泛知识角色系统 API 响应的基类。
    /// </summary>
    public abstract class ByteDanceMicroAppRoleApiResponse: CommonResponseBase, ICommonResponse {
        /// <summary>
        /// 获取字节小程序 API 返回的错误码。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("err_no")]
        [System.Text.Json.Serialization.JsonPropertyName("err_no")]
        public virtual int ErrorNumber { get; set; }

        /// <summary>
        /// 获取字节小程序 API 返回的错误描述。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("err_tips")]
        [System.Text.Json.Serialization.JsonPropertyName("err_tips")]
        public virtual string? ErrorTips { get; set; }

        /// <summary>
        /// 获取字节小程序 API 返回的错误信息。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("err")]
        [System.Text.Json.Serialization.JsonPropertyName("err")]
        public virtual ByteDanceMicroAppRoleApiResponseError? Error { get; set; }

        /// <summary>
        /// 获取一个值，该值指示调用字节小程序 API 是否成功（即 HTTP 状态码为 200、且 `err_no` 值为 0）。
        /// </summary>
        /// <returns></returns>
        public override bool IsSuccessful()
        {
            return GetRawStatus() == 200 && ErrorNumber == 0 && (Error == null || Error.ErrorCode == 0);
        }
    }

    public class ByteDanceMicroAppRoleApiResponseError
    {
        /// <summary>
        /// 获取字节小程序 API 返回的错误码。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("err_code")]
        [System.Text.Json.Serialization.JsonPropertyName("err_code")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// 获取字节小程序 API 返回的错误描述。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("err_msg")]
        [System.Text.Json.Serialization.JsonPropertyName("err_msg")]
        public string? ErrorMessage { get; set; }
    }
}
