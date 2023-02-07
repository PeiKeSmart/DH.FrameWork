using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataIotdataImageaestheticBaiQueryResponse.
    /// </summary>
    public class AlipayDataIotdataImageaestheticBaiQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 返回图片URL、打分结果和错误信息
        /// </summary>
        [JsonPropertyName("data")]
        public AestheticData Data { get; set; }
    }
}
