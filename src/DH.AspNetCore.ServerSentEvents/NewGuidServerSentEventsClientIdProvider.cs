using Microsoft.AspNetCore.Http;

namespace DH.ServerSentEvents;

internal class NewGuidServerSentEventsClientIdProvider : IServerSentEventsClientIdProvider {
    public Guid AcquireClientId(HttpContext context)
    {
        return Guid.NewGuid();
    }

    public void ReleaseClientId(Guid clientId, HttpContext context)
    { }
}