﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayOfflineProviderEquipmentAuthQuerybypageResponse.
    /// </summary>
    public class AlipayOfflineProviderEquipmentAuthQuerybypageResponse : AlipayResponse
    {
        /// <summary>
        /// 机具解绑按照条件分页查询返回信息
        /// </summary>
        [JsonPropertyName("equipmentauthremovequerybypagelist")]
        public List<EquipmentAuthRemoveQueryBypageDTO> Equipmentauthremovequerybypagelist { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        [JsonPropertyName("total")]
        public long Total { get; set; }
    }
}
