﻿using DH.AspNetCore.Extensions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace DH.AspNetCore.ResumeFileResult.Executor;

/// <summary>
/// 通过本地文件的可断点续传的FileResult执行器
/// </summary>
internal class ResumePhysicalFileResultExecutor : PhysicalFileResultExecutor, IActionResultExecutor<ResumePhysicalFileResult> {
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="loggerFactory"></param>
    public ResumePhysicalFileResultExecutor(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }

    /// <summary>
    /// 执行Result
    /// </summary>
    /// <param name="context"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public virtual Task ExecuteAsync(ActionContext context, ResumePhysicalFileResult result)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (result == null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        context.SetContentDispositionHeaderInline(result);
        return base.ExecuteAsync(context, result);
    }
}