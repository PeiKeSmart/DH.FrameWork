using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>全球区域翻译</summary>
public partial class RegionsLanModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>关联区域Id</summary>
    public Int32 RId { get; set; }

    /// <summary>关联所属语言Id</summary>
    public Int32 LId { get; set; }

    /// <summary>名称</summary>
    public String Name { get; set; }

    /// <summary>别名</summary>
    public String AliasName { get; set; }

    /// <summary>简称</summary>
    public String ShortName { get; set; }

    /// <summary>组合名</summary>
    public String MergerName { get; set; }

    /// <summary>自定义简称</summary>
    public String OtherName { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IRegionsLan model)
    {
        Id = model.Id;
        RId = model.RId;
        LId = model.LId;
        Name = model.Name;
        AliasName = model.AliasName;
        ShortName = model.ShortName;
        MergerName = model.MergerName;
        OtherName = model.OtherName;
    }
    #endregion
}
