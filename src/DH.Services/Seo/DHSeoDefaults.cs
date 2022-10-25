using DH.Core.Caching;

namespace DH.Services.Seo
{
    /// <summary>
    /// 表示与SEO服务相关的默认值
    /// </summary>
    public static partial class DHSeoDefaults
    {
        /// <summary>
        /// 获取论坛主题slug名称的最大长度
        /// </summary>
        /// <remarks>对于长URL，我们可以得到以下错误：
        /// "指定的路径、文件名或两者都太长。完全限定的文件名必须少于260个字符，目录名必须少于248个字符", 
        /// 这就是为什么我们把它限制在100</remarks>
        public static int ForumTopicLength => 100;

        /// <summary>
        /// 获取搜索引擎名称的最大长度
        /// </summary>
        /// <remarks>对于长URL，我们可以得到以下错误：
        /// "指定的路径、文件名或两者都太长。完全限定的文件名必须少于260个字符，目录名必须少于248个字符", 
        /// 这就是为什么我们将其限制为200</remarks>
        public static int SearchEngineNameLength => 200;

        /// <summary>
        /// 获取为某些其他需要保留的slug（seName）的默认列表
        /// </summary>
        public static List<string> ReservedUrlRecordSlugs => new()
        {
            // 将客户重定向到特定操作的路线
            "admin",
            "blog",
            "boards",
            "cart",
            "checkout",
            "clearcomparelist",
            "compareproducts",
            "contactus",
            "emailwishlist",
            "install",
            "login",
            "logout",
            "multi-factor-verification",
            "newproducts",
            "news",
            "onepagecheckout",
            "page-not-found",
            "passwordrecovery",
            "privatemessages",
            "profile",
            "recentlyviewedproducts",
            "register",
            "search",
            "sitemap",
            "storeclosed",
            "wishlist",
        };

        #region Sitemap

        /// <summary>
        /// 获取站点地图的日期和时间格式
        /// </summary>
        public static string SitemapDateFormat => @"yyyy-MM-dd";

        /// <summary>
        /// 获取站点地图文件中的最大URL数。目前，每个提供的站点地图文件必须不超过50000个URL
        /// </summary>
        public static int SitemapMaxUrlNumber => 50000;

        /// <summary>
        /// 获取站点地图目录的名称
        /// </summary>
        public static string SitemapXmlDirectory => "sitemaps";

        /// <summary>
        /// 获取用于生成站点地图文件名的模式
        /// </summary>
        /// <remarks>
        /// {0} : 站点Id
        /// {1} : 语言Id
        /// {0} : 站点地图索引
        /// </remarks>
        public static string SitemapXmlFilePattern => "sitemap-{0}-{1}-{2}.xml";

        #endregion

        #region Caching defaults

        /// <summary>
        /// 获取用于缓存的键
        /// </summary>
        /// <remarks>
        /// {0} : 实体ID
        /// {1} : 实体名称
        /// {2} : 语言ID
        /// </remarks>
        public static String UrlRecordCacheKey => "DH.urlrecord.{0}-{1}-{2}";

        /// <summary>
        /// 获取用于缓存的键
        /// </summary>
        /// <remarks>
        /// {0} : slug
        /// </remarks>
        public static CacheKey UrlRecordBySlugCacheKey => new("DH.urlrecord.byslug.{0}");

        #endregion
    }
}
