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
    /// <summary>语言包</summary>
    [Serializable]
    [DataObject]
    [Description("语言包")]
    [BindIndex("IU_DG_LocaleStringResource_LanKey_CultureId", true, "LanKey,CultureId")]
    [BindTable("DG_LocaleStringResource", Description = "语言包", ConnName = "DG", DbType = DatabaseType.None)]
    public partial class LocaleStringResource
    {
        #region 属性
        private Int32 _Id;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

        private String _LanKey;
        /// <summary>翻译词</summary>
        [DisplayName("翻译词")]
        [Description("翻译词")]
        [DataObjectField(false, false, true, 100)]
        [BindColumn("LanKey", "翻译词", "varchar(100)")]
        public String LanKey { get => _LanKey; set { if (OnPropertyChanging("LanKey", value)) { _LanKey = value; OnPropertyChanged("LanKey"); } } }

        private Int32 _CultureId;
        /// <summary>语言标识Id</summary>
        [DisplayName("语言标识Id")]
        [Description("语言标识Id")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CultureId", "语言标识Id", "")]
        public Int32 CultureId { get => _CultureId; set { if (OnPropertyChanging("CultureId", value)) { _CultureId = value; OnPropertyChanged("CultureId"); } } }

        private String _LanValue;
        /// <summary>翻译值</summary>
        [DisplayName("翻译值")]
        [Description("翻译值")]
        [DataObjectField(false, false, true, 2000)]
        [BindColumn("LanValue", "翻译值", "")]
        public String LanValue { get => _LanValue; set { if (OnPropertyChanging("LanValue", value)) { _LanValue = value; OnPropertyChanged("LanValue"); } } }

        private String _Module;
        /// <summary>模块</summary>
        [DisplayName("模块")]
        [Description("模块")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Module", "模块", "varchar(50)")]
        public String Module { get => _Module; set { if (OnPropertyChanging("Module", value)) { _Module = value; OnPropertyChanged("Module"); } } }

        private String _LanType;
        /// <summary>类型</summary>
        [DisplayName("类型")]
        [Description("类型")]
        [DataObjectField(false, false, true, 20)]
        [BindColumn("LanType", "类型", "varchar(20)")]
        public String LanType { get => _LanType; set { if (OnPropertyChanging("LanType", value)) { _LanType = value; OnPropertyChanged("LanType"); } } }
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
                    case "LanKey": return _LanKey;
                    case "CultureId": return _CultureId;
                    case "LanValue": return _LanValue;
                    case "Module": return _Module;
                    case "LanType": return _LanType;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "LanKey": _LanKey = Convert.ToString(value); break;
                    case "CultureId": _CultureId = value.ToInt(); break;
                    case "LanValue": _LanValue = Convert.ToString(value); break;
                    case "Module": _Module = Convert.ToString(value); break;
                    case "LanType": _LanType = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得语言包字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Id = FindByName("Id");

            /// <summary>翻译词</summary>
            public static readonly Field LanKey = FindByName("LanKey");

            /// <summary>语言标识Id</summary>
            public static readonly Field CultureId = FindByName("CultureId");

            /// <summary>翻译值</summary>
            public static readonly Field LanValue = FindByName("LanValue");

            /// <summary>模块</summary>
            public static readonly Field Module = FindByName("Module");

            /// <summary>类型</summary>
            public static readonly Field LanType = FindByName("LanType");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得语言包字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>翻译词</summary>
            public const String LanKey = "LanKey";

            /// <summary>语言标识Id</summary>
            public const String CultureId = "CultureId";

            /// <summary>翻译值</summary>
            public const String LanValue = "LanValue";

            /// <summary>模块</summary>
            public const String Module = "Module";

            /// <summary>类型</summary>
            public const String LanType = "LanType";
        }
        #endregion
    }
}