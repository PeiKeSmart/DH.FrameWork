﻿using PluginCore.Interfaces;

using System.Reflection;

namespace PluginCore.lmplements
{
    public class PluginContextPack : IPluginContextPack
    {
        public IPluginContext Pack(string pluginId)
        {
            #region 加载插件主dll

            // 插件的主dll, 不包括插件项目引用的dll
            string pluginMainDllFilePath = Path.Combine(PluginPathProvider.PluginsRootPath(), pluginId, $"{pluginId}.dll");
            // 此插件的 加载上下文
            var context = new PluginLoadContext(pluginMainDllFilePath);
            Assembly pluginMainAssembly;
            // 微软文档推荐 LoadFromAssemblyName
            pluginMainAssembly = context.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginMainDllFilePath)));

            #region 第2种方法: 未在这种情况下测试
            //using (var fs = new FileStream(pluginMainDllFilePath, FileMode.Open))
            //{
            //    // 使用此方法, 就不会导致dll被锁定
            //    pluginMainAssembly = context.LoadFromStream(fs);

            //    // 加载其中的控制器
            //    _pluginControllerManager.AddControllers(pluginMainAssembly);
            //} 
            #endregion

            #endregion


            return context;
        }
    }
}
