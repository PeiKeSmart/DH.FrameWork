using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEbppCommunityPubkeyQueryModel Data Structure.
    /// </summary>
    public class AlipayEbppCommunityPubkeyQueryModel : AlipayObject
    {
        /// <summary>
        /// ISV短名(创建ISV时生成)
        /// </summary>
        [JsonPropertyName("isv_short_name")]
        public string IsvShortName { get; set; }
    }
}
