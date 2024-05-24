using DH.Helpers.Internal;

using Microsoft.AspNetCore.Mvc;

using NewLife;

namespace DH.Helpers;

/// <summary>
/// Vue等前端专用返回
/// </summary>
public class DvResult : JsonResult {
    /// <summary>
    /// 消息标识
    /// </summary>
    public string id { get; set; }

    /// <summary>
    /// 状态码
    /// </summary>
    public StateCode code { get; set; } = StateCode.Fail;

    /// <summary>
    /// 消息
    /// </summary>
    public string message { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    public dynamic data { get; set; }

    /// <summary>
    /// 其他数据
    /// </summary>
    public dynamic extData { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime operationTime { get; set; }

    /// <summary>
    /// 初始化返回结果
    /// </summary>
    public DvResult() : base(null)
    {
        code = StateCode.Fail;
        operationTime = DateTime.Now;
    }

    /// <summary>
    /// 初始化返回结果
    /// </summary>
    /// <param name="code">状态码</param>
    /// <param name="message">消息</param>
    /// <param name="data">数据</param>
    /// <param name="extdata">其他数据</param>
    public DvResult(StateCode code, string message, dynamic data = null, dynamic extdata = null) : base(null)
    {
        this.code = code;
        this.message = message;
        this.data = data;
        this.operationTime = DateTime.Now;
        this.extData = extdata;
        this.id = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// 执行结果
    /// </summary>
    public override Task ExecuteResultAsync(ActionContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        if (id.IsNullOrWhiteSpace())
        {
            id = Guid.NewGuid().ToString();
        }

        this.Value = new
        {
            code = code.Value(),
            message,
            operationTime,
            data,
            extData,
            id
        };
        return base.ExecuteResultAsync(context);
    }

}