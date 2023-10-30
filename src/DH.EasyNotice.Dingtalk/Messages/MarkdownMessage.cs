using Newtonsoft.Json;

namespace EasyNotice.Dingtalk.Messages;

public class MarkdownMessage : MessageBase {
    public MarkdownMessage(string title, string content) : base("markdown")
    {
        this.title = title;
        markdown = new Markdown
        {
            title = title,
            text = content,
        };
    }
    public Markdown markdown { get; set; }
    public At at { get; set; }
    public class Markdown {
        public string title { get; set; }
        public string text { get; set; }
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}