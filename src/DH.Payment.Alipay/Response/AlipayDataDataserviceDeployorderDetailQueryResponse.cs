﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayDataDataserviceDeployorderDetailQueryResponse.
    /// </summary>
    public class AlipayDataDataserviceDeployorderDetailQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 部署单列表
        /// </summary>
        [JsonPropertyName("deploy_order_list")]
        public List<DxDeployOrderInfo> DeployOrderList { get; set; }
    }
}
