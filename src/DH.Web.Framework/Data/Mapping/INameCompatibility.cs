namespace DH.Data.Mapping
{
    /// <summary>
    /// 表命名的向后兼容性
    /// </summary>
    public partial interface INameCompatibility
    {
        /// <summary>
        /// 获取用于与类型映射的表名
        /// </summary>
        Dictionary<Type, string> TableNames { get; }

        /// <summary>
        ///  获取与实体的属性和类型映射的列名
        /// </summary>
        Dictionary<(Type, string), string> ColumnName { get; }
    }
}
