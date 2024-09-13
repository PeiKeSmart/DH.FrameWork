using System.Runtime.CompilerServices;

using NewLife.Reflection;

using Pek.Infrastructure;

namespace DH.Core.Infrastructure
{
    /// <summary>
    /// 提供对DH引擎的单例实例的访问。
    /// </summary>
    public partial class EngineContext
    {
        #region 方法

        /// <summary>
        /// 创建DH引擎的静态实例。
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Create()
        {
            //创建DHEngine作为引擎
            //return Singleton<IEngine>.Instance ?? (Singleton<IEngine>.Instance = new DHEngine());

            var s = Singleton<IEngine>.Instance;
            if (s == null)
            {
                var cs = typeof(IEngine).GetAllSubclasses().ToArray();
                foreach (var item in cs)
                {
                    if (item.FullName?.Contains("DGEngine", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        return Singleton<IEngine>.Instance = (IEngine)item.Assembly.CreateInstance(item.FullName);
                    }
                }

                return Singleton<IEngine>.Instance = new DHEngine();
            }

            return s;
        }

        /// <summary>
        /// 将静态引擎实例设置为提供的引擎。使用此方法提供您自己的引擎实现。
        /// </summary>
        /// <param name="engine">要使用的发动机。</param>
        /// <remarks>只有当你知道自己在做什么时，才使用这种方法。</remarks>
        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取用于访问DH服务的单例Nop引擎。
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Create();
                }

                return Singleton<IEngine>.Instance;
            }
        }

        #endregion
    }
}
