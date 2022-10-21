using DG.CoolCat.Core.DomainModel;
using DG.CoolCat.Core.Entity;

namespace DG.CoolCat.Core.Contracts
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
