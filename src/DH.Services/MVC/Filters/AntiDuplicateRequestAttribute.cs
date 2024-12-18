﻿using DH.Core.Infrastructure;
using DH.Entity;
using DH.Services.Locks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using NewLife;

using Pek.Ids;
using Pek.Models;

namespace DH.Services.MVC.Filters;

/// <summary>
/// 防止重复提交过滤器
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AntiDuplicateRequestAttribute : ActionFilterAttribute {
    /// <summary>
    /// 业务标识
    /// </summary>
    public String Key { get; set; }

    /// <summary>
    /// 锁类型
    /// </summary>
    public LockType Type { get; set; } = LockType.User;

    /// <summary>
    /// 再次提交时间间隔，单位：秒
    /// </summary>
    public int Interval { get; set; }

    /// <summary>
    /// 执行完是否自动解除锁定
    /// </summary>
    public Boolean AutoUnLock { get; set; }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="context">操作执行上下文</param>
    /// <param name="next">操作执行下一步委托</param>
    /// <exception cref="ArgumentNullException"></exception>
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context == null)
            throw new ArgumentNullException(nameof(context));
        if (next == null)
            throw new ArgumentNullException(nameof(next));

        var @lock = CreateLock();
        var key = GetKey(context);
        try
        {
            bool isSuccess = @lock.Lock(key, GetExpiration());
            if (isSuccess == false)
            {
                var result = new DResult
                {
                    msg = GetFailMessage(),
                    code = 2
                };

                context.Result = new JsonResult(result);
                return;
            }

            OnActionExecuting(context);
            if (context.Result != null)
            {
                return;
            }

            var executedContext = await next();
            OnActionExecuted(executedContext);
        }
        finally
        {
            @lock.UnLock(AutoUnLock);
        }
    }

    /// <summary>
    /// 创建业务锁
    /// </summary>
    private ILock CreateLock() => EngineContext.Current.Resolve<ILock>() ?? NullLock.Instance;

    /// <summary>
    /// 获取锁定标识
    /// </summary>
    /// <param name="context">操作执行上下文</param>
    protected virtual string GetKey(ActionExecutingContext context)
    {
        var userId = string.Empty;
        if (Type == LockType.User)
        {
            var UserId = Sessions.Session.Instance.UserId;

            if (UserId.IsNullOrWhiteSpace())
            {
                var _cookie = EngineContext.Current.Resolve<Pek.Cookies.ICookie>();
                var Sid = _cookie.GetValue<Int64>(DHSetting.Current.SidName);
                if (Sid <= 0)
                {
                    // 生成Sid
                    Sid = IdHelper.GetSId();
                    _cookie.SetValue(DHSetting.Current.SidName, Sid);
                }
                UserId = Sid.ToString();
            }

            userId = $"{UserId}_";
        }
        return string.IsNullOrWhiteSpace(Key) ? $"{userId}{Pek.Helpers.DHWeb.Request.Path}" : $"{userId}{Key}";
    }

    /// <summary>
    /// 获取到期时间间隔
    /// </summary>
    private Int32 GetExpiration()
    {
        if (Interval == 0)
            return 10;
        return Interval;
    }

    /// <summary>
    /// 获取失败消息
    /// </summary>
    protected virtual string GetFailMessage()
    {
        if (Type == LockType.User)
            return LocaleStringResource.GetResource("请不要重复提交");
        return LocaleStringResource.GetResource("其他用户正在执行该操作,请稍后再试");
    }
}

/// <summary>
/// 锁类型
/// </summary>
public enum LockType {
    /// <summary>
    /// 用户锁，当用户发出多个执行该操作的请求，只有第一个请求被执行，其它请求被抛弃，其它用户不受影响
    /// </summary>
    User = 0,

    /// <summary>
    /// 全局锁，该操作同时只有一个用户请求被执行
    /// </summary>
    Global = 1
}