namespace DH;

/// <summary>
/// 队列实体
/// </summary>
public class DelayQueue {
    /// <summary>
    /// 消息类型
    /// </summary>
    public String Type { get; set; }

    /// <summary>
    /// 入参。传递给该服务的参数，常见Json格式
    /// </summary>
    public String InputData { get; set; }

    /// <summary>
    /// 开始执行时间。用于提前下发指令后延期执行，暂时不支持取消
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 过期时间。超过该时间后不再执行，未指定时表示不限制
    /// </summary>
    public DateTime Expire { get; set; }

    /// <summary>
    /// 链路追踪
    /// </summary>
    public String TraceId { get; set; }

    /// <summary>
    /// 数据集合
    /// </summary>
    public IDictionary<String, Object> Data { get; set; }
}