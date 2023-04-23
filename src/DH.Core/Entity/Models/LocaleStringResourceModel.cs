using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;

namespace DH.Entity;

/// <summary>语言包</summary>
public partial class LocaleStringResourceModel : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>资源名称</summary>
    public String LanKey { get; set; }

    /// <summary>资源值</summary>
    public String LanValue { get; set; }

    /// <summary>语言标识符</summary>
    public Int32 CultureId { get; set; }
    #endregion

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public virtual Object this[String name]
    {
        get
        {
            return name switch
            {
                "Id" => Id,
                "LanKey" => LanKey,
                "LanValue" => LanValue,
                "CultureId" => CultureId,
                _ => null
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "LanKey": LanKey = Convert.ToString(value); break;
                case "LanValue": LanValue = Convert.ToString(value); break;
                case "CultureId": CultureId = value.ToInt(); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ILocaleStringResource model)
    {
        Id = model.Id;
        LanKey = model.LanKey;
        LanValue = model.LanValue;
        CultureId = model.CultureId;
    }
    #endregion
}
