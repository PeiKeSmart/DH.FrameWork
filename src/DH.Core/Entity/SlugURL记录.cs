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
[BindIndex("IX_DG_UrlRecord_Slug", false, "Slug")]
[BindIndex("IX_DG_UrlRecord_EntityId_EntityName_LanguageId", false, "EntityId,EntityName,LanguageId")]
[BindTable("DG_UrlRecord", Description = "SlugURL记录", ConnName = "DG", DbType = DatabaseType.None)]
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

    private String _CreateUser;
    /// <summary>创建者</summary>
    [DisplayName("创建者")]
    [Description("创建者")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateUser", "创建者", "")]
    public String CreateUser { get => _CreateUser; set { if (OnPropertyChanging("CreateUser", value)) { _CreateUser = value; OnPropertyChanged("CreateUser"); } } }

    private Int32 _CreateUserID;
    /// <summary>创建用户</summary>
    [DisplayName("创建用户")]
    [Description("创建用户")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CreateUserID", "创建用户", "")]
    public Int32 CreateUserID { get => _CreateUserID; set { if (OnPropertyChanging("CreateUserID", value)) { _CreateUserID = value; OnPropertyChanged("CreateUserID"); } } }

    private String _CreateIP;
    /// <summary>创建地址</summary>
    [DisplayName("创建地址")]
    [Description("创建地址")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateIP", "创建地址", "")]
    public String CreateIP { get => _CreateIP; set { if (OnPropertyChanging("CreateIP", value)) { _CreateIP = value; OnPropertyChanged("CreateIP"); } } }

    private DateTime _CreateTime;
    /// <summary>创建时间</summary>
    [DisplayName("创建时间")]
    [Description("创建时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("CreateTime", "创建时间", "")]
    public DateTime CreateTime { get => _CreateTime; set { if (OnPropertyChanging("CreateTime", value)) { _CreateTime = value; OnPropertyChanged("CreateTime"); } } }

    private String _UpdateUser;
    /// <summary>更新者</summary>
    [DisplayName("更新者")]
    [Description("更新者")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("UpdateUser", "更新者", "")]
    public String UpdateUser { get => _UpdateUser; set { if (OnPropertyChanging("UpdateUser", value)) { _UpdateUser = value; OnPropertyChanged("UpdateUser"); } } }

    private Int32 _UpdateUserID;
    /// <summary>更新用户</summary>
    [DisplayName("更新用户")]
    [Description("更新用户")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("UpdateUserID", "更新用户", "")]
    public Int32 UpdateUserID { get => _UpdateUserID; set { if (OnPropertyChanging("UpdateUserID", value)) { _UpdateUserID = value; OnPropertyChanged("UpdateUserID"); } } }

    private String _UpdateIP;
    /// <summary>更新地址</summary>
    [DisplayName("更新地址")]
    [Description("更新地址")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("UpdateIP", "更新地址", "")]
    public String UpdateIP { get => _UpdateIP; set { if (OnPropertyChanging("UpdateIP", value)) { _UpdateIP = value; OnPropertyChanged("UpdateIP"); } } }

    private DateTime _UpdateTime;
    /// <summary>更新时间</summary>
    [DisplayName("更新时间")]
    [Description("更新时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("UpdateTime", "更新时间", "")]
    public DateTime UpdateTime { get => _UpdateTime; set { if (OnPropertyChanging("UpdateTime", value)) { _UpdateTime = value; OnPropertyChanged("UpdateTime"); } } }
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
            "CreateUser" => _CreateUser,
            "CreateUserID" => _CreateUserID,
            "CreateIP" => _CreateIP,
            "CreateTime" => _CreateTime,
            "UpdateUser" => _UpdateUser,
            "UpdateUserID" => _UpdateUserID,
            "UpdateIP" => _UpdateIP,
            "UpdateTime" => _UpdateTime,
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
                case "CreateUser": _CreateUser = Convert.ToString(value); break;
                case "CreateUserID": _CreateUserID = value.ToInt(); break;
                case "CreateIP": _CreateIP = Convert.ToString(value); break;
                case "CreateTime": _CreateTime = value.ToDateTime(); break;
                case "UpdateUser": _UpdateUser = Convert.ToString(value); break;
                case "UpdateUserID": _UpdateUserID = value.ToInt(); break;
                case "UpdateIP": _UpdateIP = Convert.ToString(value); break;
                case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
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

        /// <summary>创建者</summary>
        public static readonly Field CreateUser = FindByName("CreateUser");

        /// <summary>创建用户</summary>
        public static readonly Field CreateUserID = FindByName("CreateUserID");

        /// <summary>创建地址</summary>
        public static readonly Field CreateIP = FindByName("CreateIP");

        /// <summary>创建时间</summary>
        public static readonly Field CreateTime = FindByName("CreateTime");

        /// <summary>更新者</summary>
        public static readonly Field UpdateUser = FindByName("UpdateUser");

        /// <summary>更新用户</summary>
        public static readonly Field UpdateUserID = FindByName("UpdateUserID");

        /// <summary>更新地址</summary>
        public static readonly Field UpdateIP = FindByName("UpdateIP");

        /// <summary>更新时间</summary>
        public static readonly Field UpdateTime = FindByName("UpdateTime");

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

        /// <summary>创建者</summary>
        public const String CreateUser = "CreateUser";

        /// <summary>创建用户</summary>
        public const String CreateUserID = "CreateUserID";

        /// <summary>创建地址</summary>
        public const String CreateIP = "CreateIP";

        /// <summary>创建时间</summary>
        public const String CreateTime = "CreateTime";

        /// <summary>更新者</summary>
        public const String UpdateUser = "UpdateUser";

        /// <summary>更新用户</summary>
        public const String UpdateUserID = "UpdateUserID";

        /// <summary>更新地址</summary>
        public const String UpdateIP = "UpdateIP";

        /// <summary>更新时间</summary>
        public const String UpdateTime = "UpdateTime";
    }
    #endregion
}
