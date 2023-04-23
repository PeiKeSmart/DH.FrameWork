using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>站点基础信息翻译表</summary>
public partial interface ISiteInfoLan
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>站点基础信息Id</summary>
    Int32 SiteInfoId { get; set; }

    /// <summary>关联所属语言Id</summary>
    Int32 LanguageId { get; set; }

    /// <summary>网站名称</summary>
    String SiteName { get; set; }

    /// <summary>网站SEO标题</summary>
    String SeoTitle { get; set; }

    /// <summary>网站SEO关键字</summary>
    String SeoKey { get; set; }

    /// <summary>网站SEO描述</summary>
    String SeoDescribe { get; set; }

    /// <summary>备案号</summary>
    String Registration { get; set; }

    /// <summary>网站版权等信息</summary>
    String SiteCopyright { get; set; }
    #endregion
}
