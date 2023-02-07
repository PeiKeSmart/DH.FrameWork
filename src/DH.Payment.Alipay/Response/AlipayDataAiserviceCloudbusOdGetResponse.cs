using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataAiserviceCloudbusOdGetResponse.
    /// </summary>
    public class AlipayDataAiserviceCloudbusOdGetResponse : AlipayResponse
    {
        /// <summary>
        /// od结果
        /// </summary>
        [JsonPropertyName("result")]
        public List<CloudBusOdItem> Result { get; set; }
    }
}
