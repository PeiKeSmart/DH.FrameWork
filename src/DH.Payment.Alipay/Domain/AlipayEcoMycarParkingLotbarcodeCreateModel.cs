﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayEcoMycarParkingLotbarcodeCreateModel Data Structure.
    /// </summary>
    public class AlipayEcoMycarParkingLotbarcodeCreateModel : AlipayObject
    {
        /// <summary>
        /// 停车场编号
        /// </summary>
        [JsonPropertyName("parking_id")]
        public string ParkingId { get; set; }
    }
}
