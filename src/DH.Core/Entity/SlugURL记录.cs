using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace DH.Entity;

/// <summary>SlugURL记录</summary>
[Serializable]
[DataObject]
[Description("SlugURL记录")]
[BindIndex("IX_DH_UrlRecord_Slug", false, "Slug")]
[BindIndex("IX_DH_UrlRecord_EntityId_EntityName_LanguageId", false, "EntityId,EntityName,LanguageId")]
[BindTable("DH_UrlRecord", Description = "SlugURL记录", ConnName = "DG", DbType = DatabaseType.None)]
public partial class UrlRecord : IUrlRecord, IEntity<UrlRecordModel>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Int32 _EntityId;
    /// <summary>对应实体标识符</summary>
    [DisplayName("对应实体标识符")]
    [Description("对应实体标识符")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("EntityId", "对应实体标识符", "")]
    public Int32 EntityId { get => _EntityId; set { if (OnPropertyChanging("EntityId", value)) { _EntityId = value; OnPropertyChanged("EntityId"); } } }

    private String _EntityName;
    /// <summary>对应实体名称</summary>
    [DisplayName("对应实体名称")]
    [Description("对应实体名称")]
    [DataObjectField(false, false, true, 400)]
    [BindColumn("EntityName", "对应实体名称", "", Master = true)]
    public String EntityName { get => _EntityName; set { if (OnPropertyChanging("EntityName", value)) { _EntityName = value; OnPropertyChanged("EntityName"); } } }

    private String _Slug;
    /// <summary>分段名称</summary>
    [DisplayName("分段名称")]
    [Description("分段名称")]
    [DataObjectField(false, false, true, 400)]
    [BindColumn("Slug", "分段名称", "")]
    public String Slug { get => _Slug; set { if (OnPropertyChanging("Slug", value)) { _Slug = value; OnPropertyChanged("Slug"); } } }

    private Boolean _IsActive;
    /// <summary>是否处于活动状态</summary>
    [DisplayName("是否处于活动状态")]
    [Description("是否处于活动状态")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsActive", "是否处于活动状态", "")]
    public Boolean IsActive { get => _IsActive; set { if (OnPropertyChanging("IsActive", value)) { _IsActive = value; OnPropertyChanged("IsActive"); } } }

    private Int32 _LanguageId;
    /// <summary>语言标识符</summary>
    [DisplayName("语言标识符")]
    [Description("语言标识符")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("LanguageId", "语言标识符", "")]
    public Int32 LanguageId { get => _LanguageId; set { if (OnPropertyChanging("LanguageId", value)) { _LanguageId = value; OnPropertyChanged("LanguageId"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(UrlRecordModel model)
    {
        Id = model.Id;
        EntityId = model.EntityId;
        EntityName = model.EntityName;
        Slug = model.Slug;
        IsActive = model.IsActive;
        LanguageId = model.LanguageId;
    }
    #endregion

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public override Object this[String name]
    {
        get => name switch
        {
            "Id" => _Id,
            "EntityId" => _EntityId,
            "EntityName" => _EntityName,
            "Slug" => _Slug,
            "IsActive" => _IsActive,
            "LanguageId" => _LanguageId,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "EntityId": _EntityId = value.ToInt(); break;
                case "EntityName": _EntityName = Convert.ToString(value); break;
                case "Slug": _Slug = Convert.ToString(value); break;
                case "IsActive": _IsActive = value.ToBoolean(); break;
                case "LanguageId": _LanguageId = value.ToInt(); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得SlugURL记录字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>对应实体标识符</summary>
        public static readonly Field EntityId = FindByName("EntityId");

        /// <summary>对应实体名称</summary>
        public static readonly Field EntityName = FindByName("EntityName");

        /// <summary>分段名称</summary>
        public static readonly Field Slug = FindByName("Slug");

        /// <summary>是否处于活动状态</summary>
        public static readonly Field IsActive = FindByName("IsActive");

        /// <summary>语言标识符</summary>
        public static readonly Field LanguageId = FindByName("LanguageId");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得SlugURL记录字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>对应实体标识符</summary>
        public const String EntityId = "EntityId";

        /// <summary>对应实体名称</summary>
        public const String EntityName = "EntityName";

        /// <summary>分段名称</summary>
        public const String Slug = "Slug";

        /// <summary>是否处于活动状态</summary>
        public const String IsActive = "IsActive";

        /// <summary>语言标识符</summary>
        public const String LanguageId = "LanguageId";
    }
    #endregion
}
