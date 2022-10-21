using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace DG.CoolCat.Core.Entity
{
    /// <summary>设置</summary>
    [Serializable]
    [DataObject]
    [Description("设置")]
    [BindTable("SiteSettings", Description = "设置", ConnName = "DG", DbType = DatabaseType.None)]
    public partial class SiteSettings
    {
        #region 属性
        private String _Key;
        /// <summary>关键字</summary>
        [DisplayName("关键字")]
        [Description("关键字")]
        [DataObjectField(true, false, true, 50)]
        [BindColumn("Key", "关键字", "")]
        public String Key { get => _Key; set { if (OnPropertyChanging("Key", value)) { _Key = value; OnPropertyChanged("Key"); } } }

        private String _Value;
        /// <summary>值</summary>
        [DisplayName("值")]
        [Description("值")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Value", "值", "")]
        public String Value { get => _Value; set { if (OnPropertyChanging("Value", value)) { _Value = value; OnPropertyChanged("Value"); } } }
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
                    case "Key": return _Key;
                    case "Value": return _Value;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Key": _Key = Convert.ToString(value); break;
                    case "Value": _Value = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得设置字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>关键字</summary>
            public static readonly Field Key = FindByName("Key");

            /// <summary>值</summary>
            public static readonly Field Value = FindByName("Value");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得设置字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>关键字</summary>
            public const String Key = "Key";

            /// <summary>值</summary>
            public const String Value = "Value";
        }
        #endregion
    }
}