using MessagePack;

namespace DH.SignalR.Dtos;

/// <summary>
/// 下线通知
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class OffLineData
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public Int64 UserId { set; get; }

    /// <summary>
    /// 连接Id
    /// </summary>
    public String ConnectionId { set; get; }

    /// <summary>
    /// 是否该用户的最后一个连接
    /// </summary>
    public Boolean IsLast { set; get; }
}