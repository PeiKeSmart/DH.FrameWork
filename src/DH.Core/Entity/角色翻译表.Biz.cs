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

namespace DH.Entity
{
    /// <summary>角色翻译表</summary>
    public partial class RoleLan : DHEntityBase<RoleLan>
    {
        #region 对象操作
        static RoleLan()
        {
            // 累加字段，生成 Update xx Set Count=Count+1234 Where xxx
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(nameof(RId));

            // 过滤器 UserModule、TimeModule、IPModule
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 在新插入数据或者修改了指定字段时进行修正
        }

        ///// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //protected override void InitData()
        //{
        //    // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
        //    if (Meta.Session.Count > 0) return;

        //    if (XTrace.Debug) XTrace.WriteLine("开始初始化RoleLan[角色翻译表]数据……");

        //    var entity = new RoleLan();
        //    entity.Id = 0;
        //    entity.RId = 0;
        //    entity.LId = 0;
        //    entity.Name = "abc";
        //    entity.Remark = "abc";
        //    entity.Insert();

        //    if (XTrace.Debug) XTrace.WriteLine("完成初始化RoleLan[角色翻译表]数据！");
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
        public static RoleLan FindById(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Id == id);

            // 单对象缓存
            return Meta.SingleCache[id];

            //return Find(_.Id == id);
        }

        /// <summary>
        /// 根据角色Id获取所属语言数据
        /// </summary>
        /// <param name="rId">角色Id</param>
        /// <returns></returns>
        public static IList<RoleLan> FindAllByRId(Int32 rId)
        {
            if (rId <= 0) return new List<RoleLan>();

            if (Meta.Session.Count < 1000) return Meta.Cache.FindAll(e => e.RId == rId);

            return FindAll(_.RId == rId);
        }

        /// <summary>
        /// 通过角色Id和语言Id获取翻译数据
        /// </summary>
        /// <param name="rId">角色Id</param>
        /// <param name="lId">语言Id</param>
        /// <param name="IsGetDefault">是否获取默认数据</param>
        /// <returns></returns>
        public static (String Name, String Remark) FindByRIdAndLId(Int32 rId, Int32 lId, Boolean IsGetDefault = true)
        {
            if (rId <= 0 || lId <= 0) return ("", "");

            if (Meta.Session.Count < 1000)
            {
                var model = Meta.Cache.Find(e => e.RId == rId && e.LId == lId);
                if (IsGetDefault)
                {
                    return FindNameAndRemark(rId, model);
                }
                else
                {
                    if (model == null)
                        return ("", "");
                    return (model.Name, model.Remark);
                }
            }

            var exp = new WhereExpression();
            exp = _.RId == rId & _.LId == lId;

            var m = Find(exp);

            if (IsGetDefault)
            {
                return FindNameAndRemark(rId, m);
            }
            else
            {
                if (m == null)
                    return ("", "");
                return (m.Name, m.Remark);
            }
        }

        /// <summary>
        /// 获取翻译数据
        /// </summary>
        /// <param name="rId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private static (String Name, String Remark) FindNameAndRemark(Int32 rId, RoleLan model)
        {
            var r = Role.FindByID(rId);

            if (model == null)
            {
                return (r.Name, r.Remark);
            }
            else
            {
                var Name = model.Name.IsNullOrWhiteSpace() ? r.Name : model.Name;
                var Remark = model.Remark.IsNullOrWhiteSpace() ? r.Remark : model.Remark;
                return (Name, Remark);
            }
        }


    /// <summary>根据关联角色表Id、关联所属语言Id查找</summary>
    /// <param name="rId">关联角色表Id</param>
    /// <param name="lId">关联所属语言Id</param>
    /// <returns>实体对象</returns>
    public static RoleLan FindByRIdAndLId(Int32 rId, Int32 lId)
    {
        // 实体缓存
        if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.RId == rId && e.LId == lId);

        return Find(_.RId == rId & _.LId == lId);
    }
        #endregion

        #region 高级查询

        // Select Count(Id) as Id,Category From RoleLan Where CreateTime>'2020-01-24 00:00:00' Group By Category Order By Id Desc limit 20
        //static readonly FieldCache<RoleLan> _CategoryCache = new FieldCache<RoleLan>(nameof(Category))
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