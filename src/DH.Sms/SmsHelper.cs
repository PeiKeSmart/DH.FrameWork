using DH.Core.Infrastructure;

using Microsoft.Extensions.Options;

namespace DG.Sms {
    public class SmsHelper
    {
        /// <summary>
        /// 获取当前短信集合
        /// </summary>
        /// <returns></returns>
        public static List<SmsOptions> GetList()
        {
            var list = new List<SmsOptions>();
            SmsOptions option = EngineContext.Current.Resolve<IOptionsMonitor<FengHuoSms>>().Get("fenghuo");
            list.Add(option);
            option = EngineContext.Current.Resolve<IOptionsMonitor<LianLuSms>>().Get("lianlu");
            list.Add(option);

            return list;
        }
    }
}
