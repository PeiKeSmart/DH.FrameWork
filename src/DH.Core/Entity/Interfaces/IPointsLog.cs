using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>会员积分日志表</summary>
public partial interface IPointsLog
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>会员ID</summary>
    Int32 UId { get; set; }

    /// <summary>会员名称</summary>
    String UName { get; set; }

    /// <summary>管理员ID</summary>
    Int32 AdminId { get; set; }

    /// <summary>管理员名称</summary>
    String AdminName { get; set; }

    /// <summary>积分数,负数为扣除</summary>
    Int32 Points { get; set; }

    /// <summary>积分操作描述</summary>
    String Desc { get; set; }

    /// <summary>积分操作阶段。regist注册,login登录,comments商品评论,order订单消费,system系统调整,pointorder礼品兑换,exchange积分兑换,signin签到,inviter推荐注册,rebate推荐返利</summary>
    String Stage { get; set; }

    /// <summary>创建者</summary>
    String CreateUser { get; set; }

    /// <summary>创建者</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>积分添加时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }
    #endregion
}
