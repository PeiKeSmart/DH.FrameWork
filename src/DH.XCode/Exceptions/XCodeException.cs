﻿using System;
using System.Runtime.Serialization;
using NewLife;

namespace XCode
{
    /// <summary>XCode异常</summary>
    [Serializable]
    public class XCodeException : XException
    {
        #region 构造
        /// <summary>初始化</summary>
        public XCodeException() { }

        /// <summary>初始化</summary>
        /// <param name="message"></param>
        public XCodeException(String message) : base(message) { }

        /// <summary>初始化</summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public XCodeException(String format, params Object[] args) : base(format, args) { }

        /// <summary>初始化</summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public XCodeException(String message, Exception innerException) : base(message, innerException) { }

        /// <summary>初始化</summary>
        /// <param name="innerException"></param>
        public XCodeException(Exception innerException) : base((innerException?.Message), innerException) { }
        #endregion
    }
}