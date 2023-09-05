using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;
using NewLife.Reflection;

namespace DH.Entity;

/// <summary>站点基础信息翻译表</summary>
public partial class SiteInfoLanModel : IModel
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

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public virtual Object this[String name]
    {
        get
        {
            return name switch
            {
                "Id" => Id,
                "SiteInfoId" => SiteInfoId,
                "LanguageId" => LanguageId,
                "SiteName" => SiteName,
                "SeoTitle" => SeoTitle,
                "SeoKey" => SeoKey,
                "SeoDescribe" => SeoDescribe,
                "Registration" => Registration,
                "SiteCopyright" => SiteCopyright,
                _ => this.GetValue(name),
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "SiteInfoId": SiteInfoId = value.ToInt(); break;
                case "LanguageId": LanguageId = value.ToInt(); break;
                case "SiteName": SiteName = Convert.ToString(value); break;
                case "SeoTitle": SeoTitle = Convert.ToString(value); break;
                case "SeoKey": SeoKey = Convert.ToString(value); break;
                case "SeoDescribe": SeoDescribe = Convert.ToString(value); break;
                case "Registration": Registration = Convert.ToString(value); break;
                case "SiteCopyright": SiteCopyright = Convert.ToString(value); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
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
