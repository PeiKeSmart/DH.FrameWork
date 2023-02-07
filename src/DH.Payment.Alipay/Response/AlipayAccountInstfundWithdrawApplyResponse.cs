using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayAccountInstfundWithdrawApplyResponse.
    /// </summary>
    public class AlipayAccountInstfundWithdrawApplyResponse : AlipayResponse
    {
        /// <summary>
        /// 资金指令唯一标识
        /// </summary>
        [JsonPropertyName("instruction_no")]
        public string InstructionNo { get; set; }
    }
}
