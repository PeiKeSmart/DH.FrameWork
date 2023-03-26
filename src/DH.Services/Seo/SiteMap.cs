namespace DH.Services.Seo;

public enum SiteMap {
    首页 = 1,
    栏目首页 = 2,
    栏目页 = 3,
    内容页 = 4,
    单页 = 5,
    其他 = 99
}

public enum SiteMapChangeFreq {
    /// <summary>
    /// 无
    /// </summary>
    none,
    /// <summary>
    /// 经常
    /// </summary>
    always,
    /// <summary>
    /// 每时
    /// </summary>
    hourly,
    /// <summary>
    /// 每天
    /// </summary>
    daily,
    /// <summary>
    /// 每周
    /// </summary>
    weekly,
    /// <summary>
    /// 每月
    /// </summary>
    monthly,
    /// <summary>
    /// 每年
    /// </summary>
    yearly
}