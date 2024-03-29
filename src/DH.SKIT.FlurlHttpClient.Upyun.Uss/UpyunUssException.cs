﻿using System;

namespace SKIT.FlurlHttpClient.Upyun.Uss
{
    /// <summary>
    /// 又拍云云存储服务 API 出错时引发的异常。
    /// </summary>
    public class UpyunUssException : CommonExceptionBase
    {
        /// <inheritdoc/>
        public UpyunUssException()
        {
        }

        /// <inheritdoc/>
        public UpyunUssException(string message)
            : base(message)
        {
        }

        /// <inheritdoc/>
        public UpyunUssException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
