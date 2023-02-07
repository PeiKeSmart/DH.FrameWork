using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEbppProdmodeUnionbankQueryModel Data Structure.
    /// </summary>
    public class AlipayEbppProdmodeUnionbankQueryModel : AlipayObject
    {
        /// <summary>
        /// 银联编号
        /// </summary>
        [JsonPropertyName("bank_code")]
        public string BankCode { get; set; }
    }
}
