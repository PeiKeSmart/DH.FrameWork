using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace DH.Entity;

/// <summary>应用系统。用于OAuthServer的子系统</summary>
public partial interface IApp
{
    #region 属性
    /// <summary>编号</summary>
    Int32 Id { get; set; }

    /// <summary>名称。AppID</summary>
    String Name { get; set; }

    /// <summary>显示名</summary>
    String DisplayName { get; set; }

    /// <summary>密钥。AppSecret</summary>
    String Secret { get; set; }

    /// <summary>是否内部项目</summary>
    Boolean IsInternal { get; set; }

    /// <summary>首页</summary>
    String HomePage { get; set; }

    /// <summary>图标。附件路径</summary>
    String Logo { get; set; }

    /// <summary>白名单</summary>
    String White { get; set; }

    /// <summary>黑名单。黑名单优先于白名单</summary>
    String Black { get; set; }

    /// <summary>启用</summary>
    Boolean Enable { get; set; }

    /// <summary>有效期。访问令牌AccessToken的有效期，单位秒，默认使用全局设置</summary>
    Int32 TokenExpire { get; set; }

    /// <summary>回调地址。用于限制回调地址安全性，多个地址逗号隔开</summary>
    String Urls { get; set; }

    /// <summary>授权角色。只允许这些角色登录该系统，多个角色逗号隔开，未填写时表示不限制</summary>
    String RoleIds { get; set; }

    /// <summary>能力集合。逗号分隔，password，client_credentials</summary>
    String Scopes { get; set; }

    /// <summary>三方OAuth。本系统作为OAuthServer时，该应用前来验证时可用的第三方OAuth提供商，多个逗号隔开</summary>
    String OAuths { get; set; }

    /// <summary>过期时间。空表示永不过期</summary>
    DateTime Expired { get; set; }

    /// <summary>次数</summary>
    Int32 Auths { get; set; }

    /// <summary>最后请求</summary>
    DateTime LastAuth { get; set; }

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
