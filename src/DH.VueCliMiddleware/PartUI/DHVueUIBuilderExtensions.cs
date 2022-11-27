using Microsoft.AspNetCore.Builder;

namespace VueCliMiddleware.PartUI;

public static class DHVueUIBuilderExtensions
{
    /// <summary>
    /// 使用提供的选项注册SwaggerUI中间件
    /// </summary>
    public static IApplicationBuilder UseDHVueUI(this IApplicationBuilder app, DHVueOptions options)
    {
        return app.UseMiddleware<DHVueMiddleware>(options);
    }

    /// <summary>
    /// 使用DI注入选项的可选设置操作注册SwaggerUI中间件
    /// </summary>
    public static IApplicationBuilder UseDHVueUI(
        this IApplicationBuilder app)
    {
        DHVueOptions options = new DHVueOptions()
        {

        };

        return app.UseDHVueUI(options);
    }

}
