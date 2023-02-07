using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayOverseasTravelContentCountSyncModel Data Structure.
    /// </summary>
    public class AlipayOverseasTravelContentCountSyncModel : AlipayObject
    {
        /// <summary>
        /// 计数信息列表
        /// </summary>
        [JsonPropertyName("count_infos")]
        public List<CountInfo> CountInfos { get; set; }
    }
}
