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

/// <summary>语言类别</summary>
[Serializable]
[DataObject]
[Description("语言类别")]
[BindIndex("IU_DH_Language_UniqueSeoCode", true, "UniqueSeoCode")]
[BindIndex("IU_DH_Language_LanguageCulture", true, "LanguageCulture")]
[BindTable("DH_Language", Description = "语言类别", ConnName = "DG", DbType = DatabaseType.None)]
public partial class Language : ILanguage, IEntity<LanguageModel>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _Name;
    /// <summary>语言名称</summary>
    [DisplayName("语言名称")]
    [Description("语言名称")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("Name", "语言名称", "varchar(30)", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private String _DisplayName;
    /// <summary>显示名称</summary>
    [DisplayName("显示名称")]
    [Description("显示名称")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("DisplayName", "显示名称", "varchar(30)", Master = true)]
    public String DisplayName { get => _DisplayName; set { if (OnPropertyChanging("DisplayName", value)) { _DisplayName = value; OnPropertyChanged("DisplayName"); } } }

    private String _EnglishName;
    /// <summary>英文名称</summary>
    [DisplayName("英文名称")]
    [Description("英文名称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("EnglishName", "英文名称", "varchar(50)")]
    public String EnglishName { get => _EnglishName; set { if (OnPropertyChanging("EnglishName", value)) { _EnglishName = value; OnPropertyChanged("EnglishName"); } } }

    private String _FlagImageFileName;
    /// <summary>旗帜文件名</summary>
    [DisplayName("旗帜文件名")]
    [Description("旗帜文件名")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("FlagImageFileName", "旗帜文件名", "varchar(50)")]
    public String FlagImageFileName { get => _FlagImageFileName; set { if (OnPropertyChanging("FlagImageFileName", value)) { _FlagImageFileName = value; OnPropertyChanged("FlagImageFileName"); } } }

    private String _LanguageCulture;
    /// <summary>本地化语言标识</summary>
    [DisplayName("本地化语言标识")]
    [Description("本地化语言标识")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("LanguageCulture", "本地化语言标识", "varchar(30)")]
    public String LanguageCulture { get => _LanguageCulture; set { if (OnPropertyChanging("LanguageCulture", value)) { _LanguageCulture = value; OnPropertyChanged("LanguageCulture"); } } }

    private String _UniqueSeoCode;
    /// <summary>Url缩写</summary>
    [DisplayName("Url缩写")]
    [Description("Url缩写")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("UniqueSeoCode", "Url缩写", "varchar(30)")]
    public String UniqueSeoCode { get => _UniqueSeoCode; set { if (OnPropertyChanging("UniqueSeoCode", value)) { _UniqueSeoCode = value; OnPropertyChanged("UniqueSeoCode"); } } }

    private String _LangAbbreviation;
    /// <summary>语言简写</summary>
    [DisplayName("语言简写")]
    [Description("语言简写")]
    [DataObjectField(false, false, true, 10)]
    [BindColumn("LangAbbreviation", "语言简写", "varchar(10)")]
    public String LangAbbreviation { get => _LangAbbreviation; set { if (OnPropertyChanging("LangAbbreviation", value)) { _LangAbbreviation = value; OnPropertyChanged("LangAbbreviation"); } } }

    private String _Flag;
    /// <summary>旗帜</summary>
    [DisplayName("旗帜")]
    [Description("旗帜")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Flag", "旗帜", "varchar(100)")]
    public String Flag { get => _Flag; set { if (OnPropertyChanging("Flag", value)) { _Flag = value; OnPropertyChanged("Flag"); } } }

    private String _Domain;
    /// <summary>域名</summary>
    [DisplayName("域名")]
    [Description("域名")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Domain", "域名", "varchar(100)")]
    public String Domain { get => _Domain; set { if (OnPropertyChanging("Domain", value)) { _Domain = value; OnPropertyChanged("Domain"); } } }

    private Int32 _Lcid;
    /// <summary>LCID</summary>
    [DisplayName("LCID")]
    [Description("LCID")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Lcid", "LCID", "int(11)")]
    public Int32 Lcid { get => _Lcid; set { if (OnPropertyChanging("Lcid", value)) { _Lcid = value; OnPropertyChanged("Lcid"); } } }

    private Boolean _Status;
    /// <summary>状态。是否启用</summary>
    [DisplayName("状态")]
    [Description("状态。是否启用")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Status", "状态。是否启用", "")]
    public Boolean Status { get => _Status; set { if (OnPropertyChanging("Status", value)) { _Status = value; OnPropertyChanged("Status"); } } }

    private Boolean _Rtl;
    /// <summary>该语言是否支持从右到左</summary>
    [DisplayName("该语言是否支持从右到左")]
    [Description("该语言是否支持从右到左")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Rtl", "该语言是否支持从右到左", "")]
    public Boolean Rtl { get => _Rtl; set { if (OnPropertyChanging("Rtl", value)) { _Rtl = value; OnPropertyChanged("Rtl"); } } }

    private Int32 _DisplayOrder;
    /// <summary>排序</summary>
    [DisplayName("排序")]
    [Description("排序")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("DisplayOrder", "排序", "int(11)")]
    public Int32 DisplayOrder { get => _DisplayOrder; set { if (OnPropertyChanging("DisplayOrder", value)) { _DisplayOrder = value; OnPropertyChanged("DisplayOrder"); } } }

    private Byte _IsDefault;
    /// <summary>是否网站打开默认语言</summary>
    [DisplayName("是否网站打开默认语言")]
    [Description("是否网站打开默认语言")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsDefault", "是否网站打开默认语言", "tinyint(1)")]
    public Byte IsDefault { get => _IsDefault; set { if (OnPropertyChanging("IsDefault", value)) { _IsDefault = value; OnPropertyChanged("IsDefault"); } } }

    private String _Remark;
    /// <summary>描述</summary>
    [DisplayName("描述")]
    [Description("描述")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("Remark", "描述", "varchar(255)")]
    public String Remark { get => _Remark; set { if (OnPropertyChanging("Remark", value)) { _Remark = value; OnPropertyChanged("Remark"); } } }

    private String _CreateUser;
    /// <summary>创建者</summary>
    [DisplayName("创建者")]
    [Description("创建者")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateUser", "创建者", "")]
    public String CreateUser { get => _CreateUser; set { if (OnPropertyChanging("CreateUser", value)) { _CreateUser = value; OnPropertyChanged("CreateUser"); } } }

    private Int32 _CreateUserID;
    /// <summary>创建者</summary>
    [DisplayName("创建者")]
    [Description("创建者")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CreateUserID", "创建者", "")]
    public Int32 CreateUserID { get => _CreateUserID; set { if (OnPropertyChanging("CreateUserID", value)) { _CreateUserID = value; OnPropertyChanged("CreateUserID"); } } }

    private DateTime _CreateTime;
    /// <summary>创建时间</summary>
    [DisplayName("创建时间")]
    [Description("创建时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("CreateTime", "创建时间", "")]
    public DateTime CreateTime { get => _CreateTime; set { if (OnPropertyChanging("CreateTime", value)) { _CreateTime = value; OnPropertyChanged("CreateTime"); } } }

    private String _CreateIP;
    /// <summary>创建地址</summary>
    [DisplayName("创建地址")]
    [Description("创建地址")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateIP", "创建地址", "")]
    public String CreateIP { get => _CreateIP; set { if (OnPropertyChanging("CreateIP", value)) { _CreateIP = value; OnPropertyChanged("CreateIP"); } } }

    private String _UpdateUser;
    /// <summary>更新者</summary>
    [DisplayName("更新者")]
    [Description("更新者")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("UpdateUser", "更新者", "")]
    public String UpdateUser { get => _UpdateUser; set { if (OnPropertyChanging("UpdateUser", value)) { _UpdateUser = value; OnPropertyChanged("UpdateUser"); } } }

    private Int32 _UpdateUserID;
    /// <summary>更新者</summary>
    [DisplayName("更新者")]
    [Description("更新者")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("UpdateUserID", "更新者", "")]
    public Int32 UpdateUserID { get => _UpdateUserID; set { if (OnPropertyChanging("UpdateUserID", value)) { _UpdateUserID = value; OnPropertyChanged("UpdateUserID"); } } }

    private DateTime _UpdateTime;
    /// <summary>更新时间</summary>
    [DisplayName("更新时间")]
    [Description("更新时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("UpdateTime", "更新时间", "")]
    public DateTime UpdateTime { get => _UpdateTime; set { if (OnPropertyChanging("UpdateTime", value)) { _UpdateTime = value; OnPropertyChanged("UpdateTime"); } } }

    private String _UpdateIP;
    /// <summary>更新地址</summary>
    [DisplayName("更新地址")]
    [Description("更新地址")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("UpdateIP", "更新地址", "")]
    public String UpdateIP { get => _UpdateIP; set { if (OnPropertyChanging("UpdateIP", value)) { _UpdateIP = value; OnPropertyChanged("UpdateIP"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(LanguageModel model)
    {
        Id = model.Id;
        Name = model.Name;
        DisplayName = model.DisplayName;
        EnglishName = model.EnglishName;
        FlagImageFileName = model.FlagImageFileName;
        LanguageCulture = model.LanguageCulture;
        UniqueSeoCode = model.UniqueSeoCode;
        LangAbbreviation = model.LangAbbreviation;
        Flag = model.Flag;
        Domain = model.Domain;
        Lcid = model.Lcid;
        Status = model.Status;
        Rtl = model.Rtl;
        DisplayOrder = model.DisplayOrder;
        IsDefault = model.IsDefault;
        Remark = model.Remark;
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

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public override Object this[String name]
    {
        get => name switch
        {
            "Id" => _Id,
            "Name" => _Name,
            "DisplayName" => _DisplayName,
            "EnglishName" => _EnglishName,
            "FlagImageFileName" => _FlagImageFileName,
            "LanguageCulture" => _LanguageCulture,
            "UniqueSeoCode" => _UniqueSeoCode,
            "LangAbbreviation" => _LangAbbreviation,
            "Flag" => _Flag,
            "Domain" => _Domain,
            "Lcid" => _Lcid,
            "Status" => _Status,
            "Rtl" => _Rtl,
            "DisplayOrder" => _DisplayOrder,
            "IsDefault" => _IsDefault,
            "Remark" => _Remark,
            "CreateUser" => _CreateUser,
            "CreateUserID" => _CreateUserID,
            "CreateTime" => _CreateTime,
            "CreateIP" => _CreateIP,
            "UpdateUser" => _UpdateUser,
            "UpdateUserID" => _UpdateUserID,
            "UpdateTime" => _UpdateTime,
            "UpdateIP" => _UpdateIP,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "DisplayName": _DisplayName = Convert.ToString(value); break;
                case "EnglishName": _EnglishName = Convert.ToString(value); break;
                case "FlagImageFileName": _FlagImageFileName = Convert.ToString(value); break;
                case "LanguageCulture": _LanguageCulture = Convert.ToString(value); break;
                case "UniqueSeoCode": _UniqueSeoCode = Convert.ToString(value); break;
                case "LangAbbreviation": _LangAbbreviation = Convert.ToString(value); break;
                case "Flag": _Flag = Convert.ToString(value); break;
                case "Domain": _Domain = Convert.ToString(value); break;
                case "Lcid": _Lcid = value.ToInt(); break;
                case "Status": _Status = value.ToBoolean(); break;
                case "Rtl": _Rtl = value.ToBoolean(); break;
                case "DisplayOrder": _DisplayOrder = value.ToInt(); break;
                case "IsDefault": _IsDefault = Convert.ToByte(value); break;
                case "Remark": _Remark = Convert.ToString(value); break;
                case "CreateUser": _CreateUser = Convert.ToString(value); break;
                case "CreateUserID": _CreateUserID = value.ToInt(); break;
                case "CreateTime": _CreateTime = value.ToDateTime(); break;
                case "CreateIP": _CreateIP = Convert.ToString(value); break;
                case "UpdateUser": _UpdateUser = Convert.ToString(value); break;
                case "UpdateUserID": _UpdateUserID = value.ToInt(); break;
                case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                case "UpdateIP": _UpdateIP = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得语言类别字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>语言名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>显示名称</summary>
        public static readonly Field DisplayName = FindByName("DisplayName");

        /// <summary>英文名称</summary>
        public static readonly Field EnglishName = FindByName("EnglishName");

        /// <summary>旗帜文件名</summary>
        public static readonly Field FlagImageFileName = FindByName("FlagImageFileName");

        /// <summary>本地化语言标识</summary>
        public static readonly Field LanguageCulture = FindByName("LanguageCulture");

        /// <summary>Url缩写</summary>
        public static readonly Field UniqueSeoCode = FindByName("UniqueSeoCode");

        /// <summary>语言简写</summary>
        public static readonly Field LangAbbreviation = FindByName("LangAbbreviation");

        /// <summary>旗帜</summary>
        public static readonly Field Flag = FindByName("Flag");

        /// <summary>域名</summary>
        public static readonly Field Domain = FindByName("Domain");

        /// <summary>LCID</summary>
        public static readonly Field Lcid = FindByName("Lcid");

        /// <summary>状态。是否启用</summary>
        public static readonly Field Status = FindByName("Status");

        /// <summary>该语言是否支持从右到左</summary>
        public static readonly Field Rtl = FindByName("Rtl");

        /// <summary>排序</summary>
        public static readonly Field DisplayOrder = FindByName("DisplayOrder");

        /// <summary>是否网站打开默认语言</summary>
        public static readonly Field IsDefault = FindByName("IsDefault");

        /// <summary>描述</summary>
        public static readonly Field Remark = FindByName("Remark");

        /// <summary>创建者</summary>
        public static readonly Field CreateUser = FindByName("CreateUser");

        /// <summary>创建者</summary>
        public static readonly Field CreateUserID = FindByName("CreateUserID");

        /// <summary>创建时间</summary>
        public static readonly Field CreateTime = FindByName("CreateTime");

        /// <summary>创建地址</summary>
        public static readonly Field CreateIP = FindByName("CreateIP");

        /// <summary>更新者</summary>
        public static readonly Field UpdateUser = FindByName("UpdateUser");

        /// <summary>更新者</summary>
        public static readonly Field UpdateUserID = FindByName("UpdateUserID");

        /// <summary>更新时间</summary>
        public static readonly Field UpdateTime = FindByName("UpdateTime");

        /// <summary>更新地址</summary>
        public static readonly Field UpdateIP = FindByName("UpdateIP");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得语言类别字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>语言名称</summary>
        public const String Name = "Name";

        /// <summary>显示名称</summary>
        public const String DisplayName = "DisplayName";

        /// <summary>英文名称</summary>
        public const String EnglishName = "EnglishName";

        /// <summary>旗帜文件名</summary>
        public const String FlagImageFileName = "FlagImageFileName";

        /// <summary>本地化语言标识</summary>
        public const String LanguageCulture = "LanguageCulture";

        /// <summary>Url缩写</summary>
        public const String UniqueSeoCode = "UniqueSeoCode";

        /// <summary>语言简写</summary>
        public const String LangAbbreviation = "LangAbbreviation";

        /// <summary>旗帜</summary>
        public const String Flag = "Flag";

        /// <summary>域名</summary>
        public const String Domain = "Domain";

        /// <summary>LCID</summary>
        public const String Lcid = "Lcid";

        /// <summary>状态。是否启用</summary>
        public const String Status = "Status";

        /// <summary>该语言是否支持从右到左</summary>
        public const String Rtl = "Rtl";

        /// <summary>排序</summary>
        public const String DisplayOrder = "DisplayOrder";

        /// <summary>是否网站打开默认语言</summary>
        public const String IsDefault = "IsDefault";

        /// <summary>描述</summary>
        public const String Remark = "Remark";

        /// <summary>创建者</summary>
        public const String CreateUser = "CreateUser";

        /// <summary>创建者</summary>
        public const String CreateUserID = "CreateUserID";

        /// <summary>创建时间</summary>
        public const String CreateTime = "CreateTime";

        /// <summary>创建地址</summary>
        public const String CreateIP = "CreateIP";

        /// <summary>更新者</summary>
        public const String UpdateUser = "UpdateUser";

        /// <summary>更新者</summary>
        public const String UpdateUserID = "UpdateUserID";

        /// <summary>更新时间</summary>
        public const String UpdateTime = "UpdateTime";

        /// <summary>更新地址</summary>
        public const String UpdateIP = "UpdateIP";
    }
    #endregion
}
