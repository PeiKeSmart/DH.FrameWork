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

/// <summary>用户扩展</summary>
[Serializable]
[DataObject]
[Description("用户扩展")]
[BindIndex("IX_DG_UserDetail_ReferrerId", false, "ReferrerId")]
[BindIndex("IX_DG_UserDetail_TenantId", false, "TenantId")]
[BindIndex("IX_DG_UserDetail_KeFuId", false, "KeFuId")]
[BindIndex("IX_DG_UserDetail_UType", false, "UType")]
[BindIndex("IX_DG_UserDetail_ParentUId", false, "ParentUId")]
[BindTable("DG_UserDetail", Description = "用户扩展", ConnName = "Membership", DbType = DatabaseType.None)]
public partial class UserDetail : IUserDetail, IEntity<IUserDetail>
{
    #region 属性
    private Int32 _Id;
    /// <summary>用户Id</summary>
    [DisplayName("用户Id")]
    [Description("用户Id")]
    [DataObjectField(true, false, false, 0)]
    [BindColumn("Id", "用户Id", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Int32 _LanguageId;
    /// <summary>语言Id</summary>
    [DisplayName("语言Id")]
    [Description("语言Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("LanguageId", "语言Id", "")]
    public Int32 LanguageId { get => _LanguageId; set { if (OnPropertyChanging("LanguageId", value)) { _LanguageId = value; OnPropertyChanged("LanguageId"); } } }

    private Boolean _IsSuper;
    /// <summary>是否超级管理员</summary>
    [DisplayName("是否超级管理员")]
    [Description("是否超级管理员")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsSuper", "是否超级管理员", "")]
    public Boolean IsSuper { get => _IsSuper; set { if (OnPropertyChanging("IsSuper", value)) { _IsSuper = value; OnPropertyChanged("IsSuper"); } } }

    private Int64 _SId;
    /// <summary>用户SessionId</summary>
    [DisplayName("用户SessionId")]
    [Description("用户SessionId")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("SId", "用户SessionId", "")]
    public Int64 SId { get => _SId; set { if (OnPropertyChanging("SId", value)) { _SId = value; OnPropertyChanged("SId"); } } }

    private Int32 _TenantId;
    /// <summary>用户所属租户Id</summary>
    [DisplayName("用户所属租户Id")]
    [Description("用户所属租户Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("TenantId", "用户所属租户Id", "")]
    public Int32 TenantId { get => _TenantId; set { if (OnPropertyChanging("TenantId", value)) { _TenantId = value; OnPropertyChanged("TenantId"); } } }

    private Int16 _UType;
    /// <summary>用户类型。类型自定义</summary>
    [DisplayName("用户类型")]
    [Description("用户类型。类型自定义")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("UType", "用户类型。类型自定义", "")]
    public Int16 UType { get => _UType; set { if (OnPropertyChanging("UType", value)) { _UType = value; OnPropertyChanged("UType"); } } }

    private String _RoleExIds;
    /// <summary>会员前台权限</summary>
    [DisplayName("会员前台权限")]
    [Description("会员前台权限")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("RoleExIds", "会员前台权限", "")]
    public String RoleExIds { get => _RoleExIds; set { if (OnPropertyChanging("RoleExIds", value)) { _RoleExIds = value; OnPropertyChanged("RoleExIds"); } } }

    private String _OtherPermissions;
    /// <summary>会员其他权限</summary>
    [DisplayName("会员其他权限")]
    [Description("会员其他权限")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("OtherPermissions", "会员其他权限", "")]
    public String OtherPermissions { get => _OtherPermissions; set { if (OnPropertyChanging("OtherPermissions", value)) { _OtherPermissions = value; OnPropertyChanged("OtherPermissions"); } } }

    private String _DepartmentIds;
    /// <summary>会员所在多部门</summary>
    [DisplayName("会员所在多部门")]
    [Description("会员所在多部门")]
    [DataObjectField(false, false, true, 200)]
    [BindColumn("DepartmentIds", "会员所在多部门", "")]
    public String DepartmentIds { get => _DepartmentIds; set { if (OnPropertyChanging("DepartmentIds", value)) { _DepartmentIds = value; OnPropertyChanged("DepartmentIds"); } } }

    private String _TrueName;
    /// <summary>真实姓名</summary>
    [DisplayName("真实姓名")]
    [Description("真实姓名")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("TrueName", "真实姓名", "")]
    public String TrueName { get => _TrueName; set { if (OnPropertyChanging("TrueName", value)) { _TrueName = value; OnPropertyChanged("TrueName"); } } }

    private String _PayPwd;
    /// <summary>支付密码</summary>
    [DisplayName("支付密码")]
    [Description("支付密码")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("PayPwd", "支付密码", "")]
    public String PayPwd { get => _PayPwd; set { if (OnPropertyChanging("PayPwd", value)) { _PayPwd = value; OnPropertyChanged("PayPwd"); } } }

    private Int16 _AuthState;
    /// <summary>个人实名认证状态（0默认1审核中2未通过3已认证）</summary>
    [DisplayName("个人实名认证状态（0默认1审核中2未通过3已认证）")]
    [Description("个人实名认证状态（0默认1审核中2未通过3已认证）")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("AuthState", "个人实名认证状态（0默认1审核中2未通过3已认证）", "")]
    public Int16 AuthState { get => _AuthState; set { if (OnPropertyChanging("AuthState", value)) { _AuthState = value; OnPropertyChanged("AuthState"); } } }

    private String _IdCard;
    /// <summary>实名认证身份证号</summary>
    [DisplayName("实名认证身份证号")]
    [Description("实名认证身份证号")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("IdCard", "实名认证身份证号", "")]
    public String IdCard { get => _IdCard; set { if (OnPropertyChanging("IdCard", value)) { _IdCard = value; OnPropertyChanged("IdCard"); } } }

    private String _IdcardImage1;
    /// <summary>手持身份证照</summary>
    [DisplayName("手持身份证照")]
    [Description("手持身份证照")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("IdcardImage1", "手持身份证照", "")]
    public String IdcardImage1 { get => _IdcardImage1; set { if (OnPropertyChanging("IdcardImage1", value)) { _IdcardImage1 = value; OnPropertyChanged("IdcardImage1"); } } }

    private String _IdcardImage2;
    /// <summary>身份证正面照</summary>
    [DisplayName("身份证正面照")]
    [Description("身份证正面照")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("IdcardImage2", "身份证正面照", "")]
    public String IdcardImage2 { get => _IdcardImage2; set { if (OnPropertyChanging("IdcardImage2", value)) { _IdcardImage2 = value; OnPropertyChanged("IdcardImage2"); } } }

    private String _IdcardImage3;
    /// <summary>身份证反面照</summary>
    [DisplayName("身份证反面照")]
    [Description("身份证反面照")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("IdcardImage3", "身份证反面照", "")]
    public String IdcardImage3 { get => _IdcardImage3; set { if (OnPropertyChanging("IdcardImage3", value)) { _IdcardImage3 = value; OnPropertyChanged("IdcardImage3"); } } }

    private String _CompanyName;
    /// <summary>公司名称</summary>
    [DisplayName("公司名称")]
    [Description("公司名称")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("CompanyName", "公司名称", "")]
    public String CompanyName { get => _CompanyName; set { if (OnPropertyChanging("CompanyName", value)) { _CompanyName = value; OnPropertyChanged("CompanyName"); } } }

    private Int16 _CompnayAuthState;
    /// <summary>企业实名认证状态（0默认1审核中2未通过3已认证）</summary>
    [DisplayName("企业实名认证状态（0默认1审核中2未通过3已认证）")]
    [Description("企业实名认证状态（0默认1审核中2未通过3已认证）")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CompnayAuthState", "企业实名认证状态（0默认1审核中2未通过3已认证）", "")]
    public Int16 CompnayAuthState { get => _CompnayAuthState; set { if (OnPropertyChanging("CompnayAuthState", value)) { _CompnayAuthState = value; OnPropertyChanged("CompnayAuthState"); } } }

    private String _CorporateName;
    /// <summary>法人姓名</summary>
    [DisplayName("法人姓名")]
    [Description("法人姓名")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("CorporateName", "法人姓名", "")]
    public String CorporateName { get => _CorporateName; set { if (OnPropertyChanging("CorporateName", value)) { _CorporateName = value; OnPropertyChanged("CorporateName"); } } }

    private String _BusinessLicenseImage;
    /// <summary>营业执照图片</summary>
    [DisplayName("营业执照图片")]
    [Description("营业执照图片")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("BusinessLicenseImage", "营业执照图片", "")]
    public String BusinessLicenseImage { get => _BusinessLicenseImage; set { if (OnPropertyChanging("BusinessLicenseImage", value)) { _BusinessLicenseImage = value; OnPropertyChanged("BusinessLicenseImage"); } } }

    private String _CorporateImage;
    /// <summary>法人手持身份证照</summary>
    [DisplayName("法人手持身份证照")]
    [Description("法人手持身份证照")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("CorporateImage", "法人手持身份证照", "")]
    public String CorporateImage { get => _CorporateImage; set { if (OnPropertyChanging("CorporateImage", value)) { _CorporateImage = value; OnPropertyChanged("CorporateImage"); } } }

    private Boolean _EmailBind;
    /// <summary>是否绑定邮箱</summary>
    [DisplayName("是否绑定邮箱")]
    [Description("是否绑定邮箱")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("EmailBind", "是否绑定邮箱", "")]
    public Boolean EmailBind { get => _EmailBind; set { if (OnPropertyChanging("EmailBind", value)) { _EmailBind = value; OnPropertyChanged("EmailBind"); } } }

    private Boolean _MobileBind;
    /// <summary>是否绑定手机</summary>
    [DisplayName("是否绑定手机")]
    [Description("是否绑定手机")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("MobileBind", "是否绑定手机", "")]
    public Boolean MobileBind { get => _MobileBind; set { if (OnPropertyChanging("MobileBind", value)) { _MobileBind = value; OnPropertyChanged("MobileBind"); } } }

    private Boolean _IsSubScribe;
    /// <summary>是否关注微信公众号</summary>
    [DisplayName("是否关注微信公众号")]
    [Description("是否关注微信公众号")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsSubScribe", "是否关注微信公众号", "")]
    public Boolean IsSubScribe { get => _IsSubScribe; set { if (OnPropertyChanging("IsSubScribe", value)) { _IsSubScribe = value; OnPropertyChanged("IsSubScribe"); } } }

    private Boolean _IsSales;
    /// <summary>是否销售人员</summary>
    [DisplayName("是否销售人员")]
    [Description("是否销售人员")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsSales", "是否销售人员", "")]
    public Boolean IsSales { get => _IsSales; set { if (OnPropertyChanging("IsSales", value)) { _IsSales = value; OnPropertyChanged("IsSales"); } } }

    private Boolean _IsEngineer;
    /// <summary>是否为工程师</summary>
    [DisplayName("是否为工程师")]
    [Description("是否为工程师")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsEngineer", "是否为工程师", "")]
    public Boolean IsEngineer { get => _IsEngineer; set { if (OnPropertyChanging("IsEngineer", value)) { _IsEngineer = value; OnPropertyChanged("IsEngineer"); } } }

    private Int32 _ReferrerId;
    /// <summary>推荐人ID</summary>
    [DisplayName("推荐人ID")]
    [Description("推荐人ID")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("ReferrerId", "推荐人ID", "")]
    public Int32 ReferrerId { get => _ReferrerId; set { if (OnPropertyChanging("ReferrerId", value)) { _ReferrerId = value; OnPropertyChanged("ReferrerId"); } } }

    private Int32 _KeFuId;
    /// <summary>所属销售ID</summary>
    [DisplayName("所属销售ID")]
    [Description("所属销售ID")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("KeFuId", "所属销售ID", "")]
    public Int32 KeFuId { get => _KeFuId; set { if (OnPropertyChanging("KeFuId", value)) { _KeFuId = value; OnPropertyChanged("KeFuId"); } } }

    private Int32 _ParentUId;
    /// <summary>所属上级会员ID</summary>
    [DisplayName("所属上级会员ID")]
    [Description("所属上级会员ID")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("ParentUId", "所属上级会员ID", "")]
    public Int32 ParentUId { get => _ParentUId; set { if (OnPropertyChanging("ParentUId", value)) { _ParentUId = value; OnPropertyChanged("ParentUId"); } } }

    private Int32 _Points;
    /// <summary>会员积分</summary>
    [DisplayName("会员积分")]
    [Description("会员积分")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Points", "会员积分", "")]
    public Int32 Points { get => _Points; set { if (OnPropertyChanging("Points", value)) { _Points = value; OnPropertyChanged("Points"); } } }

    private Int32 _ExpPoints;
    /// <summary>会员经验值</summary>
    [DisplayName("会员经验值")]
    [Description("会员经验值")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("ExpPoints", "会员经验值", "")]
    public Int32 ExpPoints { get => _ExpPoints; set { if (OnPropertyChanging("ExpPoints", value)) { _ExpPoints = value; OnPropertyChanged("ExpPoints"); } } }

    private String _QQ;
    /// <summary>QQ号码</summary>
    [DisplayName("QQ号码")]
    [Description("QQ号码")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("QQ", "QQ号码", "")]
    public String QQ { get => _QQ; set { if (OnPropertyChanging("QQ", value)) { _QQ = value; OnPropertyChanged("QQ"); } } }

    private String _WeiXin;
    /// <summary>微信号码</summary>
    [DisplayName("微信号码")]
    [Description("微信号码")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("WeiXin", "微信号码", "")]
    public String WeiXin { get => _WeiXin; set { if (OnPropertyChanging("WeiXin", value)) { _WeiXin = value; OnPropertyChanged("WeiXin"); } } }

    private String _WangWang;
    /// <summary>阿里旺旺号码</summary>
    [DisplayName("阿里旺旺号码")]
    [Description("阿里旺旺号码")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("WangWang", "阿里旺旺号码", "")]
    public String WangWang { get => _WangWang; set { if (OnPropertyChanging("WangWang", value)) { _WangWang = value; OnPropertyChanged("WangWang"); } } }

    private String _ContactName;
    /// <summary>联系人</summary>
    [DisplayName("联系人")]
    [Description("联系人")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("ContactName", "联系人", "")]
    public String ContactName { get => _ContactName; set { if (OnPropertyChanging("ContactName", value)) { _ContactName = value; OnPropertyChanged("ContactName"); } } }

    private String _Tel;
    /// <summary>固话</summary>
    [DisplayName("固话")]
    [Description("固话")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Tel", "固话", "")]
    public String Tel { get => _Tel; set { if (OnPropertyChanging("Tel", value)) { _Tel = value; OnPropertyChanged("Tel"); } } }

    private String _Fax;
    /// <summary>传真</summary>
    [DisplayName("传真")]
    [Description("传真")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Fax", "传真", "")]
    public String Fax { get => _Fax; set { if (OnPropertyChanging("Fax", value)) { _Fax = value; OnPropertyChanged("Fax"); } } }

    private String _CompanyInfo;
    /// <summary>公司介绍</summary>
    [DisplayName("公司介绍")]
    [Description("公司介绍")]
    [DataObjectField(false, false, true, 1024)]
    [BindColumn("CompanyInfo", "公司介绍", "")]
    public String CompanyInfo { get => _CompanyInfo; set { if (OnPropertyChanging("CompanyInfo", value)) { _CompanyInfo = value; OnPropertyChanged("CompanyInfo"); } } }

    private String _Address;
    /// <summary>详细地址</summary>
    [DisplayName("详细地址")]
    [Description("详细地址")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Address", "详细地址", "")]
    public String Address { get => _Address; set { if (OnPropertyChanging("Address", value)) { _Address = value; OnPropertyChanged("Address"); } } }

    private Boolean _InformAllow;
    /// <summary>是否禁止举报</summary>
    [DisplayName("是否禁止举报")]
    [Description("是否禁止举报")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("InformAllow", "是否禁止举报", "")]
    public Boolean InformAllow { get => _InformAllow; set { if (OnPropertyChanging("InformAllow", value)) { _InformAllow = value; OnPropertyChanged("InformAllow"); } } }

    private Boolean _IsBuy;
    /// <summary>是否禁止购买</summary>
    [DisplayName("是否禁止购买")]
    [Description("是否禁止购买")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsBuy", "是否禁止购买", "")]
    public Boolean IsBuy { get => _IsBuy; set { if (OnPropertyChanging("IsBuy", value)) { _IsBuy = value; OnPropertyChanged("IsBuy"); } } }

    private Boolean _IsAllowTalk;
    /// <summary>是否禁止发表言论</summary>
    [DisplayName("是否禁止发表言论")]
    [Description("是否禁止发表言论")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("IsAllowTalk", "是否禁止发表言论", "")]
    public Boolean IsAllowTalk { get => _IsAllowTalk; set { if (OnPropertyChanging("IsAllowTalk", value)) { _IsAllowTalk = value; OnPropertyChanged("IsAllowTalk"); } } }

    private Decimal _AvailablePredeposit;
    /// <summary>预存款可用金额</summary>
    [DisplayName("预存款可用金额")]
    [Description("预存款可用金额")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("AvailablePredeposit", "预存款可用金额", "")]
    public Decimal AvailablePredeposit { get => _AvailablePredeposit; set { if (OnPropertyChanging("AvailablePredeposit", value)) { _AvailablePredeposit = value; OnPropertyChanged("AvailablePredeposit"); } } }

    private Decimal _FreezePredeposit;
    /// <summary>预存款冻结金额</summary>
    [DisplayName("预存款冻结金额")]
    [Description("预存款冻结金额")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("FreezePredeposit", "预存款冻结金额", "")]
    public Decimal FreezePredeposit { get => _FreezePredeposit; set { if (OnPropertyChanging("FreezePredeposit", value)) { _FreezePredeposit = value; OnPropertyChanged("FreezePredeposit"); } } }

    private Decimal _AvailableRcBalance;
    /// <summary>可用充值卡余额</summary>
    [DisplayName("可用充值卡余额")]
    [Description("可用充值卡余额")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("AvailableRcBalance", "可用充值卡余额", "")]
    public Decimal AvailableRcBalance { get => _AvailableRcBalance; set { if (OnPropertyChanging("AvailableRcBalance", value)) { _AvailableRcBalance = value; OnPropertyChanged("AvailableRcBalance"); } } }

    private Decimal _FreezeRcBalance;
    /// <summary>冻结充值卡余额</summary>
    [DisplayName("冻结充值卡余额")]
    [Description("冻结充值卡余额")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("FreezeRcBalance", "冻结充值卡余额", "")]
    public Decimal FreezeRcBalance { get => _FreezeRcBalance; set { if (OnPropertyChanging("FreezeRcBalance", value)) { _FreezeRcBalance = value; OnPropertyChanged("FreezeRcBalance"); } } }

    private Int32 _CountryId;
    /// <summary>国家ID</summary>
    [DisplayName("国家ID")]
    [Description("国家ID")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CountryId", "国家ID", "")]
    public Int32 CountryId { get => _CountryId; set { if (OnPropertyChanging("CountryId", value)) { _CountryId = value; OnPropertyChanged("CountryId"); } } }

    private Int32 _AreaId;
    /// <summary>地区ID</summary>
    [DisplayName("地区ID")]
    [Description("地区ID")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("AreaId", "地区ID", "")]
    public Int32 AreaId { get => _AreaId; set { if (OnPropertyChanging("AreaId", value)) { _AreaId = value; OnPropertyChanged("AreaId"); } } }

    private Int32 _CityId;
    /// <summary>城市ID</summary>
    [DisplayName("城市ID")]
    [Description("城市ID")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("CityId", "城市ID", "")]
    public Int32 CityId { get => _CityId; set { if (OnPropertyChanging("CityId", value)) { _CityId = value; OnPropertyChanged("CityId"); } } }

    private Int32 _ProvinceId;
    /// <summary>省份ID</summary>
    [DisplayName("省份ID")]
    [Description("省份ID")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("ProvinceId", "省份ID", "")]
    public Int32 ProvinceId { get => _ProvinceId; set { if (OnPropertyChanging("ProvinceId", value)) { _ProvinceId = value; OnPropertyChanged("ProvinceId"); } } }

    private String _AreaInfo;
    /// <summary>地区内容</summary>
    [DisplayName("地区内容")]
    [Description("地区内容")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("AreaInfo", "地区内容", "")]
    public String AreaInfo { get => _AreaInfo; set { if (OnPropertyChanging("AreaInfo", value)) { _AreaInfo = value; OnPropertyChanged("AreaInfo"); } } }

    private String _Question1;
    /// <summary>问题1</summary>
    [DisplayName("问题1")]
    [Description("问题1")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Question1", "问题1", "")]
    public String Question1 { get => _Question1; set { if (OnPropertyChanging("Question1", value)) { _Question1 = value; OnPropertyChanged("Question1"); } } }

    private String _Answer1;
    /// <summary>回答1</summary>
    [DisplayName("回答1")]
    [Description("回答1")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Answer1", "回答1", "")]
    public String Answer1 { get => _Answer1; set { if (OnPropertyChanging("Answer1", value)) { _Answer1 = value; OnPropertyChanged("Answer1"); } } }

    private String _Question2;
    /// <summary>问题2</summary>
    [DisplayName("问题2")]
    [Description("问题2")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Question2", "问题2", "")]
    public String Question2 { get => _Question2; set { if (OnPropertyChanging("Question2", value)) { _Question2 = value; OnPropertyChanged("Question2"); } } }

    private String _Answer2;
    /// <summary>回答2</summary>
    [DisplayName("回答2")]
    [Description("回答2")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Answer2", "回答2", "")]
    public String Answer2 { get => _Answer2; set { if (OnPropertyChanging("Answer2", value)) { _Answer2 = value; OnPropertyChanged("Answer2"); } } }

    private String _Question3;
    /// <summary>问题3</summary>
    [DisplayName("问题3")]
    [Description("问题3")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Question3", "问题3", "")]
    public String Question3 { get => _Question3; set { if (OnPropertyChanging("Question3", value)) { _Question3 = value; OnPropertyChanged("Question3"); } } }

    private String _Answer3;
    /// <summary>回答3</summary>
    [DisplayName("回答3")]
    [Description("回答3")]
    [DataObjectField(false, false, true, 100)]
    [BindColumn("Answer3", "回答3", "")]
    public String Answer3 { get => _Answer3; set { if (OnPropertyChanging("Answer3", value)) { _Answer3 = value; OnPropertyChanged("Answer3"); } } }

    private Int32 _OnlineTime;
    /// <summary>在线时间。累计在线总时间，单位秒</summary>
    [Category("在线信息")]
    [DisplayName("在线时间")]
    [Description("在线时间。累计在线总时间，单位秒")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("OnlineTime", "在线时间。累计在线总时间，单位秒", "", ItemType = "TimeSpan")]
    public Int32 OnlineTime { get => _OnlineTime; set { if (OnPropertyChanging("OnlineTime", value)) { _OnlineTime = value; OnPropertyChanged("OnlineTime"); } } }

    private Int64 _LastUpdateTime;
    /// <summary>记录上一次在线时间写入的时间戳</summary>
    [Category("在线信息")]
    [DisplayName("记录上一次在线时间写入的时间戳")]
    [Description("记录上一次在线时间写入的时间戳")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("LastUpdateTime", "记录上一次在线时间写入的时间戳", "")]
    public Int64 LastUpdateTime { get => _LastUpdateTime; set { if (OnPropertyChanging("LastUpdateTime", value)) { _LastUpdateTime = value; OnPropertyChanged("LastUpdateTime"); } } }

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
    public void Copy(IUserDetail model)
    {
        Id = model.Id;
        LanguageId = model.LanguageId;
        IsSuper = model.IsSuper;
        SId = model.SId;
        TenantId = model.TenantId;
        UType = model.UType;
        RoleExIds = model.RoleExIds;
        OtherPermissions = model.OtherPermissions;
        DepartmentIds = model.DepartmentIds;
        TrueName = model.TrueName;
        PayPwd = model.PayPwd;
        AuthState = model.AuthState;
        IdCard = model.IdCard;
        IdcardImage1 = model.IdcardImage1;
        IdcardImage2 = model.IdcardImage2;
        IdcardImage3 = model.IdcardImage3;
        CompanyName = model.CompanyName;
        CompnayAuthState = model.CompnayAuthState;
        CorporateName = model.CorporateName;
        BusinessLicenseImage = model.BusinessLicenseImage;
        CorporateImage = model.CorporateImage;
        EmailBind = model.EmailBind;
        MobileBind = model.MobileBind;
        IsSubScribe = model.IsSubScribe;
        IsSales = model.IsSales;
        IsEngineer = model.IsEngineer;
        ReferrerId = model.ReferrerId;
        KeFuId = model.KeFuId;
        ParentUId = model.ParentUId;
        Points = model.Points;
        ExpPoints = model.ExpPoints;
        QQ = model.QQ;
        WeiXin = model.WeiXin;
        WangWang = model.WangWang;
        ContactName = model.ContactName;
        Tel = model.Tel;
        Fax = model.Fax;
        CompanyInfo = model.CompanyInfo;
        Address = model.Address;
        InformAllow = model.InformAllow;
        IsBuy = model.IsBuy;
        IsAllowTalk = model.IsAllowTalk;
        AvailablePredeposit = model.AvailablePredeposit;
        FreezePredeposit = model.FreezePredeposit;
        AvailableRcBalance = model.AvailableRcBalance;
        FreezeRcBalance = model.FreezeRcBalance;
        CountryId = model.CountryId;
        AreaId = model.AreaId;
        CityId = model.CityId;
        ProvinceId = model.ProvinceId;
        AreaInfo = model.AreaInfo;
        Question1 = model.Question1;
        Answer1 = model.Answer1;
        Question2 = model.Question2;
        Answer2 = model.Answer2;
        Question3 = model.Question3;
        Answer3 = model.Answer3;
        OnlineTime = model.OnlineTime;
        LastUpdateTime = model.LastUpdateTime;
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
            "LanguageId" => _LanguageId,
            "IsSuper" => _IsSuper,
            "SId" => _SId,
            "TenantId" => _TenantId,
            "UType" => _UType,
            "RoleExIds" => _RoleExIds,
            "OtherPermissions" => _OtherPermissions,
            "DepartmentIds" => _DepartmentIds,
            "TrueName" => _TrueName,
            "PayPwd" => _PayPwd,
            "AuthState" => _AuthState,
            "IdCard" => _IdCard,
            "IdcardImage1" => _IdcardImage1,
            "IdcardImage2" => _IdcardImage2,
            "IdcardImage3" => _IdcardImage3,
            "CompanyName" => _CompanyName,
            "CompnayAuthState" => _CompnayAuthState,
            "CorporateName" => _CorporateName,
            "BusinessLicenseImage" => _BusinessLicenseImage,
            "CorporateImage" => _CorporateImage,
            "EmailBind" => _EmailBind,
            "MobileBind" => _MobileBind,
            "IsSubScribe" => _IsSubScribe,
            "IsSales" => _IsSales,
            "IsEngineer" => _IsEngineer,
            "ReferrerId" => _ReferrerId,
            "KeFuId" => _KeFuId,
            "ParentUId" => _ParentUId,
            "Points" => _Points,
            "ExpPoints" => _ExpPoints,
            "QQ" => _QQ,
            "WeiXin" => _WeiXin,
            "WangWang" => _WangWang,
            "ContactName" => _ContactName,
            "Tel" => _Tel,
            "Fax" => _Fax,
            "CompanyInfo" => _CompanyInfo,
            "Address" => _Address,
            "InformAllow" => _InformAllow,
            "IsBuy" => _IsBuy,
            "IsAllowTalk" => _IsAllowTalk,
            "AvailablePredeposit" => _AvailablePredeposit,
            "FreezePredeposit" => _FreezePredeposit,
            "AvailableRcBalance" => _AvailableRcBalance,
            "FreezeRcBalance" => _FreezeRcBalance,
            "CountryId" => _CountryId,
            "AreaId" => _AreaId,
            "CityId" => _CityId,
            "ProvinceId" => _ProvinceId,
            "AreaInfo" => _AreaInfo,
            "Question1" => _Question1,
            "Answer1" => _Answer1,
            "Question2" => _Question2,
            "Answer2" => _Answer2,
            "Question3" => _Question3,
            "Answer3" => _Answer3,
            "OnlineTime" => _OnlineTime,
            "LastUpdateTime" => _LastUpdateTime,
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
                case "LanguageId": _LanguageId = value.ToInt(); break;
                case "IsSuper": _IsSuper = value.ToBoolean(); break;
                case "SId": _SId = value.ToLong(); break;
                case "TenantId": _TenantId = value.ToInt(); break;
                case "UType": _UType = Convert.ToInt16(value); break;
                case "RoleExIds": _RoleExIds = Convert.ToString(value); break;
                case "OtherPermissions": _OtherPermissions = Convert.ToString(value); break;
                case "DepartmentIds": _DepartmentIds = Convert.ToString(value); break;
                case "TrueName": _TrueName = Convert.ToString(value); break;
                case "PayPwd": _PayPwd = Convert.ToString(value); break;
                case "AuthState": _AuthState = Convert.ToInt16(value); break;
                case "IdCard": _IdCard = Convert.ToString(value); break;
                case "IdcardImage1": _IdcardImage1 = Convert.ToString(value); break;
                case "IdcardImage2": _IdcardImage2 = Convert.ToString(value); break;
                case "IdcardImage3": _IdcardImage3 = Convert.ToString(value); break;
                case "CompanyName": _CompanyName = Convert.ToString(value); break;
                case "CompnayAuthState": _CompnayAuthState = Convert.ToInt16(value); break;
                case "CorporateName": _CorporateName = Convert.ToString(value); break;
                case "BusinessLicenseImage": _BusinessLicenseImage = Convert.ToString(value); break;
                case "CorporateImage": _CorporateImage = Convert.ToString(value); break;
                case "EmailBind": _EmailBind = value.ToBoolean(); break;
                case "MobileBind": _MobileBind = value.ToBoolean(); break;
                case "IsSubScribe": _IsSubScribe = value.ToBoolean(); break;
                case "IsSales": _IsSales = value.ToBoolean(); break;
                case "IsEngineer": _IsEngineer = value.ToBoolean(); break;
                case "ReferrerId": _ReferrerId = value.ToInt(); break;
                case "KeFuId": _KeFuId = value.ToInt(); break;
                case "ParentUId": _ParentUId = value.ToInt(); break;
                case "Points": _Points = value.ToInt(); break;
                case "ExpPoints": _ExpPoints = value.ToInt(); break;
                case "QQ": _QQ = Convert.ToString(value); break;
                case "WeiXin": _WeiXin = Convert.ToString(value); break;
                case "WangWang": _WangWang = Convert.ToString(value); break;
                case "ContactName": _ContactName = Convert.ToString(value); break;
                case "Tel": _Tel = Convert.ToString(value); break;
                case "Fax": _Fax = Convert.ToString(value); break;
                case "CompanyInfo": _CompanyInfo = Convert.ToString(value); break;
                case "Address": _Address = Convert.ToString(value); break;
                case "InformAllow": _InformAllow = value.ToBoolean(); break;
                case "IsBuy": _IsBuy = value.ToBoolean(); break;
                case "IsAllowTalk": _IsAllowTalk = value.ToBoolean(); break;
                case "AvailablePredeposit": _AvailablePredeposit = Convert.ToDecimal(value); break;
                case "FreezePredeposit": _FreezePredeposit = Convert.ToDecimal(value); break;
                case "AvailableRcBalance": _AvailableRcBalance = Convert.ToDecimal(value); break;
                case "FreezeRcBalance": _FreezeRcBalance = Convert.ToDecimal(value); break;
                case "CountryId": _CountryId = value.ToInt(); break;
                case "AreaId": _AreaId = value.ToInt(); break;
                case "CityId": _CityId = value.ToInt(); break;
                case "ProvinceId": _ProvinceId = value.ToInt(); break;
                case "AreaInfo": _AreaInfo = Convert.ToString(value); break;
                case "Question1": _Question1 = Convert.ToString(value); break;
                case "Answer1": _Answer1 = Convert.ToString(value); break;
                case "Question2": _Question2 = Convert.ToString(value); break;
                case "Answer2": _Answer2 = Convert.ToString(value); break;
                case "Question3": _Question3 = Convert.ToString(value); break;
                case "Answer3": _Answer3 = Convert.ToString(value); break;
                case "OnlineTime": _OnlineTime = value.ToInt(); break;
                case "LastUpdateTime": _LastUpdateTime = value.ToLong(); break;
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
    /// <summary>取得用户扩展字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>用户Id</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>语言Id</summary>
        public static readonly Field LanguageId = FindByName("LanguageId");

        /// <summary>是否超级管理员</summary>
        public static readonly Field IsSuper = FindByName("IsSuper");

        /// <summary>用户SessionId</summary>
        public static readonly Field SId = FindByName("SId");

        /// <summary>用户所属租户Id</summary>
        public static readonly Field TenantId = FindByName("TenantId");

        /// <summary>用户类型。类型自定义</summary>
        public static readonly Field UType = FindByName("UType");

        /// <summary>会员前台权限</summary>
        public static readonly Field RoleExIds = FindByName("RoleExIds");

        /// <summary>会员其他权限</summary>
        public static readonly Field OtherPermissions = FindByName("OtherPermissions");

        /// <summary>会员所在多部门</summary>
        public static readonly Field DepartmentIds = FindByName("DepartmentIds");

        /// <summary>真实姓名</summary>
        public static readonly Field TrueName = FindByName("TrueName");

        /// <summary>支付密码</summary>
        public static readonly Field PayPwd = FindByName("PayPwd");

        /// <summary>个人实名认证状态（0默认1审核中2未通过3已认证）</summary>
        public static readonly Field AuthState = FindByName("AuthState");

        /// <summary>实名认证身份证号</summary>
        public static readonly Field IdCard = FindByName("IdCard");

        /// <summary>手持身份证照</summary>
        public static readonly Field IdcardImage1 = FindByName("IdcardImage1");

        /// <summary>身份证正面照</summary>
        public static readonly Field IdcardImage2 = FindByName("IdcardImage2");

        /// <summary>身份证反面照</summary>
        public static readonly Field IdcardImage3 = FindByName("IdcardImage3");

        /// <summary>公司名称</summary>
        public static readonly Field CompanyName = FindByName("CompanyName");

        /// <summary>企业实名认证状态（0默认1审核中2未通过3已认证）</summary>
        public static readonly Field CompnayAuthState = FindByName("CompnayAuthState");

        /// <summary>法人姓名</summary>
        public static readonly Field CorporateName = FindByName("CorporateName");

        /// <summary>营业执照图片</summary>
        public static readonly Field BusinessLicenseImage = FindByName("BusinessLicenseImage");

        /// <summary>法人手持身份证照</summary>
        public static readonly Field CorporateImage = FindByName("CorporateImage");

        /// <summary>是否绑定邮箱</summary>
        public static readonly Field EmailBind = FindByName("EmailBind");

        /// <summary>是否绑定手机</summary>
        public static readonly Field MobileBind = FindByName("MobileBind");

        /// <summary>是否关注微信公众号</summary>
        public static readonly Field IsSubScribe = FindByName("IsSubScribe");

        /// <summary>是否销售人员</summary>
        public static readonly Field IsSales = FindByName("IsSales");

        /// <summary>是否为工程师</summary>
        public static readonly Field IsEngineer = FindByName("IsEngineer");

        /// <summary>推荐人ID</summary>
        public static readonly Field ReferrerId = FindByName("ReferrerId");

        /// <summary>所属销售ID</summary>
        public static readonly Field KeFuId = FindByName("KeFuId");

        /// <summary>所属上级会员ID</summary>
        public static readonly Field ParentUId = FindByName("ParentUId");

        /// <summary>会员积分</summary>
        public static readonly Field Points = FindByName("Points");

        /// <summary>会员经验值</summary>
        public static readonly Field ExpPoints = FindByName("ExpPoints");

        /// <summary>QQ号码</summary>
        public static readonly Field QQ = FindByName("QQ");

        /// <summary>微信号码</summary>
        public static readonly Field WeiXin = FindByName("WeiXin");

        /// <summary>阿里旺旺号码</summary>
        public static readonly Field WangWang = FindByName("WangWang");

        /// <summary>联系人</summary>
        public static readonly Field ContactName = FindByName("ContactName");

        /// <summary>固话</summary>
        public static readonly Field Tel = FindByName("Tel");

        /// <summary>传真</summary>
        public static readonly Field Fax = FindByName("Fax");

        /// <summary>公司介绍</summary>
        public static readonly Field CompanyInfo = FindByName("CompanyInfo");

        /// <summary>详细地址</summary>
        public static readonly Field Address = FindByName("Address");

        /// <summary>是否禁止举报</summary>
        public static readonly Field InformAllow = FindByName("InformAllow");

        /// <summary>是否禁止购买</summary>
        public static readonly Field IsBuy = FindByName("IsBuy");

        /// <summary>是否禁止发表言论</summary>
        public static readonly Field IsAllowTalk = FindByName("IsAllowTalk");

        /// <summary>预存款可用金额</summary>
        public static readonly Field AvailablePredeposit = FindByName("AvailablePredeposit");

        /// <summary>预存款冻结金额</summary>
        public static readonly Field FreezePredeposit = FindByName("FreezePredeposit");

        /// <summary>可用充值卡余额</summary>
        public static readonly Field AvailableRcBalance = FindByName("AvailableRcBalance");

        /// <summary>冻结充值卡余额</summary>
        public static readonly Field FreezeRcBalance = FindByName("FreezeRcBalance");

        /// <summary>国家ID</summary>
        public static readonly Field CountryId = FindByName("CountryId");

        /// <summary>地区ID</summary>
        public static readonly Field AreaId = FindByName("AreaId");

        /// <summary>城市ID</summary>
        public static readonly Field CityId = FindByName("CityId");

        /// <summary>省份ID</summary>
        public static readonly Field ProvinceId = FindByName("ProvinceId");

        /// <summary>地区内容</summary>
        public static readonly Field AreaInfo = FindByName("AreaInfo");

        /// <summary>问题1</summary>
        public static readonly Field Question1 = FindByName("Question1");

        /// <summary>回答1</summary>
        public static readonly Field Answer1 = FindByName("Answer1");

        /// <summary>问题2</summary>
        public static readonly Field Question2 = FindByName("Question2");

        /// <summary>回答2</summary>
        public static readonly Field Answer2 = FindByName("Answer2");

        /// <summary>问题3</summary>
        public static readonly Field Question3 = FindByName("Question3");

        /// <summary>回答3</summary>
        public static readonly Field Answer3 = FindByName("Answer3");

        /// <summary>在线时间。累计在线总时间，单位秒</summary>
        public static readonly Field OnlineTime = FindByName("OnlineTime");

        /// <summary>记录上一次在线时间写入的时间戳</summary>
        public static readonly Field LastUpdateTime = FindByName("LastUpdateTime");

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

    /// <summary>取得用户扩展字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>用户Id</summary>
        public const String Id = "Id";

        /// <summary>语言Id</summary>
        public const String LanguageId = "LanguageId";

        /// <summary>是否超级管理员</summary>
        public const String IsSuper = "IsSuper";

        /// <summary>用户SessionId</summary>
        public const String SId = "SId";

        /// <summary>用户所属租户Id</summary>
        public const String TenantId = "TenantId";

        /// <summary>用户类型。类型自定义</summary>
        public const String UType = "UType";

        /// <summary>会员前台权限</summary>
        public const String RoleExIds = "RoleExIds";

        /// <summary>会员其他权限</summary>
        public const String OtherPermissions = "OtherPermissions";

        /// <summary>会员所在多部门</summary>
        public const String DepartmentIds = "DepartmentIds";

        /// <summary>真实姓名</summary>
        public const String TrueName = "TrueName";

        /// <summary>支付密码</summary>
        public const String PayPwd = "PayPwd";

        /// <summary>个人实名认证状态（0默认1审核中2未通过3已认证）</summary>
        public const String AuthState = "AuthState";

        /// <summary>实名认证身份证号</summary>
        public const String IdCard = "IdCard";

        /// <summary>手持身份证照</summary>
        public const String IdcardImage1 = "IdcardImage1";

        /// <summary>身份证正面照</summary>
        public const String IdcardImage2 = "IdcardImage2";

        /// <summary>身份证反面照</summary>
        public const String IdcardImage3 = "IdcardImage3";

        /// <summary>公司名称</summary>
        public const String CompanyName = "CompanyName";

        /// <summary>企业实名认证状态（0默认1审核中2未通过3已认证）</summary>
        public const String CompnayAuthState = "CompnayAuthState";

        /// <summary>法人姓名</summary>
        public const String CorporateName = "CorporateName";

        /// <summary>营业执照图片</summary>
        public const String BusinessLicenseImage = "BusinessLicenseImage";

        /// <summary>法人手持身份证照</summary>
        public const String CorporateImage = "CorporateImage";

        /// <summary>是否绑定邮箱</summary>
        public const String EmailBind = "EmailBind";

        /// <summary>是否绑定手机</summary>
        public const String MobileBind = "MobileBind";

        /// <summary>是否关注微信公众号</summary>
        public const String IsSubScribe = "IsSubScribe";

        /// <summary>是否销售人员</summary>
        public const String IsSales = "IsSales";

        /// <summary>是否为工程师</summary>
        public const String IsEngineer = "IsEngineer";

        /// <summary>推荐人ID</summary>
        public const String ReferrerId = "ReferrerId";

        /// <summary>所属销售ID</summary>
        public const String KeFuId = "KeFuId";

        /// <summary>所属上级会员ID</summary>
        public const String ParentUId = "ParentUId";

        /// <summary>会员积分</summary>
        public const String Points = "Points";

        /// <summary>会员经验值</summary>
        public const String ExpPoints = "ExpPoints";

        /// <summary>QQ号码</summary>
        public const String QQ = "QQ";

        /// <summary>微信号码</summary>
        public const String WeiXin = "WeiXin";

        /// <summary>阿里旺旺号码</summary>
        public const String WangWang = "WangWang";

        /// <summary>联系人</summary>
        public const String ContactName = "ContactName";

        /// <summary>固话</summary>
        public const String Tel = "Tel";

        /// <summary>传真</summary>
        public const String Fax = "Fax";

        /// <summary>公司介绍</summary>
        public const String CompanyInfo = "CompanyInfo";

        /// <summary>详细地址</summary>
        public const String Address = "Address";

        /// <summary>是否禁止举报</summary>
        public const String InformAllow = "InformAllow";

        /// <summary>是否禁止购买</summary>
        public const String IsBuy = "IsBuy";

        /// <summary>是否禁止发表言论</summary>
        public const String IsAllowTalk = "IsAllowTalk";

        /// <summary>预存款可用金额</summary>
        public const String AvailablePredeposit = "AvailablePredeposit";

        /// <summary>预存款冻结金额</summary>
        public const String FreezePredeposit = "FreezePredeposit";

        /// <summary>可用充值卡余额</summary>
        public const String AvailableRcBalance = "AvailableRcBalance";

        /// <summary>冻结充值卡余额</summary>
        public const String FreezeRcBalance = "FreezeRcBalance";

        /// <summary>国家ID</summary>
        public const String CountryId = "CountryId";

        /// <summary>地区ID</summary>
        public const String AreaId = "AreaId";

        /// <summary>城市ID</summary>
        public const String CityId = "CityId";

        /// <summary>省份ID</summary>
        public const String ProvinceId = "ProvinceId";

        /// <summary>地区内容</summary>
        public const String AreaInfo = "AreaInfo";

        /// <summary>问题1</summary>
        public const String Question1 = "Question1";

        /// <summary>回答1</summary>
        public const String Answer1 = "Answer1";

        /// <summary>问题2</summary>
        public const String Question2 = "Question2";

        /// <summary>回答2</summary>
        public const String Answer2 = "Answer2";

        /// <summary>问题3</summary>
        public const String Question3 = "Question3";

        /// <summary>回答3</summary>
        public const String Answer3 = "Answer3";

        /// <summary>在线时间。累计在线总时间，单位秒</summary>
        public const String OnlineTime = "OnlineTime";

        /// <summary>记录上一次在线时间写入的时间戳</summary>
        public const String LastUpdateTime = "LastUpdateTime";

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
