using DH.Core.Events;
using DH.Payment.Alipay.Notify;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DH.Payment.Alipay.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("alipay/notify")]
public class AlipayNotifyController : Controller
{
    private readonly IAlipayNotifyClient _client;
    private readonly IOptionsMonitor<AlipayOptions> _optionsAccessor;
    private readonly IEventPublisher _eventPublisher;

    public AlipayNotifyController(IAlipayNotifyClient client, IOptionsMonitor<AlipayOptions> optionsAccessor, IEventPublisher eventPublisher)
    {
        _client = client;
        _optionsAccessor = optionsAccessor;
        _eventPublisher = eventPublisher;
    }

    /// <summary>
    /// 应用网关
    /// </summary>
    /// <returns></returns>
    [Route("gateway")]
    [HttpPost]
    public async Task<IActionResult> Gateway()
    {
        try
        {
            var msg_method = Request.Form["msg_method"].ToString();
            switch (msg_method)
            {
                // 资金单据状态变更通知
                case "alipay.fund.trans.order.changed":
                    {
                        var notify = await _client.CertificateExecuteAsync<AlipayFundTransOrderChangedNotify>(Request, _optionsAccessor.CurrentValue);
                        return AlipayNotifyResult.Success;
                    }
                // 第三方应用授权取消消息
                case "alipay.open.auth.appauth.cancelled":
                    {
                        var notify = await _client.CertificateExecuteAsync<AlipayOpenAuthAppauthCancelledNotify>(Request, _optionsAccessor.CurrentValue);
                        return AlipayNotifyResult.Success;
                    }
                // 用户授权取消消息
                case "alipay.open.auth.userauth.cancelled":
                    {
                        var notify = await _client.CertificateExecuteAsync<AlipayOpenAuthUserauthCancelledNotify>(Request, _optionsAccessor.CurrentValue);
                        return AlipayNotifyResult.Success;
                    }
                // 小程序审核通过通知
                case "alipay.open.mini.version.audit.passed":
                    {
                        var notify = await _client.CertificateExecuteAsync<AlipayOpenMiniVersionAuditPassedNotify>(Request, _optionsAccessor.CurrentValue);
                        return AlipayNotifyResult.Success;
                    }
                // 小程序审核驳回通知
                case "alipay.open.mini.version.audit.rejected":
                    {
                        var notify = await _client.CertificateExecuteAsync<AlipayOpenMiniVersionAuditRejectedNotify>(Request, _optionsAccessor.CurrentValue);
                        return AlipayNotifyResult.Success;
                    }
                // 收单退款冲退完成通知
                case "alipay.trade.refund.depositback.completed":
                    {
                        var notify = await _client.CertificateExecuteAsync<AlipayTradeRefundDepositbackCompletedNotify>(Request, _optionsAccessor.CurrentValue);
                        return AlipayNotifyResult.Success;
                    }
                // 收单资金结算到银行账户，结算退票的异步通知
                case "alipay.trade.settle.dishonoured":
                    {
                        var notify = await _client.CertificateExecuteAsync<AlipayTradeSettleDishonouredNotify>(Request, _optionsAccessor.CurrentValue);
                        return AlipayNotifyResult.Success;
                    }
                // 收单资金结算到银行账户，结算失败的异步通知
                case "alipay.trade.settle.fail":
                    {
                        var notify = await _client.CertificateExecuteAsync<AlipayTradeSettleFailNotify>(Request, _optionsAccessor.CurrentValue);
                        return AlipayNotifyResult.Success;
                    }
                // 收单资金结算到银行账户，结算成功的异步通知
                case "alipay.trade.settle.success":
                    {
                        var notify = await _client.CertificateExecuteAsync<AlipayTradeSettleSuccessNotify>(Request, _optionsAccessor.CurrentValue);
                        return AlipayNotifyResult.Success;
                    }
                // 身份认证记录消息
                case "alipay.user.certify.open.notify.completed":
                    {
                        var notify = await _client.CertificateExecuteAsync<AlipayUserCertifyOpenNotifyCompletedNotify>(Request, _optionsAccessor.CurrentValue);
                        return AlipayNotifyResult.Success;
                    }
                default:
                    return AlipayNotifyResult.Failure;
            }
        }
        catch
        {
            return AlipayNotifyResult.Failure;
        }
    }

    /// <summary>
    /// 支付宝电脑网站支付异步通知
    /// </summary>
    [Route("pagepay")]
    [HttpPost]
    public async Task<IActionResult> PagePay()
    {
        try
        {
            var notify = await _client.CertificateExecuteAsync<AlipayTradePagePayNotify>(Request, _optionsAccessor.CurrentValue);
            if (notify.TradeStatus == AlipayTradeStatus.Success)
            {
                _eventPublisher.Publish(new AlipayTradePagePayNotifyEvent(notify, notify.PassbackParams));

                return AlipayNotifyResult.Success;
            }

            return AlipayNotifyResult.Failure;
        }
        catch
        {
            return AlipayNotifyResult.Failure;
        }
    }
}
