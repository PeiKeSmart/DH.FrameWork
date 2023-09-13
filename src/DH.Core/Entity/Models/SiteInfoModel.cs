using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>站点基础信息。目前表只启用部分字段</summary>
public partial class SiteInfoModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>网站域名</summary>
    public String Url { get; set; }

    /// <summary>网站主机集合。以,分隔且没有http(s)</summary>
    public String Hosts { get; set; }

    /// <summary>是否启用SSL</summary>
    public Boolean SslEnabled { get; set; }

    /// <summary>此站点的默认语言的标识符。使用默认语言时设置0</summary>
    public Int32 DefaultLanguageId { get; set; }

    /// <summary>获取或设置显示顺序</summary>
    public Int32 DisplayOrder { get; set; }

    /// <summary>公司名称</summary>
    public String CompanyName { get; set; }

    /// <summary>公司地址</summary>
    public String CompanyAddress { get; set; }

    /// <summary>公司电话号码</summary>
    public String CompanyPhoneNumber { get; set; }

    /// <summary>公司VAT。用于欧盟国家/地区</summary>
    public String CompanyVat { get; set; }

    /// <summary>网站名称</summary>
    public String SiteName { get; set; }

    /// <summary>网站Logo</summary>
    public String SiteLogo { get; set; }

    /// <summary>网站描述</summary>
    public String Summary { get; set; }

    /// <summary>公司电话</summary>
    public String SiteTel { get; set; }

    /// <summary>公司传真</summary>
    public String SiteFax { get; set; }

    /// <summary>公司人事邮箱</summary>
    public String SiteEmail { get; set; }

    /// <summary>公司客服QQ</summary>
    public String QQ { get; set; }

    /// <summary>公司客服手机</summary>
    public String SiteMobile { get; set; }

    /// <summary>微信公众号图片</summary>
    public String WeiXin { get; set; }

    /// <summary>微博链接地址或者二维码</summary>
    public String WeiBo { get; set; }

    /// <summary>公司地址</summary>
    public String SiteAddress { get; set; }

    /// <summary>网站备案号其它等信息</summary>
    public String SiteCode { get; set; }

    /// <summary>网站SEO标题</summary>
    public String SeoTitle { get; set; }

    /// <summary>网站SEO关键字</summary>
    public String SeoKey { get; set; }

    /// <summary>网站SEO描述</summary>
    public String SeoDescribe { get; set; }

    /// <summary>网站版权等信息</summary>
    public String SiteCopyright { get; set; }

    /// <summary>网站开启关闭状态</summary>
    public Byte Status { get; set; }

    /// <summary>如果状态关闭，请输入关闭网站原因</summary>
    public String CloseInfo { get; set; }

    /// <summary>备案号</summary>
    public String Registration { get; set; }

    /// <summary>创建者</summary>
    public String CreateUser { get; set; }

    /// <summary>创建者</summary>
    public Int32 CreateUserID { get; set; }

    /// <summary>创建时间</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    public String CreateIP { get; set; }

    /// <summary>更新者</summary>
    public String UpdateUser { get; set; }

    /// <summary>更新者</summary>
    public Int32 UpdateUserID { get; set; }

    /// <summary>更新时间</summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>更新地址</summary>
    public String UpdateIP { get; set; }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISiteInfo model)
    {
        Id = model.Id;
        Url = model.Url;
        Hosts = model.Hosts;
        SslEnabled = model.SslEnabled;
        DefaultLanguageId = model.DefaultLanguageId;
        DisplayOrder = model.DisplayOrder;
        CompanyName = model.CompanyName;
        CompanyAddress = model.CompanyAddress;
        CompanyPhoneNumber = model.CompanyPhoneNumber;
        CompanyVat = model.CompanyVat;
        SiteName = model.SiteName;
        SiteLogo = model.SiteLogo;
        Summary = model.Summary;
        SiteTel = model.SiteTel;
        SiteFax = model.SiteFax;
        SiteEmail = model.SiteEmail;
        QQ = model.QQ;
        SiteMobile = model.SiteMobile;
        WeiXin = model.WeiXin;
        WeiBo = model.WeiBo;
        SiteAddress = model.SiteAddress;
        SiteCode = model.SiteCode;
        SeoTitle = model.SeoTitle;
        SeoKey = model.SeoKey;
        SeoDescribe = model.SeoDescribe;
        SiteCopyright = model.SiteCopyright;
        Status = model.Status;
        CloseInfo = model.CloseInfo;
        Registration = model.Registration;
        CreateUser = model.CreateUser;
        CreateUserID = model.CreateUserID;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
        UpdateUser = model.UpdateUser;
        UpdateUserID = model.UpdateUserID;
        UpdateTime = model.UpdateTime;
        UpdateIP = model.UpdateIP;
    }
    #endregion
}
