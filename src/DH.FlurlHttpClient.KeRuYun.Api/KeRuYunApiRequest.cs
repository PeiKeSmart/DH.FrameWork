using SKIT.FlurlHttpClient;

namespace DH.FlurlHttpClient.KeRuYun.Api
{
    /// <summary>
    /// 表示客如云 API 请求的基类。
    /// </summary>
    public abstract class KeRuYunApiRequest : CommonRequestBase, ICommonRequest {

        /// <summary>
        /// 获取或设置门店id。
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Int64 ShopIdenty { get; set; }
    }
}
