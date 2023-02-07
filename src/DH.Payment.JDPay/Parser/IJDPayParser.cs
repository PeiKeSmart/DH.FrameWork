﻿namespace DH.Payment.JDPay.Parser
{
    /// <summary>
    /// 京东支付结果解析
    /// </summary>
    public interface IJDPayParser<T> where T : JDPayObject
    {
        T Parse(string body);
    }
}
