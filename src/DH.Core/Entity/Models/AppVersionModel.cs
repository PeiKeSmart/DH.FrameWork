using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>APP版本</summary>
public partial class AppVersionModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>App类型。1为Android，2为IOS</summary>
    public Int32 AType { get; set; }

    /// <summary>版本号</summary>
    public String Version { get; set; }

    /// <summary>升级内容</summary>
    public String Content { get; set; }

    /// <summary>下载地址</summary>
    public String FilePath { get; set; }

    /// <summary>国内第三方平台下载地址</summary>
    public String CstFilepath { get; set; }

    /// <summary>国外第三方平台下载地址</summary>
    public String ForeignCstFilepath { get; set; }

    /// <summary>APP包名</summary>
    public String BoundId { get; set; }

    /// <summary>文件名称</summary>
    public String FileName { get; set; }

    /// <summary>是否强制升级</summary>
    public Boolean IsQiangZhi { get; set; }

    /// <summary>文件大小</summary>
    public Int32 Size { get; set; }

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
    public void Copy(IAppVersion model)
    {
        Id = model.Id;
        AType = model.AType;
        Version = model.Version;
        Content = model.Content;
        FilePath = model.FilePath;
        CstFilepath = model.CstFilepath;
        ForeignCstFilepath = model.ForeignCstFilepath;
        BoundId = model.BoundId;
        FileName = model.FileName;
        IsQiangZhi = model.IsQiangZhi;
        Size = model.Size;
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
