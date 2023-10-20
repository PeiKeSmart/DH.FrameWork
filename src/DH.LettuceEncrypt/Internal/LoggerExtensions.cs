using Certes.Acme;
using Microsoft.Extensions.Logging;

namespace LettuceEncrypt.Internal;

internal static class LoggerExtensions
{
    public static void LogAcmeAction(this ILogger logger, string actionName)
    {
        if (!logger.IsEnabled(LogLevel.Trace))
        {
            return;
        }

        logger.LogTrace("ACMEv2 action: {name}", actionName);
    }

    public static void LogAcmeAction<T>(this ILogger logger, string actionName, IResourceContext<T> resourceContext)
    {
        if (!logger.IsEnabled(LogLevel.Trace))
        {
            return;
        }

        logger.LogTrace("ACMEv2 action: {name}, {resource}", actionName, resourceContext.Location);
    }
}
