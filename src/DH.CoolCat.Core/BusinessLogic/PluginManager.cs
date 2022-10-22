using DH.CoolCat.Core.Contracts;
using DH.CoolCat.Core.DomainModel;
using DH.CoolCat.Core.Entity;

namespace DH.CoolCat.Core.BusinessLogic
{
    public class PluginManager : IPluginManager
    {
        private readonly IMvcModuleSetup _mvcModuleSetup = null;

        public PluginManager(IMvcModuleSetup mvcModuleSetup)
        {
            _mvcModuleSetup = mvcModuleSetup;
        }

        public void AddPlugins(PluginPackage pluginPackage)
        {
            var existedPlugin = Plugins.FindByName(pluginPackage.Configuration.Name);

            if (existedPlugin == null)
            {
                InitializePlugin(pluginPackage);
            }


        }

        public void DeletePlugin(String pluginId)
        {
        }

        public void DisablePlugin(String pluginId)
        {
        }

        public void EnablePlugin(String pluginId)
        {
        }

        public IList<CollectibleAssemblyLoadContext> GetAllContexts()
        {
            return PluginsLoadContexts.All();
        }

        public IList<Plugins> GetAllPlugins()
        {
            return Plugins.GetAll();
        }

        public Plugins GetPlugin(String pluginId)
        {
            return Plugins.FindByPluginId(pluginId);
        }

        private void InitializePlugin(PluginPackage pluginPackage)
        {
            //var model = new Plugins
            //{
            //    PluginId = Guid.NewGuid().ToString(),
            //    Name = pluginPackage.Configuration.Name,
            //    DisplayName = pluginPackage.Configuration.DisplayName,
            //    UniqueKey = pluginPackage.Configuration.UniqueKey,
            //    Version = pluginPackage.Configuration.Version,
            //    Enable = 0
            //};
            //model.Insert();

            //var versions = pluginPackage.GetAllMigrations();

            //foreach (IMigration version in versions)
            //{
            //    version.MigrateUp(model.PluginId);
            //}

            //pluginPackage.SetupFolder();
        }
    }
}
