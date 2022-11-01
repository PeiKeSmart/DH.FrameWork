using Microsoft.Extensions.FileProviders;

namespace DH.VirtualFileSystem
{
    public interface IVirtualFileSet
    {
        void AddFiles(Dictionary<string, IFileInfo> files);
    }
}
