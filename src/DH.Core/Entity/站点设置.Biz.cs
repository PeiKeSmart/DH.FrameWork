using DH.Caching;
using DH.Models;

using NewLife;
using NewLife.Caching;
using NewLife.Data;
using NewLife.Log;

using System.ComponentModel;

using XCode;
using XCode.Cache;

namespace DH.Entity;

public partial class SiteSettingInfo : DHEntityBase<SiteSettingInfo>
{
    #region 对象操作
    static SiteSettingInfo()
    {

        // 过滤器 UserModule、TimeModule、IPModule
    }

    /// <summary>验证并修补数据，通过抛出异常的方式提示验证失败。</summary>
    /// <param name="isNew">是否插入</param>
    public override void Valid(Boolean isNew)
    {
        // 如果没有脏数据，则不需要进行任何处理
        if (!HasDirty) return;

        // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
        if (Key.IsNullOrEmpty()) throw new ArgumentNullException(nameof(Key), "键名称不能为空！");

        // 建议先调用基类方法，基类方法会做一些统一处理
        base.Valid(isNew);

        // 在新插入数据或者修改了指定字段时进行修正
    }

    /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected override void InitData()
    {
        // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        if (Meta.Session.Count > 0) return;

        if (XTrace.Debug) XTrace.WriteLine("开始初始化SiteSettingInfo[站点设置]数据……");

        var list = new List<SiteSettingInfo>();
        list.Add(new SiteSettingInfo { Key = "CustomerTel", Value = "" });
        list.Add(new SiteSettingInfo { Key = "Keyword", Value = "实木床" });
        list.Add(new SiteSettingInfo { Key = "Hotkeywords", Value = "三只松鼠,实木床" });
        list.Add(new SiteSettingInfo { Key = "PageFoot", Value = "" });
        list.Add(new SiteSettingInfo { Key = "MemberLogo", Value = $"{DHSetting.Current.UploadPath}/common/usercenter_logo.png" });
        list.Add(new SiteSettingInfo { Key = "MemberSmallLogo", Value = $"{DHSetting.Current.UploadPath}/common/usercenter_mini_logo.png" });
        list.Add(new SiteSettingInfo { Key = "FlowScript", Value = "FlowScript1" });
        list.Add(new SiteSettingInfo { Key = "ProdutAuditOnOff", Value = "1" });
        list.Add(new SiteSettingInfo { Key = "WithDrawMinimum", Value = "1" });
        list.Add(new SiteSettingInfo { Key = "WithDrawMaximum", Value = "2000" });
        list.Add(new SiteSettingInfo { Key = "WeekSettlement", Value = "7" });
        list.Add(new SiteSettingInfo { Key = "WorkOrderTimeout", Value = "7" });
        list.Add(new SiteSettingInfo { Key = "CodeInvalidRefund", Value = "7" });
        list.Add(new SiteSettingInfo { Key = "SiteIsClose", Value = "False" });
        list.Add(new SiteSettingInfo { Key = "RegisterType", Value = "0" });
        list.Add(new SiteSettingInfo { Key = "MobileVerifOpen", Value = "False" });
        list.Add(new SiteSettingInfo { Key = "RegisterEmailRequired", Value = "False" });
        list.Add(new SiteSettingInfo { Key = "EmailVerifOpen", Value = "False" });
        list.Add(new SiteSettingInfo { Key = "WXLogo", Value = "/Storage/Plat/Site/wxlogo.png" });
        list.Add(new SiteSettingInfo { Key = "PCLoginPic", Value = "/Storage/Plat/Site/pcloginpic.png" });
        list.Add(new SiteSettingInfo { Key = "WeixinAppId", Value = "" });
        list.Add(new SiteSettingInfo { Key = "WeixinAppSecret", Value = "" });
        list.Add(new SiteSettingInfo { Key = "WeixinToken", Value = "" });
        list.Add(new SiteSettingInfo { Key = "WeixinPartnerID", Value = "" });
        list.Add(new SiteSettingInfo { Key = "WeixinPartnerKey", Value = "" });
        list.Add(new SiteSettingInfo { Key = "WeixinLoginUrl", Value = "" });
        list.Add(new SiteSettingInfo { Key = "WeixinIsValidationService", Value = "False" });
        list.Add(new SiteSettingInfo { Key = "AdvancePaymentPercent", Value = "0" });
        list.Add(new SiteSettingInfo { Key = "AdvancePaymentLimit", Value = "0" });
        list.Add(new SiteSettingInfo { Key = "UnpaidTimeout", Value = "3" });
        list.Add(new SiteSettingInfo { Key = "NoReceivingTimeout", Value = "7" });
        list.Add(new SiteSettingInfo { Key = "OrderCommentTimeout", Value = "15" });
        list.Add(new SiteSettingInfo { Key = "SalesReturnTimeout", Value = "15" });
        list.Add(new SiteSettingInfo { Key = "AS_ShopConfirmTimeout", Value = "1" });
        list.Add(new SiteSettingInfo { Key = "AS_SendGoodsCloseTimeout", Value = "1" });
        list.Add(new SiteSettingInfo { Key = "AS_ShopNoReceivingTimeout", Value = "1" });
        list.Add(new SiteSettingInfo { Key = "WX_MSGGetCouponTemplateId", Value = "" });
        list.Add(new SiteSettingInfo { Key = "AppUpdateDescription", Value = "" });
        list.Add(new SiteSettingInfo { Key = "AppVersion", Value = "2.5" });
        list.Add(new SiteSettingInfo { Key = "AndriodDownLoad", Value = "./app/himall.apk" });
        list.Add(new SiteSettingInfo { Key = "IOSDownLoad", Value = "https://itunes.apple.com/cn/app/id1058273436" });
        list.Add(new SiteSettingInfo { Key = "CanDownload", Value = "False" });
        list.Add(new SiteSettingInfo { Key = "ShopAppVersion", Value = "" });
        list.Add(new SiteSettingInfo { Key = "ShopAndriodDownLoad", Value = "" });
        list.Add(new SiteSettingInfo { Key = "ShopIOSDownLoad", Value = "" });
        list.Add(new SiteSettingInfo { Key = "KuaidiType", Value = "0" });
        list.Add(new SiteSettingInfo { Key = "Kuaidi100Key", Value = "" });
        list.Add(new SiteSettingInfo { Key = "KuaidiApp_key", Value = "" });
        list.Add(new SiteSettingInfo { Key = "KuaidiAppSecret", Value = "" });
        list.Add(new SiteSettingInfo { Key = "Limittime", Value = "True" });
        list.Add(new SiteSettingInfo { Key = "AdvertisementImagePath", Value = "" });
        list.Add(new SiteSettingInfo { Key = "AdvertisementUrl", Value = "" });
        list.Add(new SiteSettingInfo { Key = "AdvertisementState", Value = "False" });
        list.Add(new SiteSettingInfo { Key = "IsOpenStore", Value = "true" });
        list.Add(new SiteSettingInfo { Key = "IsOpenShopApp", Value = "True" });
        list.Add(new SiteSettingInfo { Key = "WeixinAppletId", Value = "" });
        list.Add(new SiteSettingInfo { Key = "WeixinAppletSecret", Value = "" });
        list.Add(new SiteSettingInfo { Key = "IsOpenPC", Value = "True" });
        list.Add(new SiteSettingInfo { Key = "IsOpenH5", Value = "false" });
        list.Add(new SiteSettingInfo { Key = "IsOpenApp", Value = "false" });
        list.Add(new SiteSettingInfo { Key = "IsOpenMallSmallProg", Value = "false" });
        list.Add(new SiteSettingInfo { Key = "ShopWithDrawMinimum", Value = "1" });
        list.Add(new SiteSettingInfo { Key = "ShopWithDrawMaximum", Value = "5000" });
        list.Add(new SiteSettingInfo { Key = "IsCanClearDemoData", Value = "true" });
        list.Add(new SiteSettingInfo { Key = "FaviconAndAppIconsHeadCode", Value = "<link rel=\"apple-touch-icon\" sizes=\"180x180\" href=\"/icons/icons_0/apple-touch-icon.png\"><link rel=\"icon\" type=\"image/png\" sizes=\"32x32\" href=\"/icons/icons_0/favicon-32x32.png\"><link rel=\"icon\" type=\"image/png\" sizes=\"192x192\" href=\"/icons/icons_0/android-chrome-192x192.png\"><link rel=\"icon\" type=\"image/png\" sizes=\"16x16\" href=\"/icons/icons_0/favicon-16x16.png\"><link rel=\"manifest\" href=\"/icons/icons_0/site.webmanifest\"><link rel=\"mask-icon\" href=\"/icons/icons_0/safari-pinned-tab.svg\" color=\"#5bbad5\"><link rel=\"shortcut icon\" href=\"/icons/icons_0/favicon.ico\"><meta name=\"msapplication-TileColor\" content=\"#2d89ef\"><meta name=\"msapplication-TileImage\" content=\"/icons/icons_0/mstile-144x144.png\"><meta name=\"msapplication-config\" content=\"/icons/icons_0/browserconfig.xml\"><meta name=\"theme-color\" content=\"#ffffff\">" });
        list.Add(new SiteSettingInfo { Key = "PointsReg", Value = "5" });
        list.Add(new SiteSettingInfo { Key = "PointsLogin", Value = "10" });
        list.Add(new SiteSettingInfo { Key = "PointsComments", Value = "10" });
        list.Add(new SiteSettingInfo { Key = "PointsSignin", Value = "10" });
        list.Add(new SiteSettingInfo { Key = "PointsInvite", Value = "10" });
        list.Add(new SiteSettingInfo { Key = "PointsRebate", Value = "1" });
        list.Add(new SiteSettingInfo { Key = "PointsOrderrate", Value = "4" });
        list.Add(new SiteSettingInfo { Key = "PointsOrdermax", Value = "4" });
        list.Add(new SiteSettingInfo { Key = "ExpLogin", Value = "20" });
        list.Add(new SiteSettingInfo { Key = "ExpComments", Value = "10" });
        list.Add(new SiteSettingInfo { Key = "ExpOrderRate", Value = "10" });
        list.Add(new SiteSettingInfo { Key = "ExpOrderMax", Value = "10" });
        list.Add(new SiteSettingInfo { Key = "DefaultUserPortrait", Value = $"{DHSetting.Current.UploadPath}/common/default_user_portrait.gif" });
        list.Add(new SiteSettingInfo { Key = "SiteLogoWx", Value = $"{DHSetting.Current.UploadPath}/common/site_logowx.png" });
        list.Add(new SiteSettingInfo { Key = "SiteLogo", Value = $"{DHSetting.Current.UploadPath}/common/site_logo.png" });
        list.Add(new SiteSettingInfo { Key = "MemberGrade", Value = "[{\"level\":1,\"level_name\":\"V1\",\"exppoints\":0},{\"level\":2,\"level_name\":\"V2\",\"exppoints\":150},{\"level\":3,\"level_name\":\"V3\",\"exppoints\":200},{\"level\":4,\"level_name\":\"V4\",\"exppoints\":500}]" });
        list.Add(new SiteSettingInfo { Key = "CertificationFee", Value = "1.00" });
        list.Add(new SiteSettingInfo { Key = "CertificationCount", Value = "5" });
        list.Add(new SiteSettingInfo { Key = "Gathering", Value = "" });
        list.Add(new SiteSettingInfo { Key = "AutomaticallyDetectLanguage", Value = "true" });
        list.Add(new SiteSettingInfo { Key = "IsInstalled", Value = "false" });

        list.Insert();

        if (XTrace.Debug) XTrace.WriteLine("完成初始化SiteSettingInfo[站点设置]数据！");
    }

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
    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static SiteSettingInfo FindById(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据键名称查找</summary>
    /// <param name="key">键名称</param>
    /// <returns>实体列表</returns>
    public static IList<SiteSettingInfo> FindAllByKey(String key)
    {
        if (key.IsNullOrEmpty()) return new List<SiteSettingInfo>();

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.Key.EqualIgnoreCase(key));

        return FindAll(_.Key == key);
    }

    /// <summary>
    /// 获取系统设置
    /// </summary>
    public static SiteSettings SiteSettings
    {
        get
        {
            var settings = (SiteSettings)CallContext.GetData(CacheKeyCollection.SiteSettings); // 线程内缓存
            if (settings == null)
            {
                settings = Cache.Default.Get<SiteSettings>(CacheKeyCollection.SiteSettings);  // 缓存中获取
                if (settings == null)
                {
                    settings = InitSettings();  // 数据库中加载
                    Cache.Default.Set(CacheKeyCollection.SiteSettings, settings);
                }
                CallContext.SetData(CacheKeyCollection.SiteSettings, settings);
            }

            return settings;
        }
    }

    private static SiteSettings InitSettings()
    {
        var settings = new SiteSettings();
        var properties = typeof(SiteSettings).GetProperties();

        var data = GetSiteSettings();
        foreach (var property in properties)
        {
            var temp = data.FirstOrDefault(item => item.Key == property.Name);
            if (temp != null)
                property.SetValue(settings, Convert.ChangeType(temp.Value, property.PropertyType));
        }

        return settings;
    }

    /// <summary>
    /// 获取所有数据
    /// </summary>
    /// <returns>实体集合</returns>
    public static IList<SiteSettingInfo> GetSiteSettings()
    {
        return Meta.Cache.Entities;
    }

    /// <summary>
    /// 获取指定Key的实体
    /// </summary>
    /// <param name="Key"></param>
    /// <returns></returns>
    public static SiteSettingInfo FindByKey(String Key)
    {
        if (Key.IsNullOrWhiteSpace()) return null;

        return Meta.Cache.Find(e => e.Key == Key);
    }
    #endregion

    #region 高级查询

    // Select Count(Id) as Id,Key From DG_SiteSetting Where CreateTime>'2020-01-24 00:00:00' Group By Key Order By Id Desc limit 20
    static readonly FieldCache<SiteSettingInfo> _KeyCache = new FieldCache<SiteSettingInfo>(nameof(Key))
    {
        //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
    };

    /// <summary>获取键名称列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
    /// <returns></returns>
    public static IDictionary<String, String> GetKeyList() => _KeyCache.FindAllName();
    #endregion

    #region 业务操作
    public SiteSettingInfoModel ToModel()
    {
        var model = new SiteSettingInfoModel();
        model.Copy(this);

         return model;
    }

    /// <summary>
    /// 保存对配置的修改
    /// </summary>
    public static void SaveChanges()
    {
        var current = SiteSettings;
        var data = GetSiteSettings();

        var properties = typeof(SiteSettings).GetProperties();

        using (var tran1 = Meta.CreateTrans())
        {
            foreach (var property in properties)
            {
                var key = property.Name;
                var oldValue = data.FirstOrDefault(p => p.Key == key)?.Value ?? string.Empty;
                var newValue = property.GetValue(current)?.ToString() ?? String.Empty;

                if (oldValue == newValue && oldValue == "")
                {
                    continue;
                }

                var model = data.Find(e => e.Key == key);
                if (model != null)
                {
                    model.Value = newValue;
                    model.Save();
                }
                else
                {
                    model = new SiteSettingInfo();
                    model.Key = key;
                    model.Value = newValue;

                    model.Save();
                }
            }

            tran1.Commit();
        }
    }

    #endregion
}
