using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>OAuth配置。需要连接的OAuth认证方</summary>
public partial interface IOAuthConfig
{
    #region 属性
    /// <summary>编号</summary>
    Int32 ID { get; set; }

    /// <summary>名称。提供者名称</summary>
    String Name { get; set; }

    /// <summary>昵称</summary>
    String NickName { get; set; }

    /// <summary>图标</summary>
    String Logo { get; set; }

    /// <summary>应用标识</summary>
    String AppId { get; set; }

    /// <summary>应用密钥</summary>
    String Secret { get; set; }

    /// <summary>服务地址</summary>
    String Server { get; set; }

    /// <summary>令牌服务地址。可以不同于验证地址的内网直达地址</summary>
    String AccessServer { get; set; }

    /// <summary>授权类型</summary>
    GrantTypes GrantType { get; set; }

    /// <summary>授权范围</summary>
    String Scope { get; set; }

    /// <summary>验证地址。跳转SSO的验证地址</summary>
    String AuthUrl { get; set; }

    /// <summary>令牌地址。根据code换取令牌的地址</summary>
    String AccessUrl { get; set; }

    /// <summary>用户地址。根据令牌获取用户信息的地址</summary>
    String UserUrl { get; set; }

    /// <summary>应用地址。域名和端口，应用系统经过反向代理重定向时指定外部地址</summary>
    String AppUrl { get; set; }

    /// <summary>启用</summary>
    Boolean Enable { get; set; }

    /// <summary>调试。设置处于调试状态，输出详细日志</summary>
    Boolean Debug { get; set; }

    /// <summary>可见。是否在登录页面可见，不可见的提供者只能使用应用内自动登录，例如微信公众号</summary>
    Boolean Visible { get; set; }

    /// <summary>自动注册。SSO登录后，如果本地没有匹配用户，自动注册新用户，否则跳到登录页，在登录后绑定</summary>
    Boolean AutoRegister { get; set; }

    /// <summary>自动角色。该渠道登录的用户，将会自动得到指定角色名，多个角色逗号隔开</summary>
    String AutoRole { get; set; }

    /// <summary>排序。较大者在前面</summary>
    Int32 Sort { get; set; }

    /// <summary>安全密钥。公钥，用于RSA加密用户密码，在通信链路上保护用户密码安全，密钥前面可以增加keyName，形成keyName$keyValue，用于向服务端指示所使用的密钥标识，方便未来更换密钥。</summary>
    String SecurityKey { get; set; }

    /// <summary>字段映射。SSO用户字段如何映射到OAuthClient内部属性</summary>
    String FieldMap { get; set; }

    /// <summary>抓取头像。是否抓取头像并保存到本地</summary>
    Boolean FetchAvatar { get; set; }

    /// <summary>创建者</summary>
    Int32 CreateUserID { get; set; }

    /// <summary>创建时间</summary>
    DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    String CreateIP { get; set; }

    /// <summary>更新者</summary>
    Int32 UpdateUserID { get; set; }

    /// <summary>更新时间</summary>
    DateTime UpdateTime { get; set; }

    /// <summary>更新地址</summary>
    String UpdateIP { get; set; }

    /// <summary>内容</summary>
    String Remark { get; set; }
    #endregion
}
