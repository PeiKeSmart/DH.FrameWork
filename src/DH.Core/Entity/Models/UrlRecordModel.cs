using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;

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
                _ => null
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
    }
    #endregion
}
