using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>角色翻译表</summary>
public partial interface IRoleLan
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>关联角色表Id</summary>
    Int32 RId { get; set; }

    /// <summary>关联所属语言Id</summary>
    Int32 LId { get; set; }

    /// <summary>角色分组名称</summary>
    String Name { get; set; }

    /// <summary>备注</summary>
    String Remark { get; set; }
    #endregion
}
