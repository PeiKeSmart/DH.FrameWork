using System.Threading.Tasks;

namespace DH.Sms.LianLu
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
    }
}
