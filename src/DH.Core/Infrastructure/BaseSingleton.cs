namespace DH.Core.Infrastructure
{
    /// <summary>
    /// 提供对<see cref="Singleton{T}"/>存储的所有"Singleton"的访问。
    /// </summary>
    public partial class BaseSingleton
    {
        static BaseSingleton()
        {
            AllSingletons = new Dictionary<Type, object>();
        }

        /// <summary>
        /// 单例实例的类型字典。
        /// </summary>
        public static IDictionary<Type, object> AllSingletons { get; }
    }
}
