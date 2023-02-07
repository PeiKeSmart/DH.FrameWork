using DG.AspNetCore.Attributes;
using DG.Events;
using DG.Payment.WeChatPay.V3;
using DG.Payment.WeChatPay.V3.Notify;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace DG.Payment.WeChatPay.Controllers
{
    [HiddenApi]
    [Route("wechatpay/v3/notify")]
    public class WeChatPayV3NotifyController : Controller
    {
        private readonly IWeChatPayNotifyClient _client;
        private readonly IOptionsMonitor<WeChatPayOptions> _optionsAccessor;
        private readonly IEventPublisher _eventPublisher;

        public WeChatPayV3NotifyController(IWeChatPayNotifyClient client, IOptionsMonitor<WeChatPayOptions> optionsAccessor, IEventPublisher eventPublisher)
        {
            _client = client;
            _optionsAccessor = optionsAccessor;
            _eventPublisher = eventPublisher;
        }

        /// <summary>
        /// 微信支付结果通知
        /// </summary>
        /// <returns></returns>
        [Route("transactions")]
        [HttpPost]
        public async Task<IActionResult> Transactions()
        {
            try
            {
                Request.Body.Seek(0, SeekOrigin.Begin);

                var notify = await _client.ExecuteAsync<WeChatPayTransactionsNotify>(Request, _optionsAccessor.CurrentValue);

                if (notify.TradeState == WeChatPayTradeState.Success)
                {
                    _eventPublisher.Publish(new WeChatPayTransactionsNotifyEvent(notify, notify.Attach));

                    return WeChatPayNotifyResult.Success;

                }
                return WeChatPayNotifyResult.Failure;
            }
            catch
            {
                return WeChatPayNotifyResult.Failure;
            }
        }

    }
}
