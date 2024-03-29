﻿using SKIT.FlurlHttpClient;

namespace DG.FlurlHttpClient.Pospal.Api
{
    /// <summary>
    /// 表示银豹 API 请求的基类。
    /// </summary>
    public abstract class PospalApiRequest : ICommonRequest
    {
        /// <summary>
        /// 获取或设置请求超时时间（单位：毫秒）。如果不指定将使用构造 <see cref="PospalApiClient"/> 时的 <see cref="PospalApiClientOptions.Timeout"/> 参数，这在需要指定特定耗时请求（比如上传或下载文件）的超时时间时很有用。
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual int? Timeout { get; set; }

        /// <summary>
        /// 获取或设置访问凭证。
        /// </summary>
        //[Newtonsoft.Json.JsonIgnore]
        //[System.Text.Json.Serialization.JsonIgnore]
        public virtual String? AppId { get; set; }
    }
}
