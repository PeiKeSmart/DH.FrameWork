﻿using System.Collections.Concurrent;
using NewLife.Log;
using NewLife.MQTT.Messaging;
using NewLife.Net;
using NewLife.Threading;

namespace NewLife.MQTT.Handlers;

/// <summary>消息交换机</summary>
public class MqttExchange : DisposeBase
{
    #region 属性
    /// <summary>会话过期时间。默认10分钟</summary>
    public TimeSpan Expire { get; set; } = TimeSpan.FromMinutes(10);

    /// <summary>链路追踪</summary>
    public ITracer Tracer { get; set; }

    /// <summary>本地缓存，保存设备的对象引用，具备定时清理能力</summary>
    private readonly ConcurrentDictionary<Int32, IMqttHandler> _sessions = new();
    private readonly TimerX _timer;
    #endregion

    #region 构造
    /// <summary>会话管理</summary>
    /// <param name="tracer"></param>
    public MqttExchange(ITracer tracer)
    {
        _timer = new TimerX(RemoveNotAlive, null, 30_000, 30_000);
        Tracer = tracer;
    }

    /// <summary>销毁</summary>
    /// <param name="disposing"></param>
    protected override void Dispose(Boolean disposing)
    {
        base.Dispose(disposing);

        _timer.TryDispose();
    }

    private void RemoveNotAlive(Object state)
    {
        using var span = Tracer?.NewSpan("mqtt:SessionManager:RemoveNotAlive");

        // 找到不活跃会话，并销毁它
        var exp = DateTime.Now.Subtract(Expire);
        var dic = new Dictionary<Int32, IMqttHandler>();
        foreach (var item in _sessions)
            if (item.Value is MqttHandler handler)
                if (handler.Session == null)
                    dic.Add(item.Key, item.Value);
                else if (handler.Session is not NetSession session || session.Disposed || session.Session == null)
                    dic.Add(item.Key, item.Value);
                else if (session.Session.LastTime < exp)
                    dic.Add(item.Key, item.Value);

        foreach (var item in dic)
        {
            _sessions.TryRemove(item.Key, out _);

            // 销毁过期会话，促使断开连接
            item.Value.TryDispose();
        }
    }
    #endregion

    #region 会话管理
    /// <summary>添加会话</summary>
    /// <param name="sessionId"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    public Boolean Add(Int32 sessionId, IMqttHandler session) => _sessions.TryAdd(sessionId, session);

    /// <summary>获取会话</summary>
    /// <param name="sessionId"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    public Boolean TryGetValue(Int32 sessionId, out IMqttHandler session) => _sessions.TryGetValue(sessionId, out session);

    /// <summary>删除会话</summary>
    /// <param name="sessionId"></param>
    /// <returns></returns>
    public Boolean Remove(Int32 sessionId)
    {
        if (!_sessions.TryRemove(sessionId, out var session)) return false;

        session.TryDispose();

        return true;
    }
    #endregion

    #region 消息管理
    class SubscriptionItem
    {
        public Int32 Id { get; set; }
        public QualityOfService QoS { get; set; }
    }

    /// <summary>主题订阅集合</summary>
    private ConcurrentDictionary<String, List<SubscriptionItem>> _topics = new();

    /// <summary>发布消息</summary>
    /// <remarks>
    /// 找到匹配该主题的订阅者，然后发送消息
    /// </remarks>
    /// <param name="message"></param>
    public void Publish(PublishMessage message)
    {
        // 遍历所有Topic，找到匹配的订阅者
        foreach (var item in _topics)
        {
            if (MqttTopicFilter.IsMatch(message.Topic, item.Key))
            {
                // 遍历所有订阅者
                var subs = item.Value;
                foreach (var elm in subs.ToArray())
                {
                    if (_sessions.TryGetValue(elm.Id, out var session))
                    {
                        //session.PublishAsync(message);

                        // 使用指定Qos发送消息
                        var msg = new PublishMessage
                        {
                            Topic = message.Topic,
                            Payload = message.Payload,
                            QoS = elm.QoS,
                        };
                        session.PublishAsync(msg);
                    }
                    else
                    {
                        // 没有找到订阅者，删除订阅关系
                        lock (subs)
                        {
                            subs.Remove(elm);
                        }
                    }
                }
            }
        }
    }

    /// <summary>订阅主题</summary>
    /// <param name="sessionId"></param>
    /// <param name="topic"></param>
    /// <param name="qos"></param>
    public void Subscribe(Int32 sessionId, String topic, QualityOfService qos)
    {
        // 保存订阅关系
        var set = _topics.GetOrAdd(topic, new List<SubscriptionItem>());

        lock (set)
        {
            // 删除旧的订阅关系
            set.RemoveAll(e => e.Id == sessionId);
            set.Add(new SubscriptionItem { Id = sessionId, QoS = qos });
        }
    }

    /// <summary>取消主题订阅</summary>
    /// <param name="sessionId"></param>
    /// <param name="topic"></param>
    public void Unsubscribe(Int32 sessionId, String topic)
    {
        if (_topics.TryGetValue(topic, out var set))
        {
            lock (set)
            {
                set.RemoveAll(e => e.Id == sessionId);
            }

            // 没有订阅者了，删除主题
            if (set.Count == 0) _topics.TryRemove(topic, out _);
        }
    }
    #endregion
}