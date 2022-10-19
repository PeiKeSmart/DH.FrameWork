using System.Reflection;

namespace PluginCore.AspNetCore.Interfaces
{
    public interface IPluginControllerManager
    {
        void AddControllers(Assembly assembly);

        void RemoveControllers(string pluginId);
    }
}
