using DH.CoolCat.Core.Contracts;
using DH.CoolCat.Core.Exceptions;

using Newtonsoft.Json;

using System.IO.Compression;

using ZipTool = System.IO.Compression.ZipArchive;

namespace DH.CoolCat.Core.DomainModel
{
    public class PluginPackage
    {
        private PluginConfiguration _pluginConfiguration = null;
        private Stream _zipStream = null;
        private string _tempFolderName = string.Empty;
        private string _folderName = string.Empty;

        public PluginConfiguration Configuration => _pluginConfiguration;

        public PluginPackage(Stream stream)
        {
            _zipStream = stream;
            Initialize(stream);
        }

        public void Initialize(Stream stream)
        {
            _zipStream = stream;
            _tempFolderName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Guid.NewGuid()}");
            ZipTool archive = new ZipTool(_zipStream, ZipArchiveMode.Read);

            archive.ExtractToDirectory(_tempFolderName);

            DirectoryInfo folder = new DirectoryInfo(_tempFolderName);

            FileInfo[] files = folder.GetFiles();

            FileInfo configFile = files.SingleOrDefault(p => p.Name == "plugin.json");

            if (configFile == null)
            {
                throw new MissingConfigurationFileException();
            }
            else
            {
                using (FileStream s = configFile.OpenRead())
                {
                    LoadConfiguration(s);
                }
            }
        }

        public void SetupFolder()
        {
            ZipTool archive = new ZipTool(_zipStream, ZipArchiveMode.Read);
            _zipStream.Position = 0;
            _folderName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules", $"{_pluginConfiguration.Name}");

            archive.ExtractToDirectory(_folderName, true);

            DirectoryInfo folder = new DirectoryInfo(_tempFolderName);
            folder.Delete(true);
        }

        private void LoadConfiguration(Stream stream)
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                string content = sr.ReadToEnd();
                _pluginConfiguration = JsonConvert.DeserializeObject<PluginConfiguration>(content);

                if (_pluginConfiguration == null)
                {
                    throw new WrongFormatConfigurationException();
                }
            }
        }

    }
}
