using SKIT.FlurlHttpClient;

namespace DG.FlurlHttpClient.Pospal.Api;

/// <summary>
/// 银豹开放 API 响应的基类。
/// </summary>
public abstract class PospalApiResponse : CommonResponseBase, ICommonResponse
{
    /// <summary>
    /// 获取银豹 API 返回的状态值。
    /// </summary>
    [Newtonsoft.Json.JsonProperty("status")]
    [System.Text.Json.Serialization.JsonPropertyName("status")]
    public virtual String? Status { get; set; }

    /// <summary>
    /// 获取银豹 API 返回的业务处理错误代码。
    /// </summary>
    [Newtonsoft.Json.JsonProperty("errorCode")]
    [System.Text.Json.Serialization.JsonPropertyName("errorCode")]
    public virtual String? ErrorCode { get; set; }

    /// <summary>
    /// 获取一个值，该值指示调用银豹 API 是否成功（即 HTTP 状态码为 200、且 status 值为 success）。
    /// </summary>
    /// <returns></returns>
    public override bool IsSuccessful()
    {
        return GetRawStatus() == 200 && Status == "success";
    }
}
