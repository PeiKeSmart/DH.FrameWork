namespace LettuceEncrypt.Acme;

/// <summary>
/// 要为Dns-01质询更新的外部Dns提供程序
/// </summary>
public interface IDnsChallengeProvider
{
    /// <summary>
    /// 调用以在验证之前添加记录
    /// </summary>
    /// <param name="domainName">域名，包括_acme-challenge.&lt;YOUR_DOMAIN&gt;</param>
    /// <param name="txt">TXT value for DNS-01 Challenge</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>context of added txt record</returns>
    Task<DnsTxtRecordContext> AddTxtRecordAsync(string domainName, string txt, CancellationToken ct = default);

    /// <summary>
    /// 验证后回调以删除dns记录
    /// </summary>
    /// <param name="context">context from previous added txt record</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns></returns>
    Task RemoveTxtRecordAsync(DnsTxtRecordContext context, CancellationToken ct = default);
}
