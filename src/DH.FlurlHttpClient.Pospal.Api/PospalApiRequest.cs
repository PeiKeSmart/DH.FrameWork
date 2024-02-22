using SKIT.FlurlHttpClient;

namespace DG.FlurlHttpClient.Pospal.Api;

/// <summary>
/// 表示银豹 API 请求的基类。
/// </summary>
public abstract class PospalApiRequest : CommonRequestBase, ICommonRequest
{
    /// <summary>
    /// 获取或设置访问凭证。
    /// </summary>
    //[Newtonsoft.Json.JsonIgnore]
    //[System.Text.Json.Serialization.JsonIgnore]
    public virtual String? AppId { get; set; }
}
