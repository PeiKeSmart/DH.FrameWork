using System.Security.Cryptography.X509Certificates;

namespace LettuceEncrypt;

/// <summary>
/// Defines a source for certificates.
/// </summary>
public interface ICertificateSource
{
    /// <summary>
    /// Gets available certificates from the source.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A collection of certificates.</returns>
    Task<IEnumerable<X509Certificate2>> GetCertificatesAsync(CancellationToken cancellationToken);
}
