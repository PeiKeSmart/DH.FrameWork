using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using Pek.Infrastructure;

namespace DH.Services.Helpers
{
    /// <summary>
    /// 用于处理浏览器功能项目的XML文件的帮助程序类(http://browscap.org/)
    /// </summary>
    public partial class BrowscapXmlHelper
    {
        private readonly IDHFileProvider _fileProvider;
        private Regex _crawlerUserAgentsRegexp;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="userAgentStringsPath">用户代理文件路径</param>
        /// <param name="crawlerOnlyUserAgentStringsPath">仅具有爬网程序文件路径的用户代理</param>
        /// <param name="fileProvider">文件提供程序</param>
        public BrowscapXmlHelper(string userAgentStringsPath, string crawlerOnlyUserAgentStringsPath, IDHFileProvider fileProvider)
        {
            _fileProvider = fileProvider;

            Initialize(userAgentStringsPath, crawlerOnlyUserAgentStringsPath);
        }

        private static bool IsBrowscapItemIsCrawler(XElement browscapItem)
        {
            var el = browscapItem.Elements("item").FirstOrDefault(e => e.Attribute("name")?.Value == "Crawler");

            return el != null && el.Attribute("value")?.Value.ToLowerInvariant() == "true";
        }

        private static string ToRegexp(string str)
        {
            var sb = new StringBuilder(Regex.Escape(str));
            sb.Replace("&amp;", "&").Replace("\\?", ".").Replace("\\*", ".*?");
            return $"^{sb}$";
        }

        private void Initialize(string userAgentStringsPath, string crawlerOnlyUserAgentStringsPath)
        {
            List<XElement> crawlerItems = null;
            var comments = new XElement("comments");
            var needSaveCrawlerOnly = false;

            if (!string.IsNullOrEmpty(crawlerOnlyUserAgentStringsPath) && _fileProvider.FileExists(crawlerOnlyUserAgentStringsPath))
            {
                // 尝试从仅爬网程序文件加载爬网程序列表
                using var sr = new StreamReader(crawlerOnlyUserAgentStringsPath);
                crawlerItems = XDocument.Load(sr).Root?.Elements("browscapitem").ToList();
            }

            if (crawlerItems == null || !crawlerItems.Any())
            {
                // 尝试从完整的用户代理文件加载爬网程序列表
                using var sr = new StreamReader(userAgentStringsPath);
                var rootElemen = XDocument.Load(sr).Root;
                crawlerItems = rootElemen?.Element("browsercapitems")?.Elements("browscapitem")
                    // 仅爬虫
                    .Where(IsBrowscapItemIsCrawler).ToList();
                needSaveCrawlerOnly = true;
                comments = rootElemen?.Element("comments");
            }

            if (crawlerItems == null || !crawlerItems.Any())
                throw new Exception("Incorrect file format");

            var crawlerRegexpPattern = string.Join("|", crawlerItems
                // 仅获取用户代理名称
                .Select(e => e.Attribute("name"))
                .Where(e => !string.IsNullOrEmpty(e?.Value))
                .Select(e => e.Value)
                .Select(ToRegexp));

            _crawlerUserAgentsRegexp = new Regex(crawlerRegexpPattern);

            if ((string.IsNullOrEmpty(crawlerOnlyUserAgentStringsPath) || _fileProvider.FileExists(crawlerOnlyUserAgentStringsPath)) && !needSaveCrawlerOnly)
                return;

            // 尝试写入爬网程序文件
            using var sw = new StreamWriter(crawlerOnlyUserAgentStringsPath);
            var root = new XElement("browsercapitems");

            comments?.AddFirst(new XElement("comment", new XCData("DH.Web.Framework uses a short version of the \"browscap.xml\" file. This short version contains crawlers only. If you want to keep the crawlers list up to date, please download the full version of the original file from the official browscap site (http://browscap.org/). Please save it in the \\App_Data folder (The file name should be \"browscap.xml\"), delete \"browscap.crawlersonly.xml\", and restart the website.")));
            root.Add(comments);

            foreach (var crawler in crawlerItems)
            {
                foreach (var element in crawler.Elements().ToList())
                {
                    if ((element.Attribute("name")?.Value.ToLowerInvariant() ?? string.Empty) == "crawler")
                        continue;
                    element.Remove();
                }

                root.Add(crawler);
            }

            root.Save(sw);
        }

        /// <summary>
        /// 确定用户代理是否为爬网程序
        /// </summary>
        /// <param name="userAgent">用户代理字符串</param>
        /// <returns>如果用户代理是爬网程序，则为True，否则为false</returns>
        public bool IsCrawler(string userAgent)
        {
            return _crawlerUserAgentsRegexp.IsMatch(userAgent);
        }
    }
}
