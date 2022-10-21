using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace DG.CoolCat.Core.Mvc.DGCompiler;

public sealed class DHRuntimeCompilationFileProvider
{
    private readonly MvcRazorRuntimeCompilationOptions _options;
    private IFileProvider? _compositeFileProvider;

    public DHRuntimeCompilationFileProvider(IOptions<MvcRazorRuntimeCompilationOptions> options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        _options = options.Value;
    }

    public IFileProvider FileProvider
    {
        get
        {
            if (_compositeFileProvider == null)
            {
                _compositeFileProvider = GetCompositeFileProvider(_options);
            }

            return _compositeFileProvider;
        }
    }

    private static IFileProvider GetCompositeFileProvider(MvcRazorRuntimeCompilationOptions options)
    {
        var fileProviders = options.FileProviders;
        if (fileProviders.Count == 0)
        {
            //var message = Resources.FormatFileProvidersAreRequired(
            //    typeof(MvcRazorRuntimeCompilationOptions).FullName,
            //   nameof(MvcRazorRuntimeCompilationOptions.FileProviders),
            //    typeof(IFileProvider).FullName);

            var mes = $"'{typeof(MvcRazorRuntimeCompilationOptions).FullName}.{nameof(MvcRazorRuntimeCompilationOptions.FileProviders)}' must not be empty. At least one '{typeof(IFileProvider).FullName}' is required to locate a view for rendering.";
            throw new InvalidOperationException(mes);
        }
        else if (fileProviders.Count == 1)
        {
            return fileProviders[0];
        }

        return new CompositeFileProvider(fileProviders);
    }
}
