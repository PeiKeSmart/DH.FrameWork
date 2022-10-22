namespace PluginCore.Interfaces
{
    public interface IPluginManager
    {
        void LoadPlugin(string pluginId);

        void UnloadPlugin(string pluginId);
    }
}
