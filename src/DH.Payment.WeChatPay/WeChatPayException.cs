﻿using System;

namespace DH.Payment.WeChatPay
{
    public class WeChatPayException : Exception
    {
        public WeChatPayException()
        {
        }

        public WeChatPayException(string messages) : base(messages)
        {
        }

        public WeChatPayException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
