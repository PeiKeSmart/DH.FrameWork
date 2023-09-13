using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>角色翻译表</summary>
public partial class RoleLanModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>关联角色表Id</summary>
    public Int32 RId { get; set; }

    /// <summary>关联所属语言Id</summary>
    public Int32 LId { get; set; }

    /// <summary>角色分组名称</summary>
    public String Name { get; set; }

    /// <summary>备注</summary>
    public String Remark { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IRoleLan model)
    {
        Id = model.Id;
        RId = model.RId;
        LId = model.LId;
        Name = model.Name;
        Remark = model.Remark;
    }
    #endregion
}
