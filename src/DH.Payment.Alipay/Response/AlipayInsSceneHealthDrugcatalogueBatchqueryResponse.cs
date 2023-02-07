﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayInsSceneHealthDrugcatalogueBatchqueryResponse.
    /// </summary>
    public class AlipayInsSceneHealthDrugcatalogueBatchqueryResponse : AlipayResponse
    {
        /// <summary>
        /// 药品列表
        /// </summary>
        [JsonPropertyName("drug_item_list")]
        public List<HealthDrugCatalogueItem> DrugItemList { get; set; }
    }
}
