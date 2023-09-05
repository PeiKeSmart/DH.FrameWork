using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;
using NewLife.Reflection;

namespace DH.Entity;

/// <summary>SlugURL记录</summary>
public partial class UrlRecordModel : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>对应实体标识符</summary>
    public Int32 EntityId { get; set; }

    /// <summary>对应实体名称</summary>
    public String EntityName { get; set; }

    /// <summary>分段名称</summary>
    public String Slug { get; set; }

    /// <summary>是否处于活动状态</summary>
    public Boolean IsActive { get; set; }

    /// <summary>语言标识符</summary>
    public Int32 LanguageId { get; set; }

    /// <summary>创建者</summary>
    public String CreateUser { get; set; }

    /// <summary>创建用户</summary>
    public Int32 CreateUserID { get; set; }

    /// <summary>创建地址</summary>
    public String CreateIP { get; set; }

    /// <summary>创建时间</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>更新者</summary>
    public String UpdateUser { get; set; }

    /// <summary>更新用户</summary>
    public Int32 UpdateUserID { get; set; }

    /// <summary>更新地址</summary>
    public String UpdateIP { get; set; }

    /// <summary>更新时间</summary>
    public DateTime UpdateTime { get; set; }
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
                "EntityId" => EntityId,
                "EntityName" => EntityName,
                "Slug" => Slug,
                "IsActive" => IsActive,
                "LanguageId" => LanguageId,
                "CreateUser" => CreateUser,
                "CreateUserID" => CreateUserID,
                "CreateIP" => CreateIP,
                "CreateTime" => CreateTime,
                "UpdateUser" => UpdateUser,
                "UpdateUserID" => UpdateUserID,
                "UpdateIP" => UpdateIP,
                "UpdateTime" => UpdateTime,
                _ => this.GetValue(name),
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "EntityId": EntityId = value.ToInt(); break;
                case "EntityName": EntityName = Convert.ToString(value); break;
                case "Slug": Slug = Convert.ToString(value); break;
                case "IsActive": IsActive = value.ToBoolean(); break;
                case "LanguageId": LanguageId = value.ToInt(); break;
                case "CreateUser": CreateUser = Convert.ToString(value); break;
                case "CreateUserID": CreateUserID = value.ToInt(); break;
                case "CreateIP": CreateIP = Convert.ToString(value); break;
                case "CreateTime": CreateTime = value.ToDateTime(); break;
                case "UpdateUser": UpdateUser = Convert.ToString(value); break;
                case "UpdateUserID": UpdateUserID = value.ToInt(); break;
                case "UpdateIP": UpdateIP = Convert.ToString(value); break;
                case "UpdateTime": UpdateTime = value.ToDateTime(); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IUrlRecord model)
    {
        Id = model.Id;
        EntityId = model.EntityId;
        EntityName = model.EntityName;
        Slug = model.Slug;
        IsActive = model.IsActive;
        LanguageId = model.LanguageId;
        CreateUser = model.CreateUser;
        CreateUserID = model.CreateUserID;
        CreateIP = model.CreateIP;
        CreateTime = model.CreateTime;
        UpdateUser = model.UpdateUser;
        UpdateUserID = model.UpdateUserID;
        UpdateIP = model.UpdateIP;
        UpdateTime = model.UpdateTime;
    }
    #endregion
}
