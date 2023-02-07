using DH.Payment.Alipay.Notify;
using System;

namespace DH.Payment.Alipay
{
    public class AlipayTradePagePayNotifyEvent
    {
        public AlipayTradePagePayNotifyEvent(AlipayTradePagePayNotify alipayTradePagePayNotify, String pType = "0")
        {
            AlipayTradePagePayNotify = alipayTradePagePayNotify;
            PType = pType;
        }

        public AlipayTradePagePayNotify AlipayTradePagePayNotify { get; }

        public String PType { get; }
    }
}
