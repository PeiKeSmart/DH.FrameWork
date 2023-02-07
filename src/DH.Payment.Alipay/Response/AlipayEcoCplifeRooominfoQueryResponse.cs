using System.Collections.Generic;
using System.Text.Json.Serialization;
using DH.Payment.Alipay.Domain;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// AlipayEcoCplifeRooominfoQueryResponse.
    /// </summary>
    public class AlipayEcoCplifeRooominfoQueryResponse : AlipayResponse
    {
        /// <summary>
        /// 符合条件的小区房屋信息列表.
        /// </summary>
        [JsonPropertyName("room_info")]
        public List<CplifeRoomDetail> RoomInfo { get; set; }
    }
}
