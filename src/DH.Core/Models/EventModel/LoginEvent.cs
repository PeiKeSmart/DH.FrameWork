using DH.Entity;

using XCode.Membership;

namespace DH.Model.EventModel;

/// <summary>
/// 登录消费者事件
/// </summary>
public class LoginEvent
{
    public LoginEvent(Int32 loginType, IUser user, UserDetail userDetail)
    {
        LoginType = loginType;
        User = user;
        UserDetail = userDetail;
    }

    /// <summary>
    /// 登录类型。1为管理后台登录，0为普通前台登录
    /// </summary>
    public Int32 LoginType { get; }

    public IUser User { get; }

    public UserDetail UserDetail { get; }
}