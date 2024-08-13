using System.ComponentModel;

using NewLife.Configuration;

using XCode.Configuration;

namespace DH.Core.Domain.Common;

/// <summary>管理区域设置</summary>
[DisplayName("管理区域设置")]
//[XmlConfigFile("Config/DHSetting.config", 10000)]
[Config("AdminAreaSettings")]
public class AdminAreaSettings : Config<AdminAreaSettings> {
    #region 静态
    /// <summary>指向数据库参数字典表</summary>
    static AdminAreaSettings() => Provider = new DbConfigProvider { UserId = 0, Category = "AdminArea" };
    #endregion

    /// <summary>
    /// 默认网格页面大小
    /// </summary>
    public int DefaultGridPageSize { get; set; }

    /// <summary>
    /// 弹出网格页面大小（用于弹出页面）
    /// </summary>
    public int PopupGridPageSize { get; set; }

    /// <summary>
    /// 可用网格页面大小的逗号分隔列表
    /// </summary>
    public string GridPageSizes { get; set; }

    /// <summary>
    /// 富编辑器的其他设置
    /// </summary>
    public string RichEditorAdditionalSettings { get; set; }

    /// <summary>
    /// 指示富编辑器中是否支持javascript的值
    /// </summary>
    public bool RichEditorAllowJavaScript { get; set; }

    /// <summary>
    /// 指示富编辑器中是否支持样式标记的值
    /// </summary>
    public bool RichEditorAllowStyleTag { get; set; }

    /// <summary>
    /// 指示是否对客户的电子邮件使用富文本编辑器的值
    /// </summary>
    public bool UseRichEditorForCustomerEmails { get; set; }

    /// <summary>
    /// 一个值，指示是否在消息模板和活动详细信息页面上使用富编辑器
    /// </summary>
    public bool UseRichEditorInMessageTemplates { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否应隐藏广告（新闻）
    /// </summary>
    public bool HideAdvertisementsOnAdminArea { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否应显示有关版权删除密钥的建议
    /// </summary>
    public bool CheckCopyrightRemovalKey { get; set; }

    /// <summary>
    /// 获取或设置最近新闻的标题（管理区域）
    /// </summary>
    public string LastNewsTitleAdminArea { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否在JSON结果中使用IsoDateFormat（用于避免网格中的日期问题）
    /// </summary>
    public bool UseIsoDateFormatInJsonResult { get; set; }

    /// <summary>
    /// 获取或设置一个值，该值指示是否在页面上记录引用链接
    /// </summary>
    public bool ShowDocumentationReferenceLinks { get; set; }
}
