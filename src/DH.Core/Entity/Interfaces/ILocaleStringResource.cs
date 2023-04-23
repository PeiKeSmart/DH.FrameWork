using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>语言包</summary>
public partial interface ILocaleStringResource
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>资源名称</summary>
    String LanKey { get; set; }

    /// <summary>资源值</summary>
    String LanValue { get; set; }

    /// <summary>语言标识符</summary>
    Int32 CultureId { get; set; }
    #endregion
}
