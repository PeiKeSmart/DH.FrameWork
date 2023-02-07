using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayCommerceEducateCertificateInfoQueryResponse.
    /// </summary>
    public class AlipayCommerceEducateCertificateInfoQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 证书列表
        /// </summary>
        [JsonPropertyName("certificates")]
        public List<StudentCertificate> Certificates { get; set; }
    }
}
