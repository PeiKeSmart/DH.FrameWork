using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;

namespace DH.CoolCat.Core.Mvc.Infrastructure
{
    public class CoolCatActionDescriptorChangeProvider : IActionDescriptorChangeProvider
    {
        public static CoolCatActionDescriptorChangeProvider Instance { get; } = new CoolCatActionDescriptorChangeProvider();

        public CancellationTokenSource TokenSource { get; private set; }

        public bool HasChanged { get; set; }

        public IChangeToken GetChangeToken()
        {
            TokenSource = new CancellationTokenSource();
            return new CancellationChangeToken(TokenSource.Token);
        }
    }
}
