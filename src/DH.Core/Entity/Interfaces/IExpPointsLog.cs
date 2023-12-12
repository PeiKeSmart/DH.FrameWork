using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>会员经验日志表</summary>
public partial interface IExpPointsLog
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>会员ID</summary>
    Int32 UId { get; set; }

    /// <summary>会员名称</summary>
    String UName { get; set; }

    /// <summary>经验值负数为扣除</summary>
    Int32 Points { get; set; }

    /// <summary>经验值操作描述</summary>
    String Desc { get; set; }

    /// <summary>经验值操作阶段</summary>
    String Stage { get; set; }

    /// <summary>创建者</summary>
    String CreateUser { get; set; }

    /// <summary>创建者</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>经验添加时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }
    #endregion
}
