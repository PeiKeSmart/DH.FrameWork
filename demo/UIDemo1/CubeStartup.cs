using DH.Core.Infrastructure;
using DH.VirtualFileSystem;

namespace UIDemo1
{
    public class CubeStartup : IDHStartup
    {
        public int Order => 101;

        public void Configure(IApplicationBuilder application)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IEnumerable<IDHStartup> startups, IWebHostEnvironment webHostEnvironment)
        {
        }

        public void ConfigureVirtualFileSystem(DHVirtualFileSystemOptions options)
        {
            options.FileSets.AddEmbedded<CubeStartup>(typeof(CubeStartup).Namespace);
            // options.FileSets.Add(new EmbeddedFileSet(item.Assembly, item.Namespace));
        }
    }
}
