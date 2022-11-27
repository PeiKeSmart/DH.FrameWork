namespace VueCliMiddleware.PartUI;

public class DHVueConfig
{

    /// <summary>
    /// LocalEmbedded
    /// LocalFolder
    /// RemoteCDN
    /// </summary>
    public string FrontendMode { get; set; } = "LocalEmbedded";

    public string RemoteFrontend { get; set; } = "https://cdn.jsdelivr.net/gh/yiyungent/plugincore-admin-frontend@0.3.1/dist-cdn";

}
