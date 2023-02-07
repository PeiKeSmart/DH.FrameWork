using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringPosPracticeQueryResponse.
    /// </summary>
    public class KoubeiCateringPosPracticeQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 做法模型
        /// </summary>
        [JsonPropertyName("practice_entity_list")]
        public List<PracticeEntity> PracticeEntityList { get; set; }
    }
}
