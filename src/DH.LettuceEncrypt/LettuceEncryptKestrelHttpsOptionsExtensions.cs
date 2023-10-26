using LettuceEncrypt.Internal;
using McMaster.AspNetCore.Kestrel.Certificates;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Hosting;

/// <summary>
/// Methods for configuring Kestrel.
/// </summary>
public static class LettuceEncryptKestrelHttpsOptionsExtensions
{
    private const string MissingServicesMessage =
        "Missing required LettuceEncrypt services. Did you call '.AddLettuceEncrypt()' to add these your DI container?";

    /// <summary>
    /// Configured LettuceEncrypt on this HTTPS endpoint for Kestrel.
    /// </summary>
    /// <param name="httpsOptions">Kestrel's HTTPS configuration.</param>
    /// <param name="applicationServices"></param>
    /// <returns>The original HTTPS options with some required settings added to it.</returns>
    /// <exception cref="InvalidOperationException">
    /// Raised if <see cref="LettuceEncryptServiceCollectionExtensions.AddLettuceEncrypt(Microsoft.Extensions.DependencyInjection.IServiceCollection)"/>
    /// has not been used to add required services to the application service provider.
    /// </exception>
    public static HttpsConnectionAdapterOptions UseLettuceEncrypt(
        this HttpsConnectionAdapterOptions httpsOptions,
        IServiceProvider applicationServices)
    {
        var selector = applicationServices.GetService<IServerCertificateSelector>();

        if (selector is null)
        {
            throw new InvalidOperationException(MissingServicesMessage);
        }

        var tlsResponder = applicationServices.GetService<TlsAlpnChallengeResponder>();
        if (tlsResponder is null)
        {
            throw new InvalidOperationException(MissingServicesMessage);
        }

        return httpsOptions.UseLettuceEncrypt(selector, tlsResponder);
    }

    internal static HttpsConnectionAdapterOptions UseLettuceEncrypt(
        this HttpsConnectionAdapterOptions httpsOptions,
        IServerCertificateSelector selector,
        TlsAlpnChallengeResponder tlsAlpnChallengeResponder)
    {
        // 检查是否已设置此处理程序。如果是这样的话，就把我们的处理程序挂在它之前。
        var otherHandler = httpsOptions.OnAuthenticate;
        httpsOptions.OnAuthenticate = (ctx, options) =>
        {
            tlsAlpnChallengeResponder.OnSslAuthenticate(ctx, options);
            otherHandler?.Invoke(ctx, options);
        };

        httpsOptions.UseServerCertificateSelector(selector);
        return httpsOptions;
    }
}
