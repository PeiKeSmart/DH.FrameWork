﻿using System.Xml.Serialization;
using DG.Payment.JDPay.Domain;

namespace DG.Payment.JDPay.Response
{
    [XmlRoot("jdpay")]
    public class JDPayGetUserRelationResponse : JDPayResponse
    {
        /// <summary>
        /// 版本号
        /// </summary>
        [XmlElement("version")]
        public string Version { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [XmlElement("merchant")]
        public string Merchant { get; set; }

        /// <summary>
        /// 数据签名
        /// </summary>
        [XmlElement("sign")]
        public string Sign { get; set; }

        /// <summary>
        /// 是否存在关系	
        /// </summary>
        [XmlElement("isHas")]
        public string IsHas { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [XmlElement("result")]
        public Result Result { get; set; }
    }
}
