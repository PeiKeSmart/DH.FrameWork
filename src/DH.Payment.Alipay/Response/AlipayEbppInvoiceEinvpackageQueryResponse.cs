using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEbppInvoiceEinvpackageQueryResponse.
    /// </summary>
    public class AlipayEbppInvoiceEinvpackageQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 发票包明细数据
        /// </summary>
        [JsonPropertyName("package_item_info_list")]
        public List<PackageItemOpenInfo> PackageItemInfoList { get; set; }
    }
}
