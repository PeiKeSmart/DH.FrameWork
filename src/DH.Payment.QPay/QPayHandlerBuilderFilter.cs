using Microsoft.Extensions.Http;

namespace DH.Payment.QPay;

using DH.Payment.QPay.Utility;

public class QPayHandlerBuilderFilter : IHttpMessageHandlerBuilderFilter
{
    private readonly QPayCertificateManager _certificateManager;

    public QPayHandlerBuilderFilter(QPayCertificateManager certificateManager)
    {
        _certificateManager = certificateManager;
    }

    public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
    {
        if (next == null)
        {
            throw new ArgumentNullException(nameof(next));
        }

        return (builder) =>
        {
            next(builder);

            if (builder.PrimaryHandler is HttpClientHandler handler)
            {
                if (builder.Name.Contains(QPayClient.Prefix))
                {
                    var hash = builder.Name.RemovePreFix(QPayClient.Prefix);
                    if (_certificateManager.TryGet(hash, out var certificate))
                    {
                        handler.ClientCertificates.Add(certificate);
                    }
                }
            }
        };
    }
}
