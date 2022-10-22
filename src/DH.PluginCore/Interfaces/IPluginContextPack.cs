namespace PluginCore.Interfaces
{
    public interface IPluginContextPack
    {
        /// <summary>
        /// 将 此插件 打包 到一个 <see cref="IPluginContext"/> 中
        /// </summary>
        /// <param name="pluginId"></param>
        /// <returns></returns>
        IPluginContext Pack(string pluginId);
    }
}
