using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataAiserviceCloudbusPredictresultQueryResponse.
    /// </summary>
    public class AlipayDataAiserviceCloudbusPredictresultQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 预测线路结果
        /// </summary>
        [JsonPropertyName("result")]
        public CloudbusPredictResult Result { get; set; }
    }
}
