using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>邮箱配置</summary>
public partial class MailInfoModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>名称</summary>
    public String Name { get; set; }

    /// <summary>是否启用</summary>
    public Boolean IsEnabled { get; set; }

    /// <summary>是否默认</summary>
    public Boolean IsDefault { get; set; }

    /// <summary>邮箱SMTP 服务器</summary>
    public String Host { get; set; }

    /// <summary>邮箱SMTP 端口</summary>
    public String Port { get; set; }

    /// <summary>邮箱账号</summary>
    public String UserName { get; set; }

    /// <summary>邮箱密码</summary>
    public String Password { get; set; }

    /// <summary>发信人邮件地址</summary>
    public String From { get; set; }

    /// <summary>发送邮箱昵称</summary>
    public String FromName { get; set; }

    /// <summary>SMTP 协议</summary>
    public Boolean IsSSL { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IMailInfo model)
    {
        Id = model.Id;
        Name = model.Name;
        IsEnabled = model.IsEnabled;
        IsDefault = model.IsDefault;
        Host = model.Host;
        Port = model.Port;
        UserName = model.UserName;
        Password = model.Password;
        From = model.From;
        FromName = model.FromName;
        IsSSL = model.IsSSL;
    }
    #endregion
}
