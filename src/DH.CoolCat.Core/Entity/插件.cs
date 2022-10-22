using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace DH.CoolCat.Core.Entity
{
    /// <summary>插件</summary>
    [Serializable]
    [DataObject]
    [Description("插件")]
    [BindTable("Plugins", Description = "插件", ConnName = "DG", DbType = DatabaseType.None)]
    public partial class Plugins
    {
        #region 属性
        private String _PluginId;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, false, true, 50)]
        [BindColumn("PluginId", "编号", "")]
        public String PluginId { get => _PluginId; set { if (OnPropertyChanging("PluginId", value)) { _PluginId = value; OnPropertyChanged("PluginId"); } } }

        private String _UniqueKey;
        /// <summary>插件惟一码</summary>
        [DisplayName("插件惟一码")]
        [Description("插件惟一码")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("UniqueKey", "插件惟一码", "")]
        public String UniqueKey { get => _UniqueKey; set { if (OnPropertyChanging("UniqueKey", value)) { _UniqueKey = value; OnPropertyChanged("UniqueKey"); } } }

        private String _Name;
        /// <summary>插件名称</summary>
        [DisplayName("插件名称")]
        [Description("插件名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Name", "插件名称", "", Master = true)]
        public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

        private String _DisplayName;
        /// <summary>插件显示名称</summary>
        [DisplayName("插件显示名称")]
        [Description("插件显示名称")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("DisplayName", "插件显示名称", "")]
        public String DisplayName { get => _DisplayName; set { if (OnPropertyChanging("DisplayName", value)) { _DisplayName = value; OnPropertyChanged("DisplayName"); } } }

        private String _Version;
        /// <summary>版本号</summary>
        [DisplayName("版本号")]
        [Description("版本号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Version", "版本号", "")]
        public String Version { get => _Version; set { if (OnPropertyChanging("Version", value)) { _Version = value; OnPropertyChanged("Version"); } } }

        private Int16 _Enable;
        /// <summary>是否启用</summary>
        [DisplayName("是否启用")]
        [Description("是否启用")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Enable", "是否启用", "")]
        public Int16 Enable { get => _Enable; set { if (OnPropertyChanging("Enable", value)) { _Enable = value; OnPropertyChanged("Enable"); } } }
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
                    case "PluginId": return _PluginId;
                    case "UniqueKey": return _UniqueKey;
                    case "Name": return _Name;
                    case "DisplayName": return _DisplayName;
                    case "Version": return _Version;
                    case "Enable": return _Enable;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "PluginId": _PluginId = Convert.ToString(value); break;
                    case "UniqueKey": _UniqueKey = Convert.ToString(value); break;
                    case "Name": _Name = Convert.ToString(value); break;
                    case "DisplayName": _DisplayName = Convert.ToString(value); break;
                    case "Version": _Version = Convert.ToString(value); break;
                    case "Enable": _Enable = Convert.ToInt16(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得插件字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field PluginId = FindByName("PluginId");

            /// <summary>插件惟一码</summary>
            public static readonly Field UniqueKey = FindByName("UniqueKey");

            /// <summary>插件名称</summary>
            public static readonly Field Name = FindByName("Name");

            /// <summary>插件显示名称</summary>
            public static readonly Field DisplayName = FindByName("DisplayName");

            /// <summary>版本号</summary>
            public static readonly Field Version = FindByName("Version");

            /// <summary>是否启用</summary>
            public static readonly Field Enable = FindByName("Enable");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得插件字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String PluginId = "PluginId";

            /// <summary>插件惟一码</summary>
            public const String UniqueKey = "UniqueKey";

            /// <summary>插件名称</summary>
            public const String Name = "Name";

            /// <summary>插件显示名称</summary>
            public const String DisplayName = "DisplayName";

            /// <summary>版本号</summary>
            public const String Version = "Version";

            /// <summary>是否启用</summary>
            public const String Enable = "Enable";
        }
        #endregion
    }
}