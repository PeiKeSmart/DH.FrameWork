using Microsoft.AspNetCore.Server.Kestrel.Https;

namespace McMaster.AspNetCore.Kestrel.Certificates;

/// <summary>
/// API for configuring Kestrel certificate options
/// </summary>
public static class KestrelHttpsOptionsExtensions {
    /// <summary>
    /// Configure HTTPS certificates dynamically with an implementation of <see cref="IServerCertificateSelector"/>.
    /// </summary>
    /// <param name="httpsOptions">The HTTPS configuration</param>
    /// <param name="certificateSelector">The server certificate selector.</param>
    /// <returns>The HTTPS configuration</returns>
    public static HttpsConnectionAdapterOptions UseServerCertificateSelector(
        this HttpsConnectionAdapterOptions httpsOptions,
        IServerCertificateSelector certificateSelector)
    {
        httpsOptions.ServerCertificateSelector = certificateSelector.Select!;
        return httpsOptions;
    }
}