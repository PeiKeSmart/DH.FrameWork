using DG.Infrastructure;
using DG.TypeFinder;
using DG.Utils.Resource;
using DG.VirtualFileSystem;

namespace DG.Payment.WeChatPay
{
    /// <summary>
    /// 代表用于在应用程序启动时配置框架的对象
    /// </summary>
    public class DGStartup : IDGStartup
    {
        /// <summary>
        /// 获取此启动配置实现的顺序
        /// </summary>
        public int Order => 100;

        public void ChangeMenu()
        {
            
        }

        public void Configure(IApplicationBuilder application, ITypeFinder typeFinder)
        {
            
        }

        public void ConfigureArea()
        {
           
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            // 微信支付
            services.AddWeChatPay();
            services.Configure<WeChatPayOptions>(configuration.GetSection("WeChatPay"));
        }

        public void ConfigureVirtualFileSystem(DGVirtualFileSystemOptions options)
        {
            
        }

        public void GlobalInitScript(Func<string, ResourceHelper> script)
        {
            
        }

        public void GlobalInitStyle(Func<string, ResourceHelper> style)
        {
            
        }

        public void Update()
        {
            
        }
    }
}
