namespace DG.FlurlHttpClient.Pospal.Api.Exceptions
{
    public class PospalApiRequestTimeoutException : PospalApiException
    {
        /// <inheritdoc/>
        internal PospalApiRequestTimeoutException()
        {
        }

        /// <inheritdoc/>
        internal PospalApiRequestTimeoutException(string message)
            : base(message)
        {
        }

        /// <inheritdoc/>
        internal PospalApiRequestTimeoutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
