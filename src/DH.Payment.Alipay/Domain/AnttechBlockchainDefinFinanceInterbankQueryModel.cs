﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AnttechBlockchainDefinFinanceInterbankQueryModel Data Structure.
    /// </summary>
    public class AnttechBlockchainDefinFinanceInterbankQueryModel : AlipayObject
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