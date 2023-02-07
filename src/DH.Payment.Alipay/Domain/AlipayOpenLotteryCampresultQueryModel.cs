﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOpenLotteryCampresultQueryModel Data Structure.
    /// </summary>
    public class AlipayOpenLotteryCampresultQueryModel : AlipayObject
    {
        /// <summary>
        /// 环境
        /// </summary>
        [JsonPropertyName("env")]
        public string Env { get; set; }
    }
}
