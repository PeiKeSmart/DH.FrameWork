using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenMiniAmpeInvokeappBatchqueryResponse.
    /// </summary>
    public class AlipayOpenMiniAmpeInvokeappBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 调用应用信息列表
        /// </summary>
        [JsonPropertyName("invoke_app_list")]
        public List<InvokeAppInfo> InvokeAppList { get; set; }
    }
}
