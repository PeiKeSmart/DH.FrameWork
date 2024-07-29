using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>定时作业。定时执行任务</summary>
public partial interface ICronJob
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>名称</summary>
    String Name { get; set; }

    /// <summary>显示名</summary>
    String DisplayName { get; set; }

    /// <summary>Cron表达式。用于定时执行的Cron表达式</summary>
    String Cron { get; set; }

    /// <summary>命令。ICubeJob类名或静态方法全名(包含一个String参数)</summary>
    String Method { get; set; }

    /// <summary>参数。方法参数，时间日期、网址、SQL等</summary>
    String Argument { get; set; }

    /// <summary>数据。作业运行中的小量数据，可传递给下一次作业执行，例如记录数据统计的时间点</summary>
    String Data { get; set; }

    /// <summary>启用</summary>
    Boolean Enable { get; set; }

    /// <summary>启用日志</summary>
    Boolean EnableLog { get; set; }

    /// <summary>最后时间。最后一次执行作业的时间</summary>
    DateTime LastTime { get; set; }

    /// <summary>下一次时间。下一次执行作业的时间</summary>
    DateTime NextTime { get; set; }

    /// <summary>创建者</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>更新者</summary>
    Int32 UpdateUserID { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }

    /// <summary>更新地址</summary>
    String UpdateIP { get; set; }

    /// <summary>内容</summary>
    String Remark { get; set; }
    #endregion
}
