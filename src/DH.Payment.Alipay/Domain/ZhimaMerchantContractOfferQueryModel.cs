using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// ZhimaMerchantContractOfferQueryModel Data Structure.
    /// </summary>
    public class ZhimaMerchantContractOfferQueryModel : AlipayObject
    {
        /// <summary>
        /// 发约单单号
        /// </summary>
        [JsonPropertyName("offer_no")]
        public string OfferNo { get; set; }
    }
}
