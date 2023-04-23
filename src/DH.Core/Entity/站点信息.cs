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

/// <summary>站点信息</summary>
[Serializable]
[DataObject]
[Description("站点信息")]
[BindTable("DH_Store", Description = "站点信息", ConnName = "DG", DbType = DatabaseType.None)]
public partial class Store : IStore, IEntity<StoreModel>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private String _Name;
    /// <summary>站点名称</summary>
    [DisplayName("站点名称")]
    [Description("站点名称")]
    [DataObjectField(false, false, true, 400)]
    [BindColumn("Name", "站点名称", "", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private String _Url;
    /// <summary>站点Url</summary>
    [DisplayName("站点Url")]
    [Description("站点Url")]
    [DataObjectField(false, false, true, 400)]
    [BindColumn("Url", "站点Url", "")]
    public String Url { get => _Url; set { if (OnPropertyChanging("Url", value)) { _Url = value; OnPropertyChanged("Url"); } } }

    private Boolean _SslEnabled;
    /// <summary>是否启用SSL</summary>
    [DisplayName("是否启用SSL")]
    [Description("是否启用SSL")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("SslEnabled", "是否启用SSL", "")]
    public Boolean SslEnabled { get => _SslEnabled; set { if (OnPropertyChanging("SslEnabled", value)) { _SslEnabled = value; OnPropertyChanged("SslEnabled"); } } }

    private String _Hosts;
    /// <summary>可能的HTTP_HOST值的逗号分隔列表</summary>
    [DisplayName("可能的HTTP_HOST值的逗号分隔列表")]
    [Description("可能的HTTP_HOST值的逗号分隔列表")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("Hosts", "可能的HTTP_HOST值的逗号分隔列表", "")]
    public String Hosts { get => _Hosts; set { if (OnPropertyChanging("Hosts", value)) { _Hosts = value; OnPropertyChanged("Hosts"); } } }

    private Int32 _DefaultLanguageId;
    /// <summary>此站点的默认语言的标识符。使用默认语言时设置0</summary>
    [DisplayName("此站点的默认语言的标识符")]
    [Description("此站点的默认语言的标识符。使用默认语言时设置0")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("DefaultLanguageId", "此站点的默认语言的标识符。使用默认语言时设置0", "")]
    public Int32 DefaultLanguageId { get => _DefaultLanguageId; set { if (OnPropertyChanging("DefaultLanguageId", value)) { _DefaultLanguageId = value; OnPropertyChanged("DefaultLanguageId"); } } }

    private Int32 _DisplayOrder;
    /// <summary>获取或设置显示顺序</summary>
    [DisplayName("获取或设置显示顺序")]
    [Description("获取或设置显示顺序")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("DisplayOrder", "获取或设置显示顺序", "")]
    public Int32 DisplayOrder { get => _DisplayOrder; set { if (OnPropertyChanging("DisplayOrder", value)) { _DisplayOrder = value; OnPropertyChanged("DisplayOrder"); } } }

    private String _CompanyName;
    /// <summary>公司名称</summary>
    [DisplayName("公司名称")]
    [Description("公司名称")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("CompanyName", "公司名称", "")]
    public String CompanyName { get => _CompanyName; set { if (OnPropertyChanging("CompanyName", value)) { _CompanyName = value; OnPropertyChanged("CompanyName"); } } }

    private String _CompanyAddress;
    /// <summary>公司地址</summary>
    [DisplayName("公司地址")]
    [Description("公司地址")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("CompanyAddress", "公司地址", "")]
    public String CompanyAddress { get => _CompanyAddress; set { if (OnPropertyChanging("CompanyAddress", value)) { _CompanyAddress = value; OnPropertyChanged("CompanyAddress"); } } }

    private String _CompanyPhoneNumber;
    /// <summary>公司电话号码</summary>
    [DisplayName("公司电话号码")]
    [Description("公司电话号码")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("CompanyPhoneNumber", "公司电话号码", "")]
    public String CompanyPhoneNumber { get => _CompanyPhoneNumber; set { if (OnPropertyChanging("CompanyPhoneNumber", value)) { _CompanyPhoneNumber = value; OnPropertyChanged("CompanyPhoneNumber"); } } }

    private String _CompanyVat;
    /// <summary>公司VAT。用于欧盟国家/地区</summary>
    [DisplayName("公司VAT")]
    [Description("公司VAT。用于欧盟国家/地区")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("CompanyVat", "公司VAT。用于欧盟国家/地区", "")]
    public String CompanyVat { get => _CompanyVat; set { if (OnPropertyChanging("CompanyVat", value)) { _CompanyVat = value; OnPropertyChanged("CompanyVat"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(StoreModel model)
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

    #region 获取/设置 字段值
    /// <summary>获取/设置 字段值</summary>
    /// <param name="name">字段名</param>
    /// <returns></returns>
    public override Object this[String name]
    {
        get => name switch
        {
            "Id" => _Id,
            "Name" => _Name,
            "Url" => _Url,
            "SslEnabled" => _SslEnabled,
            "Hosts" => _Hosts,
            "DefaultLanguageId" => _DefaultLanguageId,
            "DisplayOrder" => _DisplayOrder,
            "CompanyName" => _CompanyName,
            "CompanyAddress" => _CompanyAddress,
            "CompanyPhoneNumber" => _CompanyPhoneNumber,
            "CompanyVat" => _CompanyVat,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "Url": _Url = Convert.ToString(value); break;
                case "SslEnabled": _SslEnabled = value.ToBoolean(); break;
                case "Hosts": _Hosts = Convert.ToString(value); break;
                case "DefaultLanguageId": _DefaultLanguageId = value.ToInt(); break;
                case "DisplayOrder": _DisplayOrder = value.ToInt(); break;
                case "CompanyName": _CompanyName = Convert.ToString(value); break;
                case "CompanyAddress": _CompanyAddress = Convert.ToString(value); break;
                case "CompanyPhoneNumber": _CompanyPhoneNumber = Convert.ToString(value); break;
                case "CompanyVat": _CompanyVat = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得站点信息字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>站点名称</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>站点Url</summary>
        public static readonly Field Url = FindByName("Url");

        /// <summary>是否启用SSL</summary>
        public static readonly Field SslEnabled = FindByName("SslEnabled");

        /// <summary>可能的HTTP_HOST值的逗号分隔列表</summary>
        public static readonly Field Hosts = FindByName("Hosts");

        /// <summary>此站点的默认语言的标识符。使用默认语言时设置0</summary>
        public static readonly Field DefaultLanguageId = FindByName("DefaultLanguageId");

        /// <summary>获取或设置显示顺序</summary>
        public static readonly Field DisplayOrder = FindByName("DisplayOrder");

        /// <summary>公司名称</summary>
        public static readonly Field CompanyName = FindByName("CompanyName");

        /// <summary>公司地址</summary>
        public static readonly Field CompanyAddress = FindByName("CompanyAddress");

        /// <summary>公司电话号码</summary>
        public static readonly Field CompanyPhoneNumber = FindByName("CompanyPhoneNumber");

        /// <summary>公司VAT。用于欧盟国家/地区</summary>
        public static readonly Field CompanyVat = FindByName("CompanyVat");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得站点信息字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>站点名称</summary>
        public const String Name = "Name";

        /// <summary>站点Url</summary>
        public const String Url = "Url";

        /// <summary>是否启用SSL</summary>
        public const String SslEnabled = "SslEnabled";

        /// <summary>可能的HTTP_HOST值的逗号分隔列表</summary>
        public const String Hosts = "Hosts";

        /// <summary>此站点的默认语言的标识符。使用默认语言时设置0</summary>
        public const String DefaultLanguageId = "DefaultLanguageId";

        /// <summary>获取或设置显示顺序</summary>
        public const String DisplayOrder = "DisplayOrder";

        /// <summary>公司名称</summary>
        public const String CompanyName = "CompanyName";

        /// <summary>公司地址</summary>
        public const String CompanyAddress = "CompanyAddress";

        /// <summary>公司电话号码</summary>
        public const String CompanyPhoneNumber = "CompanyPhoneNumber";

        /// <summary>公司VAT。用于欧盟国家/地区</summary>
        public const String CompanyVat = "CompanyVat";
    }
    #endregion
}
