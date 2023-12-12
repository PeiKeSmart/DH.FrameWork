using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>国家</summary>
public partial interface ICountry
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>国家名称</summary>
    String Name { get; set; }

    /// <summary>两个字母ISO代码</summary>
    String TwoLetterIsoCode { get; set; }

    /// <summary>三个字母ISO代码</summary>
    String ThreeLetterIsoCode { get; set; }

    /// <summary>排序</summary>
    Int32 DisplayOrder { get; set; }

    /// <summary>ISO数字代码</summary>
    Int32 NumericIsoCode { get; set; }

    /// <summary>是否启用</summary>
    Boolean IsEnabled { get; set; }

    /// <summary>是否默认</summary>
    Boolean IsDefault { get; set; }

    /// <summary>创建者</summary>
    String CreateUser { get; set; }

    /// <summary>创建者</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>更新者</summary>
    String UpdateUser { get; set; }

    /// <summary>更新者</summary>
    Int32 UpdateUserID { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }

    /// <summary>更新地址</summary>
    String UpdateIP { get; set; }
    #endregion
}
