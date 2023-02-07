﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMsaasMediarecogMmtcaftscvTransactionInitializeResponse.
    /// </summary>
    public class AlipayMsaasMediarecogMmtcaftscvTransactionInitializeResponse : AlipayResponse
    {
        /// <summary>
        /// 下发给设备的视觉算法config
        /// </summary>
        [JsonPropertyName("algorithm_config")]
        public string AlgorithmConfig { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }

        /// <summary>
        /// 云平台请求唯一标示,保证用户请求的幂等性.
        /// </summary>
        [JsonPropertyName("transaction_id")]
        public string TransactionId { get; set; }
    }
}
