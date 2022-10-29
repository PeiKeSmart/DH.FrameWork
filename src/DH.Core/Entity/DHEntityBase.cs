using DH.Core;
using DH.Core.Infrastructure;

using NewLife;
using NewLife.Reflection;

using XCode;

namespace DH.Entity
{
    /// <summary>实体基类</summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DHEntityBase<TEntity> : Entity<TEntity>, BaseDHModel where TEntity : DHEntityBase<TEntity>, new()
    {
        /// <summary>
        /// 当前语言
        /// </summary>
        public static Language CurrentLanguage
        {
            get
            {
                var _workContext = EngineContext.Current.Resolve<IWorkContext>();
                return _workContext.GetWorkingLanguage();
            }
        }

        public void CheckStringUpdate(String Field, String Value)
        {
            var fi = Meta.Fields.FirstOrDefault(e => e.Name.Contains(Field, StringComparison.OrdinalIgnoreCase));

            if (fi != null)
            {
                if (!Value.IsNullOrWhiteSpace() && this[Field].SafeString() != Value)
                {
                    this.SetItem(Field, Value);
                }
            }
        }

        /// <summary>
        /// 查询满足条件的实体
        /// </summary>
        /// <param name="where">条件，不带Where</param>
        /// <param name="selects">查询列，null表示所有字段</param>
        /// <returns></returns>
        public static TEntity FindFirstOrDefault(Expression where, string selects)
        {
            return FindAll(where, null, selects).FirstOrDefault();
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        protected override int OnDelete()
        {
            var fi = Meta.Fields.FirstOrDefault(e => e.Name == "IsDeleted");

            if (fi != null)
            {
                SetItem(fi.Name, true);
                return base.OnUpdate();
            }

            return base.OnDelete();
        }

        /// <summary>
        /// 重写新增
        /// </summary>
        /// <returns></returns>
        protected override int OnInsert()
        {
            var fi = Meta.Fields.FirstOrDefault(e => e.Name == "Id");

            if (fi != null && fi.Type == typeof(string))
            {
                var value = this.GetValue(fi.Name).SafeString();
                if (value.IsNullOrWhiteSpace())
                {
                    SetItem(fi.Name, Guid.NewGuid().ToString());
                }
            }

            return base.OnInsert();
        }
    }
}
