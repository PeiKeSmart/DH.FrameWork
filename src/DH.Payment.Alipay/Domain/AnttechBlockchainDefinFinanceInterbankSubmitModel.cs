﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AnttechBlockchainDefinFinanceInterbankSubmitModel Data Structure.
    /// </summary>
    public class AnttechBlockchainDefinFinanceInterbankSubmitModel : AlipayObject
    {
        /// <summary>
        /// base64编码的业务参数
        /// </summary>
        [JsonPropertyName("encoded_biz_param")]
        public string EncodedBizParam { get; set; }

        /// <summary>
        /// 系统参数
        /// </summary>
        [JsonPropertyName("sys_param")]
        public SystemParam SysParam { get; set; }
    }
}
