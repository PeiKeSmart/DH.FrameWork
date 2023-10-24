using LettuceEncrypt.Acme;

namespace LettuceEncrypt.Internal;

internal class NoOpDnsChallengeProvider : IDnsChallengeProvider
{
    public Task<DnsTxtRecordContext> AddTxtRecordAsync(
        string domainName,
        string txt,
        CancellationToken ct = default
    ) => Task.FromResult(new DnsTxtRecordContext(domainName, txt));

    public Task RemoveTxtRecordAsync(DnsTxtRecordContext context, CancellationToken ct = default) =>
        Task.CompletedTask;
}
