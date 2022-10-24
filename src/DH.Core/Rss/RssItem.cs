using System.Xml.Linq;

namespace DH.Core.Rss
{
    /// <summary>
    /// 表示RSS源的项
    /// </summary>
    public partial class RssItem
    {
        /// <summary>
        /// 初始化RSS源项的新实例
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="link">链接</param>
        /// <param name="id">唯一标识符</param>
        /// <param name="pubDate">上次生成日期</param>
        public RssItem(string title, string content, Uri link, string id, DateTimeOffset pubDate)
        {
            Title = new XElement(DHRssDefaults.Title, title);
            Content = new XElement(DHRssDefaults.Description, content);
            Link = new XElement(DHRssDefaults.Link, link);
            Id = new XElement(DHRssDefaults.Guid, new XAttribute("isPermaLink", false), id);
            PubDate = new XElement(DHRssDefaults.PubDate, pubDate.ToString("r"));
        }

        /// <summary>
        /// 初始化RSS源项的新实例
        /// </summary>
        /// <param name="item">rss项的XML视图</param>
        public RssItem(XContainer item)
        {
            var title = item.Element(DHRssDefaults.Title)?.Value ?? string.Empty;
            var content = item.Element(DHRssDefaults.Content)?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(content))
                content = item.Element(DHRssDefaults.Description)?.Value ?? string.Empty;
            var link = new Uri(item.Element(DHRssDefaults.Link)?.Value ?? string.Empty);
            var pubDateValue = item.Element(DHRssDefaults.PubDate)?.Value;
            var pubDate = pubDateValue == null ? DateTimeOffset.Now : DateTimeOffset.ParseExact(pubDateValue, "r", null);
            var id = item.Element(DHRssDefaults.Guid)?.Value ?? string.Empty;

            Title = new XElement(DHRssDefaults.Title, title);
            Content = new XElement(DHRssDefaults.Description, content);
            Link = new XElement(DHRssDefaults.Link, link);
            Id = new XElement(DHRssDefaults.Guid, new XAttribute("isPermaLink", false), id);
            PubDate = new XElement(DHRssDefaults.PubDate, pubDate.ToString("r"));
        }

        #region Methods

        /// <summary>
        /// 获取RSS提要的表示项作为XElement对象
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            var element = new XElement(DHRssDefaults.Item, Id, Link, Title, Content);

            foreach (var elementExtensions in ElementExtensions)
            {
                element.Add(elementExtensions);
            }

            return element;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 标题
        /// </summary>
        public XElement Title { get; }

        /// <summary>
        /// 获取标题文本
        /// </summary>
        public string TitleText => Title?.Value ?? string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        public XElement Content { get; }

        /// <summary>
        /// 链接
        /// </summary>
        public XElement Link { get; }

        /// <summary>
        /// 获取URL
        /// </summary>
        public Uri Url => new(Link.Value);

        /// <summary>
        /// 唯一标识符
        /// </summary>
        public XElement Id { get; }

        /// <summary>
        /// 上次生成日期
        /// </summary>
        public XElement PubDate { get; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTimeOffset PublishDate => PubDate?.Value == null ? DateTimeOffset.Now : DateTimeOffset.ParseExact(PubDate.Value, "r", null);

        /// <summary>
        /// 元素扩展名
        /// </summary>
        public List<XElement> ElementExtensions { get; } = new List<XElement>();

        #endregion
    }
}
