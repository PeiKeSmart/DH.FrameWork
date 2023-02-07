﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenOperationBizfeeAftechCancelResponse.
    /// </summary>
    public class AlipayOpenOperationBizfeeAftechCancelResponse : AlipayResponse
    {
        /// <summary>
        /// 结果码
        /// </summary>
        [JsonPropertyName("result_code")]
        public string ResultCode { get; set; }

        /// <summary>
        /// 结果信息
        /// </summary>
        [JsonPropertyName("result_message")]
        public string ResultMessage { get; set; }
    }
}
