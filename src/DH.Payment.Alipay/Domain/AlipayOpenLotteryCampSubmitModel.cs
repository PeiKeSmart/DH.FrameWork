﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenLotteryCampSubmitModel Data Structure.
    /// </summary>
    public class AlipayOpenLotteryCampSubmitModel : AlipayObject
    {
        /// <summary>
        /// 环境
        /// </summary>
        [JsonPropertyName("env")]
        public string Env { get; set; }
    }
}
