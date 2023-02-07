using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayMerchantPayforprivilegeProfileSetResponse.
    /// </summary>
    public class AlipayMerchantPayforprivilegeProfileSetResponse : AlipayResponse
    {
        /// <summary>
        /// 卡包充值卡面模板设置
        /// </summary>
        [JsonPropertyName("card_template_config")]
        public PayForPrivilegeCardTemplateConfig CardTemplateConfig { get; set; }
    }
}
