using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>PV统计表</summary>
public partial class SysPvstatsModel : ISysPvstats
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>分类</summary>
    public String Category { get; set; }

    /// <summary>访问者系统</summary>
    public String Value { get; set; }

    /// <summary>数量</summary>
    public Int32 Count { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISysPvstats model)
    {
        Id = model.Id;
        Category = model.Category;
        Value = model.Value;
        Count = model.Count;
    }
    #endregion
}
