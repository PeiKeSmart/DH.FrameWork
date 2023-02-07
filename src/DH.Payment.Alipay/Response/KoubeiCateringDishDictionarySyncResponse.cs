﻿using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Response
{
    /// <summary>
    /// KoubeiCateringDishDictionarySyncResponse.
    /// </summary>
    public class KoubeiCateringDishDictionarySyncResponse : AlipayResponse
    {
        /// <summary>
        /// 字典的数据id
        /// </summary>
        [JsonPropertyName("dictionary_id")]
        public string DictionaryId { get; set; }
    }
}
