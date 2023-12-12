using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>全球区域</summary>
public partial interface IRegions
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>国家编号</summary>
    Int32 CId { get; set; }

    /// <summary>层级</summary>
    Int32 Level { get; set; }

    /// <summary>父级行政代码</summary>
    Int64 ParentCode { get; set; }

    /// <summary>行政代码</summary>
    Int64 AreaCode { get; set; }

    /// <summary>邮政编码</summary>
    String ZipCode { get; set; }

    /// <summary>区号</summary>
    String CityCode { get; set; }

    /// <summary>名称</summary>
    String Name { get; set; }

    /// <summary>别名</summary>
    String AliasName { get; set; }

    /// <summary>简称</summary>
    String ShortName { get; set; }

    /// <summary>自定义简称</summary>
    String OtherName { get; set; }

    /// <summary>大区名称</summary>
    String Regional { get; set; }

    /// <summary>组合名</summary>
    String MergerName { get; set; }

    /// <summary>拼音</summary>
    String PinYin { get; set; }

    /// <summary>经度</summary>
    Decimal Lng { get; set; }

    /// <summary>纬度</summary>
    Decimal Lat { get; set; }

    /// <summary>城市Id</summary>
    Int32 CityId { get; set; }

    /// <summary>排序</summary>
    Int32 Sort { get; set; }

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
