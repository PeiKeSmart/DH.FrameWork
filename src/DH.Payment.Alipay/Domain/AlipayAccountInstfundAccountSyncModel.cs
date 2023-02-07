﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayAccountInstfundAccountSyncModel Data Structure.
    /// </summary>
    public class AlipayAccountInstfundAccountSyncModel : AlipayObject
    {
        /// <summary>
        /// 账号信息
        /// </summary>
        [JsonPropertyName("inst_account")]
        public InstAccountDTO InstAccount { get; set; }
    }
}
