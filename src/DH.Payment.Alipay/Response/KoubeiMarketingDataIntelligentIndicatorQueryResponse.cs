﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiMarketingDataIntelligentIndicatorQueryResponse.
    /// </summary>
    public class KoubeiMarketingDataIntelligentIndicatorQueryResponse : AlipayResponse
    {
        /// <summary>
        /// JSON格式数组，每个对象表示一个门店某个具体日期的指标信息，KEY为指标代码，VALUE为该指标对应的值,各biz_type入参以及返回值的详细信息参见<a href="https://doc.open.alipay.com/docs/doc.htm?spm=a219a.7629140.0.0.1AZ2QH&treeId=193&articleId=106028&docType=1">快速接入</a>
        /// </summary>
        [JsonPropertyName("indicator_infos")]
        public string IndicatorInfos { get; set; }
    }
}
