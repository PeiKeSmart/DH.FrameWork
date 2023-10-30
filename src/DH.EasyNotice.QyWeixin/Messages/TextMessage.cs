﻿namespace EasyNotice.QyWeixin.Messages;

public class TextMessage : MessageBase {
    public TextMessage(string content) : base("text")
    {
        text = new Text
        {
            content = content,
        };
    }

    public Text text { get; set; }

    public class Text {
        public string content { get; set; }
    }
}