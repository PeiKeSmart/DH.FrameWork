using DH.Payment.Alipay.Notify;
using System;

namespace DH.Payment.Alipay
{
    public class AlipayTradePagePayReturnEvent
    {
        public AlipayTradePagePayReturnEvent(AlipayTradePagePayReturn alipayTradePagePayReturn, String pType = "0")
        {
            AlipayTradePagePayReturn = alipayTradePagePayReturn;
            PType = pType;
        }

        public AlipayTradePagePayReturn AlipayTradePagePayReturn { get; }

        public String PType { get; }
    }
}
