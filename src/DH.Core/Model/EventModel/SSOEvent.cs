using DH.Entity;

using NewLife.Model;

namespace DH.Model.EventModel;

/// <summary>
/// 单点登录消费者事件
/// </summary>
public class SSOEvent
{
    public SSOEvent(IManageUser user, UserDetail userDetail)
    {
        User = user;
        UserDetail = userDetail;
    }

    public IManageUser User { get; }

    public UserDetail UserDetail { get; }
}