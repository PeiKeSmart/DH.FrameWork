using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AntfortuneContentCommunityTopicListQueryResponse.
    /// </summary>
    public class AntfortuneContentCommunityTopicListQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 话题VO列表
        /// </summary>
        [JsonPropertyName("topic_list")]
        public List<TopicItemVo> TopicList { get; set; }
    }
}
