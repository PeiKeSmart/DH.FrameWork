using DH.Core.ComponentModel;
using DH.Core.Infrastructure;

namespace DH.Data.Mapping
{
    /// <summary>
    /// 用于维护表命名的向后兼容性的Helper类
    /// </summary>
    public static partial class NameCompatibilityManager
    {
        #region 字段

        private static readonly Dictionary<Type, string> _tableNames = new();
        private static readonly Dictionary<(Type, string), string> _columnName = new();
        private static readonly IList<Type> _loadedFor = new List<Type>();
        private static bool _isInitialized;
        private static readonly ReaderWriterLockSlim _locker = new();

        #endregion

        #region 实用程序

        private static void Initialize()
        {
            // 使用对资源的锁定访问执行
            using (new ReaderWriteLockDisposable(_locker))
            {
                if (_isInitialized)
                    return;

                var typeFinder = Singleton<ITypeFinder>.Instance;
                var compatibilities = typeFinder.FindClassesOfType<INameCompatibility>()
                    ?.Select(type => EngineContext.Current.ResolveUnregistered(type) as INameCompatibility).ToList() ?? new List<INameCompatibility>();

                compatibilities.AddRange(AdditionalNameCompatibilities.Select(type => EngineContext.Current.ResolveUnregistered(type) as INameCompatibility));

                foreach (var nameCompatibility in compatibilities.Distinct())
                {
                    if (_loadedFor.Contains(nameCompatibility.GetType()))
                        continue;

                    _loadedFor.Add(nameCompatibility.GetType());

                    foreach (var (key, value) in nameCompatibility.TableNames.Where(tableName =>
                        !_tableNames.ContainsKey(tableName.Key)))
                        _tableNames.Add(key, value);

                    foreach (var (key, value) in nameCompatibility.ColumnName.Where(columnName =>
                        !_columnName.ContainsKey(columnName.Key)))
                        _columnName.Add(key, value);
                }

                _isInitialized = true;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取用于与类型映射的表名
        /// </summary>
        /// <param name="type">键入以获取表名</param>
        /// <returns>表名称</returns>
        public static string GetTableName(Type type)
        {
            if (!_isInitialized)
                Initialize();

            return _tableNames.ContainsKey(type) ? _tableNames[type] : type.Name;
        }

        /// <summary>
        /// 获取与实体属性映射的列名
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>列名称</returns>
        public static string GetColumnName(Type type, string propertyName)
        {
            if (!_isInitialized)
                Initialize();

            return _columnName.ContainsKey((type, propertyName)) ? _columnName[(type, propertyName)] : propertyName;
        }

        #endregion

        /// <summary>
        /// 其他名称兼容性类型
        /// </summary>
        public static List<Type> AdditionalNameCompatibilities { get; } = new List<Type>();
    }
}
