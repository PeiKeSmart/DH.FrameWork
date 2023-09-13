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

/// <summary>消息记录</summary>
[Serializable]
[DataObject]
[Description("消息记录")]
[BindIndex("IX_DG_SendLog_Account", false, "Account")]
[BindIndex("IX_DG_SendLog_SmsId", false, "SmsId")]
[BindTable("DG_SendLog", Description = "消息记录", ConnName = "DG", DbType = DatabaseType.None)]
public partial class SendLog : ISendLog, IEntity<ISendLog>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Int16 _SType;
    /// <summary>0为短信，1为邮箱</summary>
    [DisplayName("0为短信")]
    [Description("0为短信，1为邮箱")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("SType", "0为短信，1为邮箱", "")]
    public Int16 SType { get => _SType; set { if (OnPropertyChanging("SType", value)) { _SType = value; OnPropertyChanged("SType"); } } }

    private String _Account;
    /// <summary>手机号/邮箱</summary>
    [DisplayName("手机号_邮箱")]
    [Description("手机号/邮箱")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Account", "手机号/邮箱", "")]
    public String Account { get => _Account; set { if (OnPropertyChanging("Account", value)) { _Account = value; OnPropertyChanged("Account"); } } }

    private String _Msg;
    /// <summary>消息内容</summary>
    [DisplayName("消息内容")]
    [Description("消息内容")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("Msg", "消息内容", "")]
    public String Msg { get => _Msg; set { if (OnPropertyChanging("Msg", value)) { _Msg = value; OnPropertyChanged("Msg"); } } }

    private Int16 _MType;
    /// <summary>类型：1为注册，2为登录，3为找回密码，4绑定手机/邮箱，5安全验证,6账号申诉，0测试</summary>
    [DisplayName("类型")]
    [Description("类型：1为注册，2为登录，3为找回密码，4绑定手机/邮箱，5安全验证,6账号申诉，0测试")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("MType", "类型：1为注册，2为登录，3为找回密码，4绑定手机/邮箱，5安全验证,6账号申诉，0测试", "")]
    public Int16 MType { get => _MType; set { if (OnPropertyChanging("MType", value)) { _MType = value; OnPropertyChanged("MType"); } } }

    private String _Remark;
    /// <summary>发送回复数据</summary>
    [DisplayName("发送回复数据")]
    [Description("发送回复数据")]
    [DataObjectField(false, false, true, 300)]
    [BindColumn("Remark", "发送回复数据", "")]
    public String Remark { get => _Remark; set { if (OnPropertyChanging("Remark", value)) { _Remark = value; OnPropertyChanged("Remark"); } } }

    private String _SmsId;
    /// <summary>短信平台返回的Id</summary>
    [DisplayName("短信平台返回的Id")]
    [Description("短信平台返回的Id")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("SmsId", "短信平台返回的Id", "")]
    public String SmsId { get => _SmsId; set { if (OnPropertyChanging("SmsId", value)) { _SmsId = value; OnPropertyChanged("SmsId"); } } }

    private String _CreateUser;
    /// <summary>消息会员名</summary>
    [DisplayName("消息会员名")]
    [Description("消息会员名")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateUser", "消息会员名", "")]
    public String CreateUser { get => _CreateUser; set { if (OnPropertyChanging("CreateUser", value)) { _CreateUser = value; OnPropertyChanged("CreateUser"); } } }

    private Int32 _CreateUserID;
    /// <summary>消息会员ID，注册为0</summary>
    [DisplayName("消息会员ID")]
    [Description("消息会员ID，注册为0")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CreateUserID", "消息会员ID，注册为0", "")]
    public Int32 CreateUserID { get => _CreateUserID; set { if (OnPropertyChanging("CreateUserID", value)) { _CreateUserID = value; OnPropertyChanged("CreateUserID"); } } }

    private DateTime _CreateTime;
    /// <summary>消息添加时间</summary>
    [DisplayName("消息添加时间")]
    [Description("消息添加时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("CreateTime", "消息添加时间", "")]
    public DateTime CreateTime { get => _CreateTime; set { if (OnPropertyChanging("CreateTime", value)) { _CreateTime = value; OnPropertyChanged("CreateTime"); } } }

    private String _CreateIP;
    /// <summary>消息请求IP</summary>
    [DisplayName("消息请求IP")]
    [Description("消息请求IP")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateIP", "消息请求IP", "")]
    public String CreateIP { get => _CreateIP; set { if (OnPropertyChanging("CreateIP", value)) { _CreateIP = value; OnPropertyChanged("CreateIP"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISendLog model)
    {
        Id = model.Id;
        SType = model.SType;
        Account = model.Account;
        Msg = model.Msg;
        MType = model.MType;
        Remark = model.Remark;
        SmsId = model.SmsId;
        CreateUser = model.CreateUser;
        CreateUserID = model.CreateUserID;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
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
            "SType" => _SType,
            "Account" => _Account,
            "Msg" => _Msg,
            "MType" => _MType,
            "Remark" => _Remark,
            "SmsId" => _SmsId,
            "CreateUser" => _CreateUser,
            "CreateUserID" => _CreateUserID,
            "CreateTime" => _CreateTime,
            "CreateIP" => _CreateIP,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "SType": _SType = Convert.ToInt16(value); break;
                case "Account": _Account = Convert.ToString(value); break;
                case "Msg": _Msg = Convert.ToString(value); break;
                case "MType": _MType = Convert.ToInt16(value); break;
                case "Remark": _Remark = Convert.ToString(value); break;
                case "SmsId": _SmsId = Convert.ToString(value); break;
                case "CreateUser": _CreateUser = Convert.ToString(value); break;
                case "CreateUserID": _CreateUserID = value.ToInt(); break;
                case "CreateTime": _CreateTime = value.ToDateTime(); break;
                case "CreateIP": _CreateIP = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得消息记录字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>0为短信，1为邮箱</summary>
        public static readonly Field SType = FindByName("SType");

        /// <summary>手机号/邮箱</summary>
        public static readonly Field Account = FindByName("Account");

        /// <summary>消息内容</summary>
        public static readonly Field Msg = FindByName("Msg");

        /// <summary>类型：1为注册，2为登录，3为找回密码，4绑定手机/邮箱，5安全验证,6账号申诉，0测试</summary>
        public static readonly Field MType = FindByName("MType");

        /// <summary>发送回复数据</summary>
        public static readonly Field Remark = FindByName("Remark");

        /// <summary>短信平台返回的Id</summary>
        public static readonly Field SmsId = FindByName("SmsId");

        /// <summary>消息会员名</summary>
        public static readonly Field CreateUser = FindByName("CreateUser");

        /// <summary>消息会员ID，注册为0</summary>
        public static readonly Field CreateUserID = FindByName("CreateUserID");

        /// <summary>消息添加时间</summary>
        public static readonly Field CreateTime = FindByName("CreateTime");

        /// <summary>消息请求IP</summary>
        public static readonly Field CreateIP = FindByName("CreateIP");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得消息记录字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>0为短信，1为邮箱</summary>
        public const String SType = "SType";

        /// <summary>手机号/邮箱</summary>
        public const String Account = "Account";

        /// <summary>消息内容</summary>
        public const String Msg = "Msg";

        /// <summary>类型：1为注册，2为登录，3为找回密码，4绑定手机/邮箱，5安全验证,6账号申诉，0测试</summary>
        public const String MType = "MType";

        /// <summary>发送回复数据</summary>
        public const String Remark = "Remark";

        /// <summary>短信平台返回的Id</summary>
        public const String SmsId = "SmsId";

        /// <summary>消息会员名</summary>
        public const String CreateUser = "CreateUser";

        /// <summary>消息会员ID，注册为0</summary>
        public const String CreateUserID = "CreateUserID";

        /// <summary>消息添加时间</summary>
        public const String CreateTime = "CreateTime";

        /// <summary>消息请求IP</summary>
        public const String CreateIP = "CreateIP";
    }
    #endregion
}
