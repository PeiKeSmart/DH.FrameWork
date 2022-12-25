using Microsoft.Extensions.Options;

namespace DH.Api.MUI;

public static class DHApiUIExtension
{
    public static IApplicationBuilder UseFytApiUI(this IApplicationBuilder app, Action<ApiUIOptions> setupAction = null)
    {
        var options = new ApiUIOptions();
        if (setupAction != null)
        {
            setupAction(options);
        }
        else
        {
            options = app.ApplicationServices.GetRequiredService<IOptions<ApiUIOptions>>().Value;
        }
        app.UseMiddleware<ApiUIMiddleware>(options);
        return app;
    }
}