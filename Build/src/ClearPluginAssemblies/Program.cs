using NewLife;
using NewLife.Log;

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClearPluginAssemblies
{
    public class Program
    {
        protected const string FILES_TO_DELETE = "dotnet-bundle.exe;DH.Web.pdb;DH.Web.exe;DH.Web.exe.config";

        protected static void Clear(string paths, IList<string> fileNames, bool saveLocalesFolders)
        {
            XTrace.WriteLine($"获取到的数据为：{paths}");

            foreach (var pluginPath in paths.Split(';'))
            {
                try
                {
                    var pluginDirectoryInfo = new DirectoryInfo(pluginPath);
                    var allDirectoryInfo = new List<DirectoryInfo> { pluginDirectoryInfo };

                    if (!saveLocalesFolders)
                        allDirectoryInfo.AddRange(pluginDirectoryInfo.GetDirectories());

                    foreach (var directoryInfo in allDirectoryInfo)
                    {
                        foreach (var fileName in fileNames)
                        {
                            // 删除当前路径中存在的dll文件
                            var dllfilePath = Path.Combine(directoryInfo.FullName, fileName + ".dll");
                            if (File.Exists(dllfilePath))
                                File.Delete(dllfilePath);
                            // 删除当前路径中存在的pdb文件
                            var pdbfilePath = Path.Combine(directoryInfo.FullName, fileName + ".pdb");
                            if (File.Exists(pdbfilePath))
                                File.Delete(pdbfilePath);
                        }

                        foreach (var fileName in FILES_TO_DELETE.Split(';'))
                        {
                            // 删除当前路径中存在的文件
                            var pdbfilePath = Path.Combine(directoryInfo.FullName, fileName);
                            if (File.Exists(pdbfilePath))
                                File.Delete(pdbfilePath);
                        }

                        if (!directoryInfo.GetFiles().Any() && !directoryInfo.GetDirectories().Any() && !saveLocalesFolders)
                            directoryInfo.Delete(true);
                    }
                }
                catch
                {
                    //do nothing
                }
            }
        }

        private static void Main(string[] args)
        {
            XTrace.WriteLine($"开始执行：{args.Join()}");

            var outputPath = string.Empty;
            var pluginPaths = string.Empty;
            var saveLocalesFolders = true;

            var settings = args.FirstOrDefault(a => a.Contains("|")) ?? string.Empty;
            if(string.IsNullOrEmpty(settings))
                return;

            foreach (var arg in settings.Split('|'))
            {
                var data = arg.Split("=").Select(p => p.Trim()).ToList();

                var name = data[0];
                var value = data.Count > 1 ? data[1] : string.Empty;

                switch (name)
                {
                    case "OutputPath":
                        outputPath = value;
                        break;
                    case "PluginPath":
                        pluginPaths = value;
                        break;
                    case "SaveLocalesFolders":
                        bool.TryParse(value, out saveLocalesFolders);
                        break;
                }
            }
            
            if(!Directory.Exists(outputPath))
                return;

            var di = new DirectoryInfo(outputPath);
            var fileNames = di.GetFiles("*.dll", SearchOption.AllDirectories)
                .Where(fi => !fi.FullName.Contains(@"\Plugins\"))
                .Select(fi => fi.Name.Replace(fi.Extension, "")).ToList();
           
            if (string.IsNullOrEmpty(pluginPaths) || !fileNames.Any())
            {
                return;
            }

            Clear(pluginPaths, fileNames, saveLocalesFolders);
        }
    }
}
