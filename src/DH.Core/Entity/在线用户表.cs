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

/// <summary>在线用户表</summary>
[Serializable]
[DataObject]
[Description("在线用户表")]
[BindIndex("IU_DH_SysOnlineUsers_Sid", true, "Sid")]
[BindIndex("IX_DH_SysOnlineUsers_Updatetime", false, "Updatetime")]
[BindTable("DH_SysOnlineUsers", Description = "在线用户表", ConnName = "DG", DbType = DatabaseType.None)]
public partial class SysOnlineUsers : ISysOnlineUsers, IEntity<ISysOnlineUsers>
{
    #region 属性
    private Int32 _Id;
    /// <summary>编号</summary>
    [DisplayName("编号")]
    [Description("编号")]
    [DataObjectField(true, true, false, 0)]
    [BindColumn("Id", "编号", "")]
    public Int32 Id { get => _Id; set { if (OnPropertyChanging("Id", value)) { _Id = value; OnPropertyChanged("Id"); } } }

    private Int32 _Uid;
    /// <summary>用户id</summary>
    [DisplayName("用户id")]
    [Description("用户id")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Uid", "用户id", "")]
    public Int32 Uid { get => _Uid; set { if (OnPropertyChanging("Uid", value)) { _Uid = value; OnPropertyChanged("Uid"); } } }

    private Int64 _Sid;
    /// <summary>用户sessionid</summary>
    [DisplayName("用户sessionid")]
    [Description("用户sessionid")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Sid", "用户sessionid", "")]
    public Int64 Sid { get => _Sid; set { if (OnPropertyChanging("Sid", value)) { _Sid = value; OnPropertyChanged("Sid"); } } }

    private String _NickName;
    /// <summary>用户昵称</summary>
    [DisplayName("用户昵称")]
    [Description("用户昵称")]
    [DataObjectField(false, false, false, 50)]
    [BindColumn("NickName", "用户昵称", "")]
    public String NickName { get => _NickName; set { if (OnPropertyChanging("NickName", value)) { _NickName = value; OnPropertyChanged("NickName"); } } }

    private String _Name;
    /// <summary>用户名</summary>
    [DisplayName("用户名")]
    [Description("用户名")]
    [DataObjectField(false, false, false, 100)]
    [BindColumn("Name", "用户名", "", Master = true)]
    public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

    private String _Ip;
    /// <summary>用户ip</summary>
    [DisplayName("用户ip")]
    [Description("用户ip")]
    [DataObjectField(false, false, false, 30)]
    [BindColumn("Ip", "用户ip", "")]
    public String Ip { get => _Ip; set { if (OnPropertyChanging("Ip", value)) { _Ip = value; OnPropertyChanged("Ip"); } } }

    private String _Region;
    /// <summary>用户所在区域</summary>
    [DisplayName("用户所在区域")]
    [Description("用户所在区域")]
    [DataObjectField(false, false, true, 150)]
    [BindColumn("Region", "用户所在区域", "")]
    public String Region { get => _Region; set { if (OnPropertyChanging("Region", value)) { _Region = value; OnPropertyChanged("Region"); } } }

    private String _Network;
    /// <summary>运营商</summary>
    [DisplayName("运营商")]
    [Description("运营商")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Network", "运营商", "")]
    public String Network { get => _Network; set { if (OnPropertyChanging("Network", value)) { _Network = value; OnPropertyChanged("Network"); } } }

    private String _Numbers;
    /// <summary>代号</summary>
    [DisplayName("代号")]
    [Description("代号")]
    [DataObjectField(false, false, true, 50)]
    [BindColumn("Numbers", "代号", "")]
    public String Numbers { get => _Numbers; set { if (OnPropertyChanging("Numbers", value)) { _Numbers = value; OnPropertyChanged("Numbers"); } } }

    private Int32 _Clicks;
    /// <summary>请求次数</summary>
    [DisplayName("请求次数")]
    [Description("请求次数")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Clicks", "请求次数", "")]
    public Int32 Clicks { get => _Clicks; set { if (OnPropertyChanging("Clicks", value)) { _Clicks = value; OnPropertyChanged("Clicks"); } } }

    private String _UserAgent;
    /// <summary>特征字符串</summary>
    [DisplayName("特征字符串")]
    [Description("特征字符串")]
    [DataObjectField(false, false, true, 1000)]
    [BindColumn("UserAgent", "特征字符串", "")]
    public String UserAgent { get => _UserAgent; set { if (OnPropertyChanging("UserAgent", value)) { _UserAgent = value; OnPropertyChanged("UserAgent"); } } }

    private DateTime _Updatetime;
    /// <summary>最后更新时间</summary>
    [DisplayName("最后更新时间")]
    [Description("最后更新时间")]
    [DataObjectField(false, false, false, 0)]
    [BindColumn("Updatetime", "最后更新时间", "")]
    public DateTime Updatetime { get => _Updatetime; set { if (OnPropertyChanging("Updatetime", value)) { _Updatetime = value; OnPropertyChanged("Updatetime"); } } }
    #endregion

    #region 拷贝
    /// <summary>拷贝模型对象</summary>
    /// <param name="model">模型</param>
    public void Copy(ISysOnlineUsers model)
    {
        Id = model.Id;
        Uid = model.Uid;
        Sid = model.Sid;
        NickName = model.NickName;
        Name = model.Name;
        Ip = model.Ip;
        Region = model.Region;
        Network = model.Network;
        Numbers = model.Numbers;
        Clicks = model.Clicks;
        UserAgent = model.UserAgent;
        Updatetime = model.Updatetime;
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
            "Uid" => _Uid,
            "Sid" => _Sid,
            "NickName" => _NickName,
            "Name" => _Name,
            "Ip" => _Ip,
            "Region" => _Region,
            "Network" => _Network,
            "Numbers" => _Numbers,
            "Clicks" => _Clicks,
            "UserAgent" => _UserAgent,
            "Updatetime" => _Updatetime,
            _ => base[name]
        };
        set
        {
            switch (name)
            {
                case "Id": _Id = value.ToInt(); break;
                case "Uid": _Uid = value.ToInt(); break;
                case "Sid": _Sid = value.ToLong(); break;
                case "NickName": _NickName = Convert.ToString(value); break;
                case "Name": _Name = Convert.ToString(value); break;
                case "Ip": _Ip = Convert.ToString(value); break;
                case "Region": _Region = Convert.ToString(value); break;
                case "Network": _Network = Convert.ToString(value); break;
                case "Numbers": _Numbers = Convert.ToString(value); break;
                case "Clicks": _Clicks = value.ToInt(); break;
                case "UserAgent": _UserAgent = Convert.ToString(value); break;
                case "Updatetime": _Updatetime = value.ToDateTime(); break;
                default: base[name] = value; break;
            }
        }
    }
    #endregion

    #region 关联映射
    #endregion

    #region 扩展查询
    #endregion

    #region 字段名
    /// <summary>取得在线用户表字段信息的快捷方式</summary>
    public partial class _
    {
        /// <summary>编号</summary>
        public static readonly Field Id = FindByName("Id");

        /// <summary>用户id</summary>
        public static readonly Field Uid = FindByName("Uid");

        /// <summary>用户sessionid</summary>
        public static readonly Field Sid = FindByName("Sid");

        /// <summary>用户昵称</summary>
        public static readonly Field NickName = FindByName("NickName");

        /// <summary>用户名</summary>
        public static readonly Field Name = FindByName("Name");

        /// <summary>用户ip</summary>
        public static readonly Field Ip = FindByName("Ip");

        /// <summary>用户所在区域</summary>
        public static readonly Field Region = FindByName("Region");

        /// <summary>运营商</summary>
        public static readonly Field Network = FindByName("Network");

        /// <summary>代号</summary>
        public static readonly Field Numbers = FindByName("Numbers");

        /// <summary>请求次数</summary>
        public static readonly Field Clicks = FindByName("Clicks");

        /// <summary>特征字符串</summary>
        public static readonly Field UserAgent = FindByName("UserAgent");

        /// <summary>最后更新时间</summary>
        public static readonly Field Updatetime = FindByName("Updatetime");

        static Field FindByName(String name) => Meta.Table.FindByName(name);
    }

    /// <summary>取得在线用户表字段名称的快捷方式</summary>
    public partial class __
    {
        /// <summary>编号</summary>
        public const String Id = "Id";

        /// <summary>用户id</summary>
        public const String Uid = "Uid";

        /// <summary>用户sessionid</summary>
        public const String Sid = "Sid";

        /// <summary>用户昵称</summary>
        public const String NickName = "NickName";

        /// <summary>用户名</summary>
        public const String Name = "Name";

        /// <summary>用户ip</summary>
        public const String Ip = "Ip";

        /// <summary>用户所在区域</summary>
        public const String Region = "Region";

        /// <summary>运营商</summary>
        public const String Network = "Network";

        /// <summary>代号</summary>
        public const String Numbers = "Numbers";

        /// <summary>请求次数</summary>
        public const String Clicks = "Clicks";

        /// <summary>特征字符串</summary>
        public const String UserAgent = "UserAgent";

        /// <summary>最后更新时间</summary>
        public const String Updatetime = "Updatetime";
    }
    #endregion
}
