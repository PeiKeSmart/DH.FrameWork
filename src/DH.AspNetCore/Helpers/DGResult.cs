﻿using DH.Helpers.Internal;

using Microsoft.AspNetCore.Mvc;

using NewLife;

namespace DH.Helpers;

/// <summary>
/// 专用返回
/// </summary>
public class DGResult : JsonResult
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
    /// 其他数据
    /// </summary>
    public dynamic ExtData { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperationTime { get; set; }

    /// <summary>
    /// 标识
    /// </summary>
    public String Id { get; set; }

    /// <summary>
    /// 初始化返回结果
    /// </summary>
    public DGResult() : base(null)
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
    public DGResult(StateCode code, string message, dynamic data = null, dynamic extdata = null) : base(null)
    {
        Code = code;
        Message = message;
        Data = data;
        OperationTime = DateTime.Now;
        ExtData = extdata;
        Id = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// 执行结果
    /// </summary>
    public override Task ExecuteResultAsync(ActionContext context)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));

        if (Id.IsNullOrWhiteSpace())
        {
            Id = Guid.NewGuid().ToString();
        }

        this.Value = new
        {
            Code = Code.Value(),
            Message,
            OperationTime,
            Data,
            ExtData,
            Id
        };
        return base.ExecuteResultAsync(context);
    }

}