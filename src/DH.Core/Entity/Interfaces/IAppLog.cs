using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>应用日志。用于OAuthServer的子系统</summary>
public partial interface IAppLog
{
    #region 属性
    /// <summary>编号</summary>
    Int64 Id { get; set; }

    /// <summary>应用</summary>
    Int32 AppId { get; set; }

    /// <summary>操作</summary>
    String Action { get; set; }

    /// <summary>成功</summary>
    Boolean Success { get; set; }

    /// <summary>应用标识</summary>
    String ClientId { get; set; }

    /// <summary>回调地址</summary>
    String RedirectUri { get; set; }

    /// <summary>响应类型。默认code</summary>
    String ResponseType { get; set; }

    /// <summary>授权域</summary>
    String Scope { get; set; }

    /// <summary>状态数据</summary>
    String State { get; set; }

    /// <summary>访问令牌</summary>
    String AccessToken { get; set; }

    /// <summary>刷新令牌</summary>
    String RefreshToken { get; set; }

    /// <summary>追踪。链路追踪，用于APM性能追踪定位，还原该事件的调用链</summary>
    String TraceId { get; set; }

    /// <summary>OAuth提供商</summary>
    String Provider { get; set; }

    /// <summary>创建者。可以是设备编码等唯一使用者标识</summary>
    String CreateUser { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>更新地址</summary>
    String UpdateIP { get; set; }

    /// <summary>备注</summary>
    String Remark { get; set; }
    #endregion
}
