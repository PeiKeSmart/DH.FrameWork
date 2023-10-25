using System.Security.Cryptography.X509Certificates;

namespace LettuceEncrypt;

/// <summary>
/// ����֤�����Դ��
/// </summary>
public interface ICertificateSource
{
    /// <summary>
    /// ��Դ��ȡ����֤�顣
    /// </summary>
    /// <param name="cancellationToken">ȡ�����ơ�</param>
    /// <returns>֤��ļ��ϡ�</returns>
    Task<IEnumerable<X509Certificate2>> GetCertificatesAsync(CancellationToken cancellationToken);
}
