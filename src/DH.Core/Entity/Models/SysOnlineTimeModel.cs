using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>在线时间表</summary>
public partial class SysOnlineTimeModel : ISysOnlineTime
{
    #region 属性
    /// <summary>用户编号</summary>
    public Int32 Id { get; set; }

    /// <summary>年</summary>
    public Int32 Year { get; set; }

    /// <summary>月</summary>
    public Int32 Month { get; set; }

    /// <summary>本月在线时间。累计在线总时间，单位秒</summary>
    public Int32 MonthTimes { get; set; }

    /// <summary>本月在线时间。累计在线总时间，单位秒</summary>
    public Int32 DayTimes { get; set; }

    /// <summary>按日的在线时间。以|分隔</summary>
    public String Day { get; set; }

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
        Month = model.Month;
        MonthTimes = model.MonthTimes;
        DayTimes = model.DayTimes;
        Day = model.Day;
        UpdateTime = model.UpdateTime;
    }
    #endregion
}
