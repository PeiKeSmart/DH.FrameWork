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
using XCode.Common;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace DH.Entity;

/// <summary>计划任务</summary>
[Serializable]
[DataObject]
[Description("计划任务")]
[BindTable("DH_ScheduleTask", Description = "计划任务", ConnName = "DG", DbType = DatabaseType.None)]
public partial class ScheduleTask : IScheduleTask, IEntity<IScheduleTask>
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
    /// <summary>名称</summary>
    [DisplayName("名称")]
    [Description("名称")]
    [DataObjectField(false, false, true, 1024)]
    [BindColumn("Name", "名称", "", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private Int32 _Seconds;
    /// <summary>运行周期（秒）</summary>
    [DisplayName("运行周期（秒）")]
    [Description("运行周期（秒）")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Seconds", "运行周期（秒）", "")]
    public Int32 Seconds { get => _Seconds; set { if (OnPropertyChanging("Seconds", value)) { _Seconds = value; OnPropertyChanged("Seconds"); } } }

    private String _Type;
    /// <summary>适当的IScheduleTask类的类型</summary>
    [DisplayName("适当的IScheduleTask类的类型")]
    [Description("适当的IScheduleTask类的类型")]
    [DataObjectField(false, false, true, 1024)]
    [BindColumn("Type", "适当的IScheduleTask类的类型", "")]
    public String Type { get => _Type; set { if (OnPropertyChanging("Type", value)) { _Type = value; OnPropertyChanged("Type"); } } }

    private DateTime _LastEnabledUtc;
    /// <summary>上次启用任务的日期时间</summary>
    [DisplayName("上次启用任务的日期时间")]
    [Description("上次启用任务的日期时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("LastEnabledUtc", "上次启用任务的日期时间", "")]
    public DateTime LastEnabledUtc { get => _LastEnabledUtc; set { if (OnPropertyChanging("LastEnabledUtc", value)) { _LastEnabledUtc = value; OnPropertyChanged("LastEnabledUtc"); } } }

    private Boolean _Enabled;
    /// <summary>是否启用任务</summary>
    [DisplayName("是否启用任务")]
    [Description("是否启用任务")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Enabled", "是否启用任务", "")]
    public Boolean Enabled { get => _Enabled; set { if (OnPropertyChanging("Enabled", value)) { _Enabled = value; OnPropertyChanged("Enabled"); } } }

    private Boolean _StopOnError;
    /// <summary>是否应在出现错误时停止任务</summary>
    [DisplayName("是否应在出现错误时停止任务")]
    [Description("是否应在出现错误时停止任务")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("StopOnError", "是否应在出现错误时停止任务", "")]
    public Boolean StopOnError { get => _StopOnError; set { if (OnPropertyChanging("StopOnError", value)) { _StopOnError = value; OnPropertyChanged("StopOnError"); } } }

    private DateTime _LastStartUtc;
    /// <summary>上次启动的日期时间</summary>
    [DisplayName("上次启动的日期时间")]
    [Description("上次启动的日期时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("LastStartUtc", "上次启动的日期时间", "")]
    public DateTime LastStartUtc { get => _LastStartUtc; set { if (OnPropertyChanging("LastStartUtc", value)) { _LastStartUtc = value; OnPropertyChanged("LastStartUtc"); } } }

    private DateTime _LastEndUtc;
    /// <summary>上次完成的日期时间（无论失败还是成功）</summary>
    [DisplayName("上次完成的日期时间（无论失败还是成功）")]
    [Description("上次完成的日期时间（无论失败还是成功）")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("LastEndUtc", "上次完成的日期时间（无论失败还是成功）", "")]
    public DateTime LastEndUtc { get => _LastEndUtc; set { if (OnPropertyChanging("LastEndUtc", value)) { _LastEndUtc = value; OnPropertyChanged("LastEndUtc"); } } }

    private DateTime _LastSuccessUtc;
    /// <summary>上次成功完成的日期时间</summary>
    [DisplayName("上次成功完成的日期时间")]
    [Description("上次成功完成的日期时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("LastSuccessUtc", "上次成功完成的日期时间", "")]
    public DateTime LastSuccessUtc { get => _LastSuccessUtc; set { if (OnPropertyChanging("LastSuccessUtc", value)) { _LastSuccessUtc = value; OnPropertyChanged("LastSuccessUtc"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IScheduleTask model)
    {
        Id = model.Id;
        Name = model.Name;
        Seconds = model.Seconds;
        Type = model.Type;
        LastEnabledUtc = model.LastEnabledUtc;
        Enabled = model.Enabled;
        StopOnError = model.StopOnError;
        LastStartUtc = model.LastStartUtc;
        LastEndUtc = model.LastEndUtc;
        LastSuccessUtc = model.LastSuccessUtc;
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
            "Seconds" => _Seconds,
            "Type" => _Type,
            "LastEnabledUtc" => _LastEnabledUtc,
            "Enabled" => _Enabled,
            "StopOnError" => _StopOnError,
            "LastStartUtc" => _LastStartUtc,
            "LastEndUtc" => _LastEndUtc,
            "LastSuccessUtc" => _LastSuccessUtc,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "Seconds": _Seconds = value.ToInt(); break;
                case "Type": _Type = Convert.ToString(value); break;
                case "LastEnabledUtc": _LastEnabledUtc = value.ToDateTime(); break;
                case "Enabled": _Enabled = value.ToBoolean(); break;
                case "StopOnError": _StopOnError = value.ToBoolean(); break;
                case "LastStartUtc": _LastStartUtc = value.ToDateTime(); break;
                case "LastEndUtc": _LastEndUtc = value.ToDateTime(); break;
                case "LastSuccessUtc": _LastSuccessUtc = value.ToDateTime(); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得计划任务字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>运行周期（秒）</summary>
        public static readonly Field Seconds = FindByName("Seconds");

        /// <summary>适当的IScheduleTask类的类型</summary>
        public static readonly Field Type = FindByName("Type");

        /// <summary>上次启用任务的日期时间</summary>
        public static readonly Field LastEnabledUtc = FindByName("LastEnabledUtc");

        /// <summary>是否启用任务</summary>
        public static readonly Field Enabled = FindByName("Enabled");

        /// <summary>是否应在出现错误时停止任务</summary>
        public static readonly Field StopOnError = FindByName("StopOnError");

        /// <summary>上次启动的日期时间</summary>
        public static readonly Field LastStartUtc = FindByName("LastStartUtc");

        /// <summary>上次完成的日期时间（无论失败还是成功）</summary>
        public static readonly Field LastEndUtc = FindByName("LastEndUtc");

        /// <summary>上次成功完成的日期时间</summary>
        public static readonly Field LastSuccessUtc = FindByName("LastSuccessUtc");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得计划任务字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>名称</summary>
        public const String Name = "Name";

        /// <summary>运行周期（秒）</summary>
        public const String Seconds = "Seconds";

        /// <summary>适当的IScheduleTask类的类型</summary>
        public const String Type = "Type";

        /// <summary>上次启用任务的日期时间</summary>
        public const String LastEnabledUtc = "LastEnabledUtc";

        /// <summary>是否启用任务</summary>
        public const String Enabled = "Enabled";

        /// <summary>是否应在出现错误时停止任务</summary>
        public const String StopOnError = "StopOnError";

        /// <summary>上次启动的日期时间</summary>
        public const String LastStartUtc = "LastStartUtc";

        /// <summary>上次完成的日期时间（无论失败还是成功）</summary>
        public const String LastEndUtc = "LastEndUtc";

        /// <summary>上次成功完成的日期时间</summary>
        public const String LastSuccessUtc = "LastSuccessUtc";
    }
    #endregion
}
