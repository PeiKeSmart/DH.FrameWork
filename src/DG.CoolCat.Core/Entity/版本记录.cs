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
    /// <summary>版本记录</summary>
    [Serializable]
    [DataObject]
    [Description("版本记录")]
    [BindTable("VersionInfo", Description = "版本记录", ConnName = "DG", DbType = DatabaseType.None)]
    public partial class VersionInfo
    {
        #region 属性
        private Int64 _Version;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, false, false, 0)]
        [BindColumn("Version", "编号", "")]
        public Int64 Version { get => _Version; set { if (OnPropertyChanging("Version", value)) { _Version = value; OnPropertyChanged("Version"); } } }

        private DateTime _AppliedOn;
        /// <summary>确认时间</summary>
        [DisplayName("确认时间")]
        [Description("确认时间")]
        [DataObjectField(false, false, true, 0)]
        [BindColumn("AppliedOn", "确认时间", "")]
        public DateTime AppliedOn { get => _AppliedOn; set { if (OnPropertyChanging("AppliedOn", value)) { _AppliedOn = value; OnPropertyChanged("AppliedOn"); } } }

        private String _Description;
        /// <summary>说明</summary>
        [DisplayName("说明")]
        [Description("说明")]
        [DataObjectField(false, false, true, 1024)]
        [BindColumn("Description", "说明", "")]
        public String Description { get => _Description; set { if (OnPropertyChanging("Description", value)) { _Description = value; OnPropertyChanged("Description"); } } }
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
                    case "Version": return _Version;
                    case "AppliedOn": return _AppliedOn;
                    case "Description": return _Description;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "Version": _Version = value.ToLong(); break;
                    case "AppliedOn": _AppliedOn = value.ToDateTime(); break;
                    case "Description": _Description = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得版本记录字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field Version = FindByName("Version");

            /// <summary>确认时间</summary>
            public static readonly Field AppliedOn = FindByName("AppliedOn");

            /// <summary>说明</summary>
            public static readonly Field Description = FindByName("Description");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得版本记录字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String Version = "Version";

            /// <summary>确认时间</summary>
            public const String AppliedOn = "AppliedOn";

            /// <summary>说明</summary>
            public const String Description = "Description";
        }
        #endregion
    }
}