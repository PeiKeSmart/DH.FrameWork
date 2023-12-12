using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>国家翻译</summary>
public partial interface ICountryLan
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>关联国家Id</summary>
    Int32 CId { get; set; }

    /// <summary>关联所属语言Id</summary>
    Int32 LId { get; set; }

    /// <summary>国家名称</summary>
    String Name { get; set; }
    #endregion
}
