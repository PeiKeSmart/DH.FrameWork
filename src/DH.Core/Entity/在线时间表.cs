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
    [DataObjectField(false, false, true, 500)]
    [BindColumn("Day", "按日的在线时间。以|分隔", "")]
    public String Day { get => _Day; set { if (OnPropertyChanging("Day", value)) { _Day = value; OnPropertyChanged("Day"); } } }

    private Int32 _Day1;
    /// <summary>1号的在线时间</summary>
    [DisplayName("1号的在线时间")]
    [Description("1号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day1", "1号的在线时间", "")]
    public Int32 Day1 { get => _Day1; set { if (OnPropertyChanging("Day1", value)) { _Day1 = value; OnPropertyChanged("Day1"); } } }

    private Int32 _Day2;
    /// <summary>2号的在线时间</summary>
    [DisplayName("2号的在线时间")]
    [Description("2号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day2", "2号的在线时间", "")]
    public Int32 Day2 { get => _Day2; set { if (OnPropertyChanging("Day2", value)) { _Day2 = value; OnPropertyChanged("Day2"); } } }

    private Int32 _Day3;
    /// <summary>3号的在线时间</summary>
    [DisplayName("3号的在线时间")]
    [Description("3号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day3", "3号的在线时间", "")]
    public Int32 Day3 { get => _Day3; set { if (OnPropertyChanging("Day3", value)) { _Day3 = value; OnPropertyChanged("Day3"); } } }

    private Int32 _Day4;
    /// <summary>4号的在线时间</summary>
    [DisplayName("4号的在线时间")]
    [Description("4号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day4", "4号的在线时间", "")]
    public Int32 Day4 { get => _Day4; set { if (OnPropertyChanging("Day4", value)) { _Day4 = value; OnPropertyChanged("Day4"); } } }

    private Int32 _Day5;
    /// <summary>5号的在线时间</summary>
    [DisplayName("5号的在线时间")]
    [Description("5号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day5", "5号的在线时间", "")]
    public Int32 Day5 { get => _Day5; set { if (OnPropertyChanging("Day5", value)) { _Day5 = value; OnPropertyChanged("Day5"); } } }

    private Int32 _Day6;
    /// <summary>6号的在线时间</summary>
    [DisplayName("6号的在线时间")]
    [Description("6号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day6", "6号的在线时间", "")]
    public Int32 Day6 { get => _Day6; set { if (OnPropertyChanging("Day6", value)) { _Day6 = value; OnPropertyChanged("Day6"); } } }

    private Int32 _Day7;
    /// <summary>7号的在线时间</summary>
    [DisplayName("7号的在线时间")]
    [Description("7号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day7", "7号的在线时间", "")]
    public Int32 Day7 { get => _Day7; set { if (OnPropertyChanging("Day7", value)) { _Day7 = value; OnPropertyChanged("Day7"); } } }

    private Int32 _Day8;
    /// <summary>8号的在线时间</summary>
    [DisplayName("8号的在线时间")]
    [Description("8号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day8", "8号的在线时间", "")]
    public Int32 Day8 { get => _Day8; set { if (OnPropertyChanging("Day8", value)) { _Day8 = value; OnPropertyChanged("Day8"); } } }

    private Int32 _Day9;
    /// <summary>9号的在线时间</summary>
    [DisplayName("9号的在线时间")]
    [Description("9号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day9", "9号的在线时间", "")]
    public Int32 Day9 { get => _Day9; set { if (OnPropertyChanging("Day9", value)) { _Day9 = value; OnPropertyChanged("Day9"); } } }

    private Int32 _Day10;
    /// <summary>10号的在线时间</summary>
    [DisplayName("10号的在线时间")]
    [Description("10号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day10", "10号的在线时间", "")]
    public Int32 Day10 { get => _Day10; set { if (OnPropertyChanging("Day10", value)) { _Day10 = value; OnPropertyChanged("Day10"); } } }

    private Int32 _Day11;
    /// <summary>11号的在线时间</summary>
    [DisplayName("11号的在线时间")]
    [Description("11号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day11", "11号的在线时间", "")]
    public Int32 Day11 { get => _Day11; set { if (OnPropertyChanging("Day11", value)) { _Day11 = value; OnPropertyChanged("Day11"); } } }

    private Int32 _Day12;
    /// <summary>12号的在线时间</summary>
    [DisplayName("12号的在线时间")]
    [Description("12号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day12", "12号的在线时间", "")]
    public Int32 Day12 { get => _Day12; set { if (OnPropertyChanging("Day12", value)) { _Day12 = value; OnPropertyChanged("Day12"); } } }

    private Int32 _Day13;
    /// <summary>13号的在线时间</summary>
    [DisplayName("13号的在线时间")]
    [Description("13号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day13", "13号的在线时间", "")]
    public Int32 Day13 { get => _Day13; set { if (OnPropertyChanging("Day13", value)) { _Day13 = value; OnPropertyChanged("Day13"); } } }

    private Int32 _Day14;
    /// <summary>14号的在线时间</summary>
    [DisplayName("14号的在线时间")]
    [Description("14号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day14", "14号的在线时间", "")]
    public Int32 Day14 { get => _Day14; set { if (OnPropertyChanging("Day14", value)) { _Day14 = value; OnPropertyChanged("Day14"); } } }

    private Int32 _Day15;
    /// <summary>15号的在线时间</summary>
    [DisplayName("15号的在线时间")]
    [Description("15号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day15", "15号的在线时间", "")]
    public Int32 Day15 { get => _Day15; set { if (OnPropertyChanging("Day15", value)) { _Day15 = value; OnPropertyChanged("Day15"); } } }

    private Int32 _Day16;
    /// <summary>16号的在线时间</summary>
    [DisplayName("16号的在线时间")]
    [Description("16号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day16", "16号的在线时间", "")]
    public Int32 Day16 { get => _Day16; set { if (OnPropertyChanging("Day16", value)) { _Day16 = value; OnPropertyChanged("Day16"); } } }

    private Int32 _Day17;
    /// <summary>17号的在线时间</summary>
    [DisplayName("17号的在线时间")]
    [Description("17号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day17", "17号的在线时间", "")]
    public Int32 Day17 { get => _Day17; set { if (OnPropertyChanging("Day17", value)) { _Day17 = value; OnPropertyChanged("Day17"); } } }

    private Int32 _Day18;
    /// <summary>18号的在线时间</summary>
    [DisplayName("18号的在线时间")]
    [Description("18号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day18", "18号的在线时间", "")]
    public Int32 Day18 { get => _Day18; set { if (OnPropertyChanging("Day18", value)) { _Day18 = value; OnPropertyChanged("Day18"); } } }

    private Int32 _Day19;
    /// <summary>19号的在线时间</summary>
    [DisplayName("19号的在线时间")]
    [Description("19号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day19", "19号的在线时间", "")]
    public Int32 Day19 { get => _Day19; set { if (OnPropertyChanging("Day19", value)) { _Day19 = value; OnPropertyChanged("Day19"); } } }

    private Int32 _Day20;
    /// <summary>20号的在线时间</summary>
    [DisplayName("20号的在线时间")]
    [Description("20号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day20", "20号的在线时间", "")]
    public Int32 Day20 { get => _Day20; set { if (OnPropertyChanging("Day20", value)) { _Day20 = value; OnPropertyChanged("Day20"); } } }

    private Int32 _Day21;
    /// <summary>21号的在线时间</summary>
    [DisplayName("21号的在线时间")]
    [Description("21号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day21", "21号的在线时间", "")]
    public Int32 Day21 { get => _Day21; set { if (OnPropertyChanging("Day21", value)) { _Day21 = value; OnPropertyChanged("Day21"); } } }

    private Int32 _Day22;
    /// <summary>22号的在线时间</summary>
    [DisplayName("22号的在线时间")]
    [Description("22号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day22", "22号的在线时间", "")]
    public Int32 Day22 { get => _Day22; set { if (OnPropertyChanging("Day22", value)) { _Day22 = value; OnPropertyChanged("Day22"); } } }

    private Int32 _Day23;
    /// <summary>23号的在线时间</summary>
    [DisplayName("23号的在线时间")]
    [Description("23号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day23", "23号的在线时间", "")]
    public Int32 Day23 { get => _Day23; set { if (OnPropertyChanging("Day23", value)) { _Day23 = value; OnPropertyChanged("Day23"); } } }

    private Int32 _Day24;
    /// <summary>24号的在线时间</summary>
    [DisplayName("24号的在线时间")]
    [Description("24号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day24", "24号的在线时间", "")]
    public Int32 Day24 { get => _Day24; set { if (OnPropertyChanging("Day24", value)) { _Day24 = value; OnPropertyChanged("Day24"); } } }

    private Int32 _Day25;
    /// <summary>25号的在线时间</summary>
    [DisplayName("25号的在线时间")]
    [Description("25号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day25", "25号的在线时间", "")]
    public Int32 Day25 { get => _Day25; set { if (OnPropertyChanging("Day25", value)) { _Day25 = value; OnPropertyChanged("Day25"); } } }

    private Int32 _Day26;
    /// <summary>26号的在线时间</summary>
    [DisplayName("26号的在线时间")]
    [Description("26号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day26", "26号的在线时间", "")]
    public Int32 Day26 { get => _Day26; set { if (OnPropertyChanging("Day26", value)) { _Day26 = value; OnPropertyChanged("Day26"); } } }

    private Int32 _Day27;
    /// <summary>27号的在线时间</summary>
    [DisplayName("27号的在线时间")]
    [Description("27号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day27", "27号的在线时间", "")]
    public Int32 Day27 { get => _Day27; set { if (OnPropertyChanging("Day27", value)) { _Day27 = value; OnPropertyChanged("Day27"); } } }

    private Int32 _Day28;
    /// <summary>28号的在线时间</summary>
    [DisplayName("28号的在线时间")]
    [Description("28号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day28", "28号的在线时间", "")]
    public Int32 Day28 { get => _Day28; set { if (OnPropertyChanging("Day28", value)) { _Day28 = value; OnPropertyChanged("Day28"); } } }

    private Int32 _Day29;
    /// <summary>29号的在线时间</summary>
    [DisplayName("29号的在线时间")]
    [Description("29号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day29", "29号的在线时间", "")]
    public Int32 Day29 { get => _Day29; set { if (OnPropertyChanging("Day29", value)) { _Day29 = value; OnPropertyChanged("Day29"); } } }

    private Int32 _Day30;
    /// <summary>30号的在线时间</summary>
    [DisplayName("30号的在线时间")]
    [Description("30号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day30", "30号的在线时间", "")]
    public Int32 Day30 { get => _Day30; set { if (OnPropertyChanging("Day30", value)) { _Day30 = value; OnPropertyChanged("Day30"); } } }

    private Int32 _Day31;
    /// <summary>31号的在线时间</summary>
    [DisplayName("31号的在线时间")]
    [Description("31号的在线时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Day31", "31号的在线时间", "")]
    public Int32 Day31 { get => _Day31; set { if (OnPropertyChanging("Day31", value)) { _Day31 = value; OnPropertyChanged("Day31"); } } }

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
        Day1 = model.Day1;
        Day2 = model.Day2;
        Day3 = model.Day3;
        Day4 = model.Day4;
        Day5 = model.Day5;
        Day6 = model.Day6;
        Day7 = model.Day7;
        Day8 = model.Day8;
        Day9 = model.Day9;
        Day10 = model.Day10;
        Day11 = model.Day11;
        Day12 = model.Day12;
        Day13 = model.Day13;
        Day14 = model.Day14;
        Day15 = model.Day15;
        Day16 = model.Day16;
        Day17 = model.Day17;
        Day18 = model.Day18;
        Day19 = model.Day19;
        Day20 = model.Day20;
        Day21 = model.Day21;
        Day22 = model.Day22;
        Day23 = model.Day23;
        Day24 = model.Day24;
        Day25 = model.Day25;
        Day26 = model.Day26;
        Day27 = model.Day27;
        Day28 = model.Day28;
        Day29 = model.Day29;
        Day30 = model.Day30;
        Day31 = model.Day31;
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
            "Day1" => _Day1,
            "Day2" => _Day2,
            "Day3" => _Day3,
            "Day4" => _Day4,
            "Day5" => _Day5,
            "Day6" => _Day6,
            "Day7" => _Day7,
            "Day8" => _Day8,
            "Day9" => _Day9,
            "Day10" => _Day10,
            "Day11" => _Day11,
            "Day12" => _Day12,
            "Day13" => _Day13,
            "Day14" => _Day14,
            "Day15" => _Day15,
            "Day16" => _Day16,
            "Day17" => _Day17,
            "Day18" => _Day18,
            "Day19" => _Day19,
            "Day20" => _Day20,
            "Day21" => _Day21,
            "Day22" => _Day22,
            "Day23" => _Day23,
            "Day24" => _Day24,
            "Day25" => _Day25,
            "Day26" => _Day26,
            "Day27" => _Day27,
            "Day28" => _Day28,
            "Day29" => _Day29,
            "Day30" => _Day30,
            "Day31" => _Day31,
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
                case "Day1": _Day1 = value.ToInt(); break;
                case "Day2": _Day2 = value.ToInt(); break;
                case "Day3": _Day3 = value.ToInt(); break;
                case "Day4": _Day4 = value.ToInt(); break;
                case "Day5": _Day5 = value.ToInt(); break;
                case "Day6": _Day6 = value.ToInt(); break;
                case "Day7": _Day7 = value.ToInt(); break;
                case "Day8": _Day8 = value.ToInt(); break;
                case "Day9": _Day9 = value.ToInt(); break;
                case "Day10": _Day10 = value.ToInt(); break;
                case "Day11": _Day11 = value.ToInt(); break;
                case "Day12": _Day12 = value.ToInt(); break;
                case "Day13": _Day13 = value.ToInt(); break;
                case "Day14": _Day14 = value.ToInt(); break;
                case "Day15": _Day15 = value.ToInt(); break;
                case "Day16": _Day16 = value.ToInt(); break;
                case "Day17": _Day17 = value.ToInt(); break;
                case "Day18": _Day18 = value.ToInt(); break;
                case "Day19": _Day19 = value.ToInt(); break;
                case "Day20": _Day20 = value.ToInt(); break;
                case "Day21": _Day21 = value.ToInt(); break;
                case "Day22": _Day22 = value.ToInt(); break;
                case "Day23": _Day23 = value.ToInt(); break;
                case "Day24": _Day24 = value.ToInt(); break;
                case "Day25": _Day25 = value.ToInt(); break;
                case "Day26": _Day26 = value.ToInt(); break;
                case "Day27": _Day27 = value.ToInt(); break;
                case "Day28": _Day28 = value.ToInt(); break;
                case "Day29": _Day29 = value.ToInt(); break;
                case "Day30": _Day30 = value.ToInt(); break;
                case "Day31": _Day31 = value.ToInt(); break;
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

        /// <summary>1号的在线时间</summary>
        public static readonly Field Day1 = FindByName("Day1");

        /// <summary>2号的在线时间</summary>
        public static readonly Field Day2 = FindByName("Day2");

        /// <summary>3号的在线时间</summary>
        public static readonly Field Day3 = FindByName("Day3");

        /// <summary>4号的在线时间</summary>
        public static readonly Field Day4 = FindByName("Day4");

        /// <summary>5号的在线时间</summary>
        public static readonly Field Day5 = FindByName("Day5");

        /// <summary>6号的在线时间</summary>
        public static readonly Field Day6 = FindByName("Day6");

        /// <summary>7号的在线时间</summary>
        public static readonly Field Day7 = FindByName("Day7");

        /// <summary>8号的在线时间</summary>
        public static readonly Field Day8 = FindByName("Day8");

        /// <summary>9号的在线时间</summary>
        public static readonly Field Day9 = FindByName("Day9");

        /// <summary>10号的在线时间</summary>
        public static readonly Field Day10 = FindByName("Day10");

        /// <summary>11号的在线时间</summary>
        public static readonly Field Day11 = FindByName("Day11");

        /// <summary>12号的在线时间</summary>
        public static readonly Field Day12 = FindByName("Day12");

        /// <summary>13号的在线时间</summary>
        public static readonly Field Day13 = FindByName("Day13");

        /// <summary>14号的在线时间</summary>
        public static readonly Field Day14 = FindByName("Day14");

        /// <summary>15号的在线时间</summary>
        public static readonly Field Day15 = FindByName("Day15");

        /// <summary>16号的在线时间</summary>
        public static readonly Field Day16 = FindByName("Day16");

        /// <summary>17号的在线时间</summary>
        public static readonly Field Day17 = FindByName("Day17");

        /// <summary>18号的在线时间</summary>
        public static readonly Field Day18 = FindByName("Day18");

        /// <summary>19号的在线时间</summary>
        public static readonly Field Day19 = FindByName("Day19");

        /// <summary>20号的在线时间</summary>
        public static readonly Field Day20 = FindByName("Day20");

        /// <summary>21号的在线时间</summary>
        public static readonly Field Day21 = FindByName("Day21");

        /// <summary>22号的在线时间</summary>
        public static readonly Field Day22 = FindByName("Day22");

        /// <summary>23号的在线时间</summary>
        public static readonly Field Day23 = FindByName("Day23");

        /// <summary>24号的在线时间</summary>
        public static readonly Field Day24 = FindByName("Day24");

        /// <summary>25号的在线时间</summary>
        public static readonly Field Day25 = FindByName("Day25");

        /// <summary>26号的在线时间</summary>
        public static readonly Field Day26 = FindByName("Day26");

        /// <summary>27号的在线时间</summary>
        public static readonly Field Day27 = FindByName("Day27");

        /// <summary>28号的在线时间</summary>
        public static readonly Field Day28 = FindByName("Day28");

        /// <summary>29号的在线时间</summary>
        public static readonly Field Day29 = FindByName("Day29");

        /// <summary>30号的在线时间</summary>
        public static readonly Field Day30 = FindByName("Day30");

        /// <summary>31号的在线时间</summary>
        public static readonly Field Day31 = FindByName("Day31");

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

        /// <summary>1号的在线时间</summary>
        public const String Day1 = "Day1";

        /// <summary>2号的在线时间</summary>
        public const String Day2 = "Day2";

        /// <summary>3号的在线时间</summary>
        public const String Day3 = "Day3";

        /// <summary>4号的在线时间</summary>
        public const String Day4 = "Day4";

        /// <summary>5号的在线时间</summary>
        public const String Day5 = "Day5";

        /// <summary>6号的在线时间</summary>
        public const String Day6 = "Day6";

        /// <summary>7号的在线时间</summary>
        public const String Day7 = "Day7";

        /// <summary>8号的在线时间</summary>
        public const String Day8 = "Day8";

        /// <summary>9号的在线时间</summary>
        public const String Day9 = "Day9";

        /// <summary>10号的在线时间</summary>
        public const String Day10 = "Day10";

        /// <summary>11号的在线时间</summary>
        public const String Day11 = "Day11";

        /// <summary>12号的在线时间</summary>
        public const String Day12 = "Day12";

        /// <summary>13号的在线时间</summary>
        public const String Day13 = "Day13";

        /// <summary>14号的在线时间</summary>
        public const String Day14 = "Day14";

        /// <summary>15号的在线时间</summary>
        public const String Day15 = "Day15";

        /// <summary>16号的在线时间</summary>
        public const String Day16 = "Day16";

        /// <summary>17号的在线时间</summary>
        public const String Day17 = "Day17";

        /// <summary>18号的在线时间</summary>
        public const String Day18 = "Day18";

        /// <summary>19号的在线时间</summary>
        public const String Day19 = "Day19";

        /// <summary>20号的在线时间</summary>
        public const String Day20 = "Day20";

        /// <summary>21号的在线时间</summary>
        public const String Day21 = "Day21";

        /// <summary>22号的在线时间</summary>
        public const String Day22 = "Day22";

        /// <summary>23号的在线时间</summary>
        public const String Day23 = "Day23";

        /// <summary>24号的在线时间</summary>
        public const String Day24 = "Day24";

        /// <summary>25号的在线时间</summary>
        public const String Day25 = "Day25";

        /// <summary>26号的在线时间</summary>
        public const String Day26 = "Day26";

        /// <summary>27号的在线时间</summary>
        public const String Day27 = "Day27";

        /// <summary>28号的在线时间</summary>
        public const String Day28 = "Day28";

        /// <summary>29号的在线时间</summary>
        public const String Day29 = "Day29";

        /// <summary>30号的在线时间</summary>
        public const String Day30 = "Day30";

        /// <summary>31号的在线时间</summary>
        public const String Day31 = "Day31";

        /// <summary>最后更新时间</summary>
        public const String UpdateTime = "UpdateTime";
    }
    #endregion
}
