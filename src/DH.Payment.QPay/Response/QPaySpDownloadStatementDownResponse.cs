﻿using System.Xml.Serialization;

namespace DG.Payment.QPay.Response
{
    [XmlRoot("xml")]
    public class QPaySpDownloadStatementDownResponse : QPayResponse
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        [XmlElement("retcode")]
        public string RetCode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [XmlElement("retmsg")]
        public string RetMsg { get; set; }
    }
}
