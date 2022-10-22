using DH.CoolCat.Core.Contracts;

using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace DH.CoolCat.Core.Mvc.Infrastructure
{
    public class CollectibleAssemblyLoadContextProvider
    {
        public CollectibleAssemblyLoadContext Get(string moduleName, IMvcBuilder mvcBuilder, IServiceScope scope, IQueryDocumentation documentation)
        {
            return Get(moduleName, mvcBuilder.PartManager, scope, documentation);
        }

        public CollectibleAssemblyLoadContext Get(string moduleName, ApplicationPartManager apm, IServiceScope scope, IQueryDocumentation documentation)
        {
            CollectibleAssemblyLoadContext context = new CollectibleAssemblyLoadContext(moduleName);
            IReferenceLoader loader = scope.ServiceProvider.GetService<IReferenceLoader>();


            context.Enable();

            return context;
        }

    }
}
