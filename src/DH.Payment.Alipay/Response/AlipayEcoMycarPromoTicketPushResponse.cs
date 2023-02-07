using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcoMycarPromoTicketPushResponse.
    /// </summary>
    public class AlipayEcoMycarPromoTicketPushResponse : AlipayResponse
    {
        /// <summary>
        /// 处理结果返回码
        /// </summary>
        [JsonPropertyName("sp_apply_no")]
        public string SpApplyNo { get; set; }
    }
}
