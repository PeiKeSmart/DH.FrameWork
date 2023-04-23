using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>站点设置</summary>
public partial interface ISiteSettingInfo
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>键名称</summary>
    String Key { get; set; }

    /// <summary>键值</summary>
    String Value { get; set; }
    #endregion
}
