using DH.Core.Infrastructure;
using DH.VirtualFileSystem;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DH.Swagger;

public class DHStartup : IDHStartup {
    public int Order => 200;

    public void ChangeMenu()
    {

    }

    public void Configure(IApplicationBuilder application)
    {
        // 封装Swagger展示
        application.UseNetProSwagger();
    }

    public void ConfigureArea()
    {

    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        services.AddNetProSwagger(configuration);
    }

    public void ConfigureVirtualFileSystem(DHVirtualFileSystemOptions options)
    {
        options.FileSets.AddEmbedded<DHStartup>(typeof(DHStartup).Namespace);
        // options.FileSets.Add(new EmbeddedFileSet(item.Assembly, item.Namespace));
    }

    /// <summary>
    /// 注册路由
    /// </summary>
    /// <param name="endpoints">路由生成器</param>
    public void UseDHEndpoints(IEndpointRouteBuilder endpoints)
    {
    }

    public void Update()
    {
        
    }
}
