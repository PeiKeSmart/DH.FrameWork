using DH.Payment.WeChatPay.V3.Notify;
using System;

namespace DH.Payment.WeChatPay
{
    public class WeChatPayTransactionsNotifyEvent
    {
        public WeChatPayTransactionsNotifyEvent(WeChatPayTransactionsNotify weChatPayTransactionsNotify, String pType = "0")
        {
            WeChatPayTransactionsNotify = weChatPayTransactionsNotify;
            PType = pType;
        }

        public WeChatPayTransactionsNotify WeChatPayTransactionsNotify { get; }

        public String PType { get; }
    }
}
