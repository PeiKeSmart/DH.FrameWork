using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>预存款充值表</summary>
public partial class PdRechargeModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>记录唯一标示</summary>
    public String Sn { get; set; }

    /// <summary>会员ID</summary>
    public Int32 UId { get; set; }

    /// <summary>会员名称</summary>
    public String UName { get; set; }

    /// <summary>充值金额</summary>
    public Decimal Amount { get; set; }

    /// <summary>支付方式</summary>
    public String PCode { get; set; }

    /// <summary>第三方支付接口交易号</summary>
    public String TradeSn { get; set; }

    /// <summary>支付状态。是否支付</summary>
    public Boolean State { get; set; }

    /// <summary>支付时间</summary>
    public DateTime PayTime { get; set; }

    /// <summary>创建者</summary>
    public String CreateUser { get; set; }

    /// <summary>创建用户</summary>
    public Int32 CreateUserID { get; set; }

    /// <summary>创建地址</summary>
    public String CreateIP { get; set; }

    /// <summary>创建时间</summary>
    public DateTime CreateTime { get; set; }
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
}
