using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayBossProdMyTestQueryModel Data Structure.
    /// </summary>
    public class AlipayBossProdMyTestQueryModel : AlipayObject
    {
        /// <summary>
        /// 区
        /// </summary>
        [JsonPropertyName("area_code")]
        public string AreaCode { get; set; }
    }
}
