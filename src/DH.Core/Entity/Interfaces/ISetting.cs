using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>站点配置</summary>
public partial interface ISetting
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>键名称</summary>
    String Name { get; set; }

    /// <summary>值</summary>
    String Value { get; set; }

    /// <summary>站点Id</summary>
    Int32 StoreId { get; set; }
    #endregion
}
