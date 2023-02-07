using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingDataDishdiagnoseBatchqueryResponse.
    /// </summary>
    public class KoubeiMarketingDataDishdiagnoseBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 查询返回的详情数据
        /// </summary>
        [JsonPropertyName("item_diagnose_list")]
        public List<ItemDiagnoseDetail> ItemDiagnoseList { get; set; }

        /// <summary>
        /// 记录的总条数
        /// </summary>
        [JsonPropertyName("total")]
        public long Total { get; set; }
    }
}
