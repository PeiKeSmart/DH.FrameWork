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

/// <summary>短信配置</summary>
[Serializable]
[DataObject]
[Description("短信配置")]
[BindIndex("IU_DH_SmsInfo_Code", true, "Code")]
[BindIndex("IX_DH_SmsInfo_SType", false, "SType")]
[BindTable("DH_SmsInfo", Description = "短信配置", ConnName = "DG", DbType = DatabaseType.None)]
public partial class SmsInfo : ISmsInfo, IEntity<ISmsInfo>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _Code;
    /// <summary>编码</summary>
    [DisplayName("编码")]
    [Description("编码")]
    [DataObjectField(false, false, false, 50)]
    [BindColumn("Code", "编码", "")]
    public String Code { get => _Code; set { if (OnPropertyChanging("Code", value)) { _Code = value; OnPropertyChanged("Code"); } } }

    private Int32 _SType;
    /// <summary>类型。0为通知类，1为营销类</summary>
    [DisplayName("类型")]
    [Description("类型。0为通知类，1为营销类")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("SType", "类型。0为通知类，1为营销类", "")]
    public Int32 SType { get => _SType; set { if (OnPropertyChanging("SType", value)) { _SType = value; OnPropertyChanged("SType"); } } }

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

    private String _AccessKey;
    /// <summary>AccessKey</summary>
    [DisplayName("AccessKey")]
    [Description("AccessKey")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("AccessKey", "AccessKey", "")]
    public String AccessKey { get => _AccessKey; set { if (OnPropertyChanging("AccessKey", value)) { _AccessKey = value; OnPropertyChanged("AccessKey"); } } }

    private String _AccessKeySecret;
    /// <summary>AccessKeySecret</summary>
    [DisplayName("AccessKeySecret")]
    [Description("AccessKeySecret")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("AccessKeySecret", "AccessKeySecret", "")]
    public String AccessKeySecret { get => _AccessKeySecret; set { if (OnPropertyChanging("AccessKeySecret", value)) { _AccessKeySecret = value; OnPropertyChanged("AccessKeySecret"); } } }

    private String _PassKey;
    /// <summary>短信签名</summary>
    [DisplayName("短信签名")]
    [Description("短信签名")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("PassKey", "短信签名", "")]
    public String PassKey { get => _PassKey; set { if (OnPropertyChanging("PassKey", value)) { _PassKey = value; OnPropertyChanged("PassKey"); } } }

    private String _Content;
    /// <summary>允许的短信类型，以逗号分隔。SmsLogin为登录短信,SmsRegister为注册短信,SmsPassword为找回密码短信</summary>
    [DisplayName("允许的短信类型")]
    [Description("允许的短信类型，以逗号分隔。SmsLogin为登录短信,SmsRegister为注册短信,SmsPassword为找回密码短信")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Content", "允许的短信类型，以逗号分隔。SmsLogin为登录短信,SmsRegister为注册短信,SmsPassword为找回密码短信", "")]
    public String Content { get => _Content; set { if (OnPropertyChanging("Content", value)) { _Content = value; OnPropertyChanged("Content"); } } }
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

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public override Object this[String name]
    {
        get => name switch
        {
            "Id" => _Id,
            "Code" => _Code,
            "SType" => _SType,
            "IsEnabled" => _IsEnabled,
            "IsDefault" => _IsDefault,
            "AccessKey" => _AccessKey,
            "AccessKeySecret" => _AccessKeySecret,
            "PassKey" => _PassKey,
            "Content" => _Content,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "Code": _Code = Convert.ToString(value); break;
                case "SType": _SType = value.ToInt(); break;
                case "IsEnabled": _IsEnabled = value.ToBoolean(); break;
                case "IsDefault": _IsDefault = value.ToBoolean(); break;
                case "AccessKey": _AccessKey = Convert.ToString(value); break;
                case "AccessKeySecret": _AccessKeySecret = Convert.ToString(value); break;
                case "PassKey": _PassKey = Convert.ToString(value); break;
                case "Content": _Content = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得短信配置字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>编码</summary>
        public static readonly Field Code = FindByName("Code");

        /// <summary>类型。0为通知类，1为营销类</summary>
        public static readonly Field SType = FindByName("SType");

        /// <summary>是否启用</summary>
        public static readonly Field IsEnabled = FindByName("IsEnabled");

        /// <summary>是否默认</summary>
        public static readonly Field IsDefault = FindByName("IsDefault");

        /// <summary>AccessKey</summary>
        public static readonly Field AccessKey = FindByName("AccessKey");

        /// <summary>AccessKeySecret</summary>
        public static readonly Field AccessKeySecret = FindByName("AccessKeySecret");

        /// <summary>短信签名</summary>
        public static readonly Field PassKey = FindByName("PassKey");

        /// <summary>允许的短信类型，以逗号分隔。SmsLogin为登录短信,SmsRegister为注册短信,SmsPassword为找回密码短信</summary>
        public static readonly Field Content = FindByName("Content");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得短信配置字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>编码</summary>
        public const String Code = "Code";

        /// <summary>类型。0为通知类，1为营销类</summary>
        public const String SType = "SType";

        /// <summary>是否启用</summary>
        public const String IsEnabled = "IsEnabled";

        /// <summary>是否默认</summary>
        public const String IsDefault = "IsDefault";

        /// <summary>AccessKey</summary>
        public const String AccessKey = "AccessKey";

        /// <summary>AccessKeySecret</summary>
        public const String AccessKeySecret = "AccessKeySecret";

        /// <summary>短信签名</summary>
        public const String PassKey = "PassKey";

        /// <summary>允许的短信类型，以逗号分隔。SmsLogin为登录短信,SmsRegister为注册短信,SmsPassword为找回密码短信</summary>
        public const String Content = "Content";
    }
    #endregion
}
