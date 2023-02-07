using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringDishGroupSyncResponse.
    /// </summary>
    public class KoubeiCateringDishGroupSyncResponse : AlipayResponse
    {
        /// <summary>
        /// 组模型
        /// </summary>
        [JsonPropertyName("kb_dish_group")]
        public KbdishGroupInfo KbDishGroup { get; set; }
    }
}
