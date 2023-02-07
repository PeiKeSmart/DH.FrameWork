using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataDataserviceAdPromotepagestatisticQueryResponse.
    /// </summary>
    public class AlipayDataDataserviceAdPromotepagestatisticQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 留资统计列表
        /// </summary>
        [JsonPropertyName("collectinfo_list")]
        public List<PromotePageStatistic> CollectinfoList { get; set; }
    }
}
