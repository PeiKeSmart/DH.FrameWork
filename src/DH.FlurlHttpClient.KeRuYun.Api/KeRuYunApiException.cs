using SKIT.FlurlHttpClient;

namespace DH.FlurlHttpClient.KeRuYun.Api
{
    /// <summary>
    /// 当调用客如云 API 出错时引发的异常。
    /// </summary>
    public class KeRuYunApiException : CommonExceptionBase
    {
        /// <inheritdoc/>
        public KeRuYunApiException()
        {
        }

        /// <inheritdoc/>
        public KeRuYunApiException(string message)
            : base(message)
        {
        }

        /// <inheritdoc/>
        public KeRuYunApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
