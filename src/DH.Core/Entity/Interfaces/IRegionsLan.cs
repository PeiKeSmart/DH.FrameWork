using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>全球区域翻译</summary>
public partial interface IRegionsLan
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>关联区域Id</summary>
    Int32 RId { get; set; }

    /// <summary>关联所属语言Id</summary>
    Int32 LId { get; set; }

    /// <summary>名称</summary>
    String Name { get; set; }

    /// <summary>别名</summary>
    String AliasName { get; set; }

    /// <summary>简称</summary>
    String ShortName { get; set; }

    /// <summary>组合名</summary>
    String MergerName { get; set; }

    /// <summary>自定义简称</summary>
    String OtherName { get; set; }
    #endregion
}
