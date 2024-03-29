﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayUserMpointAuthbaseQueryResponse.
    /// </summary>
    public class AlipayUserMpointAuthbaseQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 支付宝用户的蚂蚁会员积分余额
        /// </summary>
        [JsonPropertyName("balance")]
        public long Balance { get; set; }

        /// <summary>
        /// 支付宝用户的蚂蚁会员等级
        /// </summary>
        [JsonPropertyName("grade")]
        public string Grade { get; set; }
    }
}
