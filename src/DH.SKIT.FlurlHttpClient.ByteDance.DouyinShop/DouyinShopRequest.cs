namespace SKIT.FlurlHttpClient.ByteDance.DouyinShop
{
    /// <summary>
    /// 抖店开放平台 API 请求的基类。
    /// </summary>
    public abstract class DouyinShopRequest : CommonRequestBase, ICommonRequest
    {
        /// <summary>
        /// 获取或设置抖店开放平台的 AccessToken。
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual string? AccessToken { get; set; }

        /// <summary>
        /// 获取或设置抖店开放平台的 API 协议版本。
        /// <para>默认值：2</para>
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual string? ApiVersion { get; set; } = "2";

        /// <summary>
        /// 获取或设置抖店开放平台的 API 方法名称。一般情况下不需要指定值，由系统自动生成。
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual string? ApiMethod { get; set; }
    }
}
