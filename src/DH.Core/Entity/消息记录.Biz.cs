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
    public partial class SendLog : DHEntityBase<SendLog>
    {
        #region 对象操作
        static SendLog()
        {
            // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(nameof(SmsId));

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

            // 在新插入数据或者修改了指定字段时进行修正
            // 处理当前已登录用户信息，可以由UserModule过滤器代劳
            /*var user = ManageProvider.User;
            if (user != null)
            {
                if (isNew && !Dirtys[nameof(CreateUserID)]) CreateUserID = user.ID;
            }*/
            //if (isNew && !Dirtys[nameof(CreateTime)]) CreateTime = DateTime.Now;
            //if (isNew && !Dirtys[nameof(CreateIP)]) CreateIP = ManageProvider.UserHost;
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Session.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化SendLog[消息记录]数据……");

        //    var entity = new SendLog();
        //    entity.Id = 0;
        //    entity.SType = 0;
        //    entity.Account = "abc";
        //    entity.Msg = "abc";
        //    entity.MType = 0;
        //    entity.Remark = "abc";
        //    entity.SmsId = 0;
        //    entity.CreateUser = "abc";
        //    entity.CreateUserID = 0;
        //    entity.CreateTime = DateTime.Now;
        //    entity.CreateIP = "abc";
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化SendLog[消息记录]数据！");
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
        public static SendLog FindById(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.Id == id);
        }

        /// <summary>根据手机号_邮箱查找</summary>
        /// <param name="account">手机号_邮箱</param>
        /// <returns>实体列表</returns>
        public static IList<SendLog> FindAllByAccount(String account)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.Account == account.Trim());

            return FindAll(_.Account == account.Trim());
        }

        /// <summary>根据手机号_邮箱查找</summary>
        /// <param name="account">手机号_邮箱</param>
        /// <returns>实体列表</returns>
        public static SendLog FindLastByAccount(String account)
        {
            if (account.IsNullOrWhiteSpace()) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.Account == account.Trim()).OrderByDescending(e => e.Id).FirstOrDefault();

            return Find(_.Account == account.Trim());
        }

        /// <summary>根据短信平台返回的Id查找</summary>
        /// <param name="smsId">短信平台返回的Id</param>
        /// <returns>实体列表</returns>
        public static IList<SendLog> FindAllBySmsId(String smsId)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.SmsId == smsId);

            return FindAll(_.SmsId == smsId);
        }

        #endregion

        #region 高级查询
        /// <summary>高级查询</summary>
        /// <param name="account">手机号/邮箱</param>
        /// <param name="smsId">短信平台返回的Id</param>
        /// <param name="key">关键字</param>
        /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
        /// <returns>实体列表</returns>
        public static IList<SendLog> Search(String account, Int64 smsId, String key, PageParameter page)
        {
            var exp = new WhereExpression();

            if (!account.IsNullOrEmpty()) exp &= _.Account == account;
            if (smsId >= 0) exp &= _.SmsId == smsId;
            if (!key.IsNullOrEmpty()) exp &= _.Msg.Contains(key) | _.Remark.Contains(key) | _.CreateUser.Contains(key) | _.CreateIP.Contains(key);

            return FindAll(exp, page);
        }

        /// <summary>分页查询</summary>
        /// <param name="name">短信手机号</param>
        /// <param name="key">关键字</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="page">分页参数信息。可携带统计和数据权限扩展查询等信息</param>
        /// <returns>实体列表</returns>
        public static IList<SendLog> Searchs(String name, String key, DateTime start, DateTime end, PageParameter page)
        {
            var exp = new WhereExpression();
            if (start > DateTime.MinValue)
            {
                exp &= _.CreateTime >= start;
            }
            if (end > DateTime.MinValue)
            {
                exp &= _.CreateTime < end;
            }

            if (!name.IsNullOrEmpty()) exp &= _.Account == name.Trim();
            if (!key.IsNullOrEmpty()) exp &= _.CreateUser.Contains(key.Trim());
            return FindAll(exp, page);
        }

        // Select Count(Id) as Id,Account From SendLog Where CreateTime>'2020-01-24 00:00:00' Group By Account Order By Id Desc limit 20
        static readonly FieldCache<SendLog> _AccountCache = new FieldCache<SendLog>(nameof(Account))
        {
            //Where = _.CreateTime > DateTime.Today.AddDays(-30) & Expression.Empty
        };

        /// <summary>获取手机号_邮箱列表，字段缓存10分钟，分组统计数据最多的前20种，用于魔方前台下拉选择</summary>
        /// <returns></returns>
        public static IDictionary<String, String> GetAccountList() => _AccountCache.FindAllName();
        #endregion

        #region 业务操作
        /// <summary>
        /// 根据ID集合删除数据
        /// </summary>
        /// <param name="Ids">ID集合</param>
        public static void DelByIds(String Ids)
        {
            if (Delete(_.Id.In(Ids)) > 0)
                Meta.Cache.Clear("");
        }
        #endregion
    }
}