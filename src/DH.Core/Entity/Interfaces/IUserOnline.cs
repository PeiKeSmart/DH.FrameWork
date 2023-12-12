using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>用户在线</summary>
public partial interface IUserOnline
{
    #region 属性
    /// <summary>编号</summary>
    Int32 ID { get; set; }

    /// <summary>用户。当前登录人</summary>
    Int32 UserID { get; set; }

    /// <summary>名称。当前登录人，或根据设备标识推算出来的使用人</summary>
    String Name { get; set; }

    /// <summary>会话。Web的SessionID或Server的会话编号</summary>
    String SessionID { get; set; }

    /// <summary>登录方。OAuth提供商，从哪个渠道登录</summary>
    String OAuthProvider { get; set; }

    /// <summary>次数</summary>
    Int32 Times { get; set; }

    /// <summary>页面</summary>
    String Page { get; set; }

    /// <summary>平台。操作系统平台，Windows/Linux/Android等</summary>
    String Platform { get; set; }

    /// <summary>系统。操作系统，带版本</summary>
    String OS { get; set; }

    /// <summary>设备。手机品牌型号</summary>
    String Device { get; set; }

    /// <summary>浏览器。浏览器名称，带版本</summary>
    String Brower { get; set; }

    /// <summary>网络。微信访问时，感知到WIFI或4G网络</summary>
    String NetType { get; set; }

    /// <summary>设备标识。唯一标识设备，位于浏览器Cookie，重装后改变</summary>
    String DeviceId { get; set; }

    /// <summary>状态</summary>
    String Status { get; set; }

    /// <summary>在线时间。本次在线总时间，秒</summary>
    Int32 OnlineTime { get; set; }

    /// <summary>最后错误</summary>
    DateTime LastError { get; set; }

    /// <summary>地址。根据IP计算</summary>
    String Address { get; set; }

    /// <summary>追踪。链路追踪，用于APM性能追踪定位，还原该事件的调用链</summary>
    String TraceId { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>更新地址</summary>
    String UpdateIP { get; set; }

    /// <summary>修改时间</summary>
    DateTime UpdateTime { get; set; }
    #endregion
}
