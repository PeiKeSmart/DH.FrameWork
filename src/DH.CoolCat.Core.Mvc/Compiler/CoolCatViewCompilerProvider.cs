﻿using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.Logging;

using NewLife.Log;

namespace DH.CoolCat.Core.Mvc.Infrastructure
{
    public class CoolCatViewCompilerProvider : IViewCompilerProvider
    {
        private CoolCatViewCompiler _compiler;
        private ApplicationPartManager _applicationPartManager;
        private ILoggerFactory _loggerFactory;

        public CoolCatViewCompilerProvider(
            ApplicationPartManager applicationPartManager,
            ILoggerFactory loggerFactory)
        {
            _applicationPartManager = applicationPartManager;
            _loggerFactory = loggerFactory;
            Refresh();
        }

        public void Refresh()
        {
            XTrace.WriteLine($"进入到这里了。Refresh");
            var feature = new ViewsFeature();
            _applicationPartManager.PopulateFeature(feature);

            _compiler = new CoolCatViewCompiler(feature.ViewDescriptors, _loggerFactory.CreateLogger<CoolCatViewCompiler>());
        }

        public IViewCompiler GetCompiler() => _compiler;
    }
}
