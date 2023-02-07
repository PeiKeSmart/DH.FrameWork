using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceKidsAccountCreateResponse.
    /// </summary>
    public class AlipayCommerceKidsAccountCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 儿童唯一id
        /// </summary>
        [JsonPropertyName("child_id")]
        public string ChildId { get; set; }
    }
}
