using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenInstantdeliveryMerchantshopCreateResponse.
    /// </summary>
    public class AlipayOpenInstantdeliveryMerchantshopCreateResponse : AlipayResponse
    {
        /// <summary>
        /// 门店创建返回的结果。
        /// </summary>
        [JsonPropertyName("logistics_shop_status")]
        public List<LogisticsShopStatusDTO> LogisticsShopStatus { get; set; }
    }
}
