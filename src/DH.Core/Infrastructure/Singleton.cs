namespace DH.Core.Infrastructure
{
    /// <summary>
    /// 一个静态编译的"singleton"，用于在整个应用程序域的生存期。模式中没有太多的单体这个词的意义是作为存储单个实例的标准化方法。
    /// </summary>
    /// <typeparam name="T">要存储的对象类型。</typeparam>
    /// <remarks>对实例的访问未同步。</remarks>
    public partial class Singleton<T> : BaseSingleton
    {
        private static T instance;

        /// <summary>
        /// 指定类型T的单实例。对于每种类型T，只能有一个此对象的实例。
        /// </summary>
        public static T Instance
        {
            get => instance;
            set
            {
                instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }
}
