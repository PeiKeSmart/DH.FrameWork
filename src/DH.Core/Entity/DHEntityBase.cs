using System.Xml.Serialization;

using DH.Core;
using DH.Core.Infrastructure;

using NewLife;
using NewLife.Reflection;

using Pek;

using XCode;

namespace DH.Entity
{
    /// <summary>实体基类</summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DHEntityBase<TEntity> : Entity<TEntity>, BasePekModel where TEntity : DHEntityBase<TEntity>, new()
    {
        /// <summary>
        /// 实例化
        /// </summary>
        public DHEntityBase()
        {
            CustomProperties = [];
            PostInitialize();
        }

        /// <summary>
        /// 对模型初始化执行其他操作
        /// </summary>
        /// <remarks>开发人员可以在自定义分部类中重写此方法，以便向构造函数添加一些自定义初始化代码</remarks>
        protected virtual void PostInitialize()
        {
        }

        /// <summary>
        /// 获取或设置属性以存储模型的任何自定义值
        /// </summary>
        [XmlIgnore]
        public Dictionary<String, String> CustomProperties { get; set; }

        /// <summary>
        /// 当前语言
        /// </summary>
        public static Language CurrentLanguage
        {
            get
            {
                var _workContext = EngineContext.Current.Resolve<IWorkContext>();
                return _workContext.WorkingLanguage;
            }
        }

        public void CheckStringUpdate(String Field, String Value)
        {
            var fi = Meta.Fields.FirstOrDefault(e => e.Name.Contains(Field, StringComparison.OrdinalIgnoreCase));

            if (fi != null)
            {
                if (!Value.IsNullOrWhiteSpace() && this[Field].SafeString() != Value)
                {
                    SetItem(Field, Value);
                }
            }
        }

        /// <summary>
        /// 查询满足条件的实体
        /// </summary>
        /// <param name="where">条件，不带Where</param>
        /// <param name="selects">查询列，null表示所有字段</param>
        /// <returns></returns>
        public static TEntity FindFirstOrDefault(Expression where, String selects) => FindAll(where, null, selects).FirstOrDefault();

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        protected override Int32 OnDelete()
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
        protected override Int32 OnInsert()
        {
            var fi = Meta.Fields.FirstOrDefault(e => e.Name == "Id");

            if (fi != null && fi.Type == typeof(String))
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
