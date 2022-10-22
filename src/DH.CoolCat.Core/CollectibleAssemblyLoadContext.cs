using System.Reflection;
using System.Runtime.Loader;

namespace DH.CoolCat.Core
{
    public class CollectibleAssemblyLoadContext : AssemblyLoadContext
    {
        private Assembly _entryPoint = null;
        private bool _isEnabled = false;
        private readonly string _pluginName = string.Empty;
        private Dictionary<string, byte[]> _resourceItems = null;

        public CollectibleAssemblyLoadContext(string pluginName) : base(isCollectible: true)
        {
            _pluginName = pluginName;
            _resourceItems = new Dictionary<string, byte[]>();
        }

        public string PluginName => _pluginName;

        public bool IsEnabled => _isEnabled;

        public void Enable()
        {
            _isEnabled = true;
        }


        public void Disable()
        {
            _isEnabled = false;
        }

    }
}
