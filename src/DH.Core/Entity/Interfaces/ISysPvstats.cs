using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>PV统计表</summary>
public partial interface ISysPvstats
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>分类</summary>
    String Category { get; set; }

    /// <summary>访问者系统</summary>
    String Value { get; set; }

    /// <summary>数量</summary>
    Int32 Count { get; set; }
    #endregion
}
