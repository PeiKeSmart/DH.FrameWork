namespace DH.Models;

/// <summary>
/// 云Token
/// </summary>
public record CloudToken {
    /// <summary>
    /// Token值
    /// </summary>
    public String Token { get; set; }

    /// <summary>
    /// 存储时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime ExpTime { get; set; }
}

public record CloudTokenResult {
    public String id { get; set; }

    public Int32 code { get; set; }

    public String message { get; set; }

    public CloudTokenItem data { get; set; }
}

public record CloudTokenItem {
    public String cloudToken { get; set; }

    public Int32 expireIn { get; set; }
}