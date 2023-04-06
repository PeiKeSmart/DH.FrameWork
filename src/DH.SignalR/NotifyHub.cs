using DH.Core.Infrastructure;
using DH.Extensions;
using DH.Helpers;
using DH.SignalR.Dtos;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using NewLife;
using NewLife.Caching;
using NewLife.Log;

using System.Security.Claims;

namespace DH.SignalR;

/// <summary>
/// 服务端接口
/// </summary>
public interface IServerNotifyHub
{
}

/// <summary>
/// 客户端使用的接口
/// </summary>
public interface IClientNotifyHub
{
    /// <summary>
    /// 统一的客户端通知方法
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task OnNotify(Object data);

    /// <summary>
    /// 在线
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task OnLine(Object data);

    /// <summary>
    /// 离线
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task OffLine(Object data);
}

[Authorize("jwt")]
public class NotifyHub : Hub<IClientNotifyHub>, IServerNotifyHub
{
    /// <summary>
    /// 缓存
    /// </summary>
    private readonly ICache _cache;

    public NotifyHub(ICache cache)
    {
        if (DHUtilSetting.Current.RedisEnabled)
        {
            _cache = EngineContext.Current.Resolve<FullRedis>();
        }
        else
        {
            _cache = cache;
        }
    }

    public override async Task OnConnectedAsync()
    {
        var userId = DHWeb.Identity.GetValue(ClaimTypes.Sid).ToInt();
        var dgpage = Context.GetHttpContext().Request.Query["dgpage"].FirstOrDefault();
        var iotid = Context.GetHttpContext().Request.Query["iotid"].FirstOrDefault();

#if DEBUG
        XTrace.WriteLine($"OnConnectedAsync----userId:{userId},dgpage:{dgpage},iotid:{iotid},connectionId:{Context.ConnectionId}");
#endif

        if (userId != 0)
        {
            _cache.Increment($"{SignalRSetting.Current.SignalRPrefixUser}{DHUtilSetting.Current.CacheKeyPrefix}{userId}Count", 1);
            await JoinToGroup(userId, Context.ConnectionId, dgpage, iotid);
            await DealOnLineNotify(userId, Context.ConnectionId);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = DHWeb.Identity.GetValue(ClaimTypes.Sid).ToInt();
        var dgpage = Context.GetHttpContext().Request.Query["dgpage"].FirstOrDefault();
        var iotid = Context.GetHttpContext().Request.Query["iotid"].FirstOrDefault();

#if DEBUG
        XTrace.WriteLine($"OnDisconnectedAsync----userId:{userId},dgpage:{dgpage},iotid:{iotid},connectionId:{Context.ConnectionId}");
#endif

        if (userId != 0)
        {
            _cache.Decrement($"{SignalRSetting.Current.SignalRPrefixUser}{DHUtilSetting.Current.CacheKeyPrefix}{userId}Count", 1);
            await DealOffLineNotify(userId, Context.ConnectionId);
        }

        await LeaveFromGroup(Context.ConnectionId, dgpage, iotid);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendAll(Object data)
    {
        await Clients.All.OnNotify(data);
    }

    public async Task Send(Object data, String Group)
    {
        await Clients.Group(Group).OnNotify(data);
    }

    /// <summary>
    /// 处理上线通知(只有用户第一个连接才通知)
    /// </summary>
    /// <param name="userId">用户Id</param>
    /// <param name="connectionId">连接Id</param>
    /// <returns></returns>
    private async Task DealOnLineNotify(Int32 userId, string connectionId)
    {
        var userConnectCount = _cache.Get<Int32>($"{SignalRSetting.Current.SignalRPrefixUser}{DHUtilSetting.Current.CacheKeyPrefix}{userId}Count");
        await Clients.All.OnLine(new OnLineData
        {
            UserId = userId,
            ConnectionId = connectionId,
            IsFirst = userConnectCount == 1
        });
    }

    /// <summary>
    /// 处理下线通知(只有当用户一个连接都没了 才算下线)
    /// </summary>
    /// <param name="userId">用户Id</param>
    /// <param name="connectionId">连接Id</param>
    /// <returns></returns>
    private async Task DealOffLineNotify(Int32 userId, string connectionId)
    {
        var userConnectCount = _cache.Get<Int32>($"{SignalRSetting.Current.SignalRPrefixUser}{DHUtilSetting.Current.CacheKeyPrefix}{userId}Count");
        await Clients.All.OffLine(new OffLineData
        {
            UserId = userId,
            ConnectionId = connectionId,
            IsLast = userConnectCount == 0
        });
    }

    /// <summary>
    /// 加入组
    /// </summary>
    /// <param name="userId">用户Id</param>
    /// <param name="connectionId">连接Id</param>
    /// <param name="groups">组</param>
    /// <returns></returns>
    private async Task JoinToGroup(Int32 userId, String connectionId, params String[] groups)
    {
        if (userId > 0 && groups != null && groups.Length > 0)
        {
            foreach (var group in groups)
            {
                if (!group.IsNullOrWhiteSpace())
                {
                    await Groups.AddToGroupAsync(connectionId, group);

                    var dic = _cache.GetDictionary<Int32>($"{SignalRSetting.Current.SignalRPrefixGroup}{DHUtilSetting.Current.CacheKeyPrefix}{group}");
                    dic.Add(connectionId, userId);
                }
            }
        }
    }

    /// <summary>
    /// 从组中移除
    /// </summary>
    /// <param name="connectionId">连接Id</param>
    /// <param name="groups">组</param>
    /// <returns></returns>
    private async Task LeaveFromGroup(String connectionId, params String[] groups)
    {
        if (groups != null && groups.Length > 0)
        {
            foreach (var group in groups)
            {
                if (!group.IsNullOrWhiteSpace())
                {
                    await Groups.RemoveFromGroupAsync(connectionId, group);

                    var dic = _cache.GetDictionary<Int32>($"{SignalRSetting.Current.SignalRPrefixGroup}{DHUtilSetting.Current.CacheKeyPrefix}{group}");
                    dic.Remove(connectionId);
                }
            }
        }
    }
}