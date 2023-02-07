using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AntMerchantExpandScodeEledeSignResponse.
    /// </summary>
    public class AntMerchantExpandScodeEledeSignResponse : AlipayResponse
    {
        /// <summary>
        /// 打标接口返回结果
        /// </summary>
        [JsonPropertyName("add_tag_response")]
        public AddTagResponse AddTagResponse { get; set; }
    }
}
