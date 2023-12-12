using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>模型表。实体表模型</summary>
public partial interface IModelTable
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>分类</summary>
    String Category { get; set; }

    /// <summary>名称</summary>
    String Name { get; set; }

    /// <summary>显示名</summary>
    String DisplayName { get; set; }

    /// <summary>启用</summary>
    Boolean Enable { get; set; }

    /// <summary>路径。全路径</summary>
    String Url { get; set; }

    /// <summary>控制器。控制器类型全名</summary>
    String Controller { get; set; }

    /// <summary>表名</summary>
    String TableName { get; set; }

    /// <summary>连接名</summary>
    String ConnName { get; set; }

    /// <summary>仅插入。日志型数据</summary>
    Boolean InsertOnly { get; set; }

    /// <summary>说明</summary>
    String Description { get; set; }

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
    #endregion
}
