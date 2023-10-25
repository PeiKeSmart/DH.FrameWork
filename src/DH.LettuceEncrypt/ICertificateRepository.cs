using Certes;
using Certes.Acme;

using System.Security.Cryptography.X509Certificates;

namespace LettuceEncrypt;

/// <summary>
/// 生成证书后管理证书持久性。
/// </summary>
public interface ICertificateRepository
{
    /// <summary>
    /// 保存证书。
    /// </summary>
    /// <param name="certificate">证书，包括其私钥。</param>
    /// <param name="cancellationToken">取消时应停止任何异步操作的令牌。</param>
    /// <param name="privateKey">密钥</param>
    /// <param name="acmeCert">证书链</param>
    /// <returns>保存证书后完成的任务。</returns>
    Task SaveAsync(X509Certificate2 certificate, IKey privateKey, CertificateChain acmeCert, CancellationToken cancellationToken);
}
