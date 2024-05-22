using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>APP版本翻译</summary>
public partial interface IAppVersionLan
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>APP版本Id</summary>
    Int32 AId { get; set; }

    /// <summary>关联所属语言Id</summary>
    Int32 LId { get; set; }

    /// <summary>升级内容</summary>
    String Content { get; set; }
    #endregion
}
