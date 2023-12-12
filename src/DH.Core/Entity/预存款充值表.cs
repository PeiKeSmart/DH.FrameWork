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

/// <summary>预存款充值表</summary>
[Serializable]
[DataObject]
[Description("预存款充值表")]
[BindIndex("IX_DG_PdRecharge_UId", false, "UId")]
[BindIndex("IU_DG_PdRecharge_Sn", true, "Sn")]
[BindTable("DG_PdRecharge", Description = "预存款充值表", ConnName = "DG", DbType = DatabaseType.None)]
public partial class PdRecharge : IPdRecharge, IEntity<IPdRecharge>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _Sn;
    /// <summary>记录唯一标示</summary>
    [DisplayName("记录唯一标示")]
    [Description("记录唯一标示")]
    [DataObjectField(false, false, true, 20)]
    [BindColumn("Sn", "记录唯一标示", "")]
    public String Sn { get => _Sn; set { if (OnPropertyChanging("Sn", value)) { _Sn = value; OnPropertyChanged("Sn"); } } }

    private Int32 _UId;
    /// <summary>会员ID</summary>
    [DisplayName("会员ID")]
    [Description("会员ID")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("UId", "会员ID", "")]
    public Int32 UId { get => _UId; set { if (OnPropertyChanging("UId", value)) { _UId = value; OnPropertyChanged("UId"); } } }

    private String _UName;
    /// <summary>会员名称</summary>
    [DisplayName("会员名称")]
    [Description("会员名称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("UName", "会员名称", "")]
    public String UName { get => _UName; set { if (OnPropertyChanging("UName", value)) { _UName = value; OnPropertyChanged("UName"); } } }

    private Decimal _Amount;
    /// <summary>充值金额</summary>
    [DisplayName("充值金额")]
    [Description("充值金额")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Amount", "充值金额", "")]
    public Decimal Amount { get => _Amount; set { if (OnPropertyChanging("Amount", value)) { _Amount = value; OnPropertyChanged("Amount"); } } }

    private String _PCode;
    /// <summary>支付方式</summary>
    [DisplayName("支付方式")]
    [Description("支付方式")]
    [DataObjectField(false, false, true, 20)]
    [BindColumn("PCode", "支付方式", "")]
    public String PCode { get => _PCode; set { if (OnPropertyChanging("PCode", value)) { _PCode = value; OnPropertyChanged("PCode"); } } }

    private String _TradeSn;
    /// <summary>第三方支付接口交易号</summary>
    [DisplayName("第三方支付接口交易号")]
    [Description("第三方支付接口交易号")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("TradeSn", "第三方支付接口交易号", "")]
    public String TradeSn { get => _TradeSn; set { if (OnPropertyChanging("TradeSn", value)) { _TradeSn = value; OnPropertyChanged("TradeSn"); } } }

    private Boolean _State;
    /// <summary>支付状态。是否支付</summary>
    [DisplayName("支付状态")]
    [Description("支付状态。是否支付")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("State", "支付状态。是否支付", "")]
    public Boolean State { get => _State; set { if (OnPropertyChanging("State", value)) { _State = value; OnPropertyChanged("State"); } } }

    private DateTime _PayTime;
    /// <summary>支付时间</summary>
    [DisplayName("支付时间")]
    [Description("支付时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("PayTime", "支付时间", "")]
    public DateTime PayTime { get => _PayTime; set { if (OnPropertyChanging("PayTime", value)) { _PayTime = value; OnPropertyChanged("PayTime"); } } }

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
    public void Copy(IPdRecharge model)
    {
        Id = model.Id;
        Sn = model.Sn;
        UId = model.UId;
        UName = model.UName;
        Amount = model.Amount;
        PCode = model.PCode;
        TradeSn = model.TradeSn;
        State = model.State;
        PayTime = model.PayTime;
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
            "Sn" => _Sn,
            "UId" => _UId,
            "UName" => _UName,
            "Amount" => _Amount,
            "PCode" => _PCode,
            "TradeSn" => _TradeSn,
            "State" => _State,
            "PayTime" => _PayTime,
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
                case "Sn": _Sn = Convert.ToString(value); break;
                case "UId": _UId = value.ToInt(); break;
                case "UName": _UName = Convert.ToString(value); break;
                case "Amount": _Amount = Convert.ToDecimal(value); break;
                case "PCode": _PCode = Convert.ToString(value); break;
                case "TradeSn": _TradeSn = Convert.ToString(value); break;
                case "State": _State = value.ToBoolean(); break;
                case "PayTime": _PayTime = value.ToDateTime(); break;
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
    /// <summary>取得预存款充值表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>记录唯一标示</summary>
        public static readonly Field Sn = FindByName("Sn");

        /// <summary>会员ID</summary>
        public static readonly Field UId = FindByName("UId");

        /// <summary>会员名称</summary>
        public static readonly Field UName = FindByName("UName");

        /// <summary>充值金额</summary>
        public static readonly Field Amount = FindByName("Amount");

        /// <summary>支付方式</summary>
        public static readonly Field PCode = FindByName("PCode");

        /// <summary>第三方支付接口交易号</summary>
        public static readonly Field TradeSn = FindByName("TradeSn");

        /// <summary>支付状态。是否支付</summary>
        public static readonly Field State = FindByName("State");

        /// <summary>支付时间</summary>
        public static readonly Field PayTime = FindByName("PayTime");

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

    /// <summary>取得预存款充值表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>记录唯一标示</summary>
        public const String Sn = "Sn";

        /// <summary>会员ID</summary>
        public const String UId = "UId";

        /// <summary>会员名称</summary>
        public const String UName = "UName";

        /// <summary>充值金额</summary>
        public const String Amount = "Amount";

        /// <summary>支付方式</summary>
        public const String PCode = "PCode";

        /// <summary>第三方支付接口交易号</summary>
        public const String TradeSn = "TradeSn";

        /// <summary>支付状态。是否支付</summary>
        public const String State = "State";

        /// <summary>支付时间</summary>
        public const String PayTime = "PayTime";

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
