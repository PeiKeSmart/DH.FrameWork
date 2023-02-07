using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayUserAntpaasUseridGetModel Data Structure.
    /// </summary>
    public class AlipayUserAntpaasUseridGetModel : AlipayObject
    {
        /// <summary>
        /// 账户登录号，邮箱或者手机号
        /// </summary>
        [JsonPropertyName("logon_id")]
        public string LogonId { get; set; }
    }
}
