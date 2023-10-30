namespace EasyNotice.Options;

public class NoticeOptions : EasyNoticeOptions {

    public const string SectionName = "Notice";

    /// <summary>
    /// 同一消息发送间隔
    /// </summary>
    public int IntervalSeconds { get; set; }

}