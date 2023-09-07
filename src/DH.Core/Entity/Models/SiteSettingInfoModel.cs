using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>站点设置</summary>
public partial class SiteSettingInfoModel : ISiteSettingInfo
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>键名称</summary>
    public String Key { get; set; }

    /// <summary>键值</summary>
    public String Value { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISiteSettingInfo model)
    {
        Id = model.Id;
        Key = model.Key;
        Value = model.Value;
    }
    #endregion
}
