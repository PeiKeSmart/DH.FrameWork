using DH;
using DH.VirtualFileSystem;
using DH.VirtualFileSystem.Embedded;

using JetBrains.Annotations;

using System.Text;

namespace Microsoft.Extensions.FileProviders
{
    public static class DGFileInfoExtensions
    {
        /// <summary>
        /// 使用 <see cref="Encoding.UTF8"/> 编码以字符串形式读取文件内容。
        /// </summary>
        public static string ReadAsString([NotNull] this IFileInfo fileInfo)
        {
            return fileInfo.ReadAsString(Encoding.UTF8);
        }

        /// <summary>
        /// 使用给定的 <paramref name="encoding"/> 以字符串形式读取文件内容。
        /// </summary>
        public static string ReadAsString([NotNull] this IFileInfo fileInfo, Encoding encoding)
        {
            Check.NotNull(fileInfo, nameof(fileInfo));

            using (var stream = fileInfo.CreateReadStream())
            {
                using (var streamReader = new StreamReader(stream, encoding, true))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 读取文件内容为字节数组.
        /// </summary>
        public static byte[] ReadBytes([NotNull] this IFileInfo fileInfo)
        {
            Check.NotNull(fileInfo, nameof(fileInfo));

            using (var stream = fileInfo.CreateReadStream())
            {
                return stream.GetAllBytes();
            }
        }

        /// <summary>
        /// 读取文件内容为字节数组.异步
        /// </summary>
        public static async Task<byte[]> ReadBytesAsync([NotNull] this IFileInfo fileInfo)
        {
            Check.NotNull(fileInfo, nameof(fileInfo));

            using (var stream = fileInfo.CreateReadStream())
            {
                return await stream.GetAllBytesAsync().ConfigureAwait(false);
            }
        }

        public static string GetVirtualOrPhysicalPathOrNull([NotNull] this IFileInfo fileInfo)
        {
            Check.NotNull(fileInfo, nameof(fileInfo));

            if (fileInfo is EmbeddedResourceFileInfo embeddedFileInfo)
            {
                return embeddedFileInfo.VirtualPath;
            }

            if (fileInfo is InMemoryFileInfo inMemoryFileInfo)
            {
                return inMemoryFileInfo.DynamicPath;
            }

            return fileInfo.PhysicalPath;
        }
    }
}
