using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>国家翻译</summary>
public partial class CountryLanModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>关联国家Id</summary>
    public Int32 CId { get; set; }

    /// <summary>关联所属语言Id</summary>
    public Int32 LId { get; set; }

    /// <summary>国家名称</summary>
    public String Name { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ICountryLan model)
    {
        Id = model.Id;
        CId = model.CId;
        LId = model.LId;
        Name = model.Name;
    }
    #endregion
}
