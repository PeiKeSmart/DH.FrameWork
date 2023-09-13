using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>在线时间表</summary>
public partial class SysOnlineTimeModel
{
    #region 属性
    /// <summary>用户编号</summary>
    public Int32 Id { get; set; }

    /// <summary>年</summary>
    public Int32 Year { get; set; }

    /// <summary>角色</summary>
    public Int32 RoleId { get; set; }

    /// <summary>用户名</summary>
    public String UName { get; set; }

    /// <summary>月</summary>
    public Int32 Month { get; set; }

    /// <summary>本月在线时间。累计在线总时间，单位秒</summary>
    public Int32 MonthTimes { get; set; }

    /// <summary>本月在线时间。累计在线总时间，单位秒</summary>
    public Int32 DayTimes { get; set; }

    /// <summary>1号的在线时间</summary>
    public Int32 Day1 { get; set; }

    /// <summary>2号的在线时间</summary>
    public Int32 Day2 { get; set; }

    /// <summary>3号的在线时间</summary>
    public Int32 Day3 { get; set; }

    /// <summary>4号的在线时间</summary>
    public Int32 Day4 { get; set; }

    /// <summary>5号的在线时间</summary>
    public Int32 Day5 { get; set; }

    /// <summary>6号的在线时间</summary>
    public Int32 Day6 { get; set; }

    /// <summary>7号的在线时间</summary>
    public Int32 Day7 { get; set; }

    /// <summary>8号的在线时间</summary>
    public Int32 Day8 { get; set; }

    /// <summary>9号的在线时间</summary>
    public Int32 Day9 { get; set; }

    /// <summary>10号的在线时间</summary>
    public Int32 Day10 { get; set; }

    /// <summary>11号的在线时间</summary>
    public Int32 Day11 { get; set; }

    /// <summary>12号的在线时间</summary>
    public Int32 Day12 { get; set; }

    /// <summary>13号的在线时间</summary>
    public Int32 Day13 { get; set; }

    /// <summary>14号的在线时间</summary>
    public Int32 Day14 { get; set; }

    /// <summary>15号的在线时间</summary>
    public Int32 Day15 { get; set; }

    /// <summary>16号的在线时间</summary>
    public Int32 Day16 { get; set; }

    /// <summary>17号的在线时间</summary>
    public Int32 Day17 { get; set; }

    /// <summary>18号的在线时间</summary>
    public Int32 Day18 { get; set; }

    /// <summary>19号的在线时间</summary>
    public Int32 Day19 { get; set; }

    /// <summary>20号的在线时间</summary>
    public Int32 Day20 { get; set; }

    /// <summary>21号的在线时间</summary>
    public Int32 Day21 { get; set; }

    /// <summary>22号的在线时间</summary>
    public Int32 Day22 { get; set; }

    /// <summary>23号的在线时间</summary>
    public Int32 Day23 { get; set; }

    /// <summary>24号的在线时间</summary>
    public Int32 Day24 { get; set; }

    /// <summary>25号的在线时间</summary>
    public Int32 Day25 { get; set; }

    /// <summary>26号的在线时间</summary>
    public Int32 Day26 { get; set; }

    /// <summary>27号的在线时间</summary>
    public Int32 Day27 { get; set; }

    /// <summary>28号的在线时间</summary>
    public Int32 Day28 { get; set; }

    /// <summary>29号的在线时间</summary>
    public Int32 Day29 { get; set; }

    /// <summary>30号的在线时间</summary>
    public Int32 Day30 { get; set; }

    /// <summary>31号的在线时间</summary>
    public Int32 Day31 { get; set; }

    /// <summary>最后更新时间</summary>
    public DateTime UpdateTime { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISysOnlineTime model)
    {
        Id = model.Id;
        Year = model.Year;
        RoleId = model.RoleId;
        UName = model.UName;
        Month = model.Month;
        MonthTimes = model.MonthTimes;
        DayTimes = model.DayTimes;
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
}
