using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>用户令牌。授权指定用户访问接口数据，支持有效期</summary>
public partial interface IUserToken
{
    #region 属性
    /// <summary>编号</summary>
    Int32 ID { get; set; }

    /// <summary>令牌</summary>
    String Token { get; set; }

    /// <summary>地址。锁定该令牌只能访问该资源路径</summary>
    String Url { get; set; }

    /// <summary>用户。本地用户</summary>
    Int32 UserID { get; set; }

    /// <summary>过期时间</summary>
    DateTime Expire { get; set; }

    /// <summary>启用</summary>
    Boolean Enable { get; set; }

    /// <summary>次数。该令牌使用次数</summary>
    Int32 Times { get; set; }

    /// <summary>首次地址</summary>
    String FirstIP { get; set; }

    /// <summary>首次时间</summary>
    DateTime FirstTime { get; set; }

    /// <summary>最后地址</summary>
    String LastIP { get; set; }

    /// <summary>最后时间</summary>
    DateTime LastTime { get; set; }

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
