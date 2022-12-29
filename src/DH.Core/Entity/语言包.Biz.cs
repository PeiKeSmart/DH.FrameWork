using DH.Core;
using DH.Core.Domain.Localization;
using DH.Core.Infrastructure;

using NewLife;
using NewLife.Data;
using NewLife.Log;

using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

using XCode;
using XCode.Cache;

namespace DH.Entity;

/// <summary>语言包</summary>
public partial class LocaleStringResource : DHEntityBase<LocaleStringResource>
{
    #region 对象操作
    static LocaleStringResource()
    {
        // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
        //var df = Meta.Factory.AdditionalFields;
        //df.Add(__.CultureId);

        // 过滤器 UserModule、TimeModule、IPModule
    }

    /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
    /// <param name="isNew">是否插入</param>
    public override void Valid(Boolean isNew)
    {
        // 如果没有脏数据，则不需要进行任何处理
        if (!HasDirty) return;

        // 在新插入数据或者修改了指定字段时进行修正

        // 检查唯一索引
        // CheckExist(isNew, __.LanKey, __.CultureId);
    }

    ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //protected internal override void InitData()
    //{
    //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
    //    if (Meta.Session.Count > 0) return;

    //    if (XTrace.Debug) XTrace.WriteLine("开始初始化LocaleStringResource[语言包]数据……");

    //    var entity = new LocaleStringResource();
    //    entity.Id = 0;
    //    entity.LanKey = "abc";
    //    entity.CultureId = 0;
    //    entity.LanValue = "abc";
    //    entity.Module = "abc";
    //    entity.LanType = "abc";
    //    entity.Insert();

    //    if (XTrace.Debug) XTrace.WriteLine("完成初始化LocaleStringResource[语言包]数据！");
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
    /// <summary>语言信息</summary>
    [XmlIgnore, ScriptIgnore, IgnoreDataMember]
    public Language Language => Extends.Get(nameof(Language), k => Language.FindById(CultureId));

    /// <summary>语言名称</summary>
    [XmlIgnore, ScriptIgnore, IgnoreDataMember]
    public String CultureName => Language?.Name;
    #endregion

    #region 扩展查询
    /// <summary>根据编号查找</summary>
    /// <param name="id">编号</param>
    /// <returns>实体对象</returns>
    public static LocaleStringResource FindById(Int32 id)
    {
        if (id <= 0) return null;

        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

        // 单对象缓存
        return Meta.SingleCache[id];

        //return Find(_.Id == id);
    }

    /// <summary>根据翻译词、语言标识Id查找</summary>
    /// <param name="lanKey">翻译词</param>
    /// <param name="cultureId">语言标识Id</param>
    /// <returns>实体对象</returns>
    public static LocaleStringResource FindByLanKeyAndCultureId(String lanKey, Int32 cultureId)
    {
        if (lanKey.IsNullOrWhiteSpace()) return null;

        return Meta.Cache.Find(e => e.LanKey.EqualIgnoreCase(lanKey) && e.CultureId == cultureId);
    }

    /// <summary>
    /// 根据翻译词查找
    /// </summary>
    /// <param name="lanKey">翻译词</param>
    /// <returns>实体集合</returns>
    public static IList<LocaleStringResource> FindByLanKey(String lanKey)
    {
        if (lanKey.IsNullOrWhiteSpace()) return new List<LocaleStringResource>();

        if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.LanKey.EqualIgnoreCase(lanKey));

        return FindAll(_.LanKey == lanKey);
    }

    /// <summary>
    /// 根据ID集合删除数据
    /// </summary>
    /// <param name="Ids">ID集合</param>
    public static void DelByIds(String Ids)
    {
        if (Delete(_.Id.In(Ids.Trim(','))) > 0)
            Meta.Cache.Clear("");
    }

    /// <summary>根据编号列表查找</summary>
    /// <param name="ids">编号列表</param>
    /// <returns>实体对象</returns>
    public static IList<LocaleStringResource> FindByIds(String ids)
    {
        if (ids.IsNullOrWhiteSpace()) return new List<LocaleStringResource>();

        ids = ids.Trim(',');

        if (Meta.Session.Count < 1000)
        {
            return Meta.Cache.FindAll(x => ids.SplitAsInt(",").Contains(x.Id));
        }

        return FindAll(_.Id.In(ids.Split(',')));
    }

    /// <summary>
    /// 分页获取数据
    /// </summary>
    /// <param name="pages">分页数据</param>
    /// <returns></returns>
    public static IList<LocaleStringResource> GetWithPage(PageParameter pages)
    {
        if (Meta.Session.Count < 1000)
        {
            pages.TotalCount = Meta.Session.Count;
            return FindAllWithCache().OrderBy(e => e.Id).Skip((pages.PageIndex - 1) * pages.PageSize).Take(pages.PageSize).ToList();
        }

        pages.Desc = false;
        pages.Sort = "Id";

        return FindAll(null, pages);
    }

    /// <summary>
    /// 根据指定的ResourceKey属性获取资源字符串。
    /// </summary>
    /// <param name="resourceKey">代表ResourceKey的字符串</param>
    /// <returns>代表请求的资源字符串的翻译内容</returns>
    public static String GetResource(String resourceKey)
    {
        var _workContext = EngineContext.Current.Resolve<IWorkContext>();
        if (_workContext.GetWorkingLanguage() != null)
            return GetResource(resourceKey, _workContext.GetWorkingLanguage().Id);

        return GetResource(resourceKey, Language.FindByDefault().Id);
    }

    /// <summary>
    /// 根据指定的ResourceKey属性获取资源字符串。
    /// </summary>
    /// <param name="resourceKey">代表ResourceKey的字符串</param>
    /// <param name="Lng">语言数据</param>
    /// <returns>代表请求的资源字符串的翻译内容</returns>
    public static String GetResource(String resourceKey, String Lng)
    {
        var model = Language.FindByUniqueSeoCode(Lng);
        if (model == null)
            model = Language.FindByDefault();

        return GetResource(resourceKey, model.Id);
    }

    /// <summary>
    /// 根据指定的ResourceKey属性获取资源字符串。
    /// </summary>
    /// <param name="resourceKey">表示ResourceKey的字符串。</param>
    /// <param name="languageId">语言标识符</param>
    /// <param name="logIfNotFound">一个值，指示如果找不到区域设置字符串资源，是否记录错误</param>
    /// <param name="defaultValue">默认值元素按顺序呈现</param>
    /// <param name="returnEmptyIfNotFound">一个值，指示如果找不到资源并且默认值设置为空字符串，是否将返回空字符串</param>
    /// <returns>代表请求的资源字符串的字符串。</returns>
    public static string GetResource(string resourceKey, int languageId,
        bool logIfNotFound = true, string defaultValue = "", bool returnEmptyIfNotFound = false)
    {
        var result = string.Empty;
        if (resourceKey == null)
            resourceKey = string.Empty;
        resourceKey = resourceKey.Trim();

        var model = FindByLanKeyAndCultureId(resourceKey, languageId);

        if (model != null)
        {
            result = model.LanValue;
        }
        else
        {
            try
            {
                model = new LocaleStringResource();
                model.LanKey = resourceKey;
                model.CultureId = languageId;
                model.LanValue = resourceKey;
                model.Insert();
            }
            catch (Exception ex)
            {
                XTrace.WriteException(ex);
            }
        }

        if (!string.IsNullOrEmpty(result))
            return result;

        if (logIfNotFound)
            XTrace.Log.Warn($"Resource string ({resourceKey}) is not found. Language ID = {languageId}");

        if (!string.IsNullOrEmpty(defaultValue))
        {
            result = defaultValue;
        }
        else
        {
            if (!returnEmptyIfNotFound)
                result = resourceKey;
        }

        if (result.IsNullOrWhiteSpace())
        {
            var localizationSettings = EngineContext.Current.Resolve<LocalizationSettings>();

            model = FindByLanKeyAndCultureId(resourceKey, localizationSettings.DefaultCountry);

            if (model != null)
            {
                result = model.LanValue;
            }
        }

        return result;
    }

    /// <summary>根据编号查找</summary>
    /// <param name="resourceName">资源名称</param>
    /// <param name="languageId">语言Id</param>
    /// <returns>实体对象</returns>
    public static LocaleStringResource FindByResourceNameAndLanguageId(String resourceName, Int32 languageId)
    {
        if (languageId <= 0 || resourceName.IsNullOrWhiteSpace()) return null;

        // 实体缓存
        if (Meta.Session.Count < 20000) return Meta.Cache.Find(e => e.CultureId == languageId && e.LanKey == resourceName);

        return Find(_.CultureId == languageId & _.LanKey == resourceName);
    }

    /// <summary>获取所有语言包</summary>
    /// <returns>语言集合</returns>
    public static IList<LocaleStringResource> GetAll()
    {
        // 实体缓存
        if (Meta.Session.Count < 20000) return Meta.Cache.Entities;

        return FindAll();
    }
    #endregion

    #region 高级查询
    /// <summary>高级查询</summary>
    /// <param name="lanKey">翻译词</param>
    /// <param name="cultureId">语言标识Id</param>
    /// <param name="key">关键字</param>
    /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
    /// <returns>实体列表</returns>
    public static IList<LocaleStringResource> Search(String lanKey, Int32 cultureId, String key, PageParameter page)
    {
        var exp = new WhereExpression();

        if (!lanKey.IsNullOrEmpty()) exp &= _.LanKey.Contains(lanKey);
        if (cultureId > 0)
        {
            exp &= _.CultureId == cultureId;
        }
        else
        {
            exp &= _.CultureId.In(Language.FindSQLWithKey(Language._.Status == true));
        }
        if (!key.IsNullOrEmpty()) exp &= _.LanValue.Contains(key);

        return FindAll(exp, page);
    }

    // Select Count(Id) as Id,LanKey From LocaleStringResource Where CreateTime>'2020-01-24 00:00:00' Group By LanKey Order By Id Desc limit 20
    static readonly FieldCache<LocaleStringResource> _LanKeyCache = new FieldCache<LocaleStringResource>(nameof(LanKey))
    {
        //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
    };

    /// <summary>获取翻译词列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
    /// <returns></returns>
    public static IDictionary<String, String> GetLanKeyList() => _LanKeyCache.FindAllName();
    #endregion

    #region 业务操作
    /// <summary>
    /// 初始化一些系统翻译数据
    /// </summary>
    public static void InitSystemData()
    {
        InitInsert("欢迎使用创楚软件", "欢迎使用创楚软件！", "歡迎使用創楚軟件！", "Welcome to ChuangChu Software!", true);
        InitInsert("企业版", "企业版", "企業版", "Enterprise Edition", true);
        InitInsert("试用", "试用", "試用", "Trial", true);
        InitInsert("荆州创楚科技其他产品", "荆州创楚科技其他产品：", "荊州創楚科技其他產品：", "Products Also From ChuangChu Software:", true);
        InitInsert("模块邦", "模块邦", "模塊邦", "MKB", true);
        InitInsert("创楚官网", "创楚官网", "創楚官網", "ChuangChu", true);
        InitInsert("登灏官网", "登灏官网", "登灝官網", "DengHao", true);
        InitInsert("永久授权", "永久授权", "永久授權", "Permanent Authorization", true);
        InitInsert("到期时间", "到期时间:", "到期時間:", "Expire Date:", true);
        InitInsert("不限人数", "不限人数", "不限人數", "Unlimited", true);
        InitInsert("授权人数", "授权人数:", "授權人數:", "Authorized Number:", true);
        InitInsert("关于创楚", "关于创楚", "關於創楚", "About", true);
        InitInsert("用户登录", "用户登录", "用戶登錄", "User Login");
        InitInsert("保存", "保存", "保存", "Save");
        InitInsert("稍候", "稍候...", "稍候...", "Loading...");
        InitInsert("展开全部", "展开全部", "展開全部", "Expand");
        InitInsert("连接超时", "连接超时，请检查网络环境，或重试！", "連接超時，請檢查網絡環境，或重試！", "Timeout. Check your newtwork connections, or try it again!");
        InitInsert("用户名", "用户名", "用戶名", "Account");
        InitInsert("密码", "密码", "密碼", "Password");
        InitInsert("请重复密码", "请重复密码", "請重複密碼", "Repeat Password");
        InitInsert("保持登录", "保持登录", "保持登錄", "Keep Login");
        InitInsert("登录", "登录", "登錄", "Login");
        InitInsert("后台登录", "后台登录", "後台登錄", "Manager Login");
        InitInsert("忘记密码", "忘记密码", "忘記密碼", "Forgot Password?");
        InitInsert("禁用了本地密码登录，且没有配置第三方登录", "禁用了本地密码登录，且没有配置第三方登录", "禁用了本地密碼登錄，且沒有配置第三方登錄", "Local password login is disabled, and no third-party login is configured");
        InitInsert("用户被禁用", "用户被禁用", "用戶被禁用", "User is disabled");
        InitInsert("用户没有权限", "用户没有权限", "用戶沒有權限", "User does not have permission");
        InitInsert("用户名和密码不匹配", "用户名和密码不匹配", "用戶名和密碼不匹配", "Username and password do not match");
        InitInsert("未知错误", "未知错误", "未知錯誤", "unknown error");
        InitInsert("用户登录异常", "用户{0}登录时发生异常/r/n{1}", "用戶{0}登錄時發生異常/r/n{1}", "An exception occurred when user {0} logged in/r/n{1}");
        InitInsert("请填写用户名", "请填写用户名", "請填寫用戶名", "Please enter your account");
        InitInsert("请填写密码", "请填写密码", "請填寫密碼", "Please fill in the password");
        InitInsert("密码不一致", "密码不一致", "密碼不一致", "Passwords are inconsistent");
        InitInsert("登录需要提供验证码", "{0}分钟内登录错误{1}次以上需要提供验证码", "{0}分鐘內登錄錯誤{1}次以上需要提供驗證碼", "Login error within {0} minutes more than {1} time requires verification code");
        InitInsert("验证码不能为空", "验证码不能为空", "驗證碼不能為空", "Verification code cannot be empty");
        InitInsert("验证码错误", "验证码错误", "驗證碼錯誤", "Verification code error");
        InitInsert("延时登录提醒", "登录次数超过{0}次，请{1}秒后再次登录~", "登錄次數超過{0}次，請{1}秒後再次登錄~", "You have logged in more than {0} times, please log in again in {1} seconds~");
        InitInsert("检验验证码时发生异常", "检验验证码时发生异常", "檢驗驗證碼時發生異常", "An exception occurred while verifying the verification code");
        InitInsert("普通用户请联系管理员重置密码", "普通用户请联系管理员重置密码", "普通用戶請聯繫管理員重置密碼", "Contact the Administrator to reset your password.");
        InitInsert("管理员重置密码提示", "管理员请登录系统所在的服务器，在根目录下创建<span> '{0}' </span>文件。", "管理員請登錄系統所在的服務器，在根目錄下創建<span> '{0}' </span>文件。", "If you are, please login your host and create a file named <span> '{0}' </span>.");
        InitInsert("用户不存在", "用户不存在", "用戶不存在", "User not exist");
        InitInsert("首页", "首页", "首頁", "Home");
        InitInsert("会员", "会员", "會員", "User");
        InitInsert("组织", "组织", "組織", "Organization");
        InitInsert("主题", "主题", "主題", "Theme");
        InitInsert("注意", "注意：", "注意：", "Note:");
        InitInsert("公司", "公司", "公司", "Company");
        InitInsert("编辑", "编辑", "編輯", "Edit");
        InitInsert("稍后", "稍后", "稍後", "Later");
        InitInsert("刷新", "刷新", "刷新", "Refresh");
        InitInsert("找回密码成功", "找回密码成功！", "找回密碼成功！", "Password recovery succeeded!");
        InitInsert("找回密码失败", "找回密码失败！", "找回密碼失敗！", "Failed to retrieve password!");
        InitInsert("浏览器提醒", "你目前使用的浏览器可能无法得到最佳浏览效果，建议使用Chrome、火狐、IE9+、Opera、Safari浏览器。", "你目前使用的瀏覽器可能無法得到最佳瀏覽效果，建議使用Chrome、火狐、IE9+、Opera、Safari瀏覽器。", "The browser you currently use might not get the best browsing results. It is recommended that you use Chrome, Firefox, IE9+, Opera or Safari.");
        InitInsert("用户", "用户", "用戶", "User");
        InitInsert("返回", "返回", "返回", "Back");
        InitInsert("更改密码", "更改密码", "更改密碼", "Password");
        InitInsert("个人档案", "个人档案", "個人檔案", "Profile");
        InitInsert("权限", "权限", "權限", "Privilege");
        InitInsert("授权", "授权", "授權", "Authorize");
        InitInsert("删除", "删除", "刪除", "Delete");
        InitInsert("编号", "编号", "編號", "ID");
        InitInsert("名称", "名称", "名稱", "Name");
        InitInsert("搜索", "搜索", "搜索", "Search");
        InitInsert("天", "天", "天", "Day");
        InitInsert("周", "周", "周", "Week");
        InitInsert("月", "月", "月", "Month");
        InitInsert("全选", "全选", "全選", "Select All");
        InitInsert("排序", "排序", "排序", "Rank");
        InitInsert("操作", "操作", "操作", "Action");
        InitInsert("查看", "查看", "查看", "View");
        InitInsert("添加", "添加", "添加", "Add");
        InitInsert("修改", "修改", "修改", "Edit");
        InitInsert("编辑权限", "编辑权限", "編輯權限", "Edit Privileges");
        InitInsert("技术支持带冒号", "技术支持：", "技術支持：", "Technical Support:");
        InitInsert("前往官网", "前往官网", "前往官網", "Official Website");
        InitInsert("记住密码", "记住密码", "記住密碼", "Remember");
        InitInsert("图形验证码", "图形验证码", "圖形驗證碼", "Captcha");
        InitInsert("密码不能为空", "密码不能为空", "密碼不能為空", "Password cannot be empty");
        InitInsert("原密码不能为空", "原密码不能为空", "原密碼不能為空", "The original password cannot be empty");
        InitInsert("新密码不能为空", "新密码不能为空", "新密碼不能為空", "The new password cannot be empty");
        InitInsert("两次密码应该相等", "两次密码应该相等", "兩次密碼應該相等", "The two passwords should be equal");
        InitInsert("原密码错误", "原密码错误", "原密碼錯誤", "The original password is incorrect");
        InitInsert("账号不能为空", "账号不能为空", "賬號不能為空", "Account cannot be empty");
        InitInsert("语言切换", "语言切换", "語言切換", "Language");
        InitInsert("退出", "退出", "退出", "Logout");
        InitInsert("主页", "主页", "主頁", "Home");
        InitInsert("控制台", "控制台", "控制台", "Console");
        InitInsert("修改密码", "修改密码", "修改密碼", "Password");
        InitInsert("用户名/邮箱/手机/编码", "用户名/邮箱/手机/编码", "用戶名/郵箱/手機/編碼", "UserName/Email/Mobile/Code");
        InitInsert("返回上一页", "返回上一页", "返回上一頁", "Go Back");
        InitInsert("以其它身份登录", "以其它身份登录", "以其它身份登錄", "Login Another");
        InitInsert("没有访问资源[{0}]的权限[{1}]", "没有访问资源[{0}]的权限[{1}]", "沒有訪問資源[{0}]的權限[{1}]", "No access to resource [{0}][{1}]");
        InitInsert("访问资源 {0} 需要 {1} 权限", "访问资源 {0} 需要 {1} 权限", "訪問資源 {0} 需要 {1} 權限", "Accessing resources {0} requires {1} permission");
        InitInsert("性别", "性别", "性別", "Gender");
        InitInsert("访问", "访问", "訪問", "Visit");
        InitInsert("记录", "记录", "記錄", "Record");
        InitInsert("编辑成功", "编辑成功", "編輯成功", "Edit Success");
    }

    public static void InitInsert(String LanKey, String LanValuecn, String LanValuetw, String LanValueus, Boolean Ischeck = false)
    {
        var langvalue = FindByLanKeyAndCultureId(LanKey, 1);

        if (langvalue == null || langvalue.LanValue.IsNullOrWhiteSpace())
        {
            langvalue = new LocaleStringResource();
            langvalue.LanKey = LanKey;
            langvalue.CultureId = 1;
            langvalue.LanValue = LanValuecn;
            langvalue.Insert();
        }
        else if (langvalue.LanValue.IsNullOrWhiteSpace())
        {
            langvalue.LanKey = LanKey;
            langvalue.CultureId = 1;
            langvalue.LanValue = LanValuecn;
            langvalue.Update();
        }
        else
        {
            if (Ischeck)
            {
                if (!langvalue.LanValue.Contains(LanValuecn, StringComparison.InvariantCultureIgnoreCase))
                {
                    langvalue.LanValue = LanValuecn;
                    langvalue.Update();
                }
            }
        }

        langvalue = FindByLanKeyAndCultureId(LanKey, 2);

        if (langvalue == null)
        {
            langvalue = new LocaleStringResource();
            langvalue.LanKey = LanKey;
            langvalue.CultureId = 2;
            langvalue.LanValue = LanValuetw;
            langvalue.Insert();
        }
        else if (langvalue.LanValue.IsNullOrWhiteSpace())
        {
            langvalue.LanKey = LanKey;
            langvalue.CultureId = 2;
            langvalue.LanValue = LanValuetw;
            langvalue.Update();
        }
        else
        {
            if (Ischeck)
            {
                if (!langvalue.LanValue.Contains(LanValuetw, StringComparison.InvariantCultureIgnoreCase))
                {
                    langvalue.LanValue = LanValuetw;
                    langvalue.Update();
                }
            }
        }

        langvalue = FindByLanKeyAndCultureId(LanKey, 3);

        if (langvalue == null)
        {
            langvalue = new LocaleStringResource();
            langvalue.LanKey = LanKey;
            langvalue.CultureId = 3;
            langvalue.LanValue = LanValueus;
            langvalue.Insert();
        }
        else if (langvalue.LanValue.IsNullOrWhiteSpace())
        {
            langvalue.LanKey = LanKey;
            langvalue.CultureId = 3;
            langvalue.LanValue = LanValueus;
            langvalue.Update();
        }
        else
        {
            if (Ischeck)
            {
                if (!langvalue.LanValue.Contains(LanValueus, StringComparison.InvariantCultureIgnoreCase))
                {
                    langvalue.LanValue = LanValueus;
                    langvalue.Update();
                }
            }
        }

        langvalue = FindByLanKeyAndCultureId(LanKey, 4);

        if (langvalue == null || langvalue.LanValue.IsNullOrWhiteSpace())
        {
            langvalue = new LocaleStringResource();
            langvalue.LanKey = LanKey;
            langvalue.CultureId = 4;
            langvalue.LanValue = LanValueus;
            langvalue.Insert();
        }
        else
        {
            if (Ischeck)
            {
                if (!langvalue.LanValue.Contains(LanValueus, StringComparison.InvariantCultureIgnoreCase))
                {
                    langvalue.LanValue = LanValueus;
                    langvalue.Update();
                }
            }
        }
    }

    #endregion
}