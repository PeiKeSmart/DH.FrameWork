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

/// <summary>支付方式</summary>
[Serializable]
[DataObject]
[Description("支付方式")]
[BindIndex("PRIMARY", true, "Id")]
[BindTable("DG_Payment", Description = "支付方式", ConnName = "DG", DbType = DatabaseType.None)]
public partial class Payment : IPayment, IEntity<IPayment>
{
    #region 属性
    private String _Id;
    /// <summary>支付代码</summary>
    [DisplayName("支付代码")]
    [Description("支付代码")]
    [DataObjectField(true, false, false, 20)]
    [BindColumn("Id", "支付代码", "")]
    public String Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _Name;
    /// <summary>支付名称</summary>
    [DisplayName("支付名称")]
    [Description("支付名称")]
    [DataObjectField(false, false, false, 20)]
    [BindColumn("Name", "支付名称", "", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private String _Config;
    /// <summary>支付接口配置信息</summary>
    [DisplayName("支付接口配置信息")]
    [Description("支付接口配置信息")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("Config", "支付接口配置信息", "text")]
    public String Config { get => _Config; set { if (OnPropertyChanging("Config", value)) { _Config = value; OnPropertyChanged("Config"); } } }

    private String _Platform;
    /// <summary>支付方式所适应平台 pc h5 app wm</summary>
    [DisplayName("支付方式所适应平台pch5appwm")]
    [Description("支付方式所适应平台 pc h5 app wm")]
    [DataObjectField(false, false, true, 10)]
    [BindColumn("Platform", "支付方式所适应平台 pc h5 app wm", "")]
    public String Platform { get => _Platform; set { if (OnPropertyChanging("Platform", value)) { _Platform = value; OnPropertyChanged("Platform"); } } }

    private String _LanguageIds;
    /// <summary>支持的语言Id集合</summary>
    [DisplayName("支持的语言Id集合")]
    [Description("支持的语言Id集合")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("LanguageIds", "支持的语言Id集合", "")]
    public String LanguageIds { get => _LanguageIds; set { if (OnPropertyChanging("LanguageIds", value)) { _LanguageIds = value; OnPropertyChanged("LanguageIds"); } } }

    private Boolean _State;
    /// <summary>接口状态,是否启用</summary>
    [DisplayName("接口状态")]
    [Description("接口状态,是否启用")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("State", "接口状态,是否启用", "")]
    public Boolean State { get => _State; set { if (OnPropertyChanging("State", value)) { _State = value; OnPropertyChanged("State"); } } }

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
    public void Copy(IPayment model)
    {
        Id = model.Id;
        Name = model.Name;
        Config = model.Config;
        Platform = model.Platform;
        LanguageIds = model.LanguageIds;
        State = model.State;
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
            "Config" => _Config,
            "Platform" => _Platform,
            "LanguageIds" => _LanguageIds,
            "State" => _State,
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
                case "Id": _Id = Convert.ToString(value); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "Config": _Config = Convert.ToString(value); break;
                case "Platform": _Platform = Convert.ToString(value); break;
                case "LanguageIds": _LanguageIds = Convert.ToString(value); break;
                case "State": _State = value.ToBoolean(); break;
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

    #region 扩展查询
    #endregion

    #region 字段名
    /// <summary>取得支付方式字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>支付代码</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>支付名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>支付接口配置信息</summary>
        public static readonly Field Config = FindByName("Config");

        /// <summary>支付方式所适应平台 pc h5 app wm</summary>
        public static readonly Field Platform = FindByName("Platform");

        /// <summary>支持的语言Id集合</summary>
        public static readonly Field LanguageIds = FindByName("LanguageIds");

        /// <summary>接口状态,是否启用</summary>
        public static readonly Field State = FindByName("State");

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

    /// <summary>取得支付方式字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>支付代码</summary>
        public const String Id = "Id";

        /// <summary>支付名称</summary>
        public const String Name = "Name";

        /// <summary>支付接口配置信息</summary>
        public const String Config = "Config";

        /// <summary>支付方式所适应平台 pc h5 app wm</summary>
        public const String Platform = "Platform";

        /// <summary>支持的语言Id集合</summary>
        public const String LanguageIds = "LanguageIds";

        /// <summary>接口状态,是否启用</summary>
        public const String State = "State";

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
