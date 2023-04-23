using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>用户统计</summary>
public partial interface IUserStat
{
    #region 属性
    /// <summary>编号</summary>
    Int32 ID { get; set; }

    /// <summary>统计日期</summary>
    DateTime Date { get; set; }

    /// <summary>总数。总用户数</summary>
    Int32 Total { get; set; }

    /// <summary>登录数。总登录数</summary>
    Int32 Logins { get; set; }

    /// <summary>OAuth登录。OAuth总登录数</summary>
    Int32 OAuths { get; set; }

    /// <summary>最大在线。最大在线用户数</summary>
    Int32 MaxOnline { get; set; }

    /// <summary>活跃。今天活跃用户数</summary>
    Int32 Actives { get; set; }

    /// <summary>7天活跃。7天活跃用户数</summary>
    Int32 ActivesT7 { get; set; }

    /// <summary>30天活跃。30天活跃用户数</summary>
    Int32 ActivesT30 { get; set; }

    /// <summary>新用户。今天注册新用户数</summary>
    Int32 News { get; set; }

    /// <summary>7天注册。7天内注册新用户数</summary>
    Int32 NewsT7 { get; set; }

    /// <summary>30天注册。30天注册新用户数</summary>
    Int32 NewsT30 { get; set; }

    /// <summary>在线时间。累计在线总时间，秒</summary>
    Int32 OnlineTime { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }

    /// <summary>详细信息</summary>
    String Remark { get; set; }
    #endregion
}
