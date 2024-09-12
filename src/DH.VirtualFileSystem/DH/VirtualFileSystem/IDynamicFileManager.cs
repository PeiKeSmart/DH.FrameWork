using Microsoft.Extensions.FileProviders;

namespace DH.VirtualFileSystem
{
    public interface IDynamicFileProvider : IFileProvider
    {
        void AddOrUpdate(IFileInfo fileInfo);

        bool Delete(string filePath);
    }
}
