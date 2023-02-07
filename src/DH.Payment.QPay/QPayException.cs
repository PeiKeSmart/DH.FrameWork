﻿using System;

namespace DG.Payment.QPay
{
    public class QPayException : Exception
    {
        public QPayException()
        {
        }

        public QPayException(string messages) : base(messages)
        {
        }

        public QPayException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
