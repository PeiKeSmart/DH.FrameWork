namespace DH.FlurlHttpClient.KeRuYun.Api.Exceptions
{
    public class KeRuYunApiRequestTimeoutException : KeRuYunApiException
    {
        /// <inheritdoc/>
        internal KeRuYunApiRequestTimeoutException()
        {
        }

        /// <inheritdoc/>
        internal KeRuYunApiRequestTimeoutException(string message)
            : base(message)
        {
        }

        /// <inheritdoc/>
        internal KeRuYunApiRequestTimeoutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
