using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>APP版本</summary>
public partial interface IAppVersion
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>App类型。1为Android，2为IOS</summary>
    Int32 AType { get; set; }

    /// <summary>版本号</summary>
    String Version { get; set; }

    /// <summary>升级内容</summary>
    String Content { get; set; }

    /// <summary>下载地址</summary>
    String FilePath { get; set; }

    /// <summary>国内第三方平台下载地址</summary>
    String CstFilepath { get; set; }

    /// <summary>国外第三方平台下载地址</summary>
    String ForeignCstFilepath { get; set; }

    /// <summary>APP包名</summary>
    String BoundId { get; set; }

    /// <summary>文件名称</summary>
    String FileName { get; set; }

    /// <summary>是否强制升级</summary>
    Boolean IsQiangZhi { get; set; }

    /// <summary>文件大小</summary>
    Int32 Size { get; set; }

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
