using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>买家相册表</summary>
public partial class SnsAlbumClassModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>会员ID</summary>
    public Int32 UId { get; set; }

    /// <summary>相册名称</summary>
    public String Name { get; set; }

    /// <summary>相册描述</summary>
    public String Des { get; set; }

    /// <summary>相册排序</summary>
    public Int32 Sort { get; set; }

    /// <summary>相册封面</summary>
    public String Cover { get; set; }

    /// <summary>是否为买家秀相册</summary>
    public Boolean IsDefault { get; set; }

    /// <summary>创建者</summary>
    public String CreateUser { get; set; }

    /// <summary>创建者</summary>
    public Int32 CreateUserID { get; set; }

    /// <summary>创建时间</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    public String CreateIP { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISnsAlbumClass model)
    {
        Id = model.Id;
        UId = model.UId;
        Name = model.Name;
        Des = model.Des;
        Sort = model.Sort;
        Cover = model.Cover;
        IsDefault = model.IsDefault;
        CreateUser = model.CreateUser;
        CreateUserID = model.CreateUserID;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
    }
    #endregion
}
