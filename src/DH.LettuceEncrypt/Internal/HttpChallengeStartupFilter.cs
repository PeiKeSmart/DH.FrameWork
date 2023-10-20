using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace LettuceEncrypt.Internal;

internal class HttpChallengeStartupFilter : IStartupFilter
{
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            app.UseHttpChallengeResponseMiddleware();
            next(app);
        };
    }
}
