using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using NewLife.Threading;
using NewLife.Web;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;
using XCode.Shards;

namespace DH.Entity
{
    public partial class SiteInfoLan : DHEntityBase<SiteInfoLan>
    {
        #region 对象操作
        static SiteInfoLan()
        {
            // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(nameof(SiteInfoId));

            // 过滤器 UserModule、TimeModule、IPModule
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
            // CheckExist(isNew, nameof(SiteInfoId), nameof(LanguageId));
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Session.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化SiteInfoLan[站点基础信息翻译表]数据……");

        //    var entity = new SiteInfoLan();
        //    entity.SiteInfoId = 0;
        //    entity.LanguageId = 0;
        //    entity.SiteName = "abc";
        //    entity.SeoTitle = "abc";
        //    entity.SeoKey = "abc";
        //    entity.SeoDescribe = "abc";
        //    entity.Registration = "abc";
        //    entity.SiteCopyright = "abc";
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化SiteInfoLan[站点基础信息翻译表]数据！");
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
        /// <summary>站点基础信息Id</summary>
        [XmlIgnore, IgnoreDataMember]
        //[ScriptIgnore]
        public SiteInfo SiteInfo => Extends.Get(nameof(SiteInfo), k => SiteInfo.FindById(SiteInfoId));

        /// <summary>关联所属语言Id</summary>
        [XmlIgnore, IgnoreDataMember]
        //[ScriptIgnore]
        public Language Language => Extends.Get(nameof(Language), k => Language.FindById(LanguageId));

        /// <summary>关联所属语言Id</summary>
        [Map(nameof(LanguageId), typeof(Language), "Id")]
        public String LanguageName => Language?.Name;

        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static SiteInfoLan FindById(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.Id == id);
        }

        /// <summary>根据站点基础信息Id、关联所属语言Id查找</summary>
        /// <param name="siteInfoId">站点基础信息Id</param>
        /// <param name="languageId">关联所属语言Id</param>
        /// <returns>实体对象</returns>
        public static SiteInfoLan FindBySiteInfoIdAndLanguageId(Int32 siteInfoId, Int32 languageId)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.SiteInfoId == siteInfoId && e.LanguageId == languageId);

            return Find(_.SiteInfoId == siteInfoId & _.LanguageId == languageId);
        }

        /// <summary>
        /// 根据站点Id获取其翻译数据
        /// </summary>
        /// <param name="SId">站点Id</param>
        /// <returns></returns>
        public static IList<SiteInfoLan> FindAllBySId(Int32 SId)
        {
            if (SId <= 0) return new List<SiteInfoLan>();

            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.SiteInfoId == SId);

            return FindAll(_.SiteInfoId == SId);
        }

        /// <summary>
        /// 通过站点Id和语言Id获取翻译数据
        /// </summary>
        /// <param name="sId">站点Id</param>
        /// <param name="lId">语言Id</param>
        /// <param name="IsGetDefault">当翻译为空时是否获取默认数据</param>
        /// <returns></returns>
        public static (String SiteName, String SeoTitle, String SeoKey, String SeoDescribe, String Registration, String SiteCopyright) FindBySIdAndLId(Int32 sId, Int32 lId, Boolean IsGetDefault = true)
        {
            if (sId <= 0 || lId <= 0) return ("", "", "", "", "", "");

            if (Meta.Session.Count < 1000)
            {
                var model = Meta.Cache.Find(e => e.SiteInfoId == sId && e.LanguageId == lId);

                if (IsGetDefault)
                {
                    return FindNameAndRemark(sId, model);
                }
                else
                {
                    if (model == null)
                        return ("", "", "", "", "", "");
                    else
                        return (model.SiteName, model.SeoTitle, model.SeoKey, model.SeoDescribe, model.Registration, model.SiteCopyright);
                }
            }

            var exp = new WhereExpression();
            exp = _.SiteInfoId == sId & _.LanguageId == lId;

            var m = Find(exp);

            if (IsGetDefault)
            {
                return FindNameAndRemark(sId, m);
            }
            else
            {
                if (m == null)
                    return ("", "", "", "", "", "");
                else
                    return (m.SiteName, m.SeoTitle, m.SeoKey, m.SeoDescribe, m.Registration, m.SiteCopyright);
            }
        }

        /// <summary>
        /// 获取翻译数据
        /// </summary>
        /// <param name="sId">站点id</param>
        /// <param name="model">翻译实体</param>
        /// <returns></returns>
        private static (String SiteName, String SeoTitle, String SeoKey, String SeoDescribe, String Registration, String SiteCopyright) FindNameAndRemark(Int32 sId, SiteInfoLan model)
        {
            var r = SiteInfo.FindById(sId);

            if (model == null)
            {
                return (r.SiteName, r.SeoTitle, r.SeoKey, r.SeoDescribe, r.Registration, r.SiteCopyright);
            }
            else
            {
                var SiteName = model.SiteName.IsNullOrWhiteSpace() ? r.SiteName : model.SiteName;
                var SeoTitle = model.SeoTitle.IsNullOrWhiteSpace() ? r.SeoTitle : model.SeoTitle;
                var SeoKey = model.SeoKey.IsNullOrWhiteSpace() ? r.SeoKey : model.SeoKey;
                var SeoDescribe = model.SeoDescribe.IsNullOrWhiteSpace() ? r.SeoDescribe : model.SeoDescribe;
                var Registration = model.Registration.IsNullOrWhiteSpace() ? r.Registration : model.Registration;
                var SiteCopyright = model.SiteCopyright.IsNullOrWhiteSpace() ? r.SiteCopyright : model.SiteCopyright;
                return (SiteName, SeoTitle, SeoKey, SeoDescribe, Registration, SiteCopyright);
            }
        }
        #endregion

        #region 高级查询
        /// <summary>高级查询</summary>
        /// <param name="siteInfoId">站点基础信息Id</param>
        /// <param name="languageId">关联所属语言Id</param>
        /// <param name="key">关键字</param>
        /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
        /// <returns>实体列表</returns>
        public static IList<SiteInfoLan> Search(Int32 siteInfoId, Int32 languageId, String key, PageParameter page)
        {
            var exp = new WhereExpression();

            if (siteInfoId >= 0) exp &= _.SiteInfoId == siteInfoId;
            if (languageId >= 0) exp &= _.LanguageId == languageId;
            if (!key.IsNullOrEmpty()) exp &= _.SiteName.Contains(key) | _.SeoTitle.Contains(key) | _.SeoKey.Contains(key) | _.SeoDescribe.Contains(key) | _.Registration.Contains(key) | _.SiteCopyright.Contains(key);

            return FindAll(exp, page);
        }

        // Select Count(Id) as Id,Category From DG_SiteInfoLan Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
        //static readonly FieldCache<SiteInfoLan> _CategoryCache = new FieldCache<SiteInfoLan>(nameof(Category))
        //{
        //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
        //};

        ///// <summary>获取类别列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
        ///// <returns></returns>
        //public static IDictionary<String, String> GetCategoryList() => _CategoryCache.FindAllName();
        #endregion

        #region 业务操作
        #endregion
    }
}