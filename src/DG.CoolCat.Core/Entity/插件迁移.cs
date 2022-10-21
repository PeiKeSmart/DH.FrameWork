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
    /// <summary>插件迁移</summary>
    [Serializable]
    [DataObject]
    [Description("插件迁移")]
    [BindTable("PluginMigrations", Description = "插件迁移", ConnName = "DG", DbType = DatabaseType.None)]
    public partial class PluginMigrations
    {
        #region 属性
        private String _PluginMigrationId;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, false, true, 50)]
        [BindColumn("PluginMigrationId", "编号", "")]
        public String PluginMigrationId { get => _PluginMigrationId; set { if (OnPropertyChanging("PluginMigrationId", value)) { _PluginMigrationId = value; OnPropertyChanged("PluginMigrationId"); } } }

        private String _PluginId;
        /// <summary>插件编号</summary>
        [DisplayName("插件编号")]
        [Description("插件编号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("PluginId", "插件编号", "")]
        public String PluginId { get => _PluginId; set { if (OnPropertyChanging("PluginId", value)) { _PluginId = value; OnPropertyChanged("PluginId"); } } }

        private String _Version;
        /// <summary>版本号</summary>
        [DisplayName("版本号")]
        [Description("版本号")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Version", "版本号", "")]
        public String Version { get => _Version; set { if (OnPropertyChanging("Version", value)) { _Version = value; OnPropertyChanged("Version"); } } }

        private String _Up;
        /// <summary>新增表字符</summary>
        [DisplayName("新增表字符")]
        [Description("新增表字符")]
        [DataObjectField(false, false, true, 2048)]
        [BindColumn("Up", "新增表字符", "")]
        public String Up { get => _Up; set { if (OnPropertyChanging("Up", value)) { _Up = value; OnPropertyChanged("Up"); } } }

        private String _Down;
        /// <summary>删除表</summary>
        [DisplayName("删除表")]
        [Description("删除表")]
        [DataObjectField(false, false, true, 2048)]
        [BindColumn("Down", "删除表", "")]
        public String Down { get => _Down; set { if (OnPropertyChanging("Down", value)) { _Down = value; OnPropertyChanged("Down"); } } }
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
                    case "PluginMigrationId": return _PluginMigrationId;
                    case "PluginId": return _PluginId;
                    case "Version": return _Version;
                    case "Up": return _Up;
                    case "Down": return _Down;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "PluginMigrationId": _PluginMigrationId = Convert.ToString(value); break;
                    case "PluginId": _PluginId = Convert.ToString(value); break;
                    case "Version": _Version = Convert.ToString(value); break;
                    case "Up": _Up = Convert.ToString(value); break;
                    case "Down": _Down = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得插件迁移字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field PluginMigrationId = FindByName("PluginMigrationId");

            /// <summary>插件编号</summary>
            public static readonly Field PluginId = FindByName("PluginId");

            /// <summary>版本号</summary>
            public static readonly Field Version = FindByName("Version");

            /// <summary>新增表字符</summary>
            public static readonly Field Up = FindByName("Up");

            /// <summary>删除表</summary>
            public static readonly Field Down = FindByName("Down");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得插件迁移字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String PluginMigrationId = "PluginMigrationId";

            /// <summary>插件编号</summary>
            public const String PluginId = "PluginId";

            /// <summary>版本号</summary>
            public const String Version = "Version";

            /// <summary>新增表字符</summary>
            public const String Up = "Up";

            /// <summary>删除表</summary>
            public const String Down = "Down";
        }
        #endregion
    }
}