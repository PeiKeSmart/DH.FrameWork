using System.Collections.Concurrent;

namespace DH.Web;

public class WebTool {
    private static WebTool sWTool = new WebTool();

    private WebTool()
    {
    }

    public static WebTool New()
    {
        return sWTool;
    }

    private static ConcurrentDictionary<string, HttpClient> sClients = new ConcurrentDictionary<string, HttpClient>();

    /// <summary>
    /// 本对象不支持并发，在多线程中请谨慎使用
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public HttpClientExt Client(string name = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            name = DateTime.Now.Ticks.ToString();
        }
        HttpClient client = sClients.GetOrAdd(name, new HttpClient());
        return new HttpClientExt(client);
    }
}