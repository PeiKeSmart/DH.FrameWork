using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>买家相册表</summary>
public partial interface ISnsAlbumClass
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>会员ID</summary>
    Int32 UId { get; set; }

    /// <summary>相册名称</summary>
    String Name { get; set; }

    /// <summary>相册描述</summary>
    String Des { get; set; }

    /// <summary>相册排序</summary>
    Int32 Sort { get; set; }

    /// <summary>相册封面</summary>
    String Cover { get; set; }

    /// <summary>是否为买家秀相册</summary>
    Boolean IsDefault { get; set; }

    /// <summary>创建者</summary>
    String CreateUser { get; set; }

    /// <summary>创建者</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }
    #endregion
}
