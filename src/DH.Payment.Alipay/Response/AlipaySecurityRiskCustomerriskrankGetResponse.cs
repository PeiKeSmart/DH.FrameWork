﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipaySecurityRiskCustomerriskrankGetResponse.
    /// </summary>
    public class AlipaySecurityRiskCustomerriskrankGetResponse : AlipayResponse
    {
        /// <summary>
        /// 是否有风险
        /// </summary>
        [JsonPropertyName("has_risk")]
        public bool HasRisk { get; set; }

        /// <summary>
        /// 返回本次模型的infocode
        /// </summary>
        [JsonPropertyName("info_code")]
        public List<RiskRankInfoCode> InfoCode { get; set; }

        /// <summary>
        /// 风险等级
        /// </summary>
        [JsonPropertyName("risk_rank")]
        public long RiskRank { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        [JsonPropertyName("risk_score")]
        public long RiskScore { get; set; }
    }
}
