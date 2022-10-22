using Microsoft.Extensions.FileProviders;

using System.Security.AccessControl;
using System.Text;

namespace DH.Core.Infrastructure
{
    /// <summary>
    /// 文件提供程序抽象
    /// </summary>
    public interface IDHFileProvider : IFileProvider
    {
        /// <summary>
        /// 将字符串数组组合到路径中
        /// </summary>
        /// <param name="paths">路径部分的数组</param>
        /// <returns>组合路径</returns>
        string Combine(params string[] paths);

        /// <summary>
        /// 在指定路径中创建所有目录和子目录，除非它们已经存在
        /// </summary>
        /// <param name="path">要创建的目录</param>
        void CreateDirectory(string path);

        /// <summary>
        /// 在指定路径中创建文件
        /// </summary>
        /// <param name="path">要创建的文件的路径和名称</param>
        void CreateFile(string path);

        /// <summary>
        /// 深度优先递归删除，处理在Windows资源管理器中打开的子目录。
        /// </summary>
        /// <param name="path">目录路径</param>
        void DeleteDirectory(string path);

        /// <summary>
        /// 删除指定的文件
        /// </summary>
        /// <param name="filePath">要删除的文件的名称。不支持通配符</param>
        void DeleteFile(string filePath);

        /// <summary>
        /// 确定给定路径是否引用磁盘上的现有目录
        /// </summary>
        /// <param name="path">要测试的路径</param>
        /// <returns>
        /// 如果路径指向现有目录，则为true；如果目录不存在或出现错误，则为false
        /// 试图确定指定的文件是否存在
        /// </returns>
        bool DirectoryExists(string path);

        /// <summary>
        /// 将文件或目录及其内容移动到新位置
        /// </summary>
        /// <param name="sourceDirName">要移动的文件或目录的路径</param>
        /// <param name="destDirName">
        /// sourceDirName的新位置的路径。如果sourceDirName是文件，则destDirName也必须是文件名
        /// </param>
        void DirectoryMove(string sourceDirName, string destDirName);

        /// <summary>
        /// 返回与指定路径中的搜索模式匹配的文件名的可枚举集合，并可以选择搜索子目录。
        /// </summary>
        /// <param name="directoryPath">要搜索的目录的路径</param>
        /// <param name="searchPattern">
        /// 要与路径中的文件名匹配的搜索字符串。此参数可以包含有效的文本路径和通配符（*和？）的组合字符，但不支持正则表达式。
        /// </param>
        /// <param name="topDirectoryOnly">
        /// 指定是搜索当前目录，还是搜索当前目录和所有子目录
        /// </param>
        /// <returns>
        /// 路径指定的目录中与指定搜索模式匹配的文件的全名（包括路径）的可枚举集合
        /// </returns>
        IEnumerable<string> EnumerateFiles(string directoryPath, string searchPattern, bool topDirectoryOnly = true);

        /// <summary>
        /// 将现有文件复制到新文件。允许覆盖同名文件
        /// </summary>
        /// <param name="sourceFileName">要复制的文件</param>
        /// <param name="destFileName">目标文件的名称。这不能是目录</param>
        /// <param name="overwrite">如果目标文件可以被覆盖，则为true；否则，false</param>
        void FileCopy(string sourceFileName, string destFileName, bool overwrite = false);

        /// <summary>
        /// 确定指定的文件是否存在
        /// </summary>
        /// <param name="filePath">要检查的文件</param>
        /// <returns>
        /// 如果调用方具有所需的权限并且路径包含现有文件的名称，则为True；否则为假。
        /// </returns>
        bool FileExists(string filePath);

        /// <summary>
        /// 获取文件的长度（以字节为单位），对于目录或不存在的文件，为-1
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件的长度</returns>
        long FileLength(string path);

        /// <summary>
        /// 将指定的文件移动到新位置，提供指定新文件名的选项
        /// </summary>
        /// <param name="sourceFileName">要移动的文件的名称。可以包括相对或绝对路径</param>
        /// <param name="destFileName">文件的新路径和名称</param>
        void FileMove(string sourceFileName, string destFileName);

        /// <summary>
        /// 返回目录的绝对路径
        /// </summary>
        /// <param name="paths">路径部分的数组</param>
        /// <returns>目录的绝对路径</returns>
        string GetAbsolutePath(params string[] paths);

        /// <summary>
        /// 获取System.Security.AccessControl。DirectorySecurity对象，用于封装指定目录的访问控制列表（ACL）条目
        /// </summary>
        /// <param name="path">包含System.Security.AccessControl的目录的路径。描述文件访问控制列表（ACL）信息的DirectorySecurity对象</param>
        /// <returns>封装路径参数所描述文件的访问控制规则的对象</returns>
        DirectorySecurity GetAccessControl(string path);

        /// <summary>
        /// 返回指定文件或目录的创建日期和时间
        /// </summary>
        /// <param name="path">要获取创建日期和时间信息的文件或目录</param>
        /// <returns>
        /// System.DateTime结构设置为指定文件或目录的创建日期和时间。此值以本地时间表示
        /// </returns>
        DateTime GetCreationTime(string path);

        /// <summary>
        /// 返回与指定目录中的指定搜索模式匹配的子目录的名称（包括其路径）
        /// </summary>
        /// <param name="path">要搜索的目录的路径</param>
        /// <param name="searchPattern">
        /// 要与路径中的子目录名称匹配的搜索字符串。这参数可以包含有效的文字和通配符的组合，但不支持正则表达式。
        /// </param>
        /// <param name="topDirectoryOnly">
        /// 指定是搜索当前目录，还是搜索当前目录和所有子目录
        /// </param>
        /// <returns>
        /// 匹配的子目录的全名（包括路径）数组指定的条件，如果未找到目录，则为空数组
        /// </returns>
        string[] GetDirectories(string path, string searchPattern = "", bool topDirectoryOnly = true);

        /// <summary>
        /// 返回指定路径字符串的目录信息
        /// </summary>
        /// <param name="path">文件或目录的路径</param>
        /// <returns>
        /// 路径的目录信息，如果路径表示根目录或为空，则为空。返回System.String。如果路径不包含目录信息，则为空
        /// </returns>
        string GetDirectoryName(string path);

        /// <summary>
        /// 仅返回指定路径字符串的目录名
        /// </summary>
        /// <param name="path">目录的路径</param>
        /// <returns>目录名</returns>
        string GetDirectoryNameOnly(string path);

        /// <summary>
        /// 返回指定路径字符串的扩展名
        /// </summary>
        /// <param name="filePath">从中获取扩展名的路径字符串</param>
        /// <returns>指定路径的扩展名（包括句点“.”）</returns>
        string GetFileExtension(string filePath);

        /// <summary>
        /// 返回指定路径字符串的文件名和扩展名
        /// </summary>
        /// <param name="path">从中获取文件名和扩展名的路径字符串</param>
        /// <returns>路径中最后一个目录字符后的字符</returns>
        string GetFileName(string path);

        /// <summary>
        /// 返回不带扩展名的指定路径字符串的文件名
        /// </summary>
        /// <param name="filePath">文件的路径</param>
        /// <returns>文件名，减去最后一个句点（.）和其后的所有字符</returns>
        string GetFileNameWithoutExtension(string filePath);

        /// <summary>
        /// 返回与指定目录中的指定搜索模式匹配的文件名（包括其路径），使用值确定是否搜索子目录。
        /// </summary>
        /// <param name="directoryPath">要搜索的目录的路径</param>
        /// <param name="searchPattern">
        /// 要与路径中的文件名匹配的搜索字符串。此参数可以包含有效文本路径和通配符（*和？）的组合字符，但不支持正则表达式。
        /// </param>
        /// <param name="topDirectoryOnly">
        /// 指定是搜索当前目录，还是搜索当前目录和所有子目录
        /// </param>
        /// <returns>
        /// 指定目录中与指定搜索模式匹配的文件的全名（包括路径）数组，如果未找到文件，则为空数组。
        /// </returns>
        string[] GetFiles(string directoryPath, string searchPattern = "", bool topDirectoryOnly = true);

        /// <summary>
        /// 返回上次访问指定文件或目录的日期和时间
        /// </summary>
        /// <param name="path">获取访问日期和时间信息的文件或目录</param>
        /// <returns>System.DateTime结构设置为指定文件的日期和时间</returns>
        DateTime GetLastAccessTime(string path);

        /// <summary>
        /// 返回上次写入指定文件或目录的日期和时间
        /// </summary>
        /// <param name="path">获取写入日期和时间信息的文件或目录</param>
        /// <returns>
        /// System.DateTime结构设置为上次写入指定文件或目录的日期和时间。
        /// 此值以本地时间表示
        /// </returns>
        DateTime GetLastWriteTime(string path);

        /// <summary>
        /// 返回指定文件或目录上次写入的日期和时间，单位为协调世界时（UTC）
        /// </summary>
        /// <param name="path">获取写入日期和时间信息的文件或目录</param>
        /// <returns>
        /// System.DateTime结构设置为上次写入指定文件或目录的日期和时间。
        /// 此值以UTC时间表示
        /// </returns>
        DateTime GetLastWriteTimeUtc(string path);

        /// <summary>
        /// 在指定的路径上创建或打开文件以进行读/写访问
        /// </summary>
        /// <param name="path">要创建的文件的路径和名称</param>
        /// <returns><see cref="FileStream"/> 提供对 <paramref name="path"/>中指定的文件的读/写访问</returns>
        FileStream GetOrCreateFile(string path);

        /// <summary>
        /// 检索指定路径的父目录
        /// </summary>
        /// <param name="directoryPath">检索父目录的路径</param>
        /// <returns>父目录，如果路径是根目录，则为null，包括UNC服务器或共享名的根目录</returns>
        string GetParentDirectory(string directoryPath);

        /// <summary>
        /// 从物理磁盘路径获取虚拟路径。
        /// </summary>
        /// <param name="path">物理磁盘路径</param>
        /// <returns>虚拟路径。例如："~/bin"</returns>
        string GetVirtualPath(string path);

        /// <summary>
        /// 检查路径是否为目录
        /// </summary>
        /// <param name="path">检查路径</param>
        /// <returns>如果路径是目录，则为True，否则为false</returns>
        bool IsDirectory(string path);

        /// <summary>
        /// 将虚拟路径映射到物理磁盘路径。
        /// </summary>
        /// <param name="path">要映射的路径。 例如： "~/bin"</param>
        /// <returns>物理路径。例如："c:\inetpub\wwwroot\bin"</returns>
        string MapPath(string path);

        /// <summary>
        /// 将文件内容读入字节数组
        /// </summary>
        /// <param name="filePath">用于读取的文件</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含包含文件内容的字节数组
        /// </returns>
        Task<byte[]> ReadAllBytesAsync(string filePath);

        /// <summary>
        /// 打开文件，用指定的编码读取文件的所有行，然后关闭文件。
        /// </summary>
        /// <param name="path">要打开以进行读取的文件</param>
        /// <param name="encoding">应用于文件内容的编码</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含包含文件所有行的字符串
        /// </returns>
        Task<string> ReadAllTextAsync(string path, Encoding encoding);

        /// <summary>
        /// 打开文件，用指定的编码读取文件的所有行，然后关闭文件。
        /// </summary>
        /// <param name="path">要打开以进行读取的文件</param>
        /// <param name="encoding">应用于文件内容的编码</param>
        /// <returns>包含文件所有行的字符串</returns>
        string ReadAllText(string path, Encoding encoding);

        /// <summary>
        /// 将指定的字节数组写入文件
        /// </summary>
        /// <param name="filePath">要写入的文件</param>
        /// <param name="bytes">要写入文件的字节</param>
        /// <returns>表示异步操作的任务</returns>
        Task WriteAllBytesAsync(string filePath, byte[] bytes);

        /// <summary>
        /// 创建新文件，使用指定的编码将指定的字符串写入文件，然后关闭文件。如果目标文件已经存在，则会覆盖它。
        /// </summary>
        /// <param name="path">要写入的文件</param>
        /// <param name="contents">要写入文件的字符串</param>
        /// <param name="encoding">要应用于字符串的编码</param>
        /// <returns>表示异步操作的任务</returns>
        Task WriteAllTextAsync(string path, string contents, Encoding encoding);

        /// <summary>
        /// 创建新文件，使用指定的编码将指定的字符串写入文件，然后关闭文件。如果目标文件已经存在，则会覆盖它。
        /// </summary>
        /// <param name="path">要写入的文件</param>
        /// <param name="contents">要写入文件的字符串</param>
        /// <param name="encoding">要应用于字符串的编码</param>
        void WriteAllText(string path, string contents, Encoding encoding);
    }
}
