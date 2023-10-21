using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>邮箱配置</summary>
public partial interface IMailInfo
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>名称</summary>
    String Name { get; set; }

    /// <summary>是否启用</summary>
    Boolean IsEnabled { get; set; }

    /// <summary>是否默认</summary>
    Boolean IsDefault { get; set; }

    /// <summary>邮箱SMTP 服务器</summary>
    String Host { get; set; }

    /// <summary>邮箱SMTP 端口</summary>
    String Port { get; set; }

    /// <summary>邮箱账号</summary>
    String UserName { get; set; }

    /// <summary>邮箱密码</summary>
    String Password { get; set; }

    /// <summary>发信人邮件地址</summary>
    String From { get; set; }

    /// <summary>发送邮箱昵称</summary>
    String FromName { get; set; }

    /// <summary>SMTP 协议</summary>
    Boolean IsSSL { get; set; }
    #endregion
}
