using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// ZhimaCustomerContractDetailQueryResponse.
    /// </summary>
    public class ZhimaCustomerContractDetailQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 合约详情
        /// </summary>
        [JsonPropertyName("contract_detail")]
        public ApiContractDetail ContractDetail { get; set; }
    }
}
