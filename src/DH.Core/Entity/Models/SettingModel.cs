using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;
using NewLife.Reflection;

namespace DH.Entity;

/// <summary>站点配置</summary>
public partial class SettingModel : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>键名称</summary>
    public String Name { get; set; }

    /// <summary>值</summary>
    public String Value { get; set; }

    /// <summary>站点Id</summary>
    public Int32 StoreId { get; set; }
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
                "Name" => Name,
                "Value" => Value,
                "StoreId" => StoreId,
                _ => this.GetValue(name),
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "Name": Name = Convert.ToString(value); break;
                case "Value": Value = Convert.ToString(value); break;
                case "StoreId": StoreId = value.ToInt(); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISetting model)
    {
        Id = model.Id;
        Name = model.Name;
        Value = model.Value;
        StoreId = model.StoreId;
    }
    #endregion
}
