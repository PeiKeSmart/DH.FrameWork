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

/// <summary>预付款变更日志表</summary>
[Serializable]
[DataObject]
[Description("预付款变更日志表")]
[BindIndex("IX_DG_PdLog_UId", false, "UId")]
[BindTable("DG_PdLog", Description = "预付款变更日志表", ConnName = "DG", DbType = DatabaseType.None)]
public partial class PdLog : IPdLog, IEntity<IPdLog>
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

    private String _UName;
    /// <summary>会员名称</summary>
    [DisplayName("会员名称")]
    [Description("会员名称")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("UName", "会员名称", "")]
    public String UName { get => _UName; set { if (OnPropertyChanging("UName", value)) { _UName = value; OnPropertyChanged("UName"); } } }

    private Int32 _AdminId;
    /// <summary>管理员ID</summary>
    [DisplayName("管理员ID")]
    [Description("管理员ID")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("AdminId", "管理员ID", "")]
    public Int32 AdminId { get => _AdminId; set { if (OnPropertyChanging("AdminId", value)) { _AdminId = value; OnPropertyChanged("AdminId"); } } }

    private String _AdminName;
    /// <summary>管理员名称</summary>
    [DisplayName("管理员名称")]
    [Description("管理员名称")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("AdminName", "管理员名称", "")]
    public String AdminName { get => _AdminName; set { if (OnPropertyChanging("AdminName", value)) { _AdminName = value; OnPropertyChanged("AdminName"); } } }

    private String _PdType;
    /// <summary>order_pay下单支付预存款,order_freeze下单冻结预存款,order_cancel取消订单解冻预存款,order_comb_pay下单支付被冻结的预存款,recharge充值,cash_apply申请提现冻结预存款,cash_pay提现成功,cash_del取消提现申请-解冻预存款,refund退款,sys_add_money管理员调节增加余额,sys_del_money管理员调节减少余额,order_points积分充值,sys_thaw_money管理员调整解冻余额,sys_freeze_money管理员调整冻结余额</summary>
    [DisplayName("order_pay下单支付预存款")]
    [Description("order_pay下单支付预存款,order_freeze下单冻结预存款,order_cancel取消订单解冻预存款,order_comb_pay下单支付被冻结的预存款,recharge充值,cash_apply申请提现冻结预存款,cash_pay提现成功,cash_del取消提现申请-解冻预存款,refund退款,sys_add_money管理员调节增加余额,sys_del_money管理员调节减少余额,order_points积分充值,sys_thaw_money管理员调整解冻余额,sys_freeze_money管理员调整冻结余额")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("PdType", "order_pay下单支付预存款,order_freeze下单冻结预存款,order_cancel取消订单解冻预存款,order_comb_pay下单支付被冻结的预存款,recharge充值,cash_apply申请提现冻结预存款,cash_pay提现成功,cash_del取消提现申请-解冻预存款,refund退款,sys_add_money管理员调节增加余额,sys_del_money管理员调节减少余额,order_points积分充值,sys_thaw_money管理员调整解冻余额,sys_freeze_money管理员调整冻结余额", "")]
    public String PdType { get => _PdType; set { if (OnPropertyChanging("PdType", value)) { _PdType = value; OnPropertyChanged("PdType"); } } }

    private Decimal _Amount;
    /// <summary>可用金额变更0:未变更</summary>
    [DisplayName("可用金额变更0")]
    [Description("可用金额变更0:未变更")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Amount", "可用金额变更0:未变更", "")]
    public Decimal Amount { get => _Amount; set { if (OnPropertyChanging("Amount", value)) { _Amount = value; OnPropertyChanged("Amount"); } } }

    private Decimal _FreezeAmount;
    /// <summary>冻结金额变更0:未变更</summary>
    [DisplayName("冻结金额变更0")]
    [Description("冻结金额变更0:未变更")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("FreezeAmount", "冻结金额变更0:未变更", "")]
    public Decimal FreezeAmount { get => _FreezeAmount; set { if (OnPropertyChanging("FreezeAmount", value)) { _FreezeAmount = value; OnPropertyChanged("FreezeAmount"); } } }

    private Decimal _Balance;
    /// <summary>变更后的可用余额</summary>
    [DisplayName("变更后的可用余额")]
    [Description("变更后的可用余额")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Balance", "变更后的可用余额", "")]
    public Decimal Balance { get => _Balance; set { if (OnPropertyChanging("Balance", value)) { _Balance = value; OnPropertyChanged("Balance"); } } }

    private Decimal _FreezeBalance;
    /// <summary>变更后的冻结金额</summary>
    [DisplayName("变更后的冻结金额")]
    [Description("变更后的冻结金额")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("FreezeBalance", "变更后的冻结金额", "")]
    public Decimal FreezeBalance { get => _FreezeBalance; set { if (OnPropertyChanging("FreezeBalance", value)) { _FreezeBalance = value; OnPropertyChanged("FreezeBalance"); } } }

    private String _Desc;
    /// <summary>变更描述</summary>
    [DisplayName("变更描述")]
    [Description("变更描述")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Desc", "变更描述", "")]
    public String Desc { get => _Desc; set { if (OnPropertyChanging("Desc", value)) { _Desc = value; OnPropertyChanged("Desc"); } } }

    private String _CreateUser;
    /// <summary>变更者</summary>
    [DisplayName("变更者")]
    [Description("变更者")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateUser", "变更者", "")]
    public String CreateUser { get => _CreateUser; set { if (OnPropertyChanging("CreateUser", value)) { _CreateUser = value; OnPropertyChanged("CreateUser"); } } }

    private Int32 _CreateUserID;
    /// <summary>变更者</summary>
    [DisplayName("变更者")]
    [Description("变更者")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CreateUserID", "变更者", "")]
    public Int32 CreateUserID { get => _CreateUserID; set { if (OnPropertyChanging("CreateUserID", value)) { _CreateUserID = value; OnPropertyChanged("CreateUserID"); } } }

    private DateTime _CreateTime;
    /// <summary>变更添加时间</summary>
    [DisplayName("变更添加时间")]
    [Description("变更添加时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("CreateTime", "变更添加时间", "")]
    public DateTime CreateTime { get => _CreateTime; set { if (OnPropertyChanging("CreateTime", value)) { _CreateTime = value; OnPropertyChanged("CreateTime"); } } }

    private String _CreateIP;
    /// <summary>创建地址</summary>
    [DisplayName("创建地址")]
    [Description("创建地址")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateIP", "创建地址", "")]
    public String CreateIP { get => _CreateIP; set { if (OnPropertyChanging("CreateIP", value)) { _CreateIP = value; OnPropertyChanged("CreateIP"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IPdLog model)
    {
        Id = model.Id;
        UId = model.UId;
        UName = model.UName;
        AdminId = model.AdminId;
        AdminName = model.AdminName;
        PdType = model.PdType;
        Amount = model.Amount;
        FreezeAmount = model.FreezeAmount;
        Balance = model.Balance;
        FreezeBalance = model.FreezeBalance;
        Desc = model.Desc;
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
            "UId" => _UId,
            "UName" => _UName,
            "AdminId" => _AdminId,
            "AdminName" => _AdminName,
            "PdType" => _PdType,
            "Amount" => _Amount,
            "FreezeAmount" => _FreezeAmount,
            "Balance" => _Balance,
            "FreezeBalance" => _FreezeBalance,
            "Desc" => _Desc,
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
                case "UId": _UId = value.ToInt(); break;
                case "UName": _UName = Convert.ToString(value); break;
                case "AdminId": _AdminId = value.ToInt(); break;
                case "AdminName": _AdminName = Convert.ToString(value); break;
                case "PdType": _PdType = Convert.ToString(value); break;
                case "Amount": _Amount = Convert.ToDecimal(value); break;
                case "FreezeAmount": _FreezeAmount = Convert.ToDecimal(value); break;
                case "Balance": _Balance = Convert.ToDecimal(value); break;
                case "FreezeBalance": _FreezeBalance = Convert.ToDecimal(value); break;
                case "Desc": _Desc = Convert.ToString(value); break;
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
    /// <summary>取得预付款变更日志表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>会员ID</summary>
        public static readonly Field UId = FindByName("UId");

        /// <summary>会员名称</summary>
        public static readonly Field UName = FindByName("UName");

        /// <summary>管理员ID</summary>
        public static readonly Field AdminId = FindByName("AdminId");

        /// <summary>管理员名称</summary>
        public static readonly Field AdminName = FindByName("AdminName");

        /// <summary>order_pay下单支付预存款,order_freeze下单冻结预存款,order_cancel取消订单解冻预存款,order_comb_pay下单支付被冻结的预存款,recharge充值,cash_apply申请提现冻结预存款,cash_pay提现成功,cash_del取消提现申请-解冻预存款,refund退款,sys_add_money管理员调节增加余额,sys_del_money管理员调节减少余额,order_points积分充值,sys_thaw_money管理员调整解冻余额,sys_freeze_money管理员调整冻结余额</summary>
        public static readonly Field PdType = FindByName("PdType");

        /// <summary>可用金额变更0:未变更</summary>
        public static readonly Field Amount = FindByName("Amount");

        /// <summary>冻结金额变更0:未变更</summary>
        public static readonly Field FreezeAmount = FindByName("FreezeAmount");

        /// <summary>变更后的可用余额</summary>
        public static readonly Field Balance = FindByName("Balance");

        /// <summary>变更后的冻结金额</summary>
        public static readonly Field FreezeBalance = FindByName("FreezeBalance");

        /// <summary>变更描述</summary>
        public static readonly Field Desc = FindByName("Desc");

        /// <summary>变更者</summary>
        public static readonly Field CreateUser = FindByName("CreateUser");

        /// <summary>变更者</summary>
        public static readonly Field CreateUserID = FindByName("CreateUserID");

        /// <summary>变更添加时间</summary>
        public static readonly Field CreateTime = FindByName("CreateTime");

        /// <summary>创建地址</summary>
        public static readonly Field CreateIP = FindByName("CreateIP");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得预付款变更日志表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>会员ID</summary>
        public const String UId = "UId";

        /// <summary>会员名称</summary>
        public const String UName = "UName";

        /// <summary>管理员ID</summary>
        public const String AdminId = "AdminId";

        /// <summary>管理员名称</summary>
        public const String AdminName = "AdminName";

        /// <summary>order_pay下单支付预存款,order_freeze下单冻结预存款,order_cancel取消订单解冻预存款,order_comb_pay下单支付被冻结的预存款,recharge充值,cash_apply申请提现冻结预存款,cash_pay提现成功,cash_del取消提现申请-解冻预存款,refund退款,sys_add_money管理员调节增加余额,sys_del_money管理员调节减少余额,order_points积分充值,sys_thaw_money管理员调整解冻余额,sys_freeze_money管理员调整冻结余额</summary>
        public const String PdType = "PdType";

        /// <summary>可用金额变更0:未变更</summary>
        public const String Amount = "Amount";

        /// <summary>冻结金额变更0:未变更</summary>
        public const String FreezeAmount = "FreezeAmount";

        /// <summary>变更后的可用余额</summary>
        public const String Balance = "Balance";

        /// <summary>变更后的冻结金额</summary>
        public const String FreezeBalance = "FreezeBalance";

        /// <summary>变更描述</summary>
        public const String Desc = "Desc";

        /// <summary>变更者</summary>
        public const String CreateUser = "CreateUser";

        /// <summary>变更者</summary>
        public const String CreateUserID = "CreateUserID";

        /// <summary>变更添加时间</summary>
        public const String CreateTime = "CreateTime";

        /// <summary>创建地址</summary>
        public const String CreateIP = "CreateIP";
    }
    #endregion
}
