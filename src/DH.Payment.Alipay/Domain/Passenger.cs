using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// Passenger Data Structure.
    /// </summary>
    public class Passenger : AlipayObject
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [JsonPropertyName("telephone")]
        public string Telephone { get; set; }
    }
}
