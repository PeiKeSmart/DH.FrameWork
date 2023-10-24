namespace LettuceEncrypt.Acme;

/// <summary>
/// ҪΪDns-01��ѯ���µ��ⲿDns�ṩ����
/// </summary>
public interface IDnsChallengeProvider
{
    /// <summary>
    /// ����������֤֮ǰ��Ӽ�¼
    /// </summary>
    /// <param name="domainName">����������_acme-challenge.&lt;YOUR_DOMAIN&gt;</param>
    /// <param name="txt">TXT value for DNS-01 Challenge</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>context of added txt record</returns>
    Task<DnsTxtRecordContext> AddTxtRecordAsync(string domainName, string txt, CancellationToken ct = default);

    /// <summary>
    /// ��֤��ص���ɾ��dns��¼
    /// </summary>
    /// <param name="context">context from previous added txt record</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns></returns>
    Task RemoveTxtRecordAsync(DnsTxtRecordContext context, CancellationToken ct = default);
}
