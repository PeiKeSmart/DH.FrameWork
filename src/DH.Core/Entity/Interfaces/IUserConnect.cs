using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>用户链接。第三方绑定</summary>
public partial interface IUserConnect
{
    #region 属性
    /// <summary>编号</summary>
    Int32 ID { get; set; }

    /// <summary>提供商</summary>
    String Provider { get; set; }

    /// <summary>用户。本地用户</summary>
    Int32 UserID { get; set; }

    /// <summary>身份标识。用户名、OpenID</summary>
    String OpenID { get; set; }

    /// <summary>全局标识。跨应用统一</summary>
    String UnionID { get; set; }

    /// <summary>用户编号。第三方用户编号</summary>
    Int64 LinkID { get; set; }

    /// <summary>昵称</summary>
    String NickName { get; set; }

    /// <summary>设备标识。企业微信用于唯一标识设备，重装后改变</summary>
    String DeviceId { get; set; }

    /// <summary>头像</summary>
    String Avatar { get; set; }

    /// <summary>访问令牌</summary>
    String AccessToken { get; set; }

    /// <summary>刷新令牌</summary>
    String RefreshToken { get; set; }

    /// <summary>访问令牌过期时间</summary>
    DateTime Expire { get; set; }

    /// <summary>刷新令牌过期时间</summary>
    DateTime RefreshExpire { get; set; }

    /// <summary>启用</summary>
    Boolean Enable { get; set; }

    /// <summary>创建用户</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>更新用户</summary>
    Int32 UpdateUserID { get; set; }

    /// <summary>更新地址</summary>
    String UpdateIP { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }

    /// <summary>备注</summary>
    String Remark { get; set; }
    #endregion
}
