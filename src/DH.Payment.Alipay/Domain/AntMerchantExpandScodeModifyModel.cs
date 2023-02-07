﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DH.Payment.Alipay.Domain
{
    /// <summary>
    /// AntMerchantExpandScodeModifyModel Data Structure.
    /// </summary>
    public class AntMerchantExpandScodeModifyModel : AlipayObject
    {
        /// <summary>
        /// 批量申请修改吗接口
        /// </summary>
        [JsonPropertyName("update_code_request")]
        public List<UpdateCodeRequest> UpdateCodeRequest { get; set; }
    }
}
