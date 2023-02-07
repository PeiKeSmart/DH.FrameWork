﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOpenAuthOperatorAddResponse.
    /// </summary>
    public class AlipayOpenAuthOperatorAddResponse : AlipayResponse
    {
        /// <summary>
        /// 关联账号列表
        /// </summary>
        [JsonPropertyName("accounts")]
        public List<OperatorAccountVO> Accounts { get; set; }

        /// <summary>
        /// 创建的蚂蚁操作员ID
        /// </summary>
        [JsonPropertyName("operator_id")]
        public string OperatorId { get; set; }
    }
}
