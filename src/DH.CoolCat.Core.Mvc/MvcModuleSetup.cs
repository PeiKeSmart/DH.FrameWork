using DH.CoolCat.Core.Contracts;
using DH.CoolCat.Core.Mvc.Infrastructure;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace DH.CoolCat.Core.Mvc
{
    public class MvcModuleSetup : IMvcModuleSetup
    {
        private readonly ApplicationPartManager _partManager;
        private readonly IReferenceLoader _referenceLoader = null;
        private readonly IHttpContextAccessor _context;

        public MvcModuleSetup(ApplicationPartManager partManager, IReferenceLoader referenceLoader, IHttpContextAccessor httpContextAccessor)
        {
            _partManager = partManager;
            _referenceLoader = referenceLoader;
            _context = httpContextAccessor;
        }

        public void DeleteModule(string moduleName)
        {
            
        }

        public void DisableModule(string moduleName)
        {
            
        }

        public void EnableModule(string moduleName)
        {
            ServiceProvider provider = CoolCatStartup.Services.BuildServiceProvider();
            var contextProvider = new CollectibleAssemblyLoadContextProvider();


        }
    }
}
