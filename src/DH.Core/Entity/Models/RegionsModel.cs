using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>全球区域</summary>
public partial class RegionsModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>国家编号</summary>
    public Int32 CId { get; set; }

    /// <summary>层级</summary>
    public Int32 Level { get; set; }

    /// <summary>父级行政代码</summary>
    public Int64 ParentCode { get; set; }

    /// <summary>行政代码</summary>
    public Int64 AreaCode { get; set; }

    /// <summary>邮政编码</summary>
    public String ZipCode { get; set; }

    /// <summary>区号</summary>
    public String CityCode { get; set; }

    /// <summary>名称</summary>
    public String Name { get; set; }

    /// <summary>别名</summary>
    public String AliasName { get; set; }

    /// <summary>简称</summary>
    public String ShortName { get; set; }

    /// <summary>自定义简称</summary>
    public String OtherName { get; set; }

    /// <summary>大区名称</summary>
    public String Regional { get; set; }

    /// <summary>组合名</summary>
    public String MergerName { get; set; }

    /// <summary>拼音</summary>
    public String PinYin { get; set; }

    /// <summary>经度</summary>
    public Decimal Lng { get; set; }

    /// <summary>纬度</summary>
    public Decimal Lat { get; set; }

    /// <summary>城市Id</summary>
    public Int32 CityId { get; set; }

    /// <summary>排序</summary>
    public Int32 Sort { get; set; }

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
    public void Copy(IRegions model)
    {
        Id = model.Id;
        CId = model.CId;
        Level = model.Level;
        ParentCode = model.ParentCode;
        AreaCode = model.AreaCode;
        ZipCode = model.ZipCode;
        CityCode = model.CityCode;
        Name = model.Name;
        AliasName = model.AliasName;
        ShortName = model.ShortName;
        OtherName = model.OtherName;
        Regional = model.Regional;
        MergerName = model.MergerName;
        PinYin = model.PinYin;
        Lng = model.Lng;
        Lat = model.Lat;
        CityId = model.CityId;
        Sort = model.Sort;
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
