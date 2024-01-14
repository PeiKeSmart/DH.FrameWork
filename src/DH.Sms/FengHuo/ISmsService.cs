using System;
using System.Threading.Tasks;

namespace DH.Sms.FengHuo
{
    /// <summary>
    /// 短信服务
    /// </summary>
    public interface ISmsService
    {
        /// <summary>
        /// 发送自定义短信
        /// </summary>
        /// <param name="mobile">手机号,可批量，用逗号分隔开，上限为1000个</param>
        /// <param name="content">内容</param>
        Task<SmsResult> SendAsync(string mobile, string content);

        /// <summary>
        /// 发送模板短信
        /// </summary>
        /// <param name="mobiles">手机号,可批量，用逗号分隔开，上限为1000个</param>
        /// <param name="templateId">对应的模板ID</param>
        /// <param name="paramValues">对应的参数</param>
        /// <returns></returns>
        Task<SmsResult> SendTemplateParamd(string mobiles, String templateId, String[] paramValues);
    }
}
