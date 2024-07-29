using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>访问规则。控制系统访问的安全访问规则，放行或拦截或限流</summary>
public partial interface IAccessRule
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>名称</summary>
    String Name { get; set; }

    /// <summary>启用</summary>
    Boolean Enable { get; set; }

    /// <summary>优先级。较大优先</summary>
    Int32 Priority { get; set; }

    /// <summary>URL路径。支持*模糊匹配，多个逗号隔开</summary>
    String Url { get; set; }

    /// <summary>用户代理。支持*模糊匹配，多个逗号隔开</summary>
    String UserAgent { get; set; }

    /// <summary>来源IP。支持*模糊匹配，多个逗号隔开</summary>
    String IP { get; set; }

    /// <summary>登录用户。支持*模糊匹配，多个逗号隔开</summary>
    String LoginedUser { get; set; }

    /// <summary>动作。放行/拦截/限流</summary>
    AccessActionKinds ActionKind { get; set; }

    /// <summary>拦截代码。拦截时返回Http代码，如404/500/302等</summary>
    Int32 BlockCode { get; set; }

    /// <summary>拦截内容。拦截时返回内容，返回302时此处调目标地址</summary>
    String BlockContent { get; set; }

    /// <summary>限流维度。IP/用户</summary>
    LimitDimensions LimitDimension { get; set; }

    /// <summary>限流时间。限流时的考察时间，期间累加规则触发次数，如600秒</summary>
    Int32 LimitCycle { get; set; }

    /// <summary>限流次数。限流考察期间达到该阈值时，执行拦截</summary>
    Int32 LimitTimes { get; set; }

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
