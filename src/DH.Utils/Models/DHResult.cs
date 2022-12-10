using DH.Helpers.Internal;

namespace DH.Models;

/// <summary>
/// APP专用返回
/// </summary>
public class DHResult
{
    /// <summary>
    /// 状态码
    /// </summary>
    public StateCode Code { get; set; } = StateCode.Fail;

    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    public dynamic Data { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperationTime { get; set; }

    /// <summary>
    /// 初始化返回结果
    /// </summary>
    public DHResult()
    {
        Code = StateCode.Fail;
        OperationTime = DateTime.Now;
    }

    /// <summary>
    /// 初始化返回结果
    /// </summary>
    /// <param name="code">状态码</param>
    /// <param name="message">消息</param>
    /// <param name="data">数据</param>
    public DHResult(StateCode code, string message, dynamic data = null)
    {
        Code = code;
        Message = message;
        Data = data;
        OperationTime = DateTime.Now;
    }

}