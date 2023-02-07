using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEbppInspectSessionQueryModel Data Structure.
    /// </summary>
    public class AlipayEbppInspectSessionQueryModel : AlipayObject
    {
        /// <summary>
        /// id
        /// </summary>
        [JsonPropertyName("log_name")]
        public string LogName { get; set; }
    }
}
