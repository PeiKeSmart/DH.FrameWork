﻿using System.Reflection;

namespace PluginCore.Interfaces
{
    /// <summary>
    /// 每个插件的所有 Assembly 打包到此中
    /// </summary>
    public interface IPluginContext
    {
        IEnumerable<Assembly> Assemblies { get; }

        Assembly LoadFromAssemblyName(AssemblyName assemblyName);

        void Unload();


        /// <summary>
        /// 暂时用不到
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        Assembly LoadFromStream(Stream assembly);
    }
}
