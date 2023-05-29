using System;
using System.Collections.Generic;

namespace DG.Sms.FengHuo
{
    /// <summary>
    /// 自定义短信返回数据
    /// </summary>
    public class SendResult
    {
        public Int32 code { get; set; }

        public String msg { get; set; }

        public Int32 total { get; set; }

        public IList<SRResult> result { get; set; }
    }

    public class SRResult
    {
        public String mobile { get; set; }

        public String order_id { get; set; }

        public Int32 code { get; set; }

        public String receive_time { get; set; }

        public String msg { get; set; }
    }
}
