using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;
using NewLife.Reflection;

namespace DH.Entity;

/// <summary>委托代理。委托某人代理自己的用户权限，代理人下一次登录时将得到委托人的身份</summary>
public partial class PrincipalAgentModel : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>委托人。把自己的身份权限委托给别人</summary>
    public Int32 PrincipalId { get; set; }

    /// <summary>代理人。代理获得别人身份权限</summary>
    public Int32 AgentId { get; set; }

    /// <summary>启用</summary>
    public Boolean Enable { get; set; }

    /// <summary>次数。可用代理次数，0表示已用完，-1表示无限制</summary>
    public Int32 Times { get; set; }

    /// <summary>有效期。截止时间之前有效，不设置表示无限制</summary>
    public DateTime Expire { get; set; }

    /// <summary>创建者</summary>
    public Int32 CreateUserId { get; set; }

    /// <summary>创建时间</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>创建地址</summary>
    public String CreateIP { get; set; }

    /// <summary>更新者</summary>
    public Int32 UpdateUserId { get; set; }

    /// <summary>更新时间</summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>更新地址</summary>
    public String UpdateIP { get; set; }

    /// <summary>内容</summary>
    public String Remark { get; set; }
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
                "PrincipalId" => PrincipalId,
                "AgentId" => AgentId,
                "Enable" => Enable,
                "Times" => Times,
                "Expire" => Expire,
                "CreateUserId" => CreateUserId,
                "CreateTime" => CreateTime,
                "CreateIP" => CreateIP,
                "UpdateUserId" => UpdateUserId,
                "UpdateTime" => UpdateTime,
                "UpdateIP" => UpdateIP,
                "Remark" => Remark,
                _ => this.GetValue(name),
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "PrincipalId": PrincipalId = value.ToInt(); break;
                case "AgentId": AgentId = value.ToInt(); break;
                case "Enable": Enable = value.ToBoolean(); break;
                case "Times": Times = value.ToInt(); break;
                case "Expire": Expire = value.ToDateTime(); break;
                case "CreateUserId": CreateUserId = value.ToInt(); break;
                case "CreateTime": CreateTime = value.ToDateTime(); break;
                case "CreateIP": CreateIP = Convert.ToString(value); break;
                case "UpdateUserId": UpdateUserId = value.ToInt(); break;
                case "UpdateTime": UpdateTime = value.ToDateTime(); break;
                case "UpdateIP": UpdateIP = Convert.ToString(value); break;
                case "Remark": Remark = Convert.ToString(value); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IPrincipalAgent model)
    {
        Id = model.Id;
        PrincipalId = model.PrincipalId;
        AgentId = model.AgentId;
        Enable = model.Enable;
        Times = model.Times;
        Expire = model.Expire;
        CreateUserId = model.CreateUserId;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
        UpdateUserId = model.UpdateUserId;
        UpdateTime = model.UpdateTime;
        UpdateIP = model.UpdateIP;
        Remark = model.Remark;
    }
    #endregion
}
