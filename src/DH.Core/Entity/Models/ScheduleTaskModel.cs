﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;

namespace DH.Entity;

/// <summary>计划任务</summary>
public partial class ScheduleTaskModel : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>名称</summary>
    public String Name { get; set; }

    /// <summary>运行周期（秒）</summary>
    public Int32 Seconds { get; set; }

    /// <summary>适当的IScheduleTask类的类型</summary>
    public String Type { get; set; }

    /// <summary>上次启用任务的日期时间</summary>
    public DateTime LastEnabledUtc { get; set; }

    /// <summary>是否启用任务</summary>
    public Boolean Enabled { get; set; }

    /// <summary>是否应在出现错误时停止任务</summary>
    public Boolean StopOnError { get; set; }

    /// <summary>上次启动的日期时间</summary>
    public DateTime LastStartUtc { get; set; }

    /// <summary>上次完成的日期时间（无论失败还是成功）</summary>
    public DateTime LastEndUtc { get; set; }

    /// <summary>上次成功完成的日期时间</summary>
    public DateTime LastSuccessUtc { get; set; }
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
                "Name" => Name,
                "Seconds" => Seconds,
                "Type" => Type,
                "LastEnabledUtc" => LastEnabledUtc,
                "Enabled" => Enabled,
                "StopOnError" => StopOnError,
                "LastStartUtc" => LastStartUtc,
                "LastEndUtc" => LastEndUtc,
                "LastSuccessUtc" => LastSuccessUtc,
                _ => null
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "Name": Name = Convert.ToString(value); break;
                case "Seconds": Seconds = value.ToInt(); break;
                case "Type": Type = Convert.ToString(value); break;
                case "LastEnabledUtc": LastEnabledUtc = value.ToDateTime(); break;
                case "Enabled": Enabled = value.ToBoolean(); break;
                case "StopOnError": StopOnError = value.ToBoolean(); break;
                case "LastStartUtc": LastStartUtc = value.ToDateTime(); break;
                case "LastEndUtc": LastEndUtc = value.ToDateTime(); break;
                case "LastSuccessUtc": LastSuccessUtc = value.ToDateTime(); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IScheduleTask model)
    {
        Id = model.Id;
        Name = model.Name;
        Seconds = model.Seconds;
        Type = model.Type;
        LastEnabledUtc = model.LastEnabledUtc;
        Enabled = model.Enabled;
        StopOnError = model.StopOnError;
        LastStartUtc = model.LastStartUtc;
        LastEndUtc = model.LastEndUtc;
        LastSuccessUtc = model.LastSuccessUtc;
    }
    #endregion
}