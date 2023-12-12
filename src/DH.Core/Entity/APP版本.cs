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

/// <summary>APP版本</summary>
[Serializable]
[DataObject]
[Description("APP版本")]
[BindTable("DG_AppVersion", Description = "APP版本", ConnName = "DG", DbType = DatabaseType.None)]
public partial class AppVersion : IAppVersion, IEntity<IAppVersion>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _Version;
    /// <summary>版本号</summary>
    [DisplayName("版本号")]
    [Description("版本号")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Version", "版本号", "")]
    public String Version { get => _Version; set { if (OnPropertyChanging("Version", value)) { _Version = value; OnPropertyChanged("Version"); } } }

    private String _Content;
    /// <summary>升级内容</summary>
    [DisplayName("升级内容")]
    [Description("升级内容")]
    [DataObjectField(false, false, true, 1024)]
    [BindColumn("Content", "升级内容", "")]
    public String Content { get => _Content; set { if (OnPropertyChanging("Content", value)) { _Content = value; OnPropertyChanged("Content"); } } }

    private String _FilePath;
    /// <summary>下载地址</summary>
    [DisplayName("下载地址")]
    [Description("下载地址")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("FilePath", "下载地址", "")]
    public String FilePath { get => _FilePath; set { if (OnPropertyChanging("FilePath", value)) { _FilePath = value; OnPropertyChanged("FilePath"); } } }

    private String _CstFilepath;
    /// <summary>第三方平台下载地址</summary>
    [DisplayName("第三方平台下载地址")]
    [Description("第三方平台下载地址")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("CST_FilePath", "第三方平台下载地址", "")]
    public String CstFilepath { get => _CstFilepath; set { if (OnPropertyChanging("CstFilepath", value)) { _CstFilepath = value; OnPropertyChanged("CstFilepath"); } } }

    private String _FileName;
    /// <summary>文件名称</summary>
    [DisplayName("文件名称")]
    [Description("文件名称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("FileName", "文件名称", "")]
    public String FileName { get => _FileName; set { if (OnPropertyChanging("FileName", value)) { _FileName = value; OnPropertyChanged("FileName"); } } }

    private Boolean _IsQiangZhi;
    /// <summary>是否强制升级</summary>
    [DisplayName("是否强制升级")]
    [Description("是否强制升级")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsQiangZhi", "是否强制升级", "")]
    public Boolean IsQiangZhi { get => _IsQiangZhi; set { if (OnPropertyChanging("IsQiangZhi", value)) { _IsQiangZhi = value; OnPropertyChanged("IsQiangZhi"); } } }

    private Int32 _Size;
    /// <summary>文件大小</summary>
    [DisplayName("文件大小")]
    [Description("文件大小")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Size", "文件大小", "")]
    public Int32 Size { get => _Size; set { if (OnPropertyChanging("Size", value)) { _Size = value; OnPropertyChanged("Size"); } } }

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
    public void Copy(IAppVersion model)
    {
        Id = model.Id;
        Version = model.Version;
        Content = model.Content;
        FilePath = model.FilePath;
        CstFilepath = model.CstFilepath;
        FileName = model.FileName;
        IsQiangZhi = model.IsQiangZhi;
        Size = model.Size;
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
            "Version" => _Version,
            "Content" => _Content,
            "FilePath" => _FilePath,
            "CstFilepath" => _CstFilepath,
            "FileName" => _FileName,
            "IsQiangZhi" => _IsQiangZhi,
            "Size" => _Size,
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
                case "Version": _Version = Convert.ToString(value); break;
                case "Content": _Content = Convert.ToString(value); break;
                case "FilePath": _FilePath = Convert.ToString(value); break;
                case "CstFilepath": _CstFilepath = Convert.ToString(value); break;
                case "FileName": _FileName = Convert.ToString(value); break;
                case "IsQiangZhi": _IsQiangZhi = value.ToBoolean(); break;
                case "Size": _Size = value.ToInt(); break;
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
    /// <summary>取得APP版本字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>版本号</summary>
        public static readonly Field Version = FindByName("Version");

        /// <summary>升级内容</summary>
        public static readonly Field Content = FindByName("Content");

        /// <summary>下载地址</summary>
        public static readonly Field FilePath = FindByName("FilePath");

        /// <summary>第三方平台下载地址</summary>
        public static readonly Field CstFilepath = FindByName("CstFilepath");

        /// <summary>文件名称</summary>
        public static readonly Field FileName = FindByName("FileName");

        /// <summary>是否强制升级</summary>
        public static readonly Field IsQiangZhi = FindByName("IsQiangZhi");

        /// <summary>文件大小</summary>
        public static readonly Field Size = FindByName("Size");

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

    /// <summary>取得APP版本字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>版本号</summary>
        public const String Version = "Version";

        /// <summary>升级内容</summary>
        public const String Content = "Content";

        /// <summary>下载地址</summary>
        public const String FilePath = "FilePath";

        /// <summary>第三方平台下载地址</summary>
        public const String CstFilepath = "CstFilepath";

        /// <summary>文件名称</summary>
        public const String FileName = "FileName";

        /// <summary>是否强制升级</summary>
        public const String IsQiangZhi = "IsQiangZhi";

        /// <summary>文件大小</summary>
        public const String Size = "Size";

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
