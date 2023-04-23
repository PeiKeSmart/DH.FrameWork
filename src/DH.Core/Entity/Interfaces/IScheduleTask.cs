using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>计划任务</summary>
public partial interface IScheduleTask
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>名称</summary>
    String Name { get; set; }

    /// <summary>运行周期（秒）</summary>
    Int32 Seconds { get; set; }

    /// <summary>适当的IScheduleTask类的类型</summary>
    String Type { get; set; }

    /// <summary>上次启用任务的日期时间</summary>
    DateTime LastEnabledUtc { get; set; }

    /// <summary>是否启用任务</summary>
    Boolean Enabled { get; set; }

    /// <summary>是否应在出现错误时停止任务</summary>
    Boolean StopOnError { get; set; }

    /// <summary>上次启动的日期时间</summary>
    DateTime LastStartUtc { get; set; }

    /// <summary>上次完成的日期时间（无论失败还是成功）</summary>
    DateTime LastEndUtc { get; set; }

    /// <summary>上次成功完成的日期时间</summary>
    DateTime LastSuccessUtc { get; set; }
    #endregion
}
