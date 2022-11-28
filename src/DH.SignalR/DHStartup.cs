using DH.Core.Infrastructure;
using DH.VirtualFileSystem;

namespace DH.SignalR;

public class DHStartup : IDHStartup
{
    public int Order => 103;

    public void Configure(IApplicationBuilder application)
    {
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IEnumerable<IDHStartup> startups, IWebHostEnvironment webHostEnvironment)
    {
        if (SignalRSetting.Current.IsAllowSignalR)
        {
            // 添加SignalR
            services.AddSignalR(config =>
            {
                // webHostEnvironment为通过依赖注入在Startup的构造函数中注入的 IWebHostEnvironment
                if (webHostEnvironment.IsDevelopment())
                {
                    config.EnableDetailedErrors = true;
                }
                config.MaximumParallelInvocationsPerClient = 10; // 每个客户端可以在进行排队之前并行调用的最大集线器方法数
            }) // 支持MessagePack
                .AddMessagePackProtocol()
                .AddJsonProtocol();
        }
    }

    public void ConfigureVirtualFileSystem(DHVirtualFileSystemOptions options)
    {
        options.FileSets.AddEmbedded<NotifyHub>(typeof(NotifyHub).Namespace);
        // options.FileSets.Add(new EmbeddedFileSet(item.Assembly, item.Namespace));
    }
}
