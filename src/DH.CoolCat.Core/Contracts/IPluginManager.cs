using DH.CoolCat.Core.DomainModel;
using DH.CoolCat.Core.Entity;

namespace DH.CoolCat.Core.Contracts
{
    public interface IPluginManager
    {
        IList<Plugins> GetAllPlugins();

        void AddPlugins(PluginPackage pluginPackage);

        Plugins GetPlugin(String pluginId);

        void DeletePlugin(String pluginId);

        void EnablePlugin(String pluginId);

        void DisablePlugin(String pluginId);

        IList<CollectibleAssemblyLoadContext> GetAllContexts();
    }
}
