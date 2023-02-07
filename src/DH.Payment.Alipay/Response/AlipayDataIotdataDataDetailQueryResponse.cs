using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataIotdataDataDetailQueryResponse.
    /// </summary>
    public class AlipayDataIotdataDataDetailQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 感知数据列表
        /// </summary>
        [JsonPropertyName("data")]
        public List<SummaryData> Data { get; set; }
    }
}
