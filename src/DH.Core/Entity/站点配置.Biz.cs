using DH.Core;
using DH.Core.Configuration;

using NewLife;
using NewLife.Data;

using System.ComponentModel;

using XCode;

namespace DH.Entity
{
    public partial class Setting : DHEntityBase<Setting>
    {
        #region 对象操作
        static Setting()
        {

            // 过滤器 UserModule、TimeModule、IPModule

            // 单对象缓存
            var sc = Meta.SingleCache;
            sc.FindSlaveKeyMethod = k => Find(_.Name == k);
            sc.GetSlaveKeyMethod = e => e.Name;
        }

        /// <summary>验证并修补数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 建议先调用基类方法，基类方法会做一些统一处理
            base.Valid(isNew);

            // 在新插入数据或者修改了指定字段时进行修正

            // 检查唯一索引
            // CheckExist(isNew, nameof(Name));
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Session.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化Setting[站点配置]数据……");

        //    var entity = new Setting();
        //    entity.Name = "abc";
        //    entity.Value = "abc";
        //    entity.StoreId = "abc";
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化Setting[站点配置]数据！");
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
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static Setting FindById(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 10000) return Meta.Cache.Find(e => e.Id == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.Id == id);
        }

        /// <summary>根据键名称查找</summary>
        /// <param name="name">键名称</param>
        /// <returns>实体对象</returns>
        public static Setting FindByName(String name)
        {
            // 实体缓存
            if (Meta.Session.Count < 10000) return Meta.Cache.Find(e => e.Name.EqualIgnoreCase(name));

            // 单对象缓存
            //return Meta.SingleCache.GetItemWithSlaveKey(name) as Setting;

            return Find(_.Name == name);
        }

        /// <summary>根据键名称查找</summary>
        /// <param name="name">键名称</param>
        /// <param name="storeId">站点Id</param>
        /// <returns>实体对象</returns>
        public static Setting FindByNameAndStoreId(String name, Int32 storeId)
        {
            // 实体缓存
            if (Meta.Session.Count < 10000) return Meta.Cache.Find(e => e.Name.EqualIgnoreCase(name) && e.StoreId == storeId);

            return Find(_.Name == name & _.StoreId == storeId);
        }

        /// <summary>获取全部配置项</summary>
        /// <returns>实体集合</returns>
        public static IEnumerable<Setting> GetAll()
        {
            // 实体缓存
            if (Meta.Session.Count < 10000) return Meta.Cache.Entities;

            return FindAll();
        }

        /// <summary>获取全部配置项</summary>
        /// <returns>实体集合</returns>
        public static IEnumerable<Setting> GetAllSettings()
        {
            // 实体缓存
            if (Meta.Session.Count < 10000) return Meta.Cache.Entities.OrderBy(l => l.Name).ThenBy(l => l.StoreId);

            return FindAll(null, new PageParameter { PageSize = 0, OrderBy = "Name asc, StoreId asc" });
        }

        /// <summary>
        /// 按键获取设置值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="storeId">站点标识符</param>
        /// <param name="loadSharedValueIfNotFound">一个值，指示如果找不到特定于某个值的值，是否应加载共享（针对所有存储）值</param>
        /// <returns>
        /// 设置值
        /// </returns>
        public static T GetSettingByKey<T>(string key, T defaultValue = default,
            int storeId = 0, bool loadSharedValueIfNotFound = false)
        {
            if (string.IsNullOrEmpty(key))
                return defaultValue;

            IEnumerable<Setting> list;

            // 实体缓存
            if (Meta.Session.Count < 10000)
            {
                list = Meta.Cache.FindAll(e => e.Name.EqualIgnoreCase(key.Trim().ToLowerInvariant())).OrderBy(l => l.Name).ThenBy(l => l.StoreId);
            }
            else
            {
                list = FindAll(_.Name == key.Trim().ToLowerInvariant(), new PageParameter { PageSize = 0, OrderBy = "Name asc, StoreId asc" });
            }

            if (!list.Any())
                return defaultValue;

            var setting = list.FirstOrDefault(x => x.StoreId == storeId);

            // 加载共享值?
            if (setting == null && storeId > 0 && loadSharedValueIfNotFound)
                setting = list.FirstOrDefault(x => x.StoreId == 0);

            return setting != null ? CommonHelper.To<T>(setting.Value) : defaultValue;
        }

        #endregion

        #region 高级查询
        /// <summary>高级查询</summary>
        /// <param name="name">键名称</param>
        /// <param name="key">关键字</param>
        /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
        /// <returns>实体列表</returns>
        public static IList<Setting> Search(String name, String key, PageParameter page)
        {
            var exp = new WhereExpression();

            if (!name.IsNullOrEmpty()) exp &= _.Name == name;
            if (!key.IsNullOrEmpty()) exp &= _.Name.Contains(key) | _.Value.Contains(key) | _.StoreId.Contains(key);

            return FindAll(exp, page);
        }

        // Select Count(Id) as Id,Category From DH_Setting Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
        //static readonly FieldCache<Setting> _CategoryCache = new FieldCache<Setting>(nameof(Category))
        //{
        //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
        //};

        ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
        ///// <returns></returns>
        //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();
        #endregion

        #region 业务操作

        /// <summary>
        /// 保存设置对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="storeId">站点标识</param>
        /// <param name="settings">正在设置实例</param>
        public static void SaveSetting<T>(T settings, int storeId = 0) where T : ISettings, new()
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                // 获取可以读写的属性
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                var key = typeof(T).Name + "." + prop.Name;
                var value = prop.GetValue(settings, null);
                if (value != null)
                    SetSetting(prop.PropertyType, key, value, storeId);
                else
                    SetSetting(key, string.Empty, storeId);
            }
        }

        /// <summary>
        /// 设置设定值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="storeId">站点标识符</param>
        public static void SetSetting<T>(string key, T value, int storeId = 0)
        {
            SetSetting(typeof(T), key, value, storeId);
        }

        /// <summary>
        /// 设置设定值
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="storeId">站点标识符</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetSetting(Type type, string key, object value, int storeId = 0)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            key = key.Trim().ToLowerInvariant();
            var valueStr = TypeDescriptor.GetConverter(type).ConvertToInvariantString(value);

            var model = FindByNameAndStoreId(key, storeId);
            if (model == null)
            {
                model = new Setting();
                model.Name = key;
                model.Value = valueStr;
                model.StoreId = storeId;
                model.Insert();
            }
            else
            {
                model.Value = valueStr;
                model.Update();
            }
        }
        #endregion
    }
}