using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace DH.Entity;

/// <summary>站点基础信息。目前表只启用部分字段</summary>
[Serializable]
[DataObject]
[Description("站点基础信息。目前表只启用部分字段")]
[BindTable("DH_SiteInfo", Description = "站点基础信息。目前表只启用部分字段", ConnName = "DG", DbType = DatabaseType.None)]
public partial class SiteInfo : ISiteInfo, IEntity<SiteInfoModel>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _Url;
    /// <summary>网站域名</summary>
    [DisplayName("网站域名")]
    [Description("网站域名")]
    [DataObjectField(false, false, true, 250)]
    [BindColumn("Url", "网站域名", "")]
    public String Url { get => _Url; set { if (OnPropertyChanging("Url", value)) { _Url = value; OnPropertyChanged("Url"); } } }

    private String _Hosts;
    /// <summary>网站主机集合。以,分隔且没有http(s)</summary>
    [DisplayName("网站主机集合")]
    [Description("网站主机集合。以,分隔且没有http(s)")]
    [DataObjectField(false, false, true, 250)]
    [BindColumn("Hosts", "网站主机集合。以,分隔且没有http(s)", "")]
    public String Hosts { get => _Hosts; set { if (OnPropertyChanging("Hosts", value)) { _Hosts = value; OnPropertyChanged("Hosts"); } } }

    private String _SiteName;
    /// <summary>网站名称</summary>
    [DisplayName("网站名称")]
    [Description("网站名称")]
    [DataObjectField(false, false, false, 50)]
    [BindColumn("SiteName", "网站名称", "")]
    public String SiteName { get => _SiteName; set { if (OnPropertyChanging("SiteName", value)) { _SiteName = value; OnPropertyChanged("SiteName"); } } }

    private String _SiteLogo;
    /// <summary>网站Logo</summary>
    [DisplayName("网站Logo")]
    [Description("网站Logo")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("SiteLogo", "网站Logo", "")]
    public String SiteLogo { get => _SiteLogo; set { if (OnPropertyChanging("SiteLogo", value)) { _SiteLogo = value; OnPropertyChanged("SiteLogo"); } } }

    private String _Summary;
    /// <summary>网站描述</summary>
    [DisplayName("网站描述")]
    [Description("网站描述")]
    [DataObjectField(false, false, true, 500)]
    [BindColumn("Summary", "网站描述", "")]
    public String Summary { get => _Summary; set { if (OnPropertyChanging("Summary", value)) { _Summary = value; OnPropertyChanged("Summary"); } } }

    private String _SiteTel;
    /// <summary>公司电话</summary>
    [DisplayName("公司电话")]
    [Description("公司电话")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("SiteTel", "公司电话", "")]
    public String SiteTel { get => _SiteTel; set { if (OnPropertyChanging("SiteTel", value)) { _SiteTel = value; OnPropertyChanged("SiteTel"); } } }

    private String _SiteFax;
    /// <summary>公司传真</summary>
    [DisplayName("公司传真")]
    [Description("公司传真")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("SiteFax", "公司传真", "")]
    public String SiteFax { get => _SiteFax; set { if (OnPropertyChanging("SiteFax", value)) { _SiteFax = value; OnPropertyChanged("SiteFax"); } } }

    private String _SiteEmail;
    /// <summary>公司人事邮箱</summary>
    [DisplayName("公司人事邮箱")]
    [Description("公司人事邮箱")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("SiteEmail", "公司人事邮箱", "")]
    public String SiteEmail { get => _SiteEmail; set { if (OnPropertyChanging("SiteEmail", value)) { _SiteEmail = value; OnPropertyChanged("SiteEmail"); } } }

    private String _QQ;
    /// <summary>公司客服QQ</summary>
    [DisplayName("公司客服QQ")]
    [Description("公司客服QQ")]
    [DataObjectField(false, false, true, 500)]
    [BindColumn("QQ", "公司客服QQ", "")]
    public String QQ { get => _QQ; set { if (OnPropertyChanging("QQ", value)) { _QQ = value; OnPropertyChanged("QQ"); } } }

    private String _SiteMobile;
    /// <summary>公司客服手机</summary>
    [DisplayName("公司客服手机")]
    [Description("公司客服手机")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("SiteMobile", "公司客服手机", "")]
    public String SiteMobile { get => _SiteMobile; set { if (OnPropertyChanging("SiteMobile", value)) { _SiteMobile = value; OnPropertyChanged("SiteMobile"); } } }

    private String _WeiXin;
    /// <summary>微信公众号图片</summary>
    [DisplayName("微信公众号图片")]
    [Description("微信公众号图片")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("WeiXin", "微信公众号图片", "")]
    public String WeiXin { get => _WeiXin; set { if (OnPropertyChanging("WeiXin", value)) { _WeiXin = value; OnPropertyChanged("WeiXin"); } } }

    private String _WeiBo;
    /// <summary>微博链接地址或者二维码</summary>
    [DisplayName("微博链接地址或者二维码")]
    [Description("微博链接地址或者二维码")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("WeiBo", "微博链接地址或者二维码", "")]
    public String WeiBo { get => _WeiBo; set { if (OnPropertyChanging("WeiBo", value)) { _WeiBo = value; OnPropertyChanged("WeiBo"); } } }

    private String _SiteAddress;
    /// <summary>公司地址</summary>
    [DisplayName("公司地址")]
    [Description("公司地址")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("SiteAddress", "公司地址", "")]
    public String SiteAddress { get => _SiteAddress; set { if (OnPropertyChanging("SiteAddress", value)) { _SiteAddress = value; OnPropertyChanged("SiteAddress"); } } }

    private String _SiteCode;
    /// <summary>网站备案号其它等信息</summary>
    [DisplayName("网站备案号其它等信息")]
    [Description("网站备案号其它等信息")]
    [DataObjectField(false, false, true, 2000)]
    [BindColumn("SiteCode", "网站备案号其它等信息", "")]
    public String SiteCode { get => _SiteCode; set { if (OnPropertyChanging("SiteCode", value)) { _SiteCode = value; OnPropertyChanged("SiteCode"); } } }

    private String _SeoTitle;
    /// <summary>网站SEO标题</summary>
    [DisplayName("网站SEO标题")]
    [Description("网站SEO标题")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("SeoTitle", "网站SEO标题", "")]
    public String SeoTitle { get => _SeoTitle; set { if (OnPropertyChanging("SeoTitle", value)) { _SeoTitle = value; OnPropertyChanged("SeoTitle"); } } }

    private String _SeoKey;
    /// <summary>网站SEO关键字</summary>
    [DisplayName("网站SEO关键字")]
    [Description("网站SEO关键字")]
    [DataObjectField(false, false, true, 500)]
    [BindColumn("SeoKey", "网站SEO关键字", "")]
    public String SeoKey { get => _SeoKey; set { if (OnPropertyChanging("SeoKey", value)) { _SeoKey = value; OnPropertyChanged("SeoKey"); } } }

    private String _SeoDescribe;
    /// <summary>网站SEO描述</summary>
    [DisplayName("网站SEO描述")]
    [Description("网站SEO描述")]
    [DataObjectField(false, false, true, 2000)]
    [BindColumn("SeoDescribe", "网站SEO描述", "")]
    public String SeoDescribe { get => _SeoDescribe; set { if (OnPropertyChanging("SeoDescribe", value)) { _SeoDescribe = value; OnPropertyChanged("SeoDescribe"); } } }

    private String _SiteCopyright;
    /// <summary>网站版权等信息</summary>
    [DisplayName("网站版权等信息")]
    [Description("网站版权等信息")]
    [DataObjectField(false, false, true, 2000)]
    [BindColumn("SiteCopyright", "网站版权等信息", "")]
    public String SiteCopyright { get => _SiteCopyright; set { if (OnPropertyChanging("SiteCopyright", value)) { _SiteCopyright = value; OnPropertyChanged("SiteCopyright"); } } }

    private Byte _Status;
    /// <summary>网站开启关闭状态</summary>
    [DisplayName("网站开启关闭状态")]
    [Description("网站开启关闭状态")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("Status", "网站开启关闭状态", "")]
    public Byte Status { get => _Status; set { if (OnPropertyChanging("Status", value)) { _Status = value; OnPropertyChanged("Status"); } } }

    private String _CloseInfo;
    /// <summary>如果状态关闭，请输入关闭网站原因</summary>
    [DisplayName("如果状态关闭")]
    [Description("如果状态关闭，请输入关闭网站原因")]
    [DataObjectField(false, false, true, 2000)]
    [BindColumn("CloseInfo", "如果状态关闭，请输入关闭网站原因", "")]
    public String CloseInfo { get => _CloseInfo; set { if (OnPropertyChanging("CloseInfo", value)) { _CloseInfo = value; OnPropertyChanged("CloseInfo"); } } }

    private String _Registration;
    /// <summary>备案号</summary>
    [DisplayName("备案号")]
    [Description("备案号")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Registration", "备案号", "")]
    public String Registration { get => _Registration; set { if (OnPropertyChanging("Registration", value)) { _Registration = value; OnPropertyChanged("Registration"); } } }

    private String _CreateUser;
    /// <summary>创建者</summary>
    [DisplayName("创建者")]
    [Description("创建者")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateUser", "创建者", "")]
    public String CreateUser { get => _CreateUser; set { if (OnPropertyChanging("CreateUser", value)) { _CreateUser = value; OnPropertyChanged("CreateUser"); } } }

    private Int32 _CreateUserID;
    /// <summary>创建者</summary>
    [DisplayName("创建者")]
    [Description("创建者")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CreateUserID", "创建者", "")]
    public Int32 CreateUserID { get => _CreateUserID; set { if (OnPropertyChanging("CreateUserID", value)) { _CreateUserID = value; OnPropertyChanged("CreateUserID"); } } }

    private DateTime _CreateTime;
    /// <summary>创建时间</summary>
    [DisplayName("创建时间")]
    [Description("创建时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("CreateTime", "创建时间", "")]
    public DateTime CreateTime { get => _CreateTime; set { if (OnPropertyChanging("CreateTime", value)) { _CreateTime = value; OnPropertyChanged("CreateTime"); } } }

    private String _CreateIP;
    /// <summary>创建地址</summary>
    [DisplayName("创建地址")]
    [Description("创建地址")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CreateIP", "创建地址", "")]
    public String CreateIP { get => _CreateIP; set { if (OnPropertyChanging("CreateIP", value)) { _CreateIP = value; OnPropertyChanged("CreateIP"); } } }

    private String _UpdateUser;
    /// <summary>更新者</summary>
    [DisplayName("更新者")]
    [Description("更新者")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("UpdateUser", "更新者", "")]
    public String UpdateUser { get => _UpdateUser; set { if (OnPropertyChanging("UpdateUser", value)) { _UpdateUser = value; OnPropertyChanged("UpdateUser"); } } }

    private Int32 _UpdateUserID;
    /// <summary>更新者</summary>
    [DisplayName("更新者")]
    [Description("更新者")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("UpdateUserID", "更新者", "")]
    public Int32 UpdateUserID { get => _UpdateUserID; set { if (OnPropertyChanging("UpdateUserID", value)) { _UpdateUserID = value; OnPropertyChanged("UpdateUserID"); } } }

    private DateTime _UpdateTime;
    /// <summary>更新时间</summary>
    [DisplayName("更新时间")]
    [Description("更新时间")]
    [DataObjectField(false, false, true, 0)]
    [BindColumn("UpdateTime", "更新时间", "")]
    public DateTime UpdateTime { get => _UpdateTime; set { if (OnPropertyChanging("UpdateTime", value)) { _UpdateTime = value; OnPropertyChanged("UpdateTime"); } } }

    private String _UpdateIP;
    /// <summary>更新地址</summary>
    [DisplayName("更新地址")]
    [Description("更新地址")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("UpdateIP", "更新地址", "")]
    public String UpdateIP { get => _UpdateIP; set { if (OnPropertyChanging("UpdateIP", value)) { _UpdateIP = value; OnPropertyChanged("UpdateIP"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(SiteInfoModel model)
    {
        Id = model.Id;
        Url = model.Url;
        Hosts = model.Hosts;
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

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public override Object this[String name]
    {
        get => name switch
        {
            "Id" => _Id,
            "Url" => _Url,
            "Hosts" => _Hosts,
            "SiteName" => _SiteName,
            "SiteLogo" => _SiteLogo,
            "Summary" => _Summary,
            "SiteTel" => _SiteTel,
            "SiteFax" => _SiteFax,
            "SiteEmail" => _SiteEmail,
            "QQ" => _QQ,
            "SiteMobile" => _SiteMobile,
            "WeiXin" => _WeiXin,
            "WeiBo" => _WeiBo,
            "SiteAddress" => _SiteAddress,
            "SiteCode" => _SiteCode,
            "SeoTitle" => _SeoTitle,
            "SeoKey" => _SeoKey,
            "SeoDescribe" => _SeoDescribe,
            "SiteCopyright" => _SiteCopyright,
            "Status" => _Status,
            "CloseInfo" => _CloseInfo,
            "Registration" => _Registration,
            "CreateUser" => _CreateUser,
            "CreateUserID" => _CreateUserID,
            "CreateTime" => _CreateTime,
            "CreateIP" => _CreateIP,
            "UpdateUser" => _UpdateUser,
            "UpdateUserID" => _UpdateUserID,
            "UpdateTime" => _UpdateTime,
            "UpdateIP" => _UpdateIP,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "Url": _Url = Convert.ToString(value); break;
                case "Hosts": _Hosts = Convert.ToString(value); break;
                case "SiteName": _SiteName = Convert.ToString(value); break;
                case "SiteLogo": _SiteLogo = Convert.ToString(value); break;
                case "Summary": _Summary = Convert.ToString(value); break;
                case "SiteTel": _SiteTel = Convert.ToString(value); break;
                case "SiteFax": _SiteFax = Convert.ToString(value); break;
                case "SiteEmail": _SiteEmail = Convert.ToString(value); break;
                case "QQ": _QQ = Convert.ToString(value); break;
                case "SiteMobile": _SiteMobile = Convert.ToString(value); break;
                case "WeiXin": _WeiXin = Convert.ToString(value); break;
                case "WeiBo": _WeiBo = Convert.ToString(value); break;
                case "SiteAddress": _SiteAddress = Convert.ToString(value); break;
                case "SiteCode": _SiteCode = Convert.ToString(value); break;
                case "SeoTitle": _SeoTitle = Convert.ToString(value); break;
                case "SeoKey": _SeoKey = Convert.ToString(value); break;
                case "SeoDescribe": _SeoDescribe = Convert.ToString(value); break;
                case "SiteCopyright": _SiteCopyright = Convert.ToString(value); break;
                case "Status": _Status = Convert.ToByte(value); break;
                case "CloseInfo": _CloseInfo = Convert.ToString(value); break;
                case "Registration": _Registration = Convert.ToString(value); break;
                case "CreateUser": _CreateUser = Convert.ToString(value); break;
                case "CreateUserID": _CreateUserID = value.ToInt(); break;
                case "CreateTime": _CreateTime = value.ToDateTime(); break;
                case "CreateIP": _CreateIP = Convert.ToString(value); break;
                case "UpdateUser": _UpdateUser = Convert.ToString(value); break;
                case "UpdateUserID": _UpdateUserID = value.ToInt(); break;
                case "UpdateTime": _UpdateTime = value.ToDateTime(); break;
                case "UpdateIP": _UpdateIP = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得站点基础信息字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>网站域名</summary>
        public static readonly Field Url = FindByName("Url");

        /// <summary>网站主机集合。以,分隔且没有http(s)</summary>
        public static readonly Field Hosts = FindByName("Hosts");

        /// <summary>网站名称</summary>
        public static readonly Field SiteName = FindByName("SiteName");

        /// <summary>网站Logo</summary>
        public static readonly Field SiteLogo = FindByName("SiteLogo");

        /// <summary>网站描述</summary>
        public static readonly Field Summary = FindByName("Summary");

        /// <summary>公司电话</summary>
        public static readonly Field SiteTel = FindByName("SiteTel");

        /// <summary>公司传真</summary>
        public static readonly Field SiteFax = FindByName("SiteFax");

        /// <summary>公司人事邮箱</summary>
        public static readonly Field SiteEmail = FindByName("SiteEmail");

        /// <summary>公司客服QQ</summary>
        public static readonly Field QQ = FindByName("QQ");

        /// <summary>公司客服手机</summary>
        public static readonly Field SiteMobile = FindByName("SiteMobile");

        /// <summary>微信公众号图片</summary>
        public static readonly Field WeiXin = FindByName("WeiXin");

        /// <summary>微博链接地址或者二维码</summary>
        public static readonly Field WeiBo = FindByName("WeiBo");

        /// <summary>公司地址</summary>
        public static readonly Field SiteAddress = FindByName("SiteAddress");

        /// <summary>网站备案号其它等信息</summary>
        public static readonly Field SiteCode = FindByName("SiteCode");

        /// <summary>网站SEO标题</summary>
        public static readonly Field SeoTitle = FindByName("SeoTitle");

        /// <summary>网站SEO关键字</summary>
        public static readonly Field SeoKey = FindByName("SeoKey");

        /// <summary>网站SEO描述</summary>
        public static readonly Field SeoDescribe = FindByName("SeoDescribe");

        /// <summary>网站版权等信息</summary>
        public static readonly Field SiteCopyright = FindByName("SiteCopyright");

        /// <summary>网站开启关闭状态</summary>
        public static readonly Field Status = FindByName("Status");

        /// <summary>如果状态关闭，请输入关闭网站原因</summary>
        public static readonly Field CloseInfo = FindByName("CloseInfo");

        /// <summary>备案号</summary>
        public static readonly Field Registration = FindByName("Registration");

        /// <summary>创建者</summary>
        public static readonly Field CreateUser = FindByName("CreateUser");

        /// <summary>创建者</summary>
        public static readonly Field CreateUserID = FindByName("CreateUserID");

        /// <summary>创建时间</summary>
        public static readonly Field CreateTime = FindByName("CreateTime");

        /// <summary>创建地址</summary>
        public static readonly Field CreateIP = FindByName("CreateIP");

        /// <summary>更新者</summary>
        public static readonly Field UpdateUser = FindByName("UpdateUser");

        /// <summary>更新者</summary>
        public static readonly Field UpdateUserID = FindByName("UpdateUserID");

        /// <summary>更新时间</summary>
        public static readonly Field UpdateTime = FindByName("UpdateTime");

        /// <summary>更新地址</summary>
        public static readonly Field UpdateIP = FindByName("UpdateIP");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得站点基础信息字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>网站域名</summary>
        public const String Url = "Url";

        /// <summary>网站主机集合。以,分隔且没有http(s)</summary>
        public const String Hosts = "Hosts";

        /// <summary>网站名称</summary>
        public const String SiteName = "SiteName";

        /// <summary>网站Logo</summary>
        public const String SiteLogo = "SiteLogo";

        /// <summary>网站描述</summary>
        public const String Summary = "Summary";

        /// <summary>公司电话</summary>
        public const String SiteTel = "SiteTel";

        /// <summary>公司传真</summary>
        public const String SiteFax = "SiteFax";

        /// <summary>公司人事邮箱</summary>
        public const String SiteEmail = "SiteEmail";

        /// <summary>公司客服QQ</summary>
        public const String QQ = "QQ";

        /// <summary>公司客服手机</summary>
        public const String SiteMobile = "SiteMobile";

        /// <summary>微信公众号图片</summary>
        public const String WeiXin = "WeiXin";

        /// <summary>微博链接地址或者二维码</summary>
        public const String WeiBo = "WeiBo";

        /// <summary>公司地址</summary>
        public const String SiteAddress = "SiteAddress";

        /// <summary>网站备案号其它等信息</summary>
        public const String SiteCode = "SiteCode";

        /// <summary>网站SEO标题</summary>
        public const String SeoTitle = "SeoTitle";

        /// <summary>网站SEO关键字</summary>
        public const String SeoKey = "SeoKey";

        /// <summary>网站SEO描述</summary>
        public const String SeoDescribe = "SeoDescribe";

        /// <summary>网站版权等信息</summary>
        public const String SiteCopyright = "SiteCopyright";

        /// <summary>网站开启关闭状态</summary>
        public const String Status = "Status";

        /// <summary>如果状态关闭，请输入关闭网站原因</summary>
        public const String CloseInfo = "CloseInfo";

        /// <summary>备案号</summary>
        public const String Registration = "Registration";

        /// <summary>创建者</summary>
        public const String CreateUser = "CreateUser";

        /// <summary>创建者</summary>
        public const String CreateUserID = "CreateUserID";

        /// <summary>创建时间</summary>
        public const String CreateTime = "CreateTime";

        /// <summary>创建地址</summary>
        public const String CreateIP = "CreateIP";

        /// <summary>更新者</summary>
        public const String UpdateUser = "UpdateUser";

        /// <summary>更新者</summary>
        public const String UpdateUserID = "UpdateUserID";

        /// <summary>更新时间</summary>
        public const String UpdateTime = "UpdateTime";

        /// <summary>更新地址</summary>
        public const String UpdateIP = "UpdateIP";
    }
    #endregion
}
