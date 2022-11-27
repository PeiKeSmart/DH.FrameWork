using System.Reflection;

namespace VueCliMiddleware.PartUI;

public class DHVueOptions
{
    /// <summary>
    /// 获取或设置用于访问swagger ui的路由前缀
    /// </summary>
    public string RoutePrefix { get; set; } = "PluginCore/Admin";

    /// <summary>
    /// 获取或设置用于检索swagger ui页的Stream函数
    /// </summary>
    public Func<Stream> IndexStream
    {
        get
        {
            Func<Stream> funcStream = null;

            var dhVueConfig = DHVueConfigFactory.Create();
            switch (dhVueConfig.FrontendMode?.ToLower())
            {
                default:
                case "localembedded":
                    funcStream = () => typeof(DHVueOptions).GetTypeInfo().Assembly
                        .GetManifestResourceStream("VueCliMiddleware.PartUI.node_modules.plugincore_admin_frontend.dist.index.html");
                    break;
                case "localfolder":
                    string absoluteRootPath = DHPathProvider.DHVueDir();
                    string indexFilePath = Path.Combine(absoluteRootPath, "index.html");

                    funcStream = () => (Stream)new FileStream(indexFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1, FileOptions.Asynchronous | FileOptions.SequentialScan);
                    break;
                case "remotecdn":
                    string remoteFrontendRootPath = dhVueConfig.RemoteFrontend;
                    string indexFileRemotePath = remoteFrontendRootPath + "/" + "index.html";

                    funcStream = () => new HttpClient().GetStreamAsync(indexFileRemotePath).Result;
                    break;
            }

            return funcStream;
        }
    }
}
