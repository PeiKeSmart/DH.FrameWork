using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;
using NewLife.Reflection;

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
                "CreateUser" => CreateUser,
                "CreateUserID" => CreateUserID,
                "CreateTime" => CreateTime,
                "CreateIP" => CreateIP,
                "UpdateUser" => UpdateUser,
                "UpdateUserID" => UpdateUserID,
                "UpdateTime" => UpdateTime,
                "UpdateIP" => UpdateIP,
                _ => this.GetValue(name),
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
                case "CreateUser": CreateUser = Convert.ToString(value); break;
                case "CreateUserID": CreateUserID = value.ToInt(); break;
                case "CreateTime": CreateTime = value.ToDateTime(); break;
                case "CreateIP": CreateIP = Convert.ToString(value); break;
                case "UpdateUser": UpdateUser = Convert.ToString(value); break;
                case "UpdateUserID": UpdateUserID = value.ToInt(); break;
                case "UpdateTime": UpdateTime = value.ToDateTime(); break;
                case "UpdateIP": UpdateIP = Convert.ToString(value); break;
                default: this.SetValue(name, value); break;
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
