using System.ComponentModel;

using NewLife.Configuration;

using XCode.Configuration;

namespace DH.Domain.Blogs;

/// <summary>博客设置</summary>
[DisplayName("博客设置")]
//[XmlConfigFile("Config/DHSetting.config", 10000)]
[Config("BlogSettings")]
public class BlogSettings : Config<BlogSettings> {

    #region 静态
    /// <summary>指向数据库参数字典表</summary>
    static BlogSettings() => Provider = new DbConfigProvider { UserId = 0, Category = "Blog" };
    #endregion

    /// <summary>
    /// 获取或设置一个值，该值指示是否启用博客
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// 获取或设置帖子的页面大小
    /// </summary>
    public int PostsPageSize { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示非注册用户是否可以发表评论
    /// </summary>
    public bool AllowNotRegisteredUsersToLeaveComments { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否通知新的博客评论
    /// </summary>
    public bool NotifyAboutNewBlogComments { get; set; }

    /// <summary>
    /// 获取或设置显示在标记云中的博客标记数
    /// </summary>
    public int NumberOfTags { get; set; }

    /// <summary>
    /// 在客户浏览器地址栏中启用博客 RSS 源链接
    /// </summary>
    public bool ShowHeaderRssUrl { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否必须批准博客评论
    /// </summary>
    public bool BlogCommentsMustBeApproved { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否将按存储筛选博客评论
    /// </summary>
    public bool ShowBlogCommentsPerStore { get; set; }
}