using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>在线用户表</summary>
public partial interface ISysOnlineUsers
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>用户id</summary>
    Int32 Uid { get; set; }

    /// <summary>用户sessionid</summary>
    Int64 Sid { get; set; }

    /// <summary>用户昵称</summary>
    String NickName { get; set; }

    /// <summary>用户ip</summary>
    String Ip { get; set; }

    /// <summary>用户所在区域</summary>
    String Region { get; set; }

    /// <summary>请求次数</summary>
    Int32 Clicks { get; set; }

    /// <summary>最后更新时间</summary>
    DateTime Updatetime { get; set; }
    #endregion
}
