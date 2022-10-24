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
    [BindIndex("IU_DH_LocaleStringResource_ResourceName_LanguageId", true, "ResourceName,LanguageId")]
    [BindTable("DH_LocaleStringResource", Description = "语言包", ConnName = "DH", DbType = DatabaseType.None)]
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

        private String _ResourceName;
        /// <summary>资源名称</summary>
        [DisplayName("资源名称")]
        [Description("资源名称")]
        [DataObjectField(false, false, true, 200)]
        [BindColumn("ResourceName", "资源名称", "", Master = true)]
        public String ResourceName { get => _ResourceName; set { if (OnPropertyChanging("ResourceName", value)) { _ResourceName = value; OnPropertyChanged("ResourceName"); } } }

        private String _ResourceValue;
        /// <summary>资源值</summary>
        [DisplayName("资源值")]
        [Description("资源值")]
        [DataObjectField(false, false, true, 2048)]
        [BindColumn("ResourceValue", "资源值", "")]
        public String ResourceValue { get => _ResourceValue; set { if (OnPropertyChanging("ResourceValue", value)) { _ResourceValue = value; OnPropertyChanged("ResourceValue"); } } }

        private Int32 _LanguageId;
        /// <summary>语言标识符</summary>
        [DisplayName("语言标识符")]
        [Description("语言标识符")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LanguageId", "语言标识符", "")]
        public Int32 LanguageId { get => _LanguageId; set { if (OnPropertyChanging("LanguageId", value)) { _LanguageId = value; OnPropertyChanged("LanguageId"); } } }
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
                    case "ResourceName": return _ResourceName;
                    case "ResourceValue": return _ResourceValue;
                    case "LanguageId": return _LanguageId;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Id": _Id = value.ToInt(); break;
                    case "ResourceName": _ResourceName = Convert.ToString(value); break;
                    case "ResourceValue": _ResourceValue = Convert.ToString(value); break;
                    case "LanguageId": _LanguageId = value.ToInt(); break;
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

            /// <summary>资源名称</summary>
            public static readonly Field ResourceName = FindByName("ResourceName");

            /// <summary>资源值</summary>
            public static readonly Field ResourceValue = FindByName("ResourceValue");

            /// <summary>语言标识符</summary>
            public static readonly Field LanguageId = FindByName("LanguageId");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得语言包字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Id = "Id";

            /// <summary>资源名称</summary>
            public const String ResourceName = "ResourceName";

            /// <summary>资源值</summary>
            public const String ResourceValue = "ResourceValue";

            /// <summary>语言标识符</summary>
            public const String LanguageId = "LanguageId";
        }
        #endregion
    }
}