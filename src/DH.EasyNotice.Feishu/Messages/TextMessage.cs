namespace EasyNotice.Feishu.Messages;

public class TextMessage : MessageBase {
    public TextMessage(string text) : base("text")
    {
        content = new Content
        {
            text = text,
        };
    }

    public Content content { get; set; }

    public class Content {
        public string text { get; set; }
    }
}