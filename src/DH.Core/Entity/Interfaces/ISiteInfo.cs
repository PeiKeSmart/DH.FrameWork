using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>站点基础信息。目前表只启用部分字段</summary>
public partial interface ISiteInfo
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>网站域名</summary>
    String Url { get; set; }

    /// <summary>网站主机集合。以,分隔且没有http(s)</summary>
    String Hosts { get; set; }

    /// <summary>是否启用SSL</summary>
    Boolean SslEnabled { get; set; }

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

    /// <summary>网站名称</summary>
    String SiteName { get; set; }

    /// <summary>网站Logo</summary>
    String SiteLogo { get; set; }

    /// <summary>网站描述</summary>
    String Summary { get; set; }

    /// <summary>公司电话</summary>
    String SiteTel { get; set; }

    /// <summary>公司传真</summary>
    String SiteFax { get; set; }

    /// <summary>公司人事邮箱</summary>
    String SiteEmail { get; set; }

    /// <summary>公司客服QQ</summary>
    String QQ { get; set; }

    /// <summary>公司客服手机</summary>
    String SiteMobile { get; set; }

    /// <summary>微信公众号图片</summary>
    String WeiXin { get; set; }

    /// <summary>微博链接地址或者二维码</summary>
    String WeiBo { get; set; }

    /// <summary>公司地址</summary>
    String SiteAddress { get; set; }

    /// <summary>网站备案号其它等信息</summary>
    String SiteCode { get; set; }

    /// <summary>网站SEO标题</summary>
    String SeoTitle { get; set; }

    /// <summary>网站SEO关键字</summary>
    String SeoKey { get; set; }

    /// <summary>网站SEO描述</summary>
    String SeoDescribe { get; set; }

    /// <summary>网站版权等信息</summary>
    String SiteCopyright { get; set; }

    /// <summary>网站开启关闭状态</summary>
    Byte Status { get; set; }

    /// <summary>如果状态关闭，请输入关闭网站原因</summary>
    String CloseInfo { get; set; }

    /// <summary>备案号</summary>
    String Registration { get; set; }

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
