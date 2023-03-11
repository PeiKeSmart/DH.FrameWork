﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using WebOptimizer;

namespace WebOptimizer
{
    internal class RelativePathAdjuster : Processor
    {
        private static readonly Regex _rxUrl = new Regex(@"url\s*\(\s*([""']?)([^:)]+)\1\s*\)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly string _protocol = "file:///";

        public override Task ExecuteAsync(IAssetContext config)
        {
            var content = new Dictionary<string, byte[]>();
            var env = (IWebHostEnvironment)config.HttpContext.RequestServices.GetService(typeof(IWebHostEnvironment));
            var pipeline = (IAssetPipeline)config.HttpContext.RequestServices.GetService(typeof(IAssetPipeline));
            IFileProvider fileProvider = config.Asset.GetFileProvider(env);

            foreach (string key in config.Content.Keys)
            {
                string inputPath = Path.Combine(env.WebRootPath, key.TrimStart('/'));
                string outputPath = Path.Combine(env.WebRootPath, config.Asset.Route.TrimStart('/'));
                content[key] = Adjust(config.Content[key].AsString(), inputPath, outputPath);
            }

            config.Content = content;

            return Task.CompletedTask;
        }

        private static byte[] Adjust(string cssFileContents, string inputFile, string outputPath)
        {
            // apply the RegEx to the file (to change relative paths)
            MatchCollection matches = _rxUrl.Matches(cssFileContents);

            // Ignore the file if no match
            if (matches.Count > 0)
            {
                string cssDirectoryPath = Path.GetDirectoryName(inputFile);

                foreach (Match match in matches)
                {
                    string quoteDelimiter = match.Groups[1].Value; //url('') vs url("")
                    string urlValue = match.Groups[2].Value;

                    // Ignore root relative references
                    if (urlValue.StartsWith("/", StringComparison.Ordinal) || urlValue.StartsWith("data:"))
                        continue;

                    //prevent query string from causing error
                    string[] pathAndQuery = urlValue.Split(new[] { '?' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    string pathOnly = pathAndQuery[0];
                    string queryOnly = pathAndQuery.Length == 2 ? pathAndQuery[1] : string.Empty;

                    string absolutePath = GetAbsolutePath(cssDirectoryPath, pathOnly);
                    string serverRelativeUrl = MakeRelative(outputPath, absolutePath);

                    if (!string.IsNullOrEmpty(queryOnly))
                    {
                        serverRelativeUrl += "?" + queryOnly;
                    }

                    string replace = string.Format("url({0}{1}{0})", quoteDelimiter, serverRelativeUrl);

                    cssFileContents = cssFileContents.Replace(match.Groups[0].Value, replace);
                }
            }

            return cssFileContents.AsByteArray();
        }

        private static string GetAbsolutePath(string cssFilePath, string pathOnly)
        {
            return Path.GetFullPath(Path.Combine(cssFilePath, pathOnly));
        }

        private static string MakeRelative(string baseFile, string file)
        {
            if (string.IsNullOrEmpty(file))
                return file;

            // The file:// protocol is to make it work on Linux.
            // See https://github.com/madskristensen/BundlerMinifier/commit/01fe7a050eda073f8949caa90eedc4c23e04d0ce
            var baseUri = new Uri(_protocol + baseFile, UriKind.RelativeOrAbsolute);
            var fileUri = new Uri(_protocol + file, UriKind.RelativeOrAbsolute);

            if (baseUri.IsAbsoluteUri)
            {
                return Uri.UnescapeDataString(baseUri.MakeRelativeUri(fileUri).ToString());
            }
            else
            {
                return baseUri.ToString();
            }
        }
    }
}

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for <see cref="IAssetPipeline"/>.
    /// </summary>
    public static partial class AssetPipelineExtensions
    {
        /// <summary>
        /// Adjusts the relative paths in CSS documents.
        /// </summary>
        public static IAsset AdjustRelativePaths(this IAsset bundle)
        {
            var minifier = new RelativePathAdjuster();
            bundle.Processors.Add(minifier);

            return bundle;
        }

        /// <summary>
        /// Adjusts the relative paths in CSS documents.
        /// </summary>
        public static IEnumerable<IAsset> AdjustRelativePaths(this IEnumerable<IAsset> assets)
        {
            return assets.AddProcessor(asset => asset.AdjustRelativePaths());
        }
    }
}
