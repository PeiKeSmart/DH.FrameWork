using DG.CoolCat.Core.BusinessLogic;
using DG.CoolCat.Core.Contracts;
using DG.CoolCat.Core.Entity;
using DG.CoolCat.Core.Helpers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Runtime.Loader;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using DG.CoolCat.Core.Mvc.DGCompiler;

namespace DG.CoolCat.Core.Mvc.Infrastructure
{
    public static class CoolCatStartup
    {
        private static IServiceCollection _serviceCollection;

        public static IServiceCollection Services => _serviceCollection;

        public static void CoolCatSetup(this IServiceCollection services, IConfiguration configuration)
        {
            _serviceCollection = services;

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IMvcModuleSetup, MvcModuleSetup>();
            services.AddScoped<IPluginManager, PluginManager>();

            services.AddSingleton<IActionDescriptorChangeProvider>(CoolCatActionDescriptorChangeProvider.Instance);
            services.AddSingleton<IReferenceContainer, DefaultReferenceContainer>();
            services.AddSingleton<IReferenceLoader, DefaultReferenceLoader>();

            var documentation = new CoolCatModuleDocumentation();

            services.AddSingleton<IQueryDocumentation>(documentation);
            services.AddSingleton(CoolCatActionDescriptorChangeProvider.Instance);

            IMvcBuilder mvcBuilder = services.AddMvc()
                .AddRazorRuntimeCompilation();
            services.Replace<IViewCompilerProvider, DHRuntimeViewCompilerProvider>();

            ServiceProvider provider = services.BuildServiceProvider();
            using (IServiceScope scope = provider.CreateScope())
            {
                var contextProvider = new CollectibleAssemblyLoadContextProvider();

                var allEnabledPlugins = Plugins.GetAllEnabled();
                IReferenceLoader loader = scope.ServiceProvider.GetService<IReferenceLoader>();

                foreach (var plugin in allEnabledPlugins)
                {
                    var context = contextProvider.Get(plugin.Name, mvcBuilder, scope, documentation);
                    PluginsLoadContexts.Add(plugin.Name, context);
                }
            }

            AssemblyLoadContextResoving();

            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.AreaViewLocationFormats.Add("/Modules/{2}/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            //services.Replace<IViewCompilerProvider, CoolCatViewCompilerProvider>();
        }


        private static void AssemblyLoadContextResoving()
        {
            AssemblyLoadContext.Default.Resolving += (context, assembly) =>
            {
                Func<CollectibleAssemblyLoadContext, bool> filter = p => p.Assemblies.Any(p => p.GetName().Name == assembly.Name
                                                        && p.GetName().Version == assembly.Version);

                if (PluginsLoadContexts.All().Any(filter))
                {
                    Assembly ass = PluginsLoadContexts.All().First(filter)
                        .Assemblies.First(p => p.GetName().Name == assembly.Name
                        && p.GetName().Version == assembly.Version);
                    return ass;
                }

                return null;
            };
        }

    }
}
