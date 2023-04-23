using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;

namespace DH.Entity;

/// <summary>用户扩展</summary>
public partial class UserDetailModel : IModel
{
    #region 属性
    /// <summary>用户Id</summary>
    public Int32 Id { get; set; }

    /// <summary>语言Id</summary>
    public Int32 LanguageId { get; set; }

    /// <summary>是否超级管理员</summary>
    public Boolean IsSuper { get; set; }

    /// <summary>用户SessionId</summary>
    public Int64 SId { get; set; }

    /// <summary>用户所属租户Id</summary>
    public Int64 TenantId { get; set; }

    /// <summary>用户类型。类型自定义</summary>
    public Int16 UType { get; set; }

    /// <summary>会员前台权限</summary>
    public String RoleExIds { get; set; }

    /// <summary>会员其他权限</summary>
    public String OtherPermissions { get; set; }

    /// <summary>会员所在多部门</summary>
    public String DepartmentIds { get; set; }

    /// <summary>真实姓名</summary>
    public String TrueName { get; set; }

    /// <summary>支付密码</summary>
    public String PayPwd { get; set; }

    /// <summary>个人实名认证状态（0默认1审核中2未通过3已认证）</summary>
    public Int16 AuthState { get; set; }

    /// <summary>实名认证身份证号</summary>
    public String IdCard { get; set; }

    /// <summary>手持身份证照</summary>
    public String IdcardImage1 { get; set; }

    /// <summary>身份证正面照</summary>
    public String IdcardImage2 { get; set; }

    /// <summary>身份证反面照</summary>
    public String IdcardImage3 { get; set; }

    /// <summary>公司名称</summary>
    public String CompanyName { get; set; }

    /// <summary>企业实名认证状态（0默认1审核中2未通过3已认证）</summary>
    public Int16 CompnayAuthState { get; set; }

    /// <summary>法人姓名</summary>
    public String CorporateName { get; set; }

    /// <summary>营业执照图片</summary>
    public String BusinessLicenseImage { get; set; }

    /// <summary>法人手持身份证照</summary>
    public String CorporateImage { get; set; }

    /// <summary>是否绑定邮箱</summary>
    public Boolean EmailBind { get; set; }

    /// <summary>是否绑定手机</summary>
    public Boolean MobileBind { get; set; }

    /// <summary>是否关注微信公众号</summary>
    public Boolean IsSubScribe { get; set; }

    /// <summary>是否销售人员</summary>
    public Boolean IsSales { get; set; }

    /// <summary>是否为工程师</summary>
    public Boolean IsEngineer { get; set; }

    /// <summary>推荐人ID</summary>
    public Int32 ReferrerId { get; set; }

    /// <summary>所属销售ID</summary>
    public Int32 KeFuId { get; set; }

    /// <summary>所属上级会员ID</summary>
    public Int32 ParentUId { get; set; }

    /// <summary>会员积分</summary>
    public Int32 Points { get; set; }

    /// <summary>会员经验值</summary>
    public Int32 ExpPoints { get; set; }

    /// <summary>QQ号码</summary>
    public String QQ { get; set; }

    /// <summary>微信号码</summary>
    public String WeiXin { get; set; }

    /// <summary>阿里旺旺号码</summary>
    public String WangWang { get; set; }

    /// <summary>联系人</summary>
    public String ContactName { get; set; }

    /// <summary>固话</summary>
    public String Tel { get; set; }

    /// <summary>传真</summary>
    public String Fax { get; set; }

    /// <summary>公司介绍</summary>
    public String CompanyInfo { get; set; }

    /// <summary>详细地址</summary>
    public String Address { get; set; }

    /// <summary>是否禁止举报</summary>
    public Boolean InformAllow { get; set; }

    /// <summary>是否禁止购买</summary>
    public Boolean IsBuy { get; set; }

    /// <summary>是否禁止发表言论</summary>
    public Boolean IsAllowTalk { get; set; }

    /// <summary>预存款可用金额</summary>
    public Decimal AvailablePredeposit { get; set; }

    /// <summary>预存款冻结金额</summary>
    public Decimal FreezePredeposit { get; set; }

    /// <summary>可用充值卡余额</summary>
    public Decimal AvailableRcBalance { get; set; }

    /// <summary>冻结充值卡余额</summary>
    public Decimal FreezeRcBalance { get; set; }

    /// <summary>国家ID</summary>
    public Int32 CountryId { get; set; }

    /// <summary>地区ID</summary>
    public Int32 AreaId { get; set; }

    /// <summary>城市ID</summary>
    public Int32 CityId { get; set; }

    /// <summary>省份ID</summary>
    public Int32 ProvinceId { get; set; }

    /// <summary>地区内容</summary>
    public String AreaInfo { get; set; }

    /// <summary>问题1</summary>
    public String Question1 { get; set; }

    /// <summary>回答1</summary>
    public String Answer1 { get; set; }

    /// <summary>问题2</summary>
    public String Question2 { get; set; }

    /// <summary>回答2</summary>
    public String Answer2 { get; set; }

    /// <summary>问题3</summary>
    public String Question3 { get; set; }

    /// <summary>回答3</summary>
    public String Answer3 { get; set; }

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
                "LanguageId" => LanguageId,
                "IsSuper" => IsSuper,
                "SId" => SId,
                "TenantId" => TenantId,
                "UType" => UType,
                "RoleExIds" => RoleExIds,
                "OtherPermissions" => OtherPermissions,
                "DepartmentIds" => DepartmentIds,
                "TrueName" => TrueName,
                "PayPwd" => PayPwd,
                "AuthState" => AuthState,
                "IdCard" => IdCard,
                "IdcardImage1" => IdcardImage1,
                "IdcardImage2" => IdcardImage2,
                "IdcardImage3" => IdcardImage3,
                "CompanyName" => CompanyName,
                "CompnayAuthState" => CompnayAuthState,
                "CorporateName" => CorporateName,
                "BusinessLicenseImage" => BusinessLicenseImage,
                "CorporateImage" => CorporateImage,
                "EmailBind" => EmailBind,
                "MobileBind" => MobileBind,
                "IsSubScribe" => IsSubScribe,
                "IsSales" => IsSales,
                "IsEngineer" => IsEngineer,
                "ReferrerId" => ReferrerId,
                "KeFuId" => KeFuId,
                "ParentUId" => ParentUId,
                "Points" => Points,
                "ExpPoints" => ExpPoints,
                "QQ" => QQ,
                "WeiXin" => WeiXin,
                "WangWang" => WangWang,
                "ContactName" => ContactName,
                "Tel" => Tel,
                "Fax" => Fax,
                "CompanyInfo" => CompanyInfo,
                "Address" => Address,
                "InformAllow" => InformAllow,
                "IsBuy" => IsBuy,
                "IsAllowTalk" => IsAllowTalk,
                "AvailablePredeposit" => AvailablePredeposit,
                "FreezePredeposit" => FreezePredeposit,
                "AvailableRcBalance" => AvailableRcBalance,
                "FreezeRcBalance" => FreezeRcBalance,
                "CountryId" => CountryId,
                "AreaId" => AreaId,
                "CityId" => CityId,
                "ProvinceId" => ProvinceId,
                "AreaInfo" => AreaInfo,
                "Question1" => Question1,
                "Answer1" => Answer1,
                "Question2" => Question2,
                "Answer2" => Answer2,
                "Question3" => Question3,
                "Answer3" => Answer3,
                "CreateUser" => CreateUser,
                "CreateUserID" => CreateUserID,
                "CreateTime" => CreateTime,
                "CreateIP" => CreateIP,
                "UpdateUser" => UpdateUser,
                "UpdateUserID" => UpdateUserID,
                "UpdateTime" => UpdateTime,
                "UpdateIP" => UpdateIP,
                _ => null
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "LanguageId": LanguageId = value.ToInt(); break;
                case "IsSuper": IsSuper = value.ToBoolean(); break;
                case "SId": SId = value.ToLong(); break;
                case "TenantId": TenantId = value.ToLong(); break;
                case "UType": UType = Convert.ToInt16(value); break;
                case "RoleExIds": RoleExIds = Convert.ToString(value); break;
                case "OtherPermissions": OtherPermissions = Convert.ToString(value); break;
                case "DepartmentIds": DepartmentIds = Convert.ToString(value); break;
                case "TrueName": TrueName = Convert.ToString(value); break;
                case "PayPwd": PayPwd = Convert.ToString(value); break;
                case "AuthState": AuthState = Convert.ToInt16(value); break;
                case "IdCard": IdCard = Convert.ToString(value); break;
                case "IdcardImage1": IdcardImage1 = Convert.ToString(value); break;
                case "IdcardImage2": IdcardImage2 = Convert.ToString(value); break;
                case "IdcardImage3": IdcardImage3 = Convert.ToString(value); break;
                case "CompanyName": CompanyName = Convert.ToString(value); break;
                case "CompnayAuthState": CompnayAuthState = Convert.ToInt16(value); break;
                case "CorporateName": CorporateName = Convert.ToString(value); break;
                case "BusinessLicenseImage": BusinessLicenseImage = Convert.ToString(value); break;
                case "CorporateImage": CorporateImage = Convert.ToString(value); break;
                case "EmailBind": EmailBind = value.ToBoolean(); break;
                case "MobileBind": MobileBind = value.ToBoolean(); break;
                case "IsSubScribe": IsSubScribe = value.ToBoolean(); break;
                case "IsSales": IsSales = value.ToBoolean(); break;
                case "IsEngineer": IsEngineer = value.ToBoolean(); break;
                case "ReferrerId": ReferrerId = value.ToInt(); break;
                case "KeFuId": KeFuId = value.ToInt(); break;
                case "ParentUId": ParentUId = value.ToInt(); break;
                case "Points": Points = value.ToInt(); break;
                case "ExpPoints": ExpPoints = value.ToInt(); break;
                case "QQ": QQ = Convert.ToString(value); break;
                case "WeiXin": WeiXin = Convert.ToString(value); break;
                case "WangWang": WangWang = Convert.ToString(value); break;
                case "ContactName": ContactName = Convert.ToString(value); break;
                case "Tel": Tel = Convert.ToString(value); break;
                case "Fax": Fax = Convert.ToString(value); break;
                case "CompanyInfo": CompanyInfo = Convert.ToString(value); break;
                case "Address": Address = Convert.ToString(value); break;
                case "InformAllow": InformAllow = value.ToBoolean(); break;
                case "IsBuy": IsBuy = value.ToBoolean(); break;
                case "IsAllowTalk": IsAllowTalk = value.ToBoolean(); break;
                case "AvailablePredeposit": AvailablePredeposit = Convert.ToDecimal(value); break;
                case "FreezePredeposit": FreezePredeposit = Convert.ToDecimal(value); break;
                case "AvailableRcBalance": AvailableRcBalance = Convert.ToDecimal(value); break;
                case "FreezeRcBalance": FreezeRcBalance = Convert.ToDecimal(value); break;
                case "CountryId": CountryId = value.ToInt(); break;
                case "AreaId": AreaId = value.ToInt(); break;
                case "CityId": CityId = value.ToInt(); break;
                case "ProvinceId": ProvinceId = value.ToInt(); break;
                case "AreaInfo": AreaInfo = Convert.ToString(value); break;
                case "Question1": Question1 = Convert.ToString(value); break;
                case "Answer1": Answer1 = Convert.ToString(value); break;
                case "Question2": Question2 = Convert.ToString(value); break;
                case "Answer2": Answer2 = Convert.ToString(value); break;
                case "Question3": Question3 = Convert.ToString(value); break;
                case "Answer3": Answer3 = Convert.ToString(value); break;
                case "CreateUser": CreateUser = Convert.ToString(value); break;
                case "CreateUserID": CreateUserID = value.ToInt(); break;
                case "CreateTime": CreateTime = value.ToDateTime(); break;
                case "CreateIP": CreateIP = Convert.ToString(value); break;
                case "UpdateUser": UpdateUser = Convert.ToString(value); break;
                case "UpdateUserID": UpdateUserID = value.ToInt(); break;
                case "UpdateTime": UpdateTime = value.ToDateTime(); break;
                case "UpdateIP": UpdateIP = Convert.ToString(value); break;
            }
        }
    }
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
