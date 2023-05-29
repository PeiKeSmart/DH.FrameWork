using System;

namespace DG.Sms.LianLu
{
    /// <summary>
    /// 自定义短信返回数据
    /// </summary>
    public class LianLuSendResult
    {
        public String code { get; set; }

        public String msg { get; set; }

        public LianLuSRResult data { get; set; }
    }

    public class LianLuSRResult
    {
        public String taskid { get; set; }
    }
}
