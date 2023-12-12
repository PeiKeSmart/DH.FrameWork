using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>预付款变更日志表</summary>
public partial class PdLogModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>会员ID</summary>
    public Int32 UId { get; set; }

    /// <summary>会员名称</summary>
    public String UName { get; set; }

    /// <summary>管理员ID</summary>
    public Int32 AdminId { get; set; }

    /// <summary>管理员名称</summary>
    public String AdminName { get; set; }

    /// <summary>order_pay下单支付预存款,order_freeze下单冻结预存款,order_cancel取消订单解冻预存款,order_comb_pay下单支付被冻结的预存款,recharge充值,cash_apply申请提现冻结预存款,cash_pay提现成功,cash_del取消提现申请-解冻预存款,refund退款,sys_add_money管理员调节增加余额,sys_del_money管理员调节减少余额,order_points积分充值,sys_thaw_money管理员调整解冻余额,sys_freeze_money管理员调整冻结余额</summary>
    public String PdType { get; set; }

    /// <summary>可用金额变更0:未变更</summary>
    public Decimal Amount { get; set; }

    /// <summary>冻结金额变更0:未变更</summary>
    public Decimal FreezeAmount { get; set; }

    /// <summary>变更后的可用余额</summary>
    public Decimal Balance { get; set; }

    /// <summary>变更后的冻结金额</summary>
    public Decimal FreezeBalance { get; set; }

    /// <summary>变更描述</summary>
    public String Desc { get; set; }

    /// <summary>变更者</summary>
    public String CreateUser { get; set; }

    /// <summary>变更者</summary>
    public Int32 CreateUserID { get; set; }

    /// <summary>变更添加时间</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    public String CreateIP { get; set; }
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
}
