namespace DH.FlurlHttpClient.KeRuYun.Api.Models;

/// <summary>
/// <para>表示 [GET] /open/v1/token/get 接口的响应。</para>
/// </summary>
public class TokenGetResponse: KeRuYunApiResponse
{
    public static class Types
    {
        public class Result
        {
            /// <summary>
            /// 获取或设置 token。
            /// </summary>
            [Newtonsoft.Json.JsonProperty("token")]
            [System.Text.Json.Serialization.JsonPropertyName("token")]
            public String? Token { get; set; }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Newtonsoft.Json.JsonProperty("message")]
    [System.Text.Json.Serialization.JsonPropertyName("message")]
    public String? Message { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Newtonsoft.Json.JsonProperty("apiMessage")]
    [System.Text.Json.Serialization.JsonPropertyName("apiMessage")]
    public String? ApiMessage { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Newtonsoft.Json.JsonProperty("messageUuid")]
    [System.Text.Json.Serialization.JsonPropertyName("messageUuid")]
    public String? MessageUuid { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Newtonsoft.Json.JsonProperty("result")]
    [System.Text.Json.Serialization.JsonPropertyName("result")]
    public Types.Result? Result { get; set; }
}
