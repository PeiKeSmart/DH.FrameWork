using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>附件。用于记录各系统模块使用的文件，可以是Local/NAS/OSS等</summary>
public partial interface IAttachment
{
    #region 属性
    /// <summary>编号</summary>
    Int64 Id { get; set; }

    /// <summary>业务分类</summary>
    String Category { get; set; }

    /// <summary>业务主键</summary>
    String Key { get; set; }

    /// <summary>标题。业务内容作为附件标题，便于查看管理</summary>
    String Title { get; set; }

    /// <summary>文件名。原始文件名</summary>
    String FileName { get; set; }

    /// <summary>扩展名</summary>
    String Extension { get; set; }

    /// <summary>文件大小</summary>
    Int64 Size { get; set; }

    /// <summary>内容类型。用于Http响应</summary>
    String ContentType { get; set; }

    /// <summary>路径。本地相对路径或OSS路径，本地相对路径加上附件目录的配置，方便整体转移附件</summary>
    String FilePath { get; set; }

    /// <summary>哈希。MD5</summary>
    String Hash { get; set; }

    /// <summary>启用</summary>
    Boolean Enable { get; set; }

    /// <summary>上传时间。附件上传时间，可用于构造文件存储路径</summary>
    DateTime UploadTime { get; set; }

    /// <summary>网址。链接到附件所在信息页的地址</summary>
    String Url { get; set; }

    /// <summary>来源。用于远程抓取的附件来源地址，本地文件不存在时自动依次抓取</summary>
    String Source { get; set; }

    /// <summary>创建者</summary>
    String CreateUser { get; set; }

    /// <summary>创建用户</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>更新者</summary>
    String UpdateUser { get; set; }

    /// <summary>更新用户</summary>
    Int32 UpdateUserID { get; set; }

    /// <summary>更新地址</summary>
    String UpdateIP { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }

    /// <summary>备注</summary>
    String Remark { get; set; }
    #endregion
}
