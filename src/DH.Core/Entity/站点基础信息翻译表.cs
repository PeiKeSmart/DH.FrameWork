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
using XCode.Common;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace DH.Entity;

/// <summary>站点基础信息翻译表</summary>
[Serializable]
[DataObject]
[Description("站点基础信息翻译表")]
[BindIndex("IU_DG_SiteInfoLan_SiteInfoId_LanguageId", true, "SiteInfoId,LanguageId")]
[BindTable("DG_SiteInfoLan", Description = "站点基础信息翻译表", ConnName = "DG", DbType = DatabaseType.None)]
public partial class SiteInfoLan : ISiteInfoLan, IEntity<ISiteInfoLan>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Int32 _SiteInfoId;
    /// <summary>站点基础信息Id</summary>
    [DisplayName("站点基础信息Id")]
    [Description("站点基础信息Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("SiteInfoId", "站点基础信息Id", "")]
    public Int32 SiteInfoId { get => _SiteInfoId; set { if (OnPropertyChanging("SiteInfoId", value)) { _SiteInfoId = value; OnPropertyChanged("SiteInfoId"); } } }

    private Int32 _LanguageId;
    /// <summary>关联所属语言Id</summary>
    [DisplayName("关联所属语言Id")]
    [Description("关联所属语言Id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("LanguageId", "关联所属语言Id", "")]
    public Int32 LanguageId { get => _LanguageId; set { if (OnPropertyChanging("LanguageId", value)) { _LanguageId = value; OnPropertyChanged("LanguageId"); } } }

    private String _SiteName;
    /// <summary>网站名称</summary>
    [DisplayName("网站名称")]
    [Description("网站名称")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("SiteName", "网站名称", "")]
    public String SiteName { get => _SiteName; set { if (OnPropertyChanging("SiteName", value)) { _SiteName = value; OnPropertyChanged("SiteName"); } } }

    private String _SeoTitle;
    /// <summary>网站SEO标题</summary>
    [DisplayName("网站SEO标题")]
    [Description("网站SEO标题")]
    [DataObjectField(false, false, true, 255)]
    [BindColumn("SeoTitle", "网站SEO标题", "")]
    public String SeoTitle { get => _SeoTitle; set { if (OnPropertyChanging("SeoTitle", value)) { _SeoTitle = value; OnPropertyChanged("SeoTitle"); } } }

    private String _SeoKey;
    /// <summary>网站SEO关键字</summary>
    [DisplayName("网站SEO关键字")]
    [Description("网站SEO关键字")]
    [DataObjectField(false, false, true, 500)]
    [BindColumn("SeoKey", "网站SEO关键字", "")]
    public String SeoKey { get => _SeoKey; set { if (OnPropertyChanging("SeoKey", value)) { _SeoKey = value; OnPropertyChanged("SeoKey"); } } }

    private String _SeoDescribe;
    /// <summary>网站SEO描述</summary>
    [DisplayName("网站SEO描述")]
    [Description("网站SEO描述")]
    [DataObjectField(false, false, true, 2000)]
    [BindColumn("SeoDescribe", "网站SEO描述", "")]
    public String SeoDescribe { get => _SeoDescribe; set { if (OnPropertyChanging("SeoDescribe", value)) { _SeoDescribe = value; OnPropertyChanged("SeoDescribe"); } } }

    private String _Registration;
    /// <summary>备案号</summary>
    [DisplayName("备案号")]
    [Description("备案号")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Registration", "备案号", "")]
    public String Registration { get => _Registration; set { if (OnPropertyChanging("Registration", value)) { _Registration = value; OnPropertyChanged("Registration"); } } }

    private String _SiteCopyright;
    /// <summary>网站版权等信息</summary>
    [DisplayName("网站版权等信息")]
    [Description("网站版权等信息")]
    [DataObjectField(false, false, true, 2000)]
    [BindColumn("SiteCopyright", "网站版权等信息", "")]
    public String SiteCopyright { get => _SiteCopyright; set { if (OnPropertyChanging("SiteCopyright", value)) { _SiteCopyright = value; OnPropertyChanged("SiteCopyright"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISiteInfoLan model)
    {
        Id = model.Id;
        SiteInfoId = model.SiteInfoId;
        LanguageId = model.LanguageId;
        SiteName = model.SiteName;
        SeoTitle = model.SeoTitle;
        SeoKey = model.SeoKey;
        SeoDescribe = model.SeoDescribe;
        Registration = model.Registration;
        SiteCopyright = model.SiteCopyright;
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
            "SiteInfoId" => _SiteInfoId,
            "LanguageId" => _LanguageId,
            "SiteName" => _SiteName,
            "SeoTitle" => _SeoTitle,
            "SeoKey" => _SeoKey,
            "SeoDescribe" => _SeoDescribe,
            "Registration" => _Registration,
            "SiteCopyright" => _SiteCopyright,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "SiteInfoId": _SiteInfoId = value.ToInt(); break;
                case "LanguageId": _LanguageId = value.ToInt(); break;
                case "SiteName": _SiteName = Convert.ToString(value); break;
                case "SeoTitle": _SeoTitle = Convert.ToString(value); break;
                case "SeoKey": _SeoKey = Convert.ToString(value); break;
                case "SeoDescribe": _SeoDescribe = Convert.ToString(value); break;
                case "Registration": _Registration = Convert.ToString(value); break;
                case "SiteCopyright": _SiteCopyright = Convert.ToString(value); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 字段名
    /// <summary>取得站点基础信息翻译表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>站点基础信息Id</summary>
        public static readonly Field SiteInfoId = FindByName("SiteInfoId");

        /// <summary>关联所属语言Id</summary>
        public static readonly Field LanguageId = FindByName("LanguageId");

        /// <summary>网站名称</summary>
        public static readonly Field SiteName = FindByName("SiteName");

        /// <summary>网站SEO标题</summary>
        public static readonly Field SeoTitle = FindByName("SeoTitle");

        /// <summary>网站SEO关键字</summary>
        public static readonly Field SeoKey = FindByName("SeoKey");

        /// <summary>网站SEO描述</summary>
        public static readonly Field SeoDescribe = FindByName("SeoDescribe");

        /// <summary>备案号</summary>
        public static readonly Field Registration = FindByName("Registration");

        /// <summary>网站版权等信息</summary>
        public static readonly Field SiteCopyright = FindByName("SiteCopyright");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得站点基础信息翻译表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>站点基础信息Id</summary>
        public const String SiteInfoId = "SiteInfoId";

        /// <summary>关联所属语言Id</summary>
        public const String LanguageId = "LanguageId";

        /// <summary>网站名称</summary>
        public const String SiteName = "SiteName";

        /// <summary>网站SEO标题</summary>
        public const String SeoTitle = "SeoTitle";

        /// <summary>网站SEO关键字</summary>
        public const String SeoKey = "SeoKey";

        /// <summary>网站SEO描述</summary>
        public const String SeoDescribe = "SeoDescribe";

        /// <summary>备案号</summary>
        public const String Registration = "Registration";

        /// <summary>网站版权等信息</summary>
        public const String SiteCopyright = "SiteCopyright";
    }
    #endregion
}
