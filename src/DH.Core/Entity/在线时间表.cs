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

/// <summary>在线时间表</summary>
[Serializable]
[DataObject]
[Description("在线时间表")]
[BindIndex("PRIMARY", true, "Id")]
[BindIndex("IU_DH_SysOnlineTime_Id_Year_Month", true, "Id,Year,Month")]
[BindIndex("IX_DH_SysOnlineTime_UpdateTime", false, "UpdateTime")]
[BindTable("DH_SysOnlineTime", Description = "在线时间表", ConnName = "Cube", DbType = DatabaseType.None)]
public partial class SysOnlineTime : ISysOnlineTime, IEntity<ISysOnlineTime>
{
    #region 属性
    private Int32 _Id;
    /// <summary>用户编号</summary>
    [DisplayName("用户编号")]
    [Description("用户编号")]
    [DataObjectField(true, false, false, 0)]
    [BindColumn("Id", "用户编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Int32 _Year;
    /// <summary>年</summary>
    [DisplayName("年")]
    [Description("年")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Year", "年", "")]
    public Int32 Year { get => _Year; set { if (OnPropertyChanging("Year", value)) { _Year = value; OnPropertyChanged("Year"); } } }

    private Int32 _Month;
    /// <summary>月</summary>
    [DisplayName("月")]
    [Description("月")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Month", "月", "")]
    public Int32 Month { get => _Month; set { if (OnPropertyChanging("Month", value)) { _Month = value; OnPropertyChanged("Month"); } } }

    private Int32 _MonthTimes;
    /// <summary>本月在线时间。累计在线总时间，单位秒</summary>
    [DisplayName("本月在线时间")]
    [Description("本月在线时间。累计在线总时间，单位秒")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("MonthTimes", "本月在线时间。累计在线总时间，单位秒", "", ItemType = "TimeSpan")]
    public Int32 MonthTimes { get => _MonthTimes; set { if (OnPropertyChanging("MonthTimes", value)) { _MonthTimes = value; OnPropertyChanged("MonthTimes"); } } }

    private Int32 _DayTimes;
    /// <summary>本月在线时间。累计在线总时间，单位秒</summary>
    [DisplayName("本月在线时间")]
    [Description("本月在线时间。累计在线总时间，单位秒")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("DayTimes", "本月在线时间。累计在线总时间，单位秒", "", ItemType = "TimeSpan")]
    public Int32 DayTimes { get => _DayTimes; set { if (OnPropertyChanging("DayTimes", value)) { _DayTimes = value; OnPropertyChanged("DayTimes"); } } }

    private String _Day;
    /// <summary>按日的在线时间。以|分隔</summary>
    [DisplayName("按日的在线时间")]
    [Description("按日的在线时间。以|分隔")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Day", "按日的在线时间。以|分隔", "")]
    public String Day { get => _Day; set { if (OnPropertyChanging("Day", value)) { _Day = value; OnPropertyChanged("Day"); } } }

    private DateTime _UpdateTime;
    /// <summary>最后更新时间</summary>
    [DisplayName("最后更新时间")]
    [Description("最后更新时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("UpdateTime", "最后更新时间", "")]
    public DateTime UpdateTime { get => _UpdateTime; set { if (OnPropertyChanging("UpdateTime", value)) { _UpdateTime = value; OnPropertyChanged("UpdateTime"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISysOnlineTime model)
    {
        Id = model.Id;
        Year = model.Year;
        Month = model.Month;
        MonthTimes = model.MonthTimes;
        DayTimes = model.DayTimes;
        Day = model.Day;
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
            "Year" => _Year,
            "Month" => _Month,
            "MonthTimes" => _MonthTimes,
            "DayTimes" => _DayTimes,
            "Day" => _Day,
            "UpdateTime" => _UpdateTime,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "Year": _Year = value.ToInt(); break;
                case "Month": _Month = value.ToInt(); break;
                case "MonthTimes": _MonthTimes = value.ToInt(); break;
                case "DayTimes": _DayTimes = value.ToInt(); break;
                case "Day": _Day = Convert.ToString(value); break;
                case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得在线时间表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>用户编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>年</summary>
        public static readonly Field Year = FindByName("Year");

        /// <summary>月</summary>
        public static readonly Field Month = FindByName("Month");

        /// <summary>本月在线时间。累计在线总时间，单位秒</summary>
        public static readonly Field MonthTimes = FindByName("MonthTimes");

        /// <summary>本月在线时间。累计在线总时间，单位秒</summary>
        public static readonly Field DayTimes = FindByName("DayTimes");

        /// <summary>按日的在线时间。以|分隔</summary>
        public static readonly Field Day = FindByName("Day");

        /// <summary>最后更新时间</summary>
        public static readonly Field UpdateTime = FindByName("UpdateTime");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得在线时间表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>用户编号</summary>
        public const String Id = "Id";

        /// <summary>年</summary>
        public const String Year = "Year";

        /// <summary>月</summary>
        public const String Month = "Month";

        /// <summary>本月在线时间。累计在线总时间，单位秒</summary>
        public const String MonthTimes = "MonthTimes";

        /// <summary>本月在线时间。累计在线总时间，单位秒</summary>
        public const String DayTimes = "DayTimes";

        /// <summary>按日的在线时间。以|分隔</summary>
        public const String Day = "Day";

        /// <summary>最后更新时间</summary>
        public const String UpdateTime = "UpdateTime";
    }
    #endregion
}
