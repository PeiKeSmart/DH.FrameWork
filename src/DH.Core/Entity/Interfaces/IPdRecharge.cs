using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>预存款充值表</summary>
public partial interface IPdRecharge
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>记录唯一标示</summary>
    String Sn { get; set; }

    /// <summary>会员ID</summary>
    Int32 UId { get; set; }

    /// <summary>会员名称</summary>
    String UName { get; set; }

    /// <summary>充值金额</summary>
    Decimal Amount { get; set; }

    /// <summary>支付方式</summary>
    String PCode { get; set; }

    /// <summary>第三方支付接口交易号</summary>
    String TradeSn { get; set; }

    /// <summary>支付状态。是否支付</summary>
    Boolean State { get; set; }

    /// <summary>支付时间</summary>
    DateTime PayTime { get; set; }

    /// <summary>创建者</summary>
    String CreateUser { get; set; }

    /// <summary>创建用户</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }
    #endregion
}
