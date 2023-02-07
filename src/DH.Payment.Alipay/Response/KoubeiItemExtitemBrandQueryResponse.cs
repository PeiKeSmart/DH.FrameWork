using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiItemExtitemBrandQueryResponse.
    /// </summary>
    public class KoubeiItemExtitemBrandQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 品牌列表信息
        /// </summary>
        [JsonPropertyName("brand_list")]
        public List<ExtBrand> BrandList { get; set; }
    }
}
