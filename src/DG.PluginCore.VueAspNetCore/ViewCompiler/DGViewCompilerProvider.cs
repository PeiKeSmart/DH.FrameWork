using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

namespace PluginCore.AspNetCore.ViewCompiler
{
    public class DGViewCompilerProvider : IViewCompilerProvider
    {
        private DGViewCompiler _compiler;
        private ApplicationPartManager _applicationPartManager;
        private ILoggerFactory _loggerFactory;

        public DGViewCompilerProvider(
            ApplicationPartManager applicationPartManager,
            ILoggerFactory loggerFactory)
        {
            _applicationPartManager = applicationPartManager;
            _loggerFactory = loggerFactory;
            Refresh();
        }

        public void Refresh()
        {
            var feature = new ViewsFeature();
            _applicationPartManager.PopulateFeature(feature);

            _compiler = new DGViewCompiler(feature.ViewDescriptors, _loggerFactory.CreateLogger<DGViewCompiler>());
        }

        public IViewCompiler GetCompiler() => _compiler;
    }
}
