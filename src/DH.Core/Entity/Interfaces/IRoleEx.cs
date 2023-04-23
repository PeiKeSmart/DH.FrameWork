using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>角色扩展表</summary>
public partial interface IRoleEx
{
    #region 属性
    /// <summary>角色编号</summary>
    Int32 Id { get; set; }

    /// <summary>是否管理员</summary>
    Boolean IsAdmin { get; set; }

    /// <summary>角色权限</summary>
    String Roles { get; set; }
    #endregion
}
