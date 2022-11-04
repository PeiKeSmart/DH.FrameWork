using SKIT.FlurlHttpClient;

namespace DG.FlurlHttpClient.Pospal.Api
{
    /// <summary>
    /// 当调用银豹 API 出错时引发的异常。
    /// </summary>
    public class PospalApiException : CommonExceptionBase
    {
        /// <inheritdoc/>
        public PospalApiException()
        {
        }

        /// <inheritdoc/>
        public PospalApiException(string message)
            : base(message)
        {
        }

        /// <inheritdoc/>
        public PospalApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
