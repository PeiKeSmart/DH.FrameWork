using DH;

namespace CheckSite;

/// <summary>
/// 检测的网址
/// </summary>
public class Site : CacheObject
{
    static Site()
    {
        Init();
    }

    private static void Init()
    {
        var list = cdb.FindAll<Site>();
        if (list.Count == 0)
        {
            var model = new Site();
            model.SiteUrl = "https://www.nonghaomai.com/health";
            model.SiteName = "农好卖";
            cdb.Insert(model);
        }
    }

    public static List<Site> GetAll()
    {
        return cdb.FindAll<Site>();
    }

    /// <summary>
    /// 站点网址
    /// </summary>
    public String? SiteUrl { get; set; }

    /// <summary>
    /// 站点名称
    /// </summary>
    public String? SiteName { get; set; }
}
