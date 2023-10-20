using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>短信配置</summary>
public partial interface ISmsInfo
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>编码</summary>
    String Code { get; set; }

    /// <summary>类型。0为通知类，1为营销类</summary>
    Int32 SType { get; set; }

    /// <summary>是否启用</summary>
    Boolean IsEnabled { get; set; }

    /// <summary>是否默认</summary>
    Boolean IsDefault { get; set; }

    /// <summary>AccessKey</summary>
    String AccessKey { get; set; }

    /// <summary>AccessKeySecret</summary>
    String AccessKeySecret { get; set; }

    /// <summary>短信签名</summary>
    String PassKey { get; set; }

    /// <summary>允许的短信类型，以逗号分隔。SmsLogin为登录短信,SmsRegister为注册短信,SmsPassword为找回密码短信</summary>
    String Content { get; set; }
    #endregion
}
