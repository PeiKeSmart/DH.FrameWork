using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AnttechDataServiceBlockchainContractQueryResponse.
    /// </summary>
    public class AnttechDataServiceBlockchainContractQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 合约列表
        /// </summary>
        [JsonPropertyName("contract_list")]
        public List<BlockChainContractApiDO> ContractList { get; set; }
    }
}
