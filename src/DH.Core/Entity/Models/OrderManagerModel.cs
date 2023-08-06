using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;

namespace DH.Entity;

/// <summary>指令管理</summary>
public partial class OrderManagerModel : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>指令名称</summary>
    public String Name { get; set; }

    /// <summary>指令编号</summary>
    public String Code { get; set; }

    /// <summary>操作类型</summary>
    public String OptCategory { get; set; }

    /// <summary>启用</summary>
    public Boolean Enable { get; set; }

    /// <summary>数据,进行后续操作依赖值</summary>
    public String Data { get; set; }

    /// <summary>数据类型,String、Int、Double、Decimal等</summary>
    public String DataType { get; set; }

    /// <summary>请求地址</summary>
    public String Url { get; set; }

    /// <summary>请求方式,GET、POST、PUT、DELETE</summary>
    public String Method { get; set; }

    /// <summary>值字段</summary>
    public String ValueField { get; set; }

    /// <summary>文本字段</summary>
    public String LabelField { get; set; }

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
                "Name" => Name,
                "Code" => Code,
                "OptCategory" => OptCategory,
                "Enable" => Enable,
                "Data" => Data,
                "DataType" => DataType,
                "Url" => Url,
                "Method" => Method,
                "ValueField" => ValueField,
                "LabelField" => LabelField,
                "CreateUserId" => CreateUserId,
                "CreateTime" => CreateTime,
                "CreateIP" => CreateIP,
                "UpdateUserId" => UpdateUserId,
                "UpdateTime" => UpdateTime,
                "UpdateIP" => UpdateIP,
                "Remark" => Remark,
                _ => null
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "Name": Name = Convert.ToString(value); break;
                case "Code": Code = Convert.ToString(value); break;
                case "OptCategory": OptCategory = Convert.ToString(value); break;
                case "Enable": Enable = value.ToBoolean(); break;
                case "Data": Data = Convert.ToString(value); break;
                case "DataType": DataType = Convert.ToString(value); break;
                case "Url": Url = Convert.ToString(value); break;
                case "Method": Method = Convert.ToString(value); break;
                case "ValueField": ValueField = Convert.ToString(value); break;
                case "LabelField": LabelField = Convert.ToString(value); break;
                case "CreateUserId": CreateUserId = value.ToInt(); break;
                case "CreateTime": CreateTime = value.ToDateTime(); break;
                case "CreateIP": CreateIP = Convert.ToString(value); break;
                case "UpdateUserId": UpdateUserId = value.ToInt(); break;
                case "UpdateTime": UpdateTime = value.ToDateTime(); break;
                case "UpdateIP": UpdateIP = Convert.ToString(value); break;
                case "Remark": Remark = Convert.ToString(value); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IOrderManager model)
    {
        Id = model.Id;
        Name = model.Name;
        Code = model.Code;
        OptCategory = model.OptCategory;
        Enable = model.Enable;
        Data = model.Data;
        DataType = model.DataType;
        Url = model.Url;
        Method = model.Method;
        ValueField = model.ValueField;
        LabelField = model.LabelField;
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
