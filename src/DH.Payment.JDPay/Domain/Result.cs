﻿using System.Xml.Serialization;

namespace DG.Payment.JDPay.Domain
{
    public class Result
    {
        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("desc")]
        public string Desc { get; set; }
    }
}
