namespace EasyNotice.QyWeixin.Messages;

public class MarkdownMessage : MessageBase {
    public MarkdownMessage(string content) : base("markdown")
    {
        markdown = new Markdown
        {
            content = content,
        };
    }

    public Markdown markdown { get; set; }

    public class Markdown {
        public string content { get; set; }
    }
}