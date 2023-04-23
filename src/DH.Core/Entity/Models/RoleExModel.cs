using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;

namespace DH.Entity;

/// <summary>角色扩展表</summary>
public partial class RoleExModel : IModel
{
    #region 属性
    /// <summary>角色编号</summary>
    public Int32 Id { get; set; }

    /// <summary>是否管理员</summary>
    public Boolean IsAdmin { get; set; }

    /// <summary>角色权限</summary>
    public String Roles { get; set; }
    #endregion

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public virtual Object this[String name]
    {
        get
        {
            return name switch
            {
                "Id" => Id,
                "IsAdmin" => IsAdmin,
                "Roles" => Roles,
                _ => null
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "IsAdmin": IsAdmin = value.ToBoolean(); break;
                case "Roles": Roles = Convert.ToString(value); break;
            }
        }
    }
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
