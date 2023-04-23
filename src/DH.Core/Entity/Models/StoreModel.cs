using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife.Data;

namespace DH.Entity;

/// <summary>站点信息</summary>
public partial class StoreModel : IModel
{
    #region 属性
    /// <summary>编号</summary>
    public Int32 Id { get; set; }

    /// <summary>站点名称</summary>
    public String Name { get; set; }

    /// <summary>站点Url</summary>
    public String Url { get; set; }

    /// <summary>是否启用SSL</summary>
    public Boolean SslEnabled { get; set; }

    /// <summary>可能的HTTP_HOST值的逗号分隔列表</summary>
    public String Hosts { get; set; }

    /// <summary>此站点的默认语言的标识符。使用默认语言时设置0</summary>
    public Int32 DefaultLanguageId { get; set; }

    /// <summary>获取或设置显示顺序</summary>
    public Int32 DisplayOrder { get; set; }

    /// <summary>公司名称</summary>
    public String CompanyName { get; set; }

    /// <summary>公司地址</summary>
    public String CompanyAddress { get; set; }

    /// <summary>公司电话号码</summary>
    public String CompanyPhoneNumber { get; set; }

    /// <summary>公司VAT。用于欧盟国家/地区</summary>
    public String CompanyVat { get; set; }
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
                "Url" => Url,
                "SslEnabled" => SslEnabled,
                "Hosts" => Hosts,
                "DefaultLanguageId" => DefaultLanguageId,
                "DisplayOrder" => DisplayOrder,
                "CompanyName" => CompanyName,
                "CompanyAddress" => CompanyAddress,
                "CompanyPhoneNumber" => CompanyPhoneNumber,
                "CompanyVat" => CompanyVat,
                _ => null
            };
        }
        set
        {
            switch (name)
            {
                case "Id": Id = value.ToInt(); break;
                case "Name": Name = Convert.ToString(value); break;
                case "Url": Url = Convert.ToString(value); break;
                case "SslEnabled": SslEnabled = value.ToBoolean(); break;
                case "Hosts": Hosts = Convert.ToString(value); break;
                case "DefaultLanguageId": DefaultLanguageId = value.ToInt(); break;
                case "DisplayOrder": DisplayOrder = value.ToInt(); break;
                case "CompanyName": CompanyName = Convert.ToString(value); break;
                case "CompanyAddress": CompanyAddress = Convert.ToString(value); break;
                case "CompanyPhoneNumber": CompanyPhoneNumber = Convert.ToString(value); break;
                case "CompanyVat": CompanyVat = Convert.ToString(value); break;
            }
        }
    }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(IStore model)
    {
        Id = model.Id;
        Name = model.Name;
        Url = model.Url;
        SslEnabled = model.SslEnabled;
        Hosts = model.Hosts;
        DefaultLanguageId = model.DefaultLanguageId;
        DisplayOrder = model.DisplayOrder;
        CompanyName = model.CompanyName;
        CompanyAddress = model.CompanyAddress;
        CompanyPhoneNumber = model.CompanyPhoneNumber;
        CompanyVat = model.CompanyVat;
    }
    #endregion
}
