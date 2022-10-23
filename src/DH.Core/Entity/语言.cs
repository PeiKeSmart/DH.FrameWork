using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace DH.Entity
{
    /// <summary>语言</summary>
    [Serializable]
    [DataObject]
    [Description("语言")]
    [BindIndex("IU_DH_Language_UniqueSeoCode", true, "UniqueSeoCode")]
    [BindTable("DH_Language", Description = "语言", ConnName = "DH", DbType = DatabaseType.None)]
    public partial class Language
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
        /// <summary>语言名称</summary>
        [DisplayName("语言名称")]
        [Description("语言名称")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("Name", "语言名称", "", Master = true)]
        public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

        private String _LanguageCulture;
        /// <summary>语言区域性</summary>
        [DisplayName("语言区域性")]
        [Description("语言区域性")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("LanguageCulture", "语言区域性", "")]
        public String LanguageCulture { get => _LanguageCulture; set { if (OnPropertyChanging("LanguageCulture", value)) { _LanguageCulture = value; OnPropertyChanged("LanguageCulture"); } } }

        private String _UniqueSeoCode;
        /// <summary>唯一SEO代码</summary>
        [DisplayName("唯一SEO代码")]
        [Description("唯一SEO代码")]
        [DataObjectField(false, false, true, 2)]
        [BindColumn("UniqueSeoCode", "唯一SEO代码", "")]
        public String UniqueSeoCode { get => _UniqueSeoCode; set { if (OnPropertyChanging("UniqueSeoCode", value)) { _UniqueSeoCode = value; OnPropertyChanged("UniqueSeoCode"); } } }

        private String _FlagImageFileName;
        /// <summary>标记图像文件名</summary>
        [DisplayName("标记图像文件名")]
        [Description("标记图像文件名")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("FlagImageFileName", "标记图像文件名", "")]
        public String FlagImageFileName { get => _FlagImageFileName; set { if (OnPropertyChanging("FlagImageFileName", value)) { _FlagImageFileName = value; OnPropertyChanged("FlagImageFileName"); } } }

        private Boolean _Rtl;
        /// <summary>语言是否支持从右向左</summary>
        [DisplayName("语言是否支持从右向左")]
        [Description("语言是否支持从右向左")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Rtl", "语言是否支持从右向左", "")]
        public Boolean Rtl { get => _Rtl; set { if (OnPropertyChanging("Rtl", value)) { _Rtl = value; OnPropertyChanged("Rtl"); } } }

        private Boolean _LimitedToStores;
        /// <summary>是否受限于/限制于网站系统设置</summary>
        [DisplayName("是否受限于_限制于网站系统设置")]
        [Description("是否受限于/限制于网站系统设置")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LimitedToStores", "是否受限于/限制于网站系统设置", "")]
        public Boolean LimitedToStores { get => _LimitedToStores; set { if (OnPropertyChanging("LimitedToStores", value)) { _LimitedToStores = value; OnPropertyChanged("LimitedToStores"); } } }

        private Int32 _DefaultCurrencyId;
        /// <summary>语言的默认货币的标识符。使用默认货币显示顺序时设置0</summary>
        [DisplayName("语言的默认货币的标识符")]
        [Description("语言的默认货币的标识符。使用默认货币显示顺序时设置0")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DefaultCurrencyId", "语言的默认货币的标识符。使用默认货币显示顺序时设置0", "")]
        public Int32 DefaultCurrencyId { get => _DefaultCurrencyId; set { if (OnPropertyChanging("DefaultCurrencyId", value)) { _DefaultCurrencyId = value; OnPropertyChanged("DefaultCurrencyId"); } } }

        private Boolean _Published;
        /// <summary>是否发布该语言</summary>
        [DisplayName("是否发布该语言")]
        [Description("是否发布该语言")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Published", "是否发布该语言", "")]
        public Boolean Published { get => _Published; set { if (OnPropertyChanging("Published", value)) { _Published = value; OnPropertyChanged("Published"); } } }

        private Int32 _DisplayOrder;
        /// <summary>显示顺序</summary>
        [DisplayName("显示顺序")]
        [Description("显示顺序")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("DisplayOrder", "显示顺序", "")]
        public Int32 DisplayOrder { get => _DisplayOrder; set { if (OnPropertyChanging("DisplayOrder", value)) { _DisplayOrder = value; OnPropertyChanged("DisplayOrder"); } } }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case "Id": return _Id;
                    case "Name": return _Name;
                    case "LanguageCulture": return _LanguageCulture;
                    case "UniqueSeoCode": return _UniqueSeoCode;
                    case "FlagImageFileName": return _FlagImageFileName;
                    case "Rtl": return _Rtl;
                    case "LimitedToStores": return _LimitedToStores;
                    case "DefaultCurrencyId": return _DefaultCurrencyId;
                    case "Published": return _Published;
                    case "DisplayOrder": return _DisplayOrder;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "Name": _Name = Convert.ToString(value); break;
                    case "LanguageCulture": _LanguageCulture = Convert.ToString(value); break;
                    case "UniqueSeoCode": _UniqueSeoCode = Convert.ToString(value); break;
                    case "FlagImageFileName": _FlagImageFileName = Convert.ToString(value); break;
                    case "Rtl": _Rtl = value.ToBoolean(); break;
                    case "LimitedToStores": _LimitedToStores = value.ToBoolean(); break;
                    case "DefaultCurrencyId": _DefaultCurrencyId = value.ToInt(); break;
                    case "Published": _Published = value.ToBoolean(); break;
                    case "DisplayOrder": _DisplayOrder = value.ToInt(); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得语言字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>语言名称</summary>
            public static readonly Field Name = FindByName("Name");

            /// <summary>语言区域性</summary>
            public static readonly Field LanguageCulture = FindByName("LanguageCulture");

            /// <summary>唯一SEO代码</summary>
            public static readonly Field UniqueSeoCode = FindByName("UniqueSeoCode");

            /// <summary>标记图像文件名</summary>
            public static readonly Field FlagImageFileName = FindByName("FlagImageFileName");

            /// <summary>语言是否支持从右向左</summary>
            public static readonly Field Rtl = FindByName("Rtl");

            /// <summary>是否受限于/限制于网站系统设置</summary>
            public static readonly Field LimitedToStores = FindByName("LimitedToStores");

            /// <summary>语言的默认货币的标识符。使用默认货币显示顺序时设置0</summary>
            public static readonly Field DefaultCurrencyId = FindByName("DefaultCurrencyId");

            /// <summary>是否发布该语言</summary>
            public static readonly Field Published = FindByName("Published");

            /// <summary>显示顺序</summary>
            public static readonly Field DisplayOrder = FindByName("DisplayOrder");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得语言字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>语言名称</summary>
            public const String Name = "Name";

            /// <summary>语言区域性</summary>
            public const String LanguageCulture = "LanguageCulture";

            /// <summary>唯一SEO代码</summary>
            public const String UniqueSeoCode = "UniqueSeoCode";

            /// <summary>标记图像文件名</summary>
            public const String FlagImageFileName = "FlagImageFileName";

            /// <summary>语言是否支持从右向左</summary>
            public const String Rtl = "Rtl";

            /// <summary>是否受限于/限制于网站系统设置</summary>
            public const String LimitedToStores = "LimitedToStores";

            /// <summary>语言的默认货币的标识符。使用默认货币显示顺序时设置0</summary>
            public const String DefaultCurrencyId = "DefaultCurrencyId";

            /// <summary>是否发布该语言</summary>
            public const String Published = "Published";

            /// <summary>显示顺序</summary>
            public const String DisplayOrder = "DisplayOrder";
        }
        #endregion
    }
}