using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiCateringPosPracticeDeleteModel Data Structure.
    /// </summary>
    public class KoubeiCateringPosPracticeDeleteModel : AlipayObject
    {
        /// <summary>
        /// 做法id
        /// </summary>
        [JsonPropertyName("practice_id")]
        public string PracticeId { get; set; }

        /// <summary>
        /// 门店id
        /// </summary>
        [JsonPropertyName("shop_id")]
        public string ShopId { get; set; }
    }
}
