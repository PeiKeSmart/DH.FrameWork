﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AlipayUserDeliverAddress Data Structure.
    /// </summary>
    public class AlipayUserDeliverAddress : AlipayObject
    {
        /// <summary>
        /// 地址
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        [JsonPropertyName("address_code")]
        public string AddressCode { get; set; }

        /// <summary>
        /// 是否默认收货地址
        /// </summary>
        [JsonPropertyName("default_deliver_address")]
        public string DefaultDeliverAddress { get; set; }

        /// <summary>
        /// 区/县
        /// </summary>
        [JsonPropertyName("deliver_area")]
        public string DeliverArea { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [JsonPropertyName("deliver_city")]
        public string DeliverCity { get; set; }

        /// <summary>
        /// 收货人全名
        /// </summary>
        [JsonPropertyName("deliver_fullname")]
        public string DeliverFullname { get; set; }

        /// <summary>
        /// 收货地址的联系人移动电话
        /// </summary>
        [JsonPropertyName("deliver_mobile")]
        public string DeliverMobile { get; set; }

        /// <summary>
        /// 收货地址的联系人固定电话
        /// </summary>
        [JsonPropertyName("deliver_phone")]
        public string DeliverPhone { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [JsonPropertyName("deliver_province")]
        public string DeliverProvince { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [JsonPropertyName("zip")]
        public string Zip { get; set; }
    }
}
