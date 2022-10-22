using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;

namespace DH.CoolCat.Core.Mvc.DGCompiler
{
    public sealed class DHRuntimeViewCompilerProvider : IViewCompilerProvider
    {
        private readonly RazorProjectEngine _razorProjectEngine;
        private readonly ApplicationPartManager _applicationPartManager;
        private readonly DHCSharpCompiler _csharpCompiler;
        private readonly DHRuntimeCompilationFileProvider _fileProvider;
        private readonly ILogger<DHRuntimeViewCompiler> _logger;
        private readonly Func<IViewCompiler> _createCompiler;

        private object _initializeLock = new object();
        private bool _initialized;
        private IViewCompiler? _compiler;

        public DHRuntimeViewCompilerProvider(
            ApplicationPartManager applicationPartManager,
            RazorProjectEngine razorProjectEngine,
            DHRuntimeCompilationFileProvider fileProvider,
            DHCSharpCompiler csharpCompiler,
            ILoggerFactory loggerFactory)
        {
            _applicationPartManager = applicationPartManager;
            _razorProjectEngine = razorProjectEngine;
            _csharpCompiler = csharpCompiler;
            _fileProvider = fileProvider;

            _logger = loggerFactory.CreateLogger<DHRuntimeViewCompiler>();
            _createCompiler = CreateCompiler;
        }

        public IViewCompiler GetCompiler()
        {
            return LazyInitializer.EnsureInitialized(
                ref _compiler,
                ref _initialized,
                ref _initializeLock,
                _createCompiler)!;
        }

        private IViewCompiler CreateCompiler()
        {
            var feature = new ViewsFeature();
            _applicationPartManager.PopulateFeature(feature);

            return new DHRuntimeViewCompiler(
                _fileProvider.FileProvider,
                _razorProjectEngine,
                _csharpCompiler,
                feature.ViewDescriptors,
                _logger);
        }
    }
}
