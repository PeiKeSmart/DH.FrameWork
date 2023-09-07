using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>站点基础信息翻译表</summary>
public partial class SiteInfoLanModel : ISiteInfoLan
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>站点基础信息Id</summary>
    public Int32 SiteInfoId { get; set; }

    /// <summary>关联所属语言Id</summary>
    public Int32 LanguageId { get; set; }

    /// <summary>网站名称</summary>
    public String SiteName { get; set; }

    /// <summary>网站SEO标题</summary>
    public String SeoTitle { get; set; }

    /// <summary>网站SEO关键字</summary>
    public String SeoKey { get; set; }

    /// <summary>网站SEO描述</summary>
    public String SeoDescribe { get; set; }

    /// <summary>备案号</summary>
    public String Registration { get; set; }

    /// <summary>网站版权等信息</summary>
    public String SiteCopyright { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISiteInfoLan model)
    {
        Id = model.Id;
        SiteInfoId = model.SiteInfoId;
        LanguageId = model.LanguageId;
        SiteName = model.SiteName;
        SeoTitle = model.SeoTitle;
        SeoKey = model.SeoKey;
        SeoDescribe = model.SeoDescribe;
        Registration = model.Registration;
        SiteCopyright = model.SiteCopyright;
    }
    #endregion
}
