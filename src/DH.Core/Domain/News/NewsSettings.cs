using System.ComponentModel;

using NewLife.Configuration;

using XCode.Configuration;

namespace DH.Domain.News;

/// <summary>新闻设置</summary>
[DisplayName("新闻设置")]
//[XmlConfigFile("Config/DHSetting.config", 10000)]
[Config("NewsSettings")]
public class NewsSettings : Config<NewsSettings> {
    #region 静态
    /// <summary>指向数据库参数字典表</summary>
    static NewsSettings() => Provider = new DbConfigProvider { UserId = 0, Category = "News" };
    #endregion

    /// <summary>
    /// 获取或设置一个值，该值指示是否启用新闻
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示非注册用户是否可以发表评论
    /// </summary>
    public bool AllowNotRegisteredUsersToLeaveComments { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否通知新的新闻评论
    /// </summary>
    public bool NotifyAboutNewNewsComments { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否在主页上显示新闻
    /// </summary>
    public bool ShowNewsOnMainPage { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示主页上显示的新闻计数
    /// </summary>
    public int MainPageNewsCount { get; set; }

    /// <summary>
    /// 获取或设置新闻存档的页面大小
    /// </summary>
    public int NewsArchivePageSize { get; set; }

    /// <summary>
    /// 在客户浏览器地址栏中启用新闻 RSS 源链接
    /// </summary>
    public bool ShowHeaderRssUrl { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否必须批准新闻评论
    /// </summary>
    public bool NewsCommentsMustBeApproved { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否将按存储筛选新闻评论
    /// </summary>
    public bool ShowNewsCommentsPerStore { get; set; }
}