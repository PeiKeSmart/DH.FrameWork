using Newtonsoft.Json;

namespace DH.Web.Framework.Security
{
    /// <summary>
    /// 谷歌reCAPTCHA响应
    /// </summary>
    public partial class CaptchaResponse
    {
        #region Ctor

        public CaptchaResponse()
        {
            Errors = new List<string>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取或设置此请求的操作名称（重要的是要验证）
        /// </summary>
        [JsonProperty(PropertyName = "action")]

        public string Action { get; set; }

        [JsonProperty(PropertyName = "score")]
        public decimal Score { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示验证是否成功
        /// </summary>
        [JsonProperty(PropertyName = "success")]
        public bool IsValid { get; set; }

        /// <summary>
        /// 获取或设置质询加载的日期和时间
        /// </summary>
        [JsonProperty(PropertyName = "challenge_ts")]
        public DateTime? ChallengeDateTime { get; set; }

        /// <summary>
        /// 获取或设置解决reCAPTCHA的站点的主机名
        /// </summary>
        [JsonProperty(PropertyName = "hostname")]
        public string Hostname { get; set; }

        /// <summary>
        /// 获取或设置错误
        /// </summary>
        [JsonProperty(PropertyName = "error-codes")]
        public List<string> Errors { get; set; }

        #endregion
    }
}
