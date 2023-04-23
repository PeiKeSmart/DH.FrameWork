using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>委托代理。委托某人代理自己的用户权限，代理人下一次登录时将得到委托人的身份</summary>
public partial interface IPrincipalAgent
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>委托人。把自己的身份权限委托给别人</summary>
    Int32 PrincipalId { get; set; }

    /// <summary>代理人。代理获得别人身份权限</summary>
    Int32 AgentId { get; set; }

    /// <summary>启用</summary>
    Boolean Enable { get; set; }

    /// <summary>次数。可用代理次数，0表示已用完，-1表示无限制</summary>
    Int32 Times { get; set; }

    /// <summary>有效期。截止时间之前有效，不设置表示无限制</summary>
    DateTime Expire { get; set; }

    /// <summary>创建者</summary>
    Int32 CreateUserId { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>更新者</summary>
    Int32 UpdateUserId { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }

    /// <summary>更新地址</summary>
    String UpdateIP { get; set; }

    /// <summary>内容</summary>
    String Remark { get; set; }
    #endregion
}
