﻿using SKIT.FlurlHttpClient;

namespace DH.FlurlHttpClient.KeRuYun.Api
{
    /// <summary>
    /// 表示客如云 API 请求的基类。
    /// </summary>
    public abstract class KeRuYunApiRequest : ICommonRequest
    {
        /// <summary>
        /// 获取或设置请求超时时间（单位：毫秒）。如果不指定将使用构造 <see cref="KeRuYunApiClient"/> 时的 <see cref="KeRuYunApiClientOptions.Timeout"/> 参数，这在需要指定特定耗时请求（比如上传或下载文件）的超时时间时很有用。
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual int? Timeout { get; set; }

        /// <summary>
        /// 获取或设置门店id。
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Int64 ShopIdenty { get; set; }
    }
}
