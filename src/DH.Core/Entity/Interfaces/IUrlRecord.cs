using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>SlugURL记录</summary>
public partial interface IUrlRecord
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>对应实体标识符</summary>
    Int32 EntityId { get; set; }

    /// <summary>对应实体名称</summary>
    String EntityName { get; set; }

    /// <summary>分段名称</summary>
    String Slug { get; set; }

    /// <summary>是否处于活动状态</summary>
    Boolean IsActive { get; set; }

    /// <summary>语言标识符</summary>
    Int32 LanguageId { get; set; }

    /// <summary>创建者</summary>
    String CreateUser { get; set; }

    /// <summary>创建用户</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>更新者</summary>
    String UpdateUser { get; set; }

    /// <summary>更新用户</summary>
    Int32 UpdateUserID { get; set; }

    /// <summary>更新地址</summary>
    String UpdateIP { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }
    #endregion
}
