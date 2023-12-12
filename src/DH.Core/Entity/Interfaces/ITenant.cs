using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>多租户</summary>
public partial interface ITenant
{
    #region 属性
    /// <summary>编号</summary>
    Int64 Id { get; set; }

    /// <summary>租户名称</summary>
    String TenancyName { get; set; }

    /// <summary>创建者</summary>
    String CreateUser { get; set; }

    /// <summary>创建者</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>更新者</summary>
    String UpdateUser { get; set; }

    /// <summary>更新者</summary>
    Int32 UpdateUserID { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }

    /// <summary>更新地址</summary>
    String UpdateIP { get; set; }

    /// <summary>是否禁用</summary>
    Boolean IsLocked { get; set; }

    /// <summary>是否删除 软删除，数据不会被物理删除</summary>
    Boolean IsDeleted { get; set; }
    #endregion
}
