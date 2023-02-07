using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// OuterMemberInfo Data Structure.
    /// </summary>
    public class OuterMemberInfo : AlipayObject
    {
        /// <summary>
        /// 商户记录的用户信息
        /// </summary>
        [JsonPropertyName("user_info")]
        public string UserInfo { get; set; }
    }
}
