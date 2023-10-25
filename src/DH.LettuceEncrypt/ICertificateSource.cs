using System.Security.Cryptography.X509Certificates;

namespace LettuceEncrypt;

/// <summary>
/// 定义证书的来源。
/// </summary>
public interface ICertificateSource
{
    /// <summary>
    /// 从源获取可用证书。
    /// </summary>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>证书的集合。</returns>
    Task<IEnumerable<X509Certificate2>> GetCertificatesAsync(CancellationToken cancellationToken);
}
