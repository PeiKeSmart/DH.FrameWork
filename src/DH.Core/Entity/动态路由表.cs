﻿using System;
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

/// <summary>动态路由表</summary>
[Serializable]
[DataObject]
[Description("动态路由表")]
[BindIndex("IU_DH_DynamicRoute_RegexInfo", true, "RegexInfo")]
[BindTable("DH_DynamicRoute", Description = "动态路由表", ConnName = "DG", DbType = DatabaseType.None)]
public partial class DynamicRoute : IDynamicRoute, IEntity<IDynamicRoute>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _RegexInfo;
    /// <summary>正则表达式</summary>
    [DisplayName("正则表达式")]
    [Description("正则表达式")]
    [DataObjectField(false, false, false, 50)]
    [BindColumn("RegexInfo", "正则表达式", "")]
    public String RegexInfo { get => _RegexInfo; set { if (OnPropertyChanging("RegexInfo", value)) { _RegexInfo = value; OnPropertyChanged("RegexInfo"); } } }

    private String _Controller;
    /// <summary>控制器</summary>
    [DisplayName("控制器")]
    [Description("控制器")]
    [DataObjectField(false, false, false, 50)]
    [BindColumn("Controller", "控制器", "")]
    public String Controller { get => _Controller; set { if (OnPropertyChanging("Controller", value)) { _Controller = value; OnPropertyChanged("Controller"); } } }

    private String _Action;
    /// <summary>动作</summary>
    [DisplayName("动作")]
    [Description("动作")]
    [DataObjectField(false, false, false, 50)]
    [BindColumn("Action", "动作", "")]
    public String Action { get => _Action; set { if (OnPropertyChanging("Action", value)) { _Action = value; OnPropertyChanged("Action"); } } }

    private String _Area;
    /// <summary>区域</summary>
    [DisplayName("区域")]
    [Description("区域")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Area", "区域", "")]
    public String Area { get => _Area; set { if (OnPropertyChanging("Area", value)) { _Area = value; OnPropertyChanged("Area"); } } }

    private String _Other;
    /// <summary>其他参数</summary>
    [DisplayName("其他参数")]
    [Description("其他参数")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Other", "其他参数", "")]
    public String Other { get => _Other; set { if (OnPropertyChanging("Other", value)) { _Other = value; OnPropertyChanged("Other"); } } }

    private Boolean _Enable;
    /// <summary>是否启用</summary>
    [DisplayName("是否启用")]
    [Description("是否启用")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Enable", "是否启用", "")]
    public Boolean Enable { get => _Enable; set { if (OnPropertyChanging("Enable", value)) { _Enable = value; OnPropertyChanged("Enable"); } } }

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
    public void Copy(IDynamicRoute model)
    {
        Id = model.Id;
        RegexInfo = model.RegexInfo;
        Controller = model.Controller;
        Action = model.Action;
        Area = model.Area;
        Other = model.Other;
        Enable = model.Enable;
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
            "RegexInfo" => _RegexInfo,
            "Controller" => _Controller,
            "Action" => _Action,
            "Area" => _Area,
            "Other" => _Other,
            "Enable" => _Enable,
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
                case "RegexInfo": _RegexInfo = Convert.ToString(value); break;
                case "Controller": _Controller = Convert.ToString(value); break;
                case "Action": _Action = Convert.ToString(value); break;
                case "Area": _Area = Convert.ToString(value); break;
                case "Other": _Other = Convert.ToString(value); break;
                case "Enable": _Enable = value.ToBoolean(); break;
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
    /// <summary>取得动态路由表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>正则表达式</summary>
        public static readonly Field RegexInfo = FindByName("RegexInfo");

        /// <summary>控制器</summary>
        public static readonly Field Controller = FindByName("Controller");

        /// <summary>动作</summary>
        public static readonly Field Action = FindByName("Action");

        /// <summary>区域</summary>
        public static readonly Field Area = FindByName("Area");

        /// <summary>其他参数</summary>
        public static readonly Field Other = FindByName("Other");

        /// <summary>是否启用</summary>
        public static readonly Field Enable = FindByName("Enable");

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

    /// <summary>取得动态路由表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>正则表达式</summary>
        public const String RegexInfo = "RegexInfo";

        /// <summary>控制器</summary>
        public const String Controller = "Controller";

        /// <summary>动作</summary>
        public const String Action = "Action";

        /// <summary>区域</summary>
        public const String Area = "Area";

        /// <summary>其他参数</summary>
        public const String Other = "Other";

        /// <summary>是否启用</summary>
        public const String Enable = "Enable";

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
