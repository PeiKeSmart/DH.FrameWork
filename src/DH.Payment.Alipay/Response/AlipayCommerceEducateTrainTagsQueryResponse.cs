using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceEducateTrainTagsQueryResponse.
    /// </summary>
    public class AlipayCommerceEducateTrainTagsQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 标签信息列表
        /// </summary>
        [JsonPropertyName("tag_infos")]
        public List<TagInfoVO> TagInfos { get; set; }
    }
}
