using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>短信配置</summary>
public partial class SmsInfoModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>编码</summary>
    public String Code { get; set; }

    /// <summary>类型。0为国内通知类，1为国内营销类，2为国际通知类，3为国际营销类</summary>
    public Int32 SType { get; set; }

    /// <summary>是否启用</summary>
    public Boolean IsEnabled { get; set; }

    /// <summary>是否默认</summary>
    public Boolean IsDefault { get; set; }

    /// <summary>AccessKey</summary>
    public String AccessKey { get; set; }

    /// <summary>AccessKeySecret</summary>
    public String AccessKeySecret { get; set; }

    /// <summary>短信签名</summary>
    public String PassKey { get; set; }

    /// <summary>允许的短信类型，以逗号分隔。SmsLogin为登录短信,SmsRegister为注册短信,SmsPassword为找回密码短信</summary>
    public String Content { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISmsInfo model)
    {
        Id = model.Id;
        Code = model.Code;
        SType = model.SType;
        IsEnabled = model.IsEnabled;
        IsDefault = model.IsDefault;
        AccessKey = model.AccessKey;
        AccessKeySecret = model.AccessKeySecret;
        PassKey = model.PassKey;
        Content = model.Content;
    }
    #endregion
}
