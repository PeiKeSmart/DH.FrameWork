using DH.Core.Infrastructure;
using DH.VirtualFileSystem;

namespace UIDemo1
{
    public class CubeStartup : IDHStartup
    {
        /// <summary>
        /// 配置添加的中间件的使用
        /// </summary>
        /// <param name="application">用于配置应用程序的请求管道的生成器</param>
        /// <param name="typeFinder">类型处理器</param>
        public void Configure(IApplicationBuilder application, ITypeFinder typeFinder)
        {
        }

        /// <summary>
        /// 添加并配置任何中间件
        /// </summary>
        /// <param name="services">服务描述符集合</param>
        /// <param name="configuration">应用程序的配置</param>
        /// <param name="startups">查找到的IDHStartup集合</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IEnumerable<IDHStartup> startups, IWebHostEnvironment webHostEnvironment)
        {
        }

        /// <summary>
        /// 配置虚拟文件系统
        /// </summary>
        /// <param name="options">虚拟文件配置</param>
        public void ConfigureVirtualFileSystem(DHVirtualFileSystemOptions options)
        {
            options.FileSets.AddEmbedded<CubeStartup>(typeof(CubeStartup).Namespace);
            // options.FileSets.Add(new EmbeddedFileSet(item.Assembly, item.Namespace));
        }

        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="endpoints">路由生成器</param>
        public void UseDHEndpoints(IEndpointRouteBuilder endpoints)
        {
        }

        /// <summary>
        /// 将区域路由写入数据库
        /// </summary>
        public void ConfigureArea()
        {

        }

        /// <summary>
        /// 调整菜单
        /// </summary>
        public void ChangeMenu()
        {

        }

        /// <summary>
        /// 升级处理逻辑
        /// </summary>
        public void Update()
        {

        }

        /// <summary>
        /// 获取此启动配置实现的顺序
        /// </summary>
        public int Order => 101;
    }
}
