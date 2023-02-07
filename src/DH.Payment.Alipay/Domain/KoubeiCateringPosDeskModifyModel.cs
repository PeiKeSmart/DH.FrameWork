﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// KoubeiCateringPosDeskModifyModel Data Structure.
    /// </summary>
    public class KoubeiCateringPosDeskModifyModel : AlipayObject
    {
        /// <summary>
        /// 餐桌信息
        /// </summary>
        [JsonPropertyName("pos_desk")]
        public DeskEntity PosDesk { get; set; }
    }
}
