using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayIserviceCcmFunctionQueryResponse.
    /// </summary>
    public class AlipayIserviceCcmFunctionQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 功能点列表
        /// </summary>
        [JsonPropertyName("functions")]
        public List<Function> Functions { get; set; }
    }
}
