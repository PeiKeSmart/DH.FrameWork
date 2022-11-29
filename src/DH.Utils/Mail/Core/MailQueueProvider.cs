﻿using DH.Mail.Abstractions;

using System.Collections.Concurrent;

namespace DH.Mail.Core;

/// <summary>
/// 邮件队列提供程序
/// </summary>
public class MailQueueProvider : IMailQueueProvider
{
    /// <summary>
    /// 线程安全的邮件队列
    /// </summary>
    private static readonly ConcurrentQueue<EmailBox> MailQueue = new ConcurrentQueue<EmailBox>();

    /// <summary>
    /// 队列邮件数量
    /// </summary>
    public int Count => MailQueue.Count;

    /// <summary>
    /// 队列是否为空
    /// </summary>
    public bool IsEmpty => MailQueue.IsEmpty;

    /// <summary>
    /// 入队
    /// </summary>
    /// <param name="box">电子邮件</param>
    public void Enqueue(EmailBox box)
    {
        MailQueue.Enqueue(box);
    }

    /// <summary>
    /// 尝试出队，获取电子邮件
    /// </summary>
    /// <param name="box">电子邮件</param>
    /// <returns></returns>
    public bool TryDequeue(out EmailBox box)
    {
        return MailQueue.TryDequeue(out box);
    }
}