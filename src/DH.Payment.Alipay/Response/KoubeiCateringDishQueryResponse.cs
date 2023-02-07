using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringDishQueryResponse.
    /// </summary>
    public class KoubeiCateringDishQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 口碑菜品模型列表
        /// </summary>
        [JsonPropertyName("kb_dish_info_list")]
        public List<KbdishInfo> KbDishInfoList { get; set; }
    }
}
