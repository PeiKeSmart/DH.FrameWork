﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayInsSceneCommandReceiveModel Data Structure.
    /// </summary>
    public class AlipayInsSceneCommandReceiveModel : AlipayObject
    {
        /// <summary>
        /// 命令对象
        /// </summary>
        [JsonPropertyName("command")]
        public Command Command { get; set; }
    }
}
