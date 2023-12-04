using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>OAuth日志。用于记录OAuth客户端请求，同时Id作为state，避免向OAuthServer泄漏本机Url</summary>
public partial interface IOAuthLog
{
    #region 属性
    /// <summary>编号</summary>
    Int64 Id { get; set; }

    /// <summary>提供商</summary>
    String Provider { get; set; }

    /// <summary>链接</summary>
    Int32 ConnectId { get; set; }

    /// <summary>用户</summary>
    Int32 UserId { get; set; }

    /// <summary>操作</summary>
    String Action { get; set; }

    /// <summary>成功</summary>
    Boolean Success { get; set; }

    /// <summary>回调地址</summary>
    String RedirectUri { get; set; }

    /// <summary>响应类型。默认code</summary>
    String ResponseType { get; set; }

    /// <summary>授权域</summary>
    String Scope { get; set; }

    /// <summary>状态数据</summary>
    String State { get; set; }

    /// <summary>来源</summary>
    String Source { get; set; }

    /// <summary>访问令牌</summary>
    String AccessToken { get; set; }

    /// <summary>刷新令牌</summary>
    String RefreshToken { get; set; }

    /// <summary>追踪。链路追踪，用于APM性能追踪定位，还原该事件的调用链</summary>
    String TraceId { get; set; }

    /// <summary>详细信息</summary>
    String Remark { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }
    #endregion
}
