using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>角色扩展表</summary>
public partial class RoleExModel : IRoleEx
{
    #region 属性
    /// <summary>角色编号</summary>
    public Int32 Id { get; set; }

    /// <summary>是否管理员</summary>
    public Boolean IsAdmin { get; set; }

    /// <summary>角色权限</summary>
    public String Roles { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IRoleEx model)
    {
        Id = model.Id;
        IsAdmin = model.IsAdmin;
        Roles = model.Roles;
    }
    #endregion
}
