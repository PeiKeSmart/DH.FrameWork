using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppCommunityPropertyCreateResponse.
    /// </summary>
    public class AlipayEbppCommunityPropertyCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 外部联系人
        /// </summary>
        [JsonPropertyName("contacts")]
        public ExternalContact Contacts { get; set; }

        /// <summary>
        /// 物业公司名称拼音首字母大写+YYYYMMDD+防重位
        /// </summary>
        [JsonPropertyName("property_short_name")]
        public string PropertyShortName { get; set; }
    }
}
