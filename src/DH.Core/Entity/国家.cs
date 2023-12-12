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

/// <summary>国家</summary>
[Serializable]
[DataObject]
[Description("国家")]
[BindTable("DG_Country", Description = "国家", ConnName = "Regions", DbType = DatabaseType.None)]
public partial class Country : ICountry, IEntity<ICountry>
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
    /// <summary>国家名称</summary>
    [DisplayName("国家名称")]
    [Description("国家名称")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Name", "国家名称", "varchar(100)", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private String _TwoLetterIsoCode;
    /// <summary>两个字母ISO代码</summary>
    [DisplayName("两个字母ISO代码")]
    [Description("两个字母ISO代码")]
    [DataObjectField(false, false, true, 2)]
    [BindColumn("TwoLetterIsoCode", "两个字母ISO代码", "")]
    public String TwoLetterIsoCode { get => _TwoLetterIsoCode; set { if (OnPropertyChanging("TwoLetterIsoCode", value)) { _TwoLetterIsoCode = value; OnPropertyChanged("TwoLetterIsoCode"); } } }

    private String _ThreeLetterIsoCode;
    /// <summary>三个字母ISO代码</summary>
    [DisplayName("三个字母ISO代码")]
    [Description("三个字母ISO代码")]
    [DataObjectField(false, false, true, 3)]
    [BindColumn("ThreeLetterIsoCode", "三个字母ISO代码", "")]
    public String ThreeLetterIsoCode { get => _ThreeLetterIsoCode; set { if (OnPropertyChanging("ThreeLetterIsoCode", value)) { _ThreeLetterIsoCode = value; OnPropertyChanged("ThreeLetterIsoCode"); } } }

    private Int32 _DisplayOrder;
    /// <summary>排序</summary>
    [DisplayName("排序")]
    [Description("排序")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("DisplayOrder", "排序", "int(11)")]
    public Int32 DisplayOrder { get => _DisplayOrder; set { if (OnPropertyChanging("DisplayOrder", value)) { _DisplayOrder = value; OnPropertyChanged("DisplayOrder"); } } }

    private Int32 _NumericIsoCode;
    /// <summary>ISO数字代码</summary>
    [DisplayName("ISO数字代码")]
    [Description("ISO数字代码")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("NumericIsoCode", "ISO数字代码", "")]
    public Int32 NumericIsoCode { get => _NumericIsoCode; set { if (OnPropertyChanging("NumericIsoCode", value)) { _NumericIsoCode = value; OnPropertyChanged("NumericIsoCode"); } } }

    private Boolean _IsEnabled;
    /// <summary>是否启用</summary>
    [DisplayName("是否启用")]
    [Description("是否启用")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsEnabled", "是否启用", "")]
    public Boolean IsEnabled { get => _IsEnabled; set { if (OnPropertyChanging("IsEnabled", value)) { _IsEnabled = value; OnPropertyChanged("IsEnabled"); } } }

    private Boolean _IsDefault;
    /// <summary>是否默认</summary>
    [DisplayName("是否默认")]
    [Description("是否默认")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsDefault", "是否默认", "")]
    public Boolean IsDefault { get => _IsDefault; set { if (OnPropertyChanging("IsDefault", value)) { _IsDefault = value; OnPropertyChanged("IsDefault"); } } }

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
    public void Copy(ICountry model)
    {
        Id = model.Id;
        Name = model.Name;
        TwoLetterIsoCode = model.TwoLetterIsoCode;
        ThreeLetterIsoCode = model.ThreeLetterIsoCode;
        DisplayOrder = model.DisplayOrder;
        NumericIsoCode = model.NumericIsoCode;
        IsEnabled = model.IsEnabled;
        IsDefault = model.IsDefault;
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
            "TwoLetterIsoCode" => _TwoLetterIsoCode,
            "ThreeLetterIsoCode" => _ThreeLetterIsoCode,
            "DisplayOrder" => _DisplayOrder,
            "NumericIsoCode" => _NumericIsoCode,
            "IsEnabled" => _IsEnabled,
            "IsDefault" => _IsDefault,
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
                case "TwoLetterIsoCode": _TwoLetterIsoCode = Convert.ToString(value); break;
                case "ThreeLetterIsoCode": _ThreeLetterIsoCode = Convert.ToString(value); break;
                case "DisplayOrder": _DisplayOrder = value.ToInt(); break;
                case "NumericIsoCode": _NumericIsoCode = value.ToInt(); break;
                case "IsEnabled": _IsEnabled = value.ToBoolean(); break;
                case "IsDefault": _IsDefault = value.ToBoolean(); break;
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
    /// <summary>取得国家字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>国家名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>两个字母ISO代码</summary>
        public static readonly Field TwoLetterIsoCode = FindByName("TwoLetterIsoCode");

        /// <summary>三个字母ISO代码</summary>
        public static readonly Field ThreeLetterIsoCode = FindByName("ThreeLetterIsoCode");

        /// <summary>排序</summary>
        public static readonly Field DisplayOrder = FindByName("DisplayOrder");

        /// <summary>ISO数字代码</summary>
        public static readonly Field NumericIsoCode = FindByName("NumericIsoCode");

        /// <summary>是否启用</summary>
        public static readonly Field IsEnabled = FindByName("IsEnabled");

        /// <summary>是否默认</summary>
        public static readonly Field IsDefault = FindByName("IsDefault");

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

    /// <summary>取得国家字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>国家名称</summary>
        public const String Name = "Name";

        /// <summary>两个字母ISO代码</summary>
        public const String TwoLetterIsoCode = "TwoLetterIsoCode";

        /// <summary>三个字母ISO代码</summary>
        public const String ThreeLetterIsoCode = "ThreeLetterIsoCode";

        /// <summary>排序</summary>
        public const String DisplayOrder = "DisplayOrder";

        /// <summary>ISO数字代码</summary>
        public const String NumericIsoCode = "NumericIsoCode";

        /// <summary>是否启用</summary>
        public const String IsEnabled = "IsEnabled";

        /// <summary>是否默认</summary>
        public const String IsDefault = "IsDefault";

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
