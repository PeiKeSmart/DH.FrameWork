namespace DH.Models.EventModel;

/// <summary>
/// 单点登录消费者事件
/// </summary>
public class ThirdLoginInfoEvent {
    public ThirdLoginInfoEvent(Int64 userId, Int32 ucId, String openId, string provider, string unionID)
    {
        UserId = userId;
        UcId = ucId;
        OpenId = openId;
        Provider = provider;
        UnionID = unionID;
    }

    public Int64 UserId { get; set; }

    public Int32 UcId { get; set; }

    public String OpenId { get; set; }

    public String UnionID { get; set; }

    public String Provider { get; set; }
}