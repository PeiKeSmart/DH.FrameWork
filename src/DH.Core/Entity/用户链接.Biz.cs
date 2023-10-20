using DH.AspNetCore.Webs;
using DH.Core.Webs;

using NewLife;
using NewLife.Data;
using NewLife.Serialization;

using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

using XCode;
using XCode.Cache;
using XCode.Membership;

namespace DH.Entity;


/// <summary>用户链接。第三方绑定</summary>
public partial class UserConnect : DHEntityBase<UserConnect> {
    #region 对象操作
    static UserConnect()
    {
        // 累加字段
        //Meta.Factory.AdditionalFields.Add(__.Logins);

        // 过滤器 UserModule、TimeModule、IPModule
        Meta.Modules.Add<UserModule>();
        Meta.Modules.Add<TimeModule>();
        Meta.Modules.Add<IPModule>();
    }

    /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
    /// <param name="isNew">是否插入</param>
    public override void Valid(Boolean isNew)
    {
        // 如果没有脏数据，则不需要进行任何处理
        if (!HasDirty) return;

        // 备注字段超长截取
        var len = _.Remark.Length;
        if (!Remark.IsNullOrEmpty() && len > 0 && Remark.Length > len) Remark = Remark[..len];

        base.Valid(isNew);
    }
    #endregion

    #region 扩展属性
    /// <summary>用户</summary>
    [XmlIgnore, ScriptIgnore, IgnoreDataMember]
    public User User => Extends.Get(nameof(User), k => UserE.FindByID(UserID));

    /// <summary>用户</summary>
    [Map(__.UserID)]
    public String UserName => User?.ToString();
    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static UserConnect FindByID(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.ID == id);
    }

    /// <summary>根据UnionID查找。跨多应用，因为微信公众号和微信开放平台可能共用UnionID</summary>
    /// <param name="unionId"></param>
    /// <returns></returns>
    public static IList<UserConnect> FindAllByUnionId(String unionId)
    {
        if (unionId.IsNullOrEmpty()) return new List<UserConnect>();

        return FindAll(_.UnionID == unionId);
    }

    /// <summary>根据第三方用户编号查找</summary>
    /// <param name="linkID">第三方用户编号</param>
    /// <returns>实体对象</returns>
    public static UserConnect FindByLinkID(Int32 linkID)
    {
        if (linkID <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.LinkID == linkID);

        return Find(_.LinkID == linkID);
    }

    /// <summary>根据提供商、身份标识查找</summary>
    /// <param name="provider">提供商</param>
    /// <param name="openid">身份标识</param>
    /// <returns>实体对象</returns>
    public static UserConnect FindByProviderAndOpenID(String provider, String openid)
    {
        if (provider.IsNullOrEmpty() || openid.IsNullOrEmpty()) return null;

        //// 实体缓存
        //if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Provider == provider && e.OpenID == openid);

        return Find(_.Provider == provider & _.OpenID == openid);
    }

    /// <summary>根据提供商、第三方用户编号查找</summary>
    /// <param name="provider">提供商</param>
    /// <param name="LinkID">第三方用户编号</param>
    /// <returns>实体对象</returns>
    public static UserConnect FindByProviderAndLinkID(String provider, String LinkID)
    {
        if (provider.IsNullOrEmpty() || LinkID.IsNullOrEmpty()) return null;

        //// 实体缓存
        //if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Provider == provider && e.OpenID == openid);

        return Find(_.Provider == provider & _.LinkID == LinkID);
    }


    /// <summary>根据提供商、身份标识查找</summary>
    /// <param name="provider">提供商</param>
    /// <param name="unionid">身份标识</param>
    /// <returns>实体对象</returns>
    public static UserConnect FindByProviderAndUnionID(String provider, String unionid)
    {
        if (provider.IsNullOrEmpty() || unionid.IsNullOrEmpty()) return null;

        //// 实体缓存
        //if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Provider == provider && e.OpenID == openid);

        return Find(_.Provider == provider & _.UnionID == unionid);
    }

    /// <summary>根据提供商、身份标识查找</summary>
    /// <param name="provider">提供商</param>
    /// <param name="userID">身份标识</param>
    /// <returns>实体对象</returns>
    public static UserConnect FindByProviderAndUserID(String provider, Int32 userID)
    {
        if (provider.IsNullOrEmpty() || userID <= 0) return null;

        //// 实体缓存
        //if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Provider == provider && e.UserID == userID);

        return Find(_.Provider == provider & _.UserID == userID);
    }

    /// <summary>根据用户查找</summary>
    /// <param name="userid">用户</param>
    /// <returns>实体列表</returns>
    public static IList<UserConnect> FindAllByUserID(Int32 userid)
    {
        //// 实体缓存
        //if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.UserID == userid);

        return FindAll(_.UserID == userid);
    }

    /// <summary>根据用户编号查找</summary>
    /// <param name="linkId">用户编号</param>
    /// <returns>实体列表</returns>
    public static IList<UserConnect> FindAllByLinkID(Int64 linkId)
    {
        if (linkId <= 0) return new List<UserConnect>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.LinkID == linkId);

        return FindAll(_.LinkID == linkId);
    }

    /// <summary>根据身份标识查找</summary>
    /// <param name="openId">身份标识</param>
    /// <returns>实体列表</returns>
    public static IList<UserConnect> FindAllByOpenID(String openId)
    {
        if (openId.IsNullOrEmpty()) return new List<UserConnect>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.OpenID.EqualIgnoreCase(openId));

        return FindAll(_.OpenID == openId);
    }

    /// <summary>根据设备标识查找</summary>
    /// <param name="deviceId">设备标识</param>
    /// <returns>实体列表</returns>
    public static IList<UserConnect> FindAllByDeviceId(String deviceId)
    {
        if (deviceId.IsNullOrEmpty()) return new List<UserConnect>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.DeviceId.EqualIgnoreCase(deviceId));

        return FindAll(_.DeviceId == deviceId);
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="provider"></param>
    /// <param name="userid"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="key"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static IList<UserConnect> Search(String provider, Int32 userid, DateTime start, DateTime end, String key, PageParameter p)
    {
        var exp = new WhereExpression();

        if (!provider.IsNullOrEmpty()) exp &= _.Provider == provider;
        if (userid > 0) exp &= _.UserID == userid;
        exp &= _.UpdateTime.Between(start, end);
        if (!key.IsNullOrEmpty()) exp &= _.NickName.Contains(key) | _.OpenID == key | _.UnionID == key | _.DeviceId == key;

        return FindAll(exp, p);
    }
    #endregion

    #region 业务操作
    /// <summary>填充用户</summary>
    /// <param name="client"></param>
    public virtual void Fill(OAuthClient client)
    {
        var uc = this;
        if (!client.NickName.IsNullOrEmpty()) uc.NickName = client.NickName;
        if (!client.Avatar.IsNullOrEmpty()) uc.Avatar = client.Avatar;

        uc.LinkID = client.UserID;
        uc.OpenID = client.OpenID;
        uc.UnionID = client.UnionID;
        uc.AccessToken = client.AccessToken;
        uc.RefreshToken = client.RefreshToken;
        uc.Expire = client.Expire;
        uc.RefreshExpire = client.RefreshExpire;
        uc.DeviceId = client.DeviceId;

        if (client.Items != null)
        {
            uc.Remark = client.Items
                .Where(e => e.Value == null || e.Value.Length < 200)
                .ToDictionary(e => e.Key, e => e.Value)
                .ToJson();
        }
    }

    static FieldCache<UserConnect> ProviderCache = new FieldCache<UserConnect>(__.Provider);

    /// <summary>获取所有提供商名称</summary>
    /// <returns></returns>
    public static IDictionary<String, String> FindAllProviderName() => ProviderCache.FindAllName();
    #endregion
}