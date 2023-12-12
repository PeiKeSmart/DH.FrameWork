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

/// <summary>认证日志</summary>
[Serializable]
[DataObject]
[Description("认证日志")]
[BindIndex("IX_DG_AuthCheckLog_UId_CheckType", false, "UId,CheckType")]
[BindTable("DG_AuthCheckLog", Description = "认证日志", ConnName = "DG", DbType = DatabaseType.None)]
public partial class AuthCheckLog : IAuthCheckLog, IEntity<IAuthCheckLog>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Int32 _UId;
    /// <summary>会员ID</summary>
    [DisplayName("会员ID")]
    [Description("会员ID")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("UId", "会员ID", "")]
    public Int32 UId { get => _UId; set { if (OnPropertyChanging("UId", value)) { _UId = value; OnPropertyChanged("UId"); } } }

    private String _IdCard;
    /// <summary>身份证号</summary>
    [DisplayName("身份证号")]
    [Description("身份证号")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("IdCard", "身份证号", "")]
    public String IdCard { get => _IdCard; set { if (OnPropertyChanging("IdCard", value)) { _IdCard = value; OnPropertyChanged("IdCard"); } } }

    private String _Mobile;
    /// <summary>手机号</summary>
    [DisplayName("手机号")]
    [Description("手机号")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Mobile", "手机号", "")]
    public String Mobile { get => _Mobile; set { if (OnPropertyChanging("Mobile", value)) { _Mobile = value; OnPropertyChanged("Mobile"); } } }

    private String _TrueName;
    /// <summary>真实姓名</summary>
    [DisplayName("真实姓名")]
    [Description("真实姓名")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("TrueName", "真实姓名", "")]
    public String TrueName { get => _TrueName; set { if (OnPropertyChanging("TrueName", value)) { _TrueName = value; OnPropertyChanged("TrueName"); } } }

    private Int16 _CheckType;
    /// <summary>认证类型。1为身份证认证，2为手机号认证，3为银行卡认证</summary>
    [DisplayName("认证类型")]
    [Description("认证类型。1为身份证认证，2为手机号认证，3为银行卡认证")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CheckType", "认证类型。1为身份证认证，2为手机号认证，3为银行卡认证", "")]
    public Int16 CheckType { get => _CheckType; set { if (OnPropertyChanging("CheckType", value)) { _CheckType = value; OnPropertyChanged("CheckType"); } } }

    private Boolean _State;
    /// <summary>认证是否成功</summary>
    [DisplayName("认证是否成功")]
    [Description("认证是否成功")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("State", "认证是否成功", "")]
    public Boolean State { get => _State; set { if (OnPropertyChanging("State", value)) { _State = value; OnPropertyChanged("State"); } } }

    private String _Remark;
    /// <summary>返回的数据值</summary>
    [DisplayName("返回的数据值")]
    [Description("返回的数据值")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("Remark", "返回的数据值", "")]
    public String Remark { get => _Remark; set { if (OnPropertyChanging("Remark", value)) { _Remark = value; OnPropertyChanged("Remark"); } } }

    private String _CreateUser;
    /// <summary>创建者</summary>
    [DisplayName("创建者")]
    [Description("创建者")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateUser", "创建者", "")]
    public String CreateUser { get => _CreateUser; set { if (OnPropertyChanging("CreateUser", value)) { _CreateUser = value; OnPropertyChanged("CreateUser"); } } }

    private Int32 _CreateUserID;
    /// <summary>创建用户</summary>
    [DisplayName("创建用户")]
    [Description("创建用户")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CreateUserID", "创建用户", "")]
    public Int32 CreateUserID { get => _CreateUserID; set { if (OnPropertyChanging("CreateUserID", value)) { _CreateUserID = value; OnPropertyChanged("CreateUserID"); } } }

    private String _CreateIP;
    /// <summary>创建地址</summary>
    [DisplayName("创建地址")]
    [Description("创建地址")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateIP", "创建地址", "")]
    public String CreateIP { get => _CreateIP; set { if (OnPropertyChanging("CreateIP", value)) { _CreateIP = value; OnPropertyChanged("CreateIP"); } } }

    private DateTime _CreateTime;
    /// <summary>创建时间</summary>
    [DisplayName("创建时间")]
    [Description("创建时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("CreateTime", "创建时间", "")]
    public DateTime CreateTime { get => _CreateTime; set { if (OnPropertyChanging("CreateTime", value)) { _CreateTime = value; OnPropertyChanged("CreateTime"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IAuthCheckLog model)
    {
        Id = model.Id;
        UId = model.UId;
        IdCard = model.IdCard;
        Mobile = model.Mobile;
        TrueName = model.TrueName;
        CheckType = model.CheckType;
        State = model.State;
        Remark = model.Remark;
        CreateUser = model.CreateUser;
        CreateUserID = model.CreateUserID;
        CreateIP = model.CreateIP;
        CreateTime = model.CreateTime;
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
            "UId" => _UId,
            "IdCard" => _IdCard,
            "Mobile" => _Mobile,
            "TrueName" => _TrueName,
            "CheckType" => _CheckType,
            "State" => _State,
            "Remark" => _Remark,
            "CreateUser" => _CreateUser,
            "CreateUserID" => _CreateUserID,
            "CreateIP" => _CreateIP,
            "CreateTime" => _CreateTime,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "UId": _UId = value.ToInt(); break;
                case "IdCard": _IdCard = Convert.ToString(value); break;
                case "Mobile": _Mobile = Convert.ToString(value); break;
                case "TrueName": _TrueName = Convert.ToString(value); break;
                case "CheckType": _CheckType = Convert.ToInt16(value); break;
                case "State": _State = value.ToBoolean(); break;
                case "Remark": _Remark = Convert.ToString(value); break;
                case "CreateUser": _CreateUser = Convert.ToString(value); break;
                case "CreateUserID": _CreateUserID = value.ToInt(); break;
                case "CreateIP": _CreateIP = Convert.ToString(value); break;
                case "CreateTime": _CreateTime = value.ToDateTime(); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得认证日志字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>会员ID</summary>
        public static readonly Field UId = FindByName("UId");

        /// <summary>身份证号</summary>
        public static readonly Field IdCard = FindByName("IdCard");

        /// <summary>手机号</summary>
        public static readonly Field Mobile = FindByName("Mobile");

        /// <summary>真实姓名</summary>
        public static readonly Field TrueName = FindByName("TrueName");

        /// <summary>认证类型。1为身份证认证，2为手机号认证，3为银行卡认证</summary>
        public static readonly Field CheckType = FindByName("CheckType");

        /// <summary>认证是否成功</summary>
        public static readonly Field State = FindByName("State");

        /// <summary>返回的数据值</summary>
        public static readonly Field Remark = FindByName("Remark");

        /// <summary>创建者</summary>
        public static readonly Field CreateUser = FindByName("CreateUser");

        /// <summary>创建用户</summary>
        public static readonly Field CreateUserID = FindByName("CreateUserID");

        /// <summary>创建地址</summary>
        public static readonly Field CreateIP = FindByName("CreateIP");

        /// <summary>创建时间</summary>
        public static readonly Field CreateTime = FindByName("CreateTime");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得认证日志字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>会员ID</summary>
        public const String UId = "UId";

        /// <summary>身份证号</summary>
        public const String IdCard = "IdCard";

        /// <summary>手机号</summary>
        public const String Mobile = "Mobile";

        /// <summary>真实姓名</summary>
        public const String TrueName = "TrueName";

        /// <summary>认证类型。1为身份证认证，2为手机号认证，3为银行卡认证</summary>
        public const String CheckType = "CheckType";

        /// <summary>认证是否成功</summary>
        public const String State = "State";

        /// <summary>返回的数据值</summary>
        public const String Remark = "Remark";

        /// <summary>创建者</summary>
        public const String CreateUser = "CreateUser";

        /// <summary>创建用户</summary>
        public const String CreateUserID = "CreateUserID";

        /// <summary>创建地址</summary>
        public const String CreateIP = "CreateIP";

        /// <summary>创建时间</summary>
        public const String CreateTime = "CreateTime";
    }
    #endregion
}
