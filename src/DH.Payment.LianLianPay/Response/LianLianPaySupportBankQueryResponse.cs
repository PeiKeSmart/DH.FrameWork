using System.Collections.Generic;
using DH.Payment.LianLianPay.Domain;
using Newtonsoft.Json;

namespace DH.Payment.LianLianPay.Response
{
    /// <summary>
    /// 支持银行查询
    /// </summary>
    public class LianLianPaySupportBankQueryResponse : LianLianPayResponse
    {
        /// <summary>
        /// 请求结果代码 
        /// </summary>
        [JsonProperty("ret_code")]
        public string RetCode { get; set; }

        /// <summary>
        /// 请求结果描述
        /// </summary>
        [JsonProperty("ret_msg")]
        public string RetMsg { get; set; }

        /// <summary>
        /// 支持银行列表
        /// </summary>
        [JsonProperty("support_banklist", ItemIsReference = true)]
        public List<Bank> SupportBanklist { get; set; }
    }
}
