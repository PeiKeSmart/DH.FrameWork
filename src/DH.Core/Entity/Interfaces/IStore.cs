using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>站点信息</summary>
public partial interface IStore
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>站点名称</summary>
    String Name { get; set; }

    /// <summary>站点Url</summary>
    String Url { get; set; }

    /// <summary>是否启用SSL</summary>
    Boolean SslEnabled { get; set; }

    /// <summary>可能的HTTP_HOST值的逗号分隔列表</summary>
    String Hosts { get; set; }

    /// <summary>此站点的默认语言的标识符。使用默认语言时设置0</summary>
    Int32 DefaultLanguageId { get; set; }

    /// <summary>获取或设置显示顺序</summary>
    Int32 DisplayOrder { get; set; }

    /// <summary>公司名称</summary>
    String CompanyName { get; set; }

    /// <summary>公司地址</summary>
    String CompanyAddress { get; set; }

    /// <summary>公司电话号码</summary>
    String CompanyPhoneNumber { get; set; }

    /// <summary>公司VAT。用于欧盟国家/地区</summary>
    String CompanyVat { get; set; }
    #endregion
}
