﻿using NewLife.Collections;
using NewLife.Net;

namespace NewLife.Caching.Clusters;

/// <summary>集群中的节点</summary>
public class RedisNode : IRedisNode
{
    #region 属性
    /// <summary>拥有者</summary>
    public Redis Owner { get; set; }

    /// <summary>节点地址</summary>
    public String EndPoint { get; set; }

    /// <summary>是否从节点</summary>
    public Boolean Slave { get; set; }

    /// <summary>连续错误次数。达到阈值后屏蔽该节点</summary>
    public Int32 Error { get; set; }

    /// <summary>下一次时间。节点出错时，将禁用一段时间</summary>
    public DateTime NextTime { get; set; }
    #endregion

    #region 构造
    /// <summary>已重载。友好显示节点地址</summary>
    /// <returns></returns>
    public override String ToString() => EndPoint ?? base.ToString();
    #endregion

    #region 客户端池
    class MyPool : ObjectPool<RedisClient>
    {
        public RedisNode Node { get; set; }

        protected override RedisClient OnCreate()
        {
            var node = Node;
            var rds = node.Owner;
            var addr = node.EndPoint;
            if (addr.IsNullOrEmpty()) throw new ArgumentNullException(nameof(node.EndPoint));

            var uri = new NetUri("tcp://" + addr);
            if (uri.Port == 0) uri.Port = 6379;

            var rc = new RedisClient(rds, uri)
            {
                Name = $"{uri.Address}-{uri.Port}",
                Log = rds.ClientLog
            };
            //if (rds.Db > 0 && (rds is not FullRedis rds2 || !rds2.Mode.EqualIgnoreCase("cluster", "sentinel"))) rc.Select(rds.Db);

            return rc;
        }

        protected override Boolean OnGet(RedisClient value)
        {
            // 借出时清空残留
            value?.Reset();

            return base.OnGet(value);
        }
    }

    //private MyPool _Pool;
    ///// <summary>连接池</summary>
    //public IPool<RedisClient> Pool
    //{
    //    get
    //    {
    //        if (_Pool != null) return _Pool;
    //        lock (this)
    //        {
    //            if (_Pool != null) return _Pool;

    //            var pool = new MyPool
    //            {
    //                Name = Owner.Name + "Pool",
    //                Node = this,
    //                Min = 10,
    //                Max = 100000,
    //                IdleTime = 30,
    //                AllIdleTime = 300,
    //                Log = Owner.ClientLog,
    //            };

    //            Owner.WriteLog("使用Redis节点：{0}", EndPoint);

    //            return _Pool = pool;
    //        }
    //    }
    //}

    ///// <summary>执行命令</summary>
    ///// <typeparam name="TResult">返回类型</typeparam>
    ///// <param name="func">回调函数</param>
    ///// <param name="write">是否写入操作</param>
    ///// <returns></returns>
    //public virtual TResult Execute<TResult>(Func<RedisClient, TResult> func, Boolean write = false)
    //{
    //    // 统计性能
    //    var sw = Owner.Counter?.StartCount();

    //    var i = 0;
    //    do
    //    {
    //        // 每次重试都需要重新从池里借出连接
    //        var client = Pool.Get();
    //        try
    //        {
    //            client.Reset();
    //            var rs = func(client);

    //            Owner.Counter?.StopCount(sw);

    //            return rs;
    //        }
    //        catch (InvalidDataException)
    //        {
    //            if (i++ >= Owner.Retry) throw;
    //        }
    //        finally
    //        {
    //            Pool.Put(client);
    //        }
    //    } while (true);
    //}
    #endregion
}
