using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMarketingActivityOrdervoucherSendResponse.
    /// </summary>
    public class AlipayMarketingActivityOrdervoucherSendResponse : AlipayResponse
    {
        /// <summary>
        /// 本次发放的券码
        /// </summary>
        [JsonPropertyName("voucher_code")]
        public string VoucherCode { get; set; }
    }
}
