using DH.Timing;

using NewLife;
using NewLife.Caching;
using NewLife.Log;

using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

using XCode;
using XCode.Membership;

namespace DH.Entity;

public partial class SysOnlineUsers : DHEntityBase<SysOnlineUsers> {
    #region 对象操作
    static SysOnlineUsers()
    {
        // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
        var df = Meta.Factory.AdditionalFields;
        df.Add(nameof(Clicks));

        // 过滤器 UserModule、TimeModule、IPModule
        Meta.Modules.Add<TimeModule>();
    }

    /// <summary>验证并修补数据，通过抛出异常的方式提示验证失败。</summary>
    /// <param name="isNew">是否插入</param>
    public override void Valid(Boolean isNew)
    {
        // 如果没有脏数据，则不需要进行任何处理
        if (!HasDirty) return;

        // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
        if (NickName.IsNullOrEmpty()) throw new ArgumentNullException(nameof(NickName), "用户昵称不能为空！");
        if (Ip.IsNullOrEmpty()) throw new ArgumentNullException(nameof(Ip), "用户ip不能为空！");

        // 建议先调用基类方法，基类方法会做一些统一处理
        base.Valid(isNew);

        // 在新插入数据或者修改了指定字段时进行修正
        //if (!Dirtys[nameof(Updatetime)]) Updatetime = DateTime.Now;
    }

    ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //protected override void InitData()
    //{
    //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
    //    if (Meta.Session.Count > 0) return;

    //    if (XTrace.Debug) XTrace.WriteLine("开始初始化SysOnlineUsers[在线用户表]数据……");

    //    var entity = new SysOnlineUsers();
    //    entity.Uid = 0;
    //    entity.Sid = "abc";
    //    entity.NickName = "abc";
    //    entity.Ip = "abc";
    //    entity.Regionid = 0;
    //    entity.Updatetime = DateTime.Now;
    //    entity.Insert();

    //    if (XTrace.Debug) XTrace.WriteLine("完成初始化SysOnlineUsers[在线用户表]数据！");
    //}

    ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
    ///// <returns></returns>
    //public override Int32 Insert()
    //{
    //    return base.Insert();
    //}

    ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
    ///// <returns></returns>
    //protected override Int32 OnDelete()
    //{
    //    return base.OnDelete();
    //}
    #endregion

    #region 扩展属性
    /// <summary>用户</summary>
    [XmlIgnore, ScriptIgnore, IgnoreDataMember]
    //[ScriptIgnore]
    public User User => Extends.Get(nameof(User), k => User.FindByID(Uid));
    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static SysOnlineUsers FindById(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 10000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>
    /// 获得全部在线用户数量
    /// </summary>
    /// <returns></returns>
    public static Int64 GetOnlineUserCount()
    {
        var onlineUserExpire = DHSetting.Current.OnlineCountExpire;
        if (onlineUserExpire == 0)
            return GetOnlineUserCount(0);

        var cacheAllCount = Cache.Default.Get<Int64>($"{DHUtilSetting.Current.CacheKeyPrefix}.OnlineAllUserCount"); // 获取在线人数缓存数据

        if (cacheAllCount == 0)
        {
            cacheAllCount = GetOnlineUserCount(0);
            Cache.Default.Add($"{DHUtilSetting.Current.CacheKeyPrefix}.OnlineAllUserCount", cacheAllCount);
        }

        return cacheAllCount;
    }

    /// <summary>
    /// 获得在线用户数量
    /// </summary>
    /// <param name="userType">在线用户类型</param>
    /// <returns></returns>
    public static Int64 GetOnlineUserCount(Int32 userType)
    {
        if (userType == 0)
            return FindCount();
        else
            return FindCount(_.Uid == 0);
    }

    /// <summary>根据用户sessionid查找</summary>
    /// <param name="sid">用户sessionid</param>
    /// <returns>实体列表</returns>
    public static IList<SysOnlineUsers> FindAllBySid(Int64 sid)
    {
        if (sid <= 0) return new List<SysOnlineUsers>();

        // 实体缓存
        if (Meta.Session.Count < 10000) return Meta.Cache.FindAll(e => e.Sid == sid);

        return FindAll(_.Sid == sid);
    }

    /// <summary>
    /// 获取过期用户
    /// </summary>
    /// <param name="expiretime">过期时间</param>
    /// <returns></returns>
    public static IList<SysOnlineUsers> GetExpiredOnlineUser(DateTime expiretime)
    {
        if (Meta.Session.Count < 10000)
        {
            return Meta.Cache.FindAll(e => e.Updatetime < expiretime);
        }

        return FindAll(_.Updatetime < expiretime);
    }

    /// <summary>根据用户sessionid查找</summary>
    /// <param name="sid">用户sessionid</param>
    /// <returns>实体对象</returns>
    public static SysOnlineUsers FindBySid(Int64 sid)
    {
        if (sid <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Sid == sid);

        return Find(_.Sid == sid);
    }

    /// <summary>根据编号查找</summary>
    /// <param name="selects">字段</param>
    /// <returns>实体对象</returns>
    public static IList<SysOnlineUsers> GetAll(String selects = null)
    {
        // 实体缓存
        if (Meta.Session.Count < 10000) return Meta.Cache.Entities;

        return FindAll(null, null, selects);
    }
    #endregion

    #region 高级查询

    // Select Count(Id) as Id,Category From DH_OnlineUsers Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
    //static readonly FieldCache<SysOnlineUsers> _CategoryCache = new FieldCache<SysOnlineUsers>(nameof(Category))
    //{
    //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
    //};

    ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
    ///// <returns></returns>
    //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();
    #endregion

    #region 业务操作
    public SysOnlineUsersModel ToModel()
    {
        var model = new SysOnlineUsersModel();
        model.Copy(this);

        return model;
    }

    /// <summary>
    /// 重置在线用户表
    /// </summary>
    public static void ResetOnlineUserTable()
    {
        Delete(_.Id > 0);
    }

    /// <summary>
    /// 更新在线用户
    /// </summary>
    /// <param name="uid">用户Id</param>
    /// <param name="sid">用户惟一Id</param>
    /// <param name="nickName">昵称</param>
    /// <param name="Name">用户名</param>
    /// <param name="ip">Ip地址</param>
    /// <param name="region">区域</param>
    /// <param name="userAgent">特征字符串</param>
    /// <param name="network">运营商</param>
    /// <param name="numbers">代号</param>
    public static void UpdateOnlineUser(Int32 uid, Int64 sid, string nickName, String Name, string ip, String region, String userAgent, String network = "", String numbers = "")
    {
        using var span = DefaultTracer.Instance?.NewSpan(nameof(UpdateOnlineUser));

        if (sid <= 0) return;

        var onlineUserInfo = GetOnlineUserBySid(sid);
        if (onlineUserInfo != null)
        {
            try
            {
                onlineUserInfo.Uid = uid;
                onlineUserInfo.Sid = sid;
                onlineUserInfo.NickName = nickName;
                onlineUserInfo.Ip = ip;
                onlineUserInfo.Region = region;
                onlineUserInfo.Network = network;
                onlineUserInfo.Numbers = numbers;
                onlineUserInfo.UserAgent = userAgent;
                onlineUserInfo.Name = Name;
                onlineUserInfo.Clicks++;
                onlineUserInfo.SaveAsync();
            }
            catch (Exception ex)
            {
                span.SetError(ex, null);

                onlineUserInfo = new SysOnlineUsers();
                onlineUserInfo.Uid = uid;
                onlineUserInfo.Sid = sid;
                onlineUserInfo.NickName = nickName;
                onlineUserInfo.Ip = ip;
                onlineUserInfo.Region = region;
                onlineUserInfo.Network = network;
                onlineUserInfo.Numbers = numbers;
                onlineUserInfo.UserAgent = userAgent;
                onlineUserInfo.Name = Name;
                onlineUserInfo.Clicks = 1;
                onlineUserInfo.Insert();
            }
        }
        else
        {
            try
            {
                onlineUserInfo = new SysOnlineUsers();
                onlineUserInfo.Uid = uid;
                onlineUserInfo.Sid = sid;
                onlineUserInfo.NickName = nickName;
                onlineUserInfo.Ip = ip;
                onlineUserInfo.Region = region;
                onlineUserInfo.Network = network;
                onlineUserInfo.Numbers = numbers;
                onlineUserInfo.UserAgent = userAgent;
                onlineUserInfo.Name = Name;
                onlineUserInfo.Clicks = 1;
                onlineUserInfo.Insert();
            }
            catch (Exception ex)
            {
                span.SetError(ex, null);

                onlineUserInfo = GetOnlineUserBySid(sid);
                if (onlineUserInfo != null)
                {
                    onlineUserInfo.Uid = uid;
                    onlineUserInfo.Sid = sid;
                    onlineUserInfo.NickName = nickName;
                    onlineUserInfo.Ip = ip;
                    onlineUserInfo.Region = region;
                    onlineUserInfo.Network = network;
                    onlineUserInfo.Numbers = numbers;
                    onlineUserInfo.UserAgent = userAgent;
                    onlineUserInfo.Name = Name;
                    onlineUserInfo.Clicks++;
                    onlineUserInfo.SaveAsync();
                }
            }
        }

        if (uid > 0)
        {
            var model = User.FindByID(uid);
            if (model != null)
            {
                model.Online = true;
                model.SaveAsync();
            }
        }

        DeleteExpiredOnlineUser();
    }

    /// <summary>根据用户SessionId查找</summary>
    /// <param name="sid">SessionId</param>
    /// <returns>实体对象</returns>
    public static SysOnlineUsers GetOnlineUserBySid(Int64 sid)
    {
        if (sid <= 0) return null;

        if (Meta.Session.Count < 10000)
        {
            return Meta.Cache.Find(e => e.Sid == sid);
        }

        return Find(_.Sid == sid);
    }

    /// <summary>
    /// 最后一次删除过期在线用户的时间
    /// </summary>
    private static int _lastdeleteexpiredonlineuserstime = 0;
    /// <summary>
    /// 删除过期在线用户
    /// </summary>
    private static void DeleteExpiredOnlineUser()
    {
        if (_lastdeleteexpiredonlineuserstime == 0 || _lastdeleteexpiredonlineuserstime < DateTime.Now.AddMinutes(-DHSetting.Current.OnlineUserExpire).ToTimeStamp())
        {
            var expiretime = DateTime.Now.AddMinutes(-DHSetting.Current.OnlineUserExpire);

            var list = GetExpiredOnlineUser(expiretime);

            foreach (var item in list)
            {
                if (item.Uid > 0)
                {
                    var model = UserE.FindByID(item.Uid);
                    if (model != null)
                    {
                        model.Online = false;
                        model.SaveAsync();
                    }
                }
            }

            list.Delete();
            _lastdeleteexpiredonlineuserstime = DateTime.Now.ToTimeStamp();
        }
    }
    #endregion
}