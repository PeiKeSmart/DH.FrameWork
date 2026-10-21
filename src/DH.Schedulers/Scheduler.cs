using System.Reflection;

using NewLife.Reflection;

using Pek;
using Pek.Helpers;

using Quartz;
using Quartz.Impl;

using Qz = Quartz;

namespace DH.Schedulers;

/// <summary>
/// Quartz调度器
/// </summary>
public class Scheduler : IScheduler {
    /// <summary>
    /// Quartz调度器
    /// </summary>
    private Qz.IScheduler _scheduler;

    /// <summary>
    /// 启动
    /// </summary>
    public async Task StartAsync()
    {
        _scheduler = await GetScheduler().ConfigureAwait(false);
#if NET10_0_OR_GREATER
        if (_scheduler.Status == SchedulerStatus.Running)
            return;
#else
        if (_scheduler.IsStarted)
            return;
#endif
        await _scheduler.Start().ConfigureAwait(false);
    }

    /// <summary>
    /// 获取调度器
    /// </summary>
    private async Task<Qz.IScheduler> GetScheduler()
    {
        if (_scheduler != null)
            return _scheduler;
#if NET10_0_OR_GREATER
        // Quartz 4.x 移除 StdSchedulerFactory，改用独立的调度器构建器
        var factory = QuartzSchedulerBuilder.Create(null).Build();
#else
        var factory = new StdSchedulerFactory();
#endif
        return await factory.GetScheduler().ConfigureAwait(false);
    }

    /// <summary>
    /// 暂停
    /// </summary>
    public async Task PauseAsync()
    {
        if (_scheduler == null)
            return;
        await _scheduler.PauseAll().ConfigureAwait(false);
    }

    /// <summary>
    /// 恢复
    /// </summary>
    public async Task ResumeAsync()
    {
        if (_scheduler == null)
            return;
        await _scheduler.ResumeAll().ConfigureAwait(false);
    }

    /// <summary>
    /// 停止
    /// </summary>
    public async Task StopAsync()
    {
        if (_scheduler == null)
            return;
#if NET10_0_OR_GREATER
        if (_scheduler.Status == SchedulerStatus.Shutdown)
            return;
#else
        if (_scheduler.IsShutdown)
            return;
#endif
        await _scheduler.Shutdown(true).ConfigureAwait(false);
    }

    /// <summary>
    /// 扫描并添加作业
    /// </summary>
    /// <param name="assemblies">程序集列表</param>
    public async Task ScanJobsAsync(params Assembly[] assemblies)
    {
        var jobs = new List<IJob>();
        if (assemblies != null && assemblies.Length > 0)
        {
            jobs = Reflection.GetInstancesByInterface<IJob>(assemblies);
        }
        else
        {
            jobs = typeof(IJob).GetAllSubclasses().Select(e => e.CreateInstance() as IJob).ToList();
        }

        if (jobs == null)
            return;
        foreach (var job in jobs)
            await AddJobAsync(job).ConfigureAwait(false);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    public async Task AddJobAsync<TJob>() where TJob : IJob, new()
    {
        await AddJobAsync(new TJob()).ConfigureAwait(false);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="job">作业</param>
    public async Task AddJobAsync(IJob job)
    {
        if (!(job is JobBase quartzJob))
            throw new InvalidOperationException("Ding.Schedulers.Quartz.JobBase派生");
        var jobDetail = CreateJob(quartzJob);
        var trigger = CreateTrigger(quartzJob);
        await AddJobAsync(jobDetail, trigger).ConfigureAwait(false);
    }

    /// <summary>
    /// 创建作业
    /// </summary>
    private IJobDetail CreateJob(JobBase job)
    {
#if NET10_0_OR_GREATER
        // Quartz 4.x 只能通过泛型构建器指定作业类型
        return JobBuilder.Create().OfType(job.GetType())
            .WithIdentity(job.GetJobName(), job.GetGroupName())
            .Build();
#else
        return JobBuilder.Create(job.GetType())
            .WithIdentity(job.GetJobName(), job.GetGroupName())
            .Build();
#endif
    }

    /// <summary>
    /// 创建触发器
    /// </summary>
    private ITrigger CreateTrigger(JobBase job)
    {
        var builder = TriggerBuilder.Create()
            .WithIdentity(job.GetTriggerName(), job.GetGroupName())
            .WithSimpleSchedule(scheduleBuilder => Schedule(scheduleBuilder, job));
        SetStartTime(builder, job);
        SetEndTime(builder, job);
        SetCron(builder, job);
        return builder.Build();
    }

    /// <summary>
    /// 设置调度策略
    /// </summary>
    private void Schedule(SimpleScheduleBuilder builder, JobBase job)
    {
        SetRepeatCount(builder, job);
        if (job.GetInterval() != null)
            builder.WithInterval(job.GetInterval().SafeValue());
        // Quartz 4.x 移除了 WithIntervalInHours/Minutes/Seconds，统一用 WithInterval(TimeSpan) 兼容两版
        if (job.GetIntervalInHours() != null)
            builder.WithInterval(TimeSpan.FromHours(job.GetIntervalInHours().SafeValue()));
        if (job.GetIntervalInMinutes() != null)
            builder.WithInterval(TimeSpan.FromMinutes(job.GetIntervalInMinutes().SafeValue()));
        if (job.GetIntervalInSeconds() != null)
            builder.WithInterval(TimeSpan.FromSeconds(job.GetIntervalInSeconds().SafeValue()));
    }

    /// <summary>
    /// 设置重复执行次数
    /// </summary>
    private void SetRepeatCount(SimpleScheduleBuilder builder, JobBase job)
    {
        if (job.GetRepeatCount() == null)
        {
            builder.RepeatForever();
            return;
        }
        builder.WithRepeatCount(job.GetRepeatCount().SafeValue());
    }

    /// <summary>
    /// 设置作业开始时间
    /// </summary>
#if NET10_0_OR_GREATER
    private void SetStartTime<TJob>(TriggerBuilder<TJob> builder, JobBase job) where TJob : Qz.IJob
#else
    private void SetStartTime(TriggerBuilder builder, JobBase job)
#endif
    {
        if (job.GetStartTime() == null)
        {
            builder.StartNow();
            return;
        }
        builder.StartAt(job.GetStartTime().SafeValue());
    }

    /// <summary>
    /// 设置作业结束时间
    /// </summary>
#if NET10_0_OR_GREATER
    private void SetEndTime<TJob>(TriggerBuilder<TJob> builder, JobBase job) where TJob : Qz.IJob
#else
    private void SetEndTime(TriggerBuilder builder, JobBase job)
#endif
    {
        if (job.GetEndTime() == null)
            return;
        builder.EndAt(job.GetEndTime().SafeValue());
    }

    /// <summary>
    /// 设置Cron表达式
    /// </summary>
#if NET10_0_OR_GREATER
    private void SetCron<TJob>(TriggerBuilder<TJob> builder, JobBase job) where TJob : Qz.IJob
#else
    private void SetCron(TriggerBuilder builder, JobBase job)
#endif
    {
        if (job.GetCron() == null)
            return;
        builder.WithCronSchedule(job.GetCron());
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <typeparam name="TJob">作业类型</typeparam>
    /// <param name="configureJob">作业配置操作</param>
    /// <param name="configureTrigger">触发器配置操作</param>
#if NET10_0_OR_GREATER
    public async Task AddJobAsync<TJob>(Action<JobBuilder<Qz.IJob>> configureJob, Action<TriggerBuilder<Qz.IJob>> configureTrigger) where TJob : Qz.IScheduler
#else
    public async Task AddJobAsync<TJob>(Action<JobBuilder> configureJob, Action<TriggerBuilder> configureTrigger) where TJob : Qz.IScheduler
#endif
    {
#if NET10_0_OR_GREATER
        var jobBuilder = JobBuilder.Create().OfType(typeof(TJob));
#else
        var jobBuilder = JobBuilder.Create(typeof(TJob));
#endif
        configureJob(jobBuilder);
        var job = jobBuilder.Build();
        var triggerBuilder = TriggerBuilder.Create();
        configureTrigger(triggerBuilder);
        var trigger = triggerBuilder.Build();
        await AddJobAsync(job, trigger).ConfigureAwait(false);
    }

    /// <summary>
    /// 添加作业
    /// </summary>
    /// <param name="job">作业</param>
    /// <param name="trigger">触发器</param>
    public async Task AddJobAsync(IJobDetail job, ITrigger trigger)
    {
        _scheduler = await GetScheduler().ConfigureAwait(false);
        await _scheduler.ScheduleJob(job, trigger).ConfigureAwait(false);
    }
}