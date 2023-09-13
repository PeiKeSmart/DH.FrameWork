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

/// <summary>路由管理</summary>
[Serializable]
[DataObject]
[Description("路由管理")]
[BindIndex("IX_DH_SystemRout_FromUrl", false, "FromUrl")]
[BindIndex("IU_DH_SystemRout_Url", true, "Url")]
[BindTable("DH_SystemRout", Description = "路由管理", ConnName = "DG", DbType = DatabaseType.None)]
public partial class SystemRout : ISystemRout, IEntity<ISystemRout>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Byte _RType;
    /// <summary>类型，1为控制器，2为Razor Page</summary>
    [DisplayName("类型")]
    [Description("类型，1为控制器，2为Razor Page")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("RType", "类型，1为控制器，2为Razor Page", "tinyint(1)")]
    public Byte RType { get => _RType; set { if (OnPropertyChanging("RType", value)) { _RType = value; OnPropertyChanged("RType"); } } }

    private String _Name;
    /// <summary>路由名称</summary>
    [DisplayName("路由名称")]
    [Description("路由名称")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("Name", "路由名称", "varchar(30)", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private String _Url;
    /// <summary>Url路由</summary>
    [DisplayName("Url路由")]
    [Description("Url路由")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Url", "Url路由", "varchar(50)")]
    public String Url { get => _Url; set { if (OnPropertyChanging("Url", value)) { _Url = value; OnPropertyChanged("Url"); } } }

    private String _Parms;
    /// <summary>Url路由参数</summary>
    [DisplayName("Url路由参数")]
    [Description("Url路由参数")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Parms", "Url路由参数", "varchar(100)")]
    public String Parms { get => _Parms; set { if (OnPropertyChanging("Parms", value)) { _Parms = value; OnPropertyChanged("Parms"); } } }

    private String _Pages;
    /// <summary>Razor Page实际路径</summary>
    [DisplayName("RazorPage实际路径")]
    [Description("Razor Page实际路径")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Pages", "Razor Page实际路径", "varchar(50)")]
    public String Pages { get => _Pages; set { if (OnPropertyChanging("Pages", value)) { _Pages = value; OnPropertyChanged("Pages"); } } }

    private String _AreaName;
    /// <summary>区域名称</summary>
    [DisplayName("区域名称")]
    [Description("区域名称")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("AreaName", "区域名称", "varchar(30)")]
    public String AreaName { get => _AreaName; set { if (OnPropertyChanging("AreaName", value)) { _AreaName = value; OnPropertyChanged("AreaName"); } } }

    private String _ControllerName;
    /// <summary>控制器</summary>
    [DisplayName("控制器")]
    [Description("控制器")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("ControllerName", "控制器", "varchar(30)")]
    public String ControllerName { get => _ControllerName; set { if (OnPropertyChanging("ControllerName", value)) { _ControllerName = value; OnPropertyChanged("ControllerName"); } } }

    private String _ActionName;
    /// <summary>控制器动作</summary>
    [DisplayName("控制器动作")]
    [Description("控制器动作")]
    [DataObjectField(false, false, true, 30)]
    [BindColumn("ActionName", "控制器动作", "varchar(30)")]
    public String ActionName { get => _ActionName; set { if (OnPropertyChanging("ActionName", value)) { _ActionName = value; OnPropertyChanged("ActionName"); } } }

    private String _FromUrl;
    /// <summary>映射路由</summary>
    [DisplayName("映射路由")]
    [Description("映射路由")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("FromUrl", "映射路由", "varchar(50)")]
    public String FromUrl { get => _FromUrl; set { if (OnPropertyChanging("FromUrl", value)) { _FromUrl = value; OnPropertyChanged("FromUrl"); } } }

    private DateTime _CreateTime;
    /// <summary>创建时间</summary>
    [DisplayName("创建时间")]
    [Description("创建时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("CreateTime", "创建时间", "")]
    public DateTime CreateTime { get => _CreateTime; set { if (OnPropertyChanging("CreateTime", value)) { _CreateTime = value; OnPropertyChanged("CreateTime"); } } }

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
    public void Copy(ISystemRout model)
    {
        Id = model.Id;
        RType = model.RType;
        Name = model.Name;
        Url = model.Url;
        Parms = model.Parms;
        Pages = model.Pages;
        AreaName = model.AreaName;
        ControllerName = model.ControllerName;
        ActionName = model.ActionName;
        FromUrl = model.FromUrl;
        CreateTime = model.CreateTime;
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
            "RType" => _RType,
            "Name" => _Name,
            "Url" => _Url,
            "Parms" => _Parms,
            "Pages" => _Pages,
            "AreaName" => _AreaName,
            "ControllerName" => _ControllerName,
            "ActionName" => _ActionName,
            "FromUrl" => _FromUrl,
            "CreateTime" => _CreateTime,
            "UpdateTime" => _UpdateTime,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "RType": _RType = Convert.ToByte(value); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "Url": _Url = Convert.ToString(value); break;
                case "Parms": _Parms = Convert.ToString(value); break;
                case "Pages": _Pages = Convert.ToString(value); break;
                case "AreaName": _AreaName = Convert.ToString(value); break;
                case "ControllerName": _ControllerName = Convert.ToString(value); break;
                case "ActionName": _ActionName = Convert.ToString(value); break;
                case "FromUrl": _FromUrl = Convert.ToString(value); break;
                case "CreateTime": _CreateTime = value.ToDateTime(); break;
                case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得路由管理字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>类型，1为控制器，2为Razor Page</summary>
        public static readonly Field RType = FindByName("RType");

        /// <summary>路由名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>Url路由</summary>
        public static readonly Field Url = FindByName("Url");

        /// <summary>Url路由参数</summary>
        public static readonly Field Parms = FindByName("Parms");

        /// <summary>Razor Page实际路径</summary>
        public static readonly Field Pages = FindByName("Pages");

        /// <summary>区域名称</summary>
        public static readonly Field AreaName = FindByName("AreaName");

        /// <summary>控制器</summary>
        public static readonly Field ControllerName = FindByName("ControllerName");

        /// <summary>控制器动作</summary>
        public static readonly Field ActionName = FindByName("ActionName");

        /// <summary>映射路由</summary>
        public static readonly Field FromUrl = FindByName("FromUrl");

        /// <summary>创建时间</summary>
        public static readonly Field CreateTime = FindByName("CreateTime");

        /// <summary>更新时间</summary>
        public static readonly Field UpdateTime = FindByName("UpdateTime");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得路由管理字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>类型，1为控制器，2为Razor Page</summary>
        public const String RType = "RType";

        /// <summary>路由名称</summary>
        public const String Name = "Name";

        /// <summary>Url路由</summary>
        public const String Url = "Url";

        /// <summary>Url路由参数</summary>
        public const String Parms = "Parms";

        /// <summary>Razor Page实际路径</summary>
        public const String Pages = "Pages";

        /// <summary>区域名称</summary>
        public const String AreaName = "AreaName";

        /// <summary>控制器</summary>
        public const String ControllerName = "ControllerName";

        /// <summary>控制器动作</summary>
        public const String ActionName = "ActionName";

        /// <summary>映射路由</summary>
        public const String FromUrl = "FromUrl";

        /// <summary>创建时间</summary>
        public const String CreateTime = "CreateTime";

        /// <summary>更新时间</summary>
        public const String UpdateTime = "UpdateTime";
    }
    #endregion
}
