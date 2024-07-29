﻿using System.Data;
using System.Data.Common;
using NewLife;
using NewLife.Log;

namespace XCode.DataAccessLayer;

/// <summary>事务对象</summary>
public interface ITransaction : IDisposable
{
    /// <summary>事务隔离级别</summary>
    IsolationLevel Level { get; }

    /// <summary>事务计数。当且仅当事务计数等于1时，才提交或回滚。</summary>
    Int32 Count { get; }

    /// <summary>执行次数。其决定是否更新缓存</summary>
    Int32 Executes { get; }

    /// <summary>数据库事务</summary>
    DbTransaction? Tran { get; }

    /// <summary>获取事务</summary>
    /// <param name="cmd">命令</param>
    /// <param name="execute">是否执行增删改</param>
    /// <returns></returns>
    DbTransaction? Check(DbCommand cmd, Boolean execute);

    /// <summary>增加事务计数</summary>
    /// <returns></returns>
    ITransaction Begin();

    /// <summary>提交事务</summary>
    /// <returns></returns>
    ITransaction Commit();

    /// <summary>回滚事务</summary>
    /// <returns></returns>
    ITransaction Rollback();
}

class Transaction : DisposeBase, ITransaction
{
    #region 属性
    public IsolationLevel Level { get; private set; }

    private Int32 _Count;
    /// <summary>事务嵌套层数</summary>
    public Int32 Count => _Count;

    /// <summary>执行次数。其决定是否更新缓存</summary>
    public Int32 Executes { get; set; }

    private static Int32 _gid;
    /// <summary>事务唯一编号</summary>
    private Int32 ID { get; set; }

    IDbSession _Session;

    /// <summary>连接对象</summary>
    public DbConnection? Conn { get; private set; }
    #endregion

    #region 构造
    public Transaction(IDbSession session, IsolationLevel level)
    {
        _Session = session;
        Level = level;
#if DEBUG
        Log = XTrace.Log;
#endif
    }

    protected override void Dispose(Boolean disposing)
    {
        base.Dispose(disposing);

        using var span = Tracer?.NewSpan($"db:{_Session.Database.ConnName}:Transaction:Dispose", Count);

        // 存在未提交层数，则回滚
        if (Count > 0)
        {
            //_Count = 1;
            //Rollback();
            OnRollback();
        }

        Tran.TryDispose();
        Tran = null;
        //_Session = null;

        Conn?.Close();
        Conn = null;
    }
    #endregion

    #region 延迟打开事务
    /// <summary>数据库事务。首次使用打开事务</summary>
    public DbTransaction? Tran { get; private set; }

    /// <summary>给命令设置事务和连接</summary>
    /// <param name="cmd">命令</param>
    /// <param name="execute">是否执行增删改</param>
    /// <returns></returns>
    public DbTransaction? Check(DbCommand cmd, Boolean execute)
    {
        var conn = Conn;
        if (conn == null || cmd.Transaction != null) return cmd.Transaction;

        // 此时事务可能为空
        var tr = Tran;
        if (cmd.Connection != null && cmd.Connection != conn) return tr;

        cmd.Transaction = tr;
        cmd.Connection = conn;

        // 不要为查询打开事务
        if (!execute) return tr;

        Executes++;

        if (tr != null) return tr;

        using var span = Tracer?.NewSpan($"db:{_Session.Database.ConnName}:Transaction:Check", Level);

        // 真正打开事务
        tr = Tran = conn.BeginTransaction(Level);
        cmd.Transaction = tr;
        cmd.Connection = conn;

        Level = tr.IsolationLevel;
        ID = Interlocked.Increment(ref _gid);
        Log.Debug("Tran.Begin {0} {1}", ID, Level);

        span?.SetTag(new { ID, Level, ThreadId = Thread.CurrentThread.ManagedThreadId });

        return tr;
    }
    #endregion

    #region 方法
    public ITransaction Begin()
    {
        var n = Interlocked.Increment(ref _Count);
        if (n <= 0) throw new ArgumentOutOfRangeException(nameof(Count), $"事务[{ID}]不能重新开始");

        if (n == 1)
        {
            using var span = Tracer?.NewSpan($"db:{_Session.Database.ConnName}:Transaction:Begin", new { ID, ThreadId = Thread.CurrentThread.ManagedThreadId });

            // 打开事务后，由事务管理连接
            Conn = _Session.Database.OpenConnection();
        }

        return this;
    }

    public ITransaction Commit()
    {
        var n = Interlocked.Decrement(ref _Count);
        if (n <= -1) throw new ArgumentOutOfRangeException(nameof(Count), $"事务[{ID}]未开始或已结束");

        if (n == 0)
        {
            using var span = Tracer?.NewSpan($"db:{_Session.Database.ConnName}:Transaction:Commit", new { ID, Executes, ThreadId = Thread.CurrentThread.ManagedThreadId });

            var tr = Tran;
            try
            {
                if (tr != null)
                {
                    Log.Debug("Tran.Commit {0} {1} Executes={2}", ID, Level, Executes);

                    tr.Commit();
                }
            }
            catch (Exception ex)
            {
                span?.SetError(ex, null);

                throw;
            }
            finally
            {
                Tran.TryDispose();
                Tran = null;

                Conn?.Close();
                Conn = null;
            }
        }

        return this;
    }

    public ITransaction Rollback()
    {
        var n = Interlocked.Decrement(ref _Count);
        if (n <= -1) throw new ArgumentOutOfRangeException(nameof(Count), $"事务[{ID}]未开始或已结束");

        if (n == 0)
        {
            OnRollback();
        }

        return this;
    }

    void OnRollback()
    {
        using var span = Tracer?.NewSpan($"db:{_Session.Database.ConnName}:Transaction:Rollback", new { ID, Executes, ThreadId = Thread.CurrentThread.ManagedThreadId });

        var tr = Tran;
        try
        {
            if (tr != null)
            {
                Log.Debug("Tran.Rollback {0} {1} Executes={2}", ID, Level, Executes);

                tr.Rollback();
            }
        }
        catch (Exception ex)
        {
            span?.SetError(ex, null);

            throw;
        }
        finally
        {
            Tran.TryDispose();
            Tran = null;

            Conn?.Close();
            Conn = null;
        }
    }
    #endregion

    #region 日志
    /// <summary>链路追踪</summary>
    public ITracer? Tracer { get; set; }

    /// <summary>日志</summary>
    public ILog Log { get; set; } = Logger.Null;
    #endregion
}