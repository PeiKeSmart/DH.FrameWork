﻿using DH.Helpers;

using Quartz;

using Qz = Quartz;

namespace DH.Schedulers;

/// <summary>
/// Quartz作业
/// </summary>
public abstract class JobBase : IJob, Qz.IJob {
    /// <summary>
    /// 作业名称
    /// </summary>
    private readonly string _jobName;
    /// <summary>
    /// 触发器名称
    /// </summary>
    private readonly string _triggerName;
    /// <summary>
    /// 组名称
    /// </summary>
    private readonly string _groupName;

    /// <summary>
    /// 初始化
    /// </summary>
    protected JobBase()
    {
        _jobName = Id.Guid();
        _triggerName = Id.Guid();
        _groupName = Id.Guid();
    }

    /// <summary>
    /// 获取作业名称
    /// </summary>
    public virtual string GetJobName()
    {
        return _jobName;
    }

    /// <summary>
    /// 获取触发器名称
    /// </summary>
    public virtual string GetTriggerName()
    {
        return _triggerName;
    }

    /// <summary>
    /// 获取组名称
    /// </summary>
    public virtual string GetGroupName()
    {
        return _groupName;
    }

    /// <summary>
    /// 获取Cron表达式
    /// </summary>
    public virtual string GetCron()
    {
        return null;
    }

    /// <summary>
    /// 获取重复执行次数，默认返回null，表示持续重复执行
    /// </summary>
    public virtual int? GetRepeatCount()
    {
        return null;
    }

    /// <summary>
    /// 获取开始执行时间
    /// </summary>
    public virtual DateTimeOffset? GetStartTime()
    {
        return null;
    }

    /// <summary>
    /// 获取结束执行时间
    /// </summary>
    public virtual DateTimeOffset? GetEndTime()
    {
        return null;
    }

    /// <summary>
    /// 获取重复执行间隔时间
    /// </summary>
    public virtual TimeSpan? GetInterval()
    {
        return null;
    }

    /// <summary>
    /// 获取重复执行间隔时间，单位：小时
    /// </summary>
    public virtual int? GetIntervalInHours()
    {
        return null;
    }

    /// <summary>
    /// 获取重复执行间隔时间，单位：分
    /// </summary>
    public virtual int? GetIntervalInMinutes()
    {
        return null;
    }

    /// <summary>
    /// 获取重复执行间隔时间，单位：秒
    /// </summary>
    public virtual int? GetIntervalInSeconds()
    {
        return null;
    }

    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="context">上下文</param>
    public abstract Task Execute(IJobExecutionContext context);
}