﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayPcreditLoanContractInfoQueryModel Data Structure.
    /// </summary>
    public class AlipayPcreditLoanContractInfoQueryModel : AlipayObject
    {
        /// <summary>
        /// 贷款申请受理单号，与外部业务单号二者不能同时为空
        /// </summary>
        [JsonPropertyName("apply_receipt_no")]
        public string ApplyReceiptNo { get; set; }

        /// <summary>
        /// 签署合同类型，取值{LOAN}：LOAN-贷款合同;
        /// </summary>
        [JsonPropertyName("contract_type")]
        public string ContractType { get; set; }

        /// <summary>
        /// 外部业务单号，与贷款申请受理单号二者不能同时为空
        /// </summary>
        [JsonPropertyName("out_biz_no")]
        public string OutBizNo { get; set; }

        /// <summary>
        /// 代表了一次请求，作为业务幂等性控制
        /// </summary>
        [JsonPropertyName("out_request_no")]
        public string OutRequestNo { get; set; }
    }
}
