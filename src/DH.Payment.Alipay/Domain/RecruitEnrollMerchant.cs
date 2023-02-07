using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// RecruitEnrollMerchant Data Structure.
    /// </summary>
    public class RecruitEnrollMerchant : AlipayObject
    {
        /// <summary>
        /// 商户唯一ID(商户pid或商户smid)
        /// </summary>
        [JsonPropertyName("merchant_uid")]
        public string MerchantUid { get; set; }
    }
}
