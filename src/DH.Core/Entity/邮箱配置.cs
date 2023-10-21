using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace DH.Entity;

/// <summary>邮箱配置</summary>
[Serializable]
[DataObject]
[Description("邮箱配置")]
[BindTable("DH_MailInfo", Description = "邮箱配置", ConnName = "DG", DbType = DatabaseType.None)]
public partial class MailInfo : IMailInfo, IEntity<IMailInfo>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _Name;
    /// <summary>名称</summary>
    [DisplayName("名称")]
    [Description("名称")]
    [DataObjectField(false, false, false, 50)]
    [BindColumn("Name", "名称", "", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private Boolean _IsEnabled;
    /// <summary>是否启用</summary>
    [DisplayName("是否启用")]
    [Description("是否启用")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsEnabled", "是否启用", "")]
    public Boolean IsEnabled { get => _IsEnabled; set { if (OnPropertyChanging("IsEnabled", value)) { _IsEnabled = value; OnPropertyChanged("IsEnabled"); } } }

    private Boolean _IsDefault;
    /// <summary>是否默认</summary>
    [DisplayName("是否默认")]
    [Description("是否默认")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsDefault", "是否默认", "")]
    public Boolean IsDefault { get => _IsDefault; set { if (OnPropertyChanging("IsDefault", value)) { _IsDefault = value; OnPropertyChanged("IsDefault"); } } }

    private String _Host;
    /// <summary>邮箱SMTP 服务器</summary>
    [DisplayName("邮箱SMTP服务器")]
    [Description("邮箱SMTP 服务器")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Host", "邮箱SMTP 服务器", "")]
    public String Host { get => _Host; set { if (OnPropertyChanging("Host", value)) { _Host = value; OnPropertyChanged("Host"); } } }

    private String _Port;
    /// <summary>邮箱SMTP 端口</summary>
    [DisplayName("邮箱SMTP端口")]
    [Description("邮箱SMTP 端口")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Port", "邮箱SMTP 端口", "")]
    public String Port { get => _Port; set { if (OnPropertyChanging("Port", value)) { _Port = value; OnPropertyChanged("Port"); } } }

    private String _UserName;
    /// <summary>邮箱账号</summary>
    [DisplayName("邮箱账号")]
    [Description("邮箱账号")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("UserName", "邮箱账号", "")]
    public String UserName { get => _UserName; set { if (OnPropertyChanging("UserName", value)) { _UserName = value; OnPropertyChanged("UserName"); } } }

    private String _Password;
    /// <summary>邮箱密码</summary>
    [DisplayName("邮箱密码")]
    [Description("邮箱密码")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Password", "邮箱密码", "")]
    public String Password { get => _Password; set { if (OnPropertyChanging("Password", value)) { _Password = value; OnPropertyChanged("Password"); } } }

    private String _From;
    /// <summary>发信人邮件地址</summary>
    [DisplayName("发信人邮件地址")]
    [Description("发信人邮件地址")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("From", "发信人邮件地址", "")]
    public String From { get => _From; set { if (OnPropertyChanging("From", value)) { _From = value; OnPropertyChanged("From"); } } }

    private String _FromName;
    /// <summary>发送邮箱昵称</summary>
    [DisplayName("发送邮箱昵称")]
    [Description("发送邮箱昵称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("FromName", "发送邮箱昵称", "")]
    public String FromName { get => _FromName; set { if (OnPropertyChanging("FromName", value)) { _FromName = value; OnPropertyChanged("FromName"); } } }

    private Boolean _IsSSL;
    /// <summary>SMTP 协议</summary>
    [DisplayName("SMTP协议")]
    [Description("SMTP 协议")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsSSL", "SMTP 协议", "")]
    public Boolean IsSSL { get => _IsSSL; set { if (OnPropertyChanging("IsSSL", value)) { _IsSSL = value; OnPropertyChanged("IsSSL"); } } }
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

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public override Object this[String name]
    {
        get => name switch
        {
            "Id" => _Id,
            "Name" => _Name,
            "IsEnabled" => _IsEnabled,
            "IsDefault" => _IsDefault,
            "Host" => _Host,
            "Port" => _Port,
            "UserName" => _UserName,
            "Password" => _Password,
            "From" => _From,
            "FromName" => _FromName,
            "IsSSL" => _IsSSL,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "IsEnabled": _IsEnabled = value.ToBoolean(); break;
                case "IsDefault": _IsDefault = value.ToBoolean(); break;
                case "Host": _Host = Convert.ToString(value); break;
                case "Port": _Port = Convert.ToString(value); break;
                case "UserName": _UserName = Convert.ToString(value); break;
                case "Password": _Password = Convert.ToString(value); break;
                case "From": _From = Convert.ToString(value); break;
                case "FromName": _FromName = Convert.ToString(value); break;
                case "IsSSL": _IsSSL = value.ToBoolean(); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得邮箱配置字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>是否启用</summary>
        public static readonly Field IsEnabled = FindByName("IsEnabled");

        /// <summary>是否默认</summary>
        public static readonly Field IsDefault = FindByName("IsDefault");

        /// <summary>邮箱SMTP 服务器</summary>
        public static readonly Field Host = FindByName("Host");

        /// <summary>邮箱SMTP 端口</summary>
        public static readonly Field Port = FindByName("Port");

        /// <summary>邮箱账号</summary>
        public static readonly Field UserName = FindByName("UserName");

        /// <summary>邮箱密码</summary>
        public static readonly Field Password = FindByName("Password");

        /// <summary>发信人邮件地址</summary>
        public static readonly Field From = FindByName("From");

        /// <summary>发送邮箱昵称</summary>
        public static readonly Field FromName = FindByName("FromName");

        /// <summary>SMTP 协议</summary>
        public static readonly Field IsSSL = FindByName("IsSSL");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得邮箱配置字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>名称</summary>
        public const String Name = "Name";

        /// <summary>是否启用</summary>
        public const String IsEnabled = "IsEnabled";

        /// <summary>是否默认</summary>
        public const String IsDefault = "IsDefault";

        /// <summary>邮箱SMTP 服务器</summary>
        public const String Host = "Host";

        /// <summary>邮箱SMTP 端口</summary>
        public const String Port = "Port";

        /// <summary>邮箱账号</summary>
        public const String UserName = "UserName";

        /// <summary>邮箱密码</summary>
        public const String Password = "Password";

        /// <summary>发信人邮件地址</summary>
        public const String From = "From";

        /// <summary>发送邮箱昵称</summary>
        public const String FromName = "FromName";

        /// <summary>SMTP 协议</summary>
        public const String IsSSL = "IsSSL";
    }
    #endregion
}
