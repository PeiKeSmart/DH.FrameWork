using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringDishAreaSyncResponse.
    /// </summary>
    public class KoubeiCateringDishAreaSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 餐区餐台模型
        /// </summary>
        [JsonPropertyName("kb_dish_area")]
        public KbdishAreaInfo KbDishArea { get; set; }
    }
}
