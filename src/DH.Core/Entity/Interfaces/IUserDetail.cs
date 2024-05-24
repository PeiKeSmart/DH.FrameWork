using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>用户扩展</summary>
public partial interface IUserDetail
{
    #region 属性
    /// <summary>用户Id</summary>
    Int32 Id { get; set; }

    /// <summary>语言Id</summary>
    Int32 LanguageId { get; set; }

    /// <summary>是否超级管理员</summary>
    Boolean IsSuper { get; set; }

    /// <summary>用户SessionId</summary>
    Int64 SId { get; set; }

    /// <summary>用户所属租户Id</summary>
    Int32 TenantId { get; set; }

    /// <summary>用户类型。类型自定义</summary>
    UserKinds UType { get; set; }

    /// <summary>会员前台权限</summary>
    String RoleExIds { get; set; }

    /// <summary>会员其他权限</summary>
    String OtherPermissions { get; set; }

    /// <summary>会员所在多部门</summary>
    String DepartmentIds { get; set; }

    /// <summary>真实姓名</summary>
    String TrueName { get; set; }

    /// <summary>支付密码</summary>
    String PayPwd { get; set; }

    /// <summary>个人实名认证状态（0默认1审核中2未通过3已认证）</summary>
    Int16 AuthState { get; set; }

    /// <summary>实名认证身份证号</summary>
    String IdCard { get; set; }

    /// <summary>手持身份证照</summary>
    String IdcardImage1 { get; set; }

    /// <summary>身份证正面照</summary>
    String IdcardImage2 { get; set; }

    /// <summary>身份证反面照</summary>
    String IdcardImage3 { get; set; }

    /// <summary>公司名称</summary>
    String CompanyName { get; set; }

    /// <summary>企业实名认证状态（0默认1审核中2未通过3已认证）</summary>
    Int16 CompnayAuthState { get; set; }

    /// <summary>法人姓名</summary>
    String CorporateName { get; set; }

    /// <summary>营业执照图片</summary>
    String BusinessLicenseImage { get; set; }

    /// <summary>法人手持身份证照</summary>
    String CorporateImage { get; set; }

    /// <summary>是否绑定邮箱</summary>
    Boolean EmailBind { get; set; }

    /// <summary>是否绑定手机</summary>
    Boolean MobileBind { get; set; }

    /// <summary>是否关注微信公众号</summary>
    Boolean IsSubScribe { get; set; }

    /// <summary>是否销售人员</summary>
    Boolean IsSales { get; set; }

    /// <summary>是否为工程师</summary>
    Boolean IsEngineer { get; set; }

    /// <summary>推荐人ID</summary>
    Int32 ReferrerId { get; set; }

    /// <summary>所属销售ID</summary>
    Int32 KeFuId { get; set; }

    /// <summary>所属上级会员ID</summary>
    Int32 ParentUId { get; set; }

    /// <summary>会员积分</summary>
    Int32 Points { get; set; }

    /// <summary>会员经验值</summary>
    Int32 ExpPoints { get; set; }

    /// <summary>QQ号码</summary>
    String QQ { get; set; }

    /// <summary>微信号码</summary>
    String WeiXin { get; set; }

    /// <summary>阿里旺旺号码</summary>
    String WangWang { get; set; }

    /// <summary>联系人</summary>
    String ContactName { get; set; }

    /// <summary>固话</summary>
    String Tel { get; set; }

    /// <summary>传真</summary>
    String Fax { get; set; }

    /// <summary>公司介绍</summary>
    String CompanyInfo { get; set; }

    /// <summary>详细地址</summary>
    String Address { get; set; }

    /// <summary>是否禁止举报</summary>
    Boolean InformAllow { get; set; }

    /// <summary>是否禁止购买</summary>
    Boolean IsBuy { get; set; }

    /// <summary>是否禁止发表言论</summary>
    Boolean IsAllowTalk { get; set; }

    /// <summary>预存款可用金额</summary>
    Decimal AvailablePredeposit { get; set; }

    /// <summary>预存款冻结金额</summary>
    Decimal FreezePredeposit { get; set; }

    /// <summary>可用充值卡余额</summary>
    Decimal AvailableRcBalance { get; set; }

    /// <summary>冻结充值卡余额</summary>
    Decimal FreezeRcBalance { get; set; }

    /// <summary>国家区号</summary>
    String CountryCode { get; set; }

    /// <summary>国家ID</summary>
    Int32 CountryId { get; set; }

    /// <summary>地区ID</summary>
    Int32 AreaId { get; set; }

    /// <summary>城市ID</summary>
    Int32 CityId { get; set; }

    /// <summary>省份ID</summary>
    Int32 ProvinceId { get; set; }

    /// <summary>地区内容</summary>
    String AreaInfo { get; set; }

    /// <summary>问题1</summary>
    String Question1 { get; set; }

    /// <summary>回答1</summary>
    String Answer1 { get; set; }

    /// <summary>问题2</summary>
    String Question2 { get; set; }

    /// <summary>回答2</summary>
    String Answer2 { get; set; }

    /// <summary>问题3</summary>
    String Question3 { get; set; }

    /// <summary>回答3</summary>
    String Answer3 { get; set; }

    /// <summary>在线时间。累计在线总时间，单位秒</summary>
    Int32 OnlineTime { get; set; }

    /// <summary>记录上一次在线时间写入的时间戳</summary>
    Int64 LastUpdateTime { get; set; }

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
