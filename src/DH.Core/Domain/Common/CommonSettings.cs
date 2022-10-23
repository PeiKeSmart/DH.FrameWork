using DH.Core.Configuration;

namespace DH.Core.Domain.Common
{
    /// <summary>
    /// 常用设置
    /// </summary>
    public partial class CommonSettings : ISettings
    {
        public CommonSettings()
        {
            IgnoreLogWordlist = new List<string>();
        }

        /// <summary>
        /// 获取或设置一个值，该值指示联系人窗体是否应具有“主题”
        /// </summary>
        public bool SubjectFieldOnContactUsForm { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示联系人窗体是否应使用系统电子邮件
        /// </summary>
        public bool UseSystemEmailForContactUsForm { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示在禁用JavaScript时是否显示警告
        /// </summary>
        public bool DisplayJavaScriptDisabledWarning { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否应记录404个错误（未找到页面或文件）
        /// </summary>
        public bool Log404Errors { get; set; }

        /// <summary>
        /// 获取或设置网站上使用的面包屑分隔符
        /// </summary>
        public string BreadcrumbDelimiter { get; set; }

        /// <summary>
        /// 获取或设置在记录错误/消息时要忽略的忽略单词（短语）
        /// </summary>
        public List<string> IgnoreLogWordlist { get; set; }

        /// <summary>
        /// 获取或设置在根据计划执行相应任务时清除日志时将保存在日志中的天数。如果要全部清除整个日志，请设置为0
        /// </summary>
        public int ClearLogOlderThanDays { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示BBCode编辑器生成的链接是否应在新窗口中打开
        /// </summary>
        public bool BbcodeEditorOpenLinksInNewWindow { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否应在弹出窗口中打开“接受服务条款”链接。如果禁用，则它们将在新页面上打开。
        /// </summary>
        public bool PopupForTermsOfServiceLinks { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示jQuery迁移脚本日志记录是否处于活动状态
        /// </summary>
        public bool JqueryMigrateScriptLoggingActive { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否压缩响应（默认情况下为gzip）。
        /// 您可能希望禁用它，例如，如果在服务器级别配置了活动的IIS动态压缩模块
        /// </summary>
        public bool UseResponseCompression { get; set; }

        /// <summary>
        /// 获取或设置favicon和app图标<head/>代码的值
        /// </summary>
        public string FaviconAndAppIconsHeadCode { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否启用标记缩小
        /// </summary>
        public bool EnableHtmlMinification { get; set; }

        /// <summary>
        /// 获取或设置重新启动应用程序之前的超时（以毫秒为单位）；将null设置为使用默认值
        /// </summary>
        public int? RestartTimeout { get; set; }

        /// <summary>
        /// 获取或设置标头自定义HTML的值
        /// </summary>
        public string HeaderCustomHtml { get; set; }

        /// <summary>
        /// 获取或设置页脚自定义HTML的值
        /// </summary>
        public string FooterCustomHtml { get; set; }
    }
}
