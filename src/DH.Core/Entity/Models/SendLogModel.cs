using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;
using NewLife.Reflection;

namespace DH.Entity;

/// <summary>消息记录</summary>
public partial class SendLogModel : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>0为短信，1为邮箱</summary>
    public Int16 SType { get; set; }

    /// <summary>手机号/邮箱</summary>
    public String Account { get; set; }

    /// <summary>消息内容</summary>
    public String Msg { get; set; }

    /// <summary>类型：1为注册，2为登录，3为找回密码，4绑定手机/邮箱，5安全验证,6账号申诉，0测试</summary>
    public Int16 MType { get; set; }

    /// <summary>发送回复数据</summary>
    public String Remark { get; set; }

    /// <summary>短信平台返回的Id</summary>
    public String SmsId { get; set; }

    /// <summary>消息会员名</summary>
    public String CreateUser { get; set; }

    /// <summary>消息会员ID，注册为0</summary>
    public Int32 CreateUserID { get; set; }

    /// <summary>消息添加时间</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>消息请求IP</summary>
    public String CreateIP { get; set; }
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
                "SType" => SType,
                "Account" => Account,
                "Msg" => Msg,
                "MType" => MType,
                "Remark" => Remark,
                "SmsId" => SmsId,
                "CreateUser" => CreateUser,
                "CreateUserID" => CreateUserID,
                "CreateTime" => CreateTime,
                "CreateIP" => CreateIP,
                _ => this.GetValue(name),
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "SType": SType = Convert.ToInt16(value); break;
                case "Account": Account = Convert.ToString(value); break;
                case "Msg": Msg = Convert.ToString(value); break;
                case "MType": MType = Convert.ToInt16(value); break;
                case "Remark": Remark = Convert.ToString(value); break;
                case "SmsId": SmsId = Convert.ToString(value); break;
                case "CreateUser": CreateUser = Convert.ToString(value); break;
                case "CreateUserID": CreateUserID = value.ToInt(); break;
                case "CreateTime": CreateTime = value.ToDateTime(); break;
                case "CreateIP": CreateIP = Convert.ToString(value); break;
                default: this.SetValue(name, value); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISendLog model)
    {
        Id = model.Id;
        SType = model.SType;
        Account = model.Account;
        Msg = model.Msg;
        MType = model.MType;
        Remark = model.Remark;
        SmsId = model.SmsId;
        CreateUser = model.CreateUser;
        CreateUserID = model.CreateUserID;
        CreateTime = model.CreateTime;
        CreateIP = model.CreateIP;
    }
    #endregion
}
