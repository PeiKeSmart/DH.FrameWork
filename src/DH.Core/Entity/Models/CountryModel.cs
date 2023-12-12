using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>国家</summary>
public partial class CountryModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>国家名称</summary>
    public String Name { get; set; }

    /// <summary>两个字母ISO代码</summary>
    public String TwoLetterIsoCode { get; set; }

    /// <summary>三个字母ISO代码</summary>
    public String ThreeLetterIsoCode { get; set; }

    /// <summary>排序</summary>
    public Int32 DisplayOrder { get; set; }

    /// <summary>ISO数字代码</summary>
    public Int32 NumericIsoCode { get; set; }

    /// <summary>是否启用</summary>
    public Boolean IsEnabled { get; set; }

    /// <summary>是否默认</summary>
    public Boolean IsDefault { get; set; }

    /// <summary>创建者</summary>
    public String CreateUser { get; set; }

    /// <summary>创建者</summary>
    public Int32 CreateUserID { get; set; }

    /// <summary>创建时间</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    public String CreateIP { get; set; }

    /// <summary>更新者</summary>
    public String UpdateUser { get; set; }

    /// <summary>更新者</summary>
    public Int32 UpdateUserID { get; set; }

    /// <summary>更新时间</summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>更新地址</summary>
    public String UpdateIP { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ICountry model)
    {
        Id = model.Id;
        Name = model.Name;
        TwoLetterIsoCode = model.TwoLetterIsoCode;
        ThreeLetterIsoCode = model.ThreeLetterIsoCode;
        DisplayOrder = model.DisplayOrder;
        NumericIsoCode = model.NumericIsoCode;
        IsEnabled = model.IsEnabled;
        IsDefault = model.IsDefault;
        CreateUser = model.CreateUser;
        CreateUserID = model.CreateUserID;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
        UpdateUser = model.UpdateUser;
        UpdateUserID = model.UpdateUserID;
        UpdateTime = model.UpdateTime;
        UpdateIP = model.UpdateIP;
    }
    #endregion
}
