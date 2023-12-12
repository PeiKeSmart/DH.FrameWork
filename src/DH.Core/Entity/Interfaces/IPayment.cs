using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>支付方式</summary>
public partial interface IPayment
{
    #region 属性
    /// <summary>支付代码</summary>
    String Id { get; set; }

    /// <summary>支付名称</summary>
    String Name { get; set; }

    /// <summary>支付接口配置信息</summary>
    String Config { get; set; }

    /// <summary>支付方式所适应平台 pc h5 app wm</summary>
    String Platform { get; set; }

    /// <summary>支持的语言Id集合</summary>
    String LanguageIds { get; set; }

    /// <summary>接口状态,是否启用</summary>
    Boolean State { get; set; }

    /// <summary>创建者</summary>
    String CreateUser { get; set; }

    /// <summary>创建者</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>更新者</summary>
    String UpdateUser { get; set; }

    /// <summary>更新者</summary>
    Int32 UpdateUserID { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }

    /// <summary>更新地址</summary>
    String UpdateIP { get; set; }
    #endregion
}
