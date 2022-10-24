using System.Xml;
using System.Xml.Linq;

namespace DH.Core.Rss
{
    /// <summary>
    /// 表示RSS源
    /// </summary>
    public partial class RssFeed
    {
        #region Ctor

        /// <summary>
        /// 初始化RSS源的新实例
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        /// <param name="link">链接</param>
        /// <param name="lastBuildDate">上次生成日期</param>
        public RssFeed(string title, string description, Uri link, DateTimeOffset lastBuildDate)
        {
            Title = new XElement(DHRssDefaults.Title, title);
            Description = new XElement(DHRssDefaults.Description, description);
            Link = new XElement(DHRssDefaults.Link, link);
            LastBuildDate = new XElement(DHRssDefaults.LastBuildDate, lastBuildDate.ToString("r"));
        }

        /// <summary>
        /// 初始化RSS源的新实例
        /// </summary>
        /// <param name="link">统一资源定位地址</param>
        public RssFeed(Uri link) : this(string.Empty, string.Empty, link, DateTimeOffset.Now)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// 属性扩展名
        /// </summary>
        public KeyValuePair<XmlQualifiedName, string> AttributeExtension { get; set; }

        /// <summary>
        /// 元素扩展名
        /// </summary>
        public List<XElement> ElementExtensions { get; } = new List<XElement>();

        /// <summary>
        /// rss项目列表
        /// </summary>
        public List<RssItem> Items { get; set; } = new List<RssItem>();

        /// <summary>
        /// 标题
        /// </summary>
        public XElement Title { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public XElement Description { get; private set; }

        /// <summary>
        /// 链接
        /// </summary>
        public XElement Link { get; private set; }

        /// <summary>
        /// 上次生成日期
        /// </summary>
        public XElement LastBuildDate { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// 从传递的流加载RSS源
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含异步任务，其结果包含RSS提要
        /// </returns>
        public static async Task<RssFeed> LoadAsync(Stream stream)
        {
            try
            {
                var document = await XDocument.LoadAsync(stream, LoadOptions.None, default);

                var channel = document.Root?.Element(DHRssDefaults.Channel);

                if (channel == null)
                    return null;

                var title = channel.Element(DHRssDefaults.Title)?.Value ?? string.Empty;
                var description = channel.Element(DHRssDefaults.Description)?.Value ?? string.Empty;
                var link = new Uri(channel.Element(DHRssDefaults.Link)?.Value ?? string.Empty);
                var lastBuildDateValue = channel.Element(DHRssDefaults.LastBuildDate)?.Value;
                var lastBuildDate = lastBuildDateValue == null ? DateTimeOffset.Now : DateTimeOffset.ParseExact(lastBuildDateValue, "r", null);

                var feed = new RssFeed(title, description, link, lastBuildDate);

                foreach (var item in channel.Elements(DHRssDefaults.Item))
                {
                    feed.Items.Add(new RssItem(item));
                }

                return feed;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取此RSS源的内容
        /// </summary>
        /// <returns>Content of RSS feed</returns>
        public string GetContent()
        {
            var document = new XDocument();
            var root = new XElement(DHRssDefaults.RSS, new XAttribute("version", "2.0"));
            var channel = new XElement(DHRssDefaults.Channel,
                new XAttribute(XName.Get(AttributeExtension.Key.Name, AttributeExtension.Key.Namespace), AttributeExtension.Value));

            channel.Add(Title, Description, Link, LastBuildDate);

            foreach (var element in ElementExtensions)
            {
                channel.Add(element);
            }

            foreach (var item in Items)
            {
                channel.Add(item.ToXElement());
            }

            root.Add(channel);
            document.Add(root);

            return document.ToString();
        }

        #endregion
    }
}
