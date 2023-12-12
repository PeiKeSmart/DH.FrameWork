using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>操作日志</summary>
public partial interface IUserLog
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>操作内容</summary>
    String Content { get; set; }

    /// <summary>操作Url</summary>
    String Url { get; set; }

    /// <summary>创建者</summary>
    String CreateUser { get; set; }

    /// <summary>创建用户</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }
    #endregion
}
