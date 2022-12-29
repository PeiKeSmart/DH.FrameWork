using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

using System.Runtime.Versioning;
using System.Security.AccessControl;
using System.Text;

namespace DH.Core.Infrastructure
{
    /// <summary>
    /// 使用磁盘上文件系统的IO功能
    /// </summary>
    public partial class DHFileProvider : PhysicalFileProvider, IDHFileProvider
    {
        /// <summary>
        /// 初始化DHFileProvider的新实例
        /// </summary>
        /// <param name="webHostEnvironment">托管环境</param>
        public DHFileProvider(IWebHostEnvironment webHostEnvironment)
            : base(File.Exists(webHostEnvironment.ContentRootPath) ? Path.GetDirectoryName(webHostEnvironment.ContentRootPath) : webHostEnvironment.ContentRootPath)
        {
            WebRootPath = File.Exists(webHostEnvironment.WebRootPath)
                ? Path.GetDirectoryName(webHostEnvironment.WebRootPath)
                : webHostEnvironment.WebRootPath;
        }

        #region Utilities

        private static void DeleteDirectoryRecursive(string path)
        {
            Directory.Delete(path, true);
            const int maxIterationToWait = 10;
            var curIteration = 0;

            //根据文件(https://msdn.microsoft.com/ru-ru/library/windows/desktop/aa365488.aspx) 
            //System.IO.Directory.Delete 方法最终（删除文件后）调用本机
            //RemoveDirectory函数，将目录标记为“已删除”。这就是为什么我们要等到目录被删除。有关详细信息，请参阅 https://stackoverflow.com/a/4245121
            while (Directory.Exists(path))
            {
                curIteration += 1;
                if (curIteration > maxIterationToWait)
                    return;
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 确定该字符串是否是服务器和共享路径的有效通用命名约定（UNC）。
        /// </summary>
        /// <param name="path">要测试的路径。</param>
        /// <returns>如果路径是有效的UNC路径，请参见<see langword="true"/>；否则，<see langword="false"/>.</returns>
        protected static bool IsUncPath(string path)
        {
            return Uri.TryCreate(path, UriKind.Absolute, out var uri) && uri.IsUnc;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 将字符串数组组合到路径中
        /// </summary>
        /// <param name="paths">路径部分的数组</param>
        /// <returns>组合路径</returns>
        public virtual string Combine(params string[] paths)
        {
            var path = Path.Combine(paths.SelectMany(p => IsUncPath(p) ? new[] { p } : p.Split('\\', '/')).ToArray());

            if (Environment.OSVersion.Platform == PlatformID.Unix && !IsUncPath(path))
                //add leading slash to correctly form path in the UNIX system
                path = "/" + path;

            return path;
        }

        /// <summary>
        /// 在指定路径中创建所有目录和子目录，除非它们已经存在
        /// </summary>
        /// <param name="path">要创建的目录</param>
        public virtual void CreateDirectory(string path)
        {
            if (!DirectoryExists(path))
                Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 在指定路径中创建文件
        /// </summary>
        /// <param name="path">要创建的文件的路径和名称</param>
        public virtual void CreateFile(string path)
        {
            if (FileExists(path))
                return;

            var fileInfo = new FileInfo(path);
            CreateDirectory(fileInfo.DirectoryName);

            // 我们使用 'using' 在文件创建后关闭它
            using (File.Create(path))
            {
            }
        }

        /// <summary>
        /// 深度优先递归删除，处理在Windows资源管理器中打开的子目录。
        /// </summary>
        /// <param name="path">目录路径</param>
        public virtual void DeleteDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(path);

            // 查找有关目录删除的详细信息
            //以及为什么我们在 https://stackoverflow.com/questions/329355/cannot-delete-directory-with-directory-deletepath-true

            foreach (var directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }

            try
            {
                DeleteDirectoryRecursive(path);
            }
            catch (IOException)
            {
                DeleteDirectoryRecursive(path);
            }
            catch (UnauthorizedAccessException)
            {
                DeleteDirectoryRecursive(path);
            }
        }

        /// <summary>
        /// 删除指定的文件
        /// </summary>
        /// <param name="filePath">要删除的文件的名称。不支持通配符</param>
        public virtual void DeleteFile(string filePath)
        {
            if (!FileExists(filePath))
                return;

            File.Delete(filePath);
        }

        /// <summary>
        /// 确定给定路径是否引用磁盘上的现有目录
        /// </summary>
        /// <param name="path">要测试的路径</param>
        /// <returns>
        /// 如果路径指向现有目录，则为true；如果目录不存在或出现错误，则为false
        /// 试图确定指定的文件是否存在
        /// </returns>
        public virtual bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// 将文件或目录及其内容移动到新位置
        /// </summary>
        /// <param name="sourceDirName">要移动的文件或目录的路径</param>
        /// <param name="destDirName">
        /// sourceDirName的新位置的路径。如果sourceDirName是文件，则destDirName也必须是文件名
        /// </param>
        public virtual void DirectoryMove(string sourceDirName, string destDirName)
        {
            Directory.Move(sourceDirName, destDirName);
        }

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
        public virtual IEnumerable<string> EnumerateFiles(string directoryPath, string searchPattern,
            bool topDirectoryOnly = true)
        {
            return Directory.EnumerateFiles(directoryPath, searchPattern,
                topDirectoryOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
        }

        /// <summary>
        /// 将现有文件复制到新文件。允许覆盖同名文件
        /// </summary>
        /// <param name="sourceFileName">要复制的文件</param>
        /// <param name="destFileName">目标文件的名称。这不能是目录</param>
        /// <param name="overwrite">如果目标文件可以被覆盖，则为true；否则，false</param>
        public virtual void FileCopy(string sourceFileName, string destFileName, bool overwrite = false)
        {
            File.Copy(sourceFileName, destFileName, overwrite);
        }

        /// <summary>
        /// 确定指定的文件是否存在
        /// </summary>
        /// <param name="filePath">要检查的文件</param>
        /// <returns>
        /// 如果调用方具有所需的权限并且路径包含现有文件的名称，则为True；否则为假。
        /// </returns>
        public virtual bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 获取文件的长度（以字节为单位），对于目录或不存在的文件，为-1
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件的长度</returns>
        public virtual long FileLength(string path)
        {
            if (!FileExists(path))
                return -1;

            return new FileInfo(path).Length;
        }

        /// <summary>
        /// 将指定的文件移动到新位置，提供指定新文件名的选项
        /// </summary>
        /// <param name="sourceFileName">要移动的文件的名称。可以包括相对或绝对路径</param>
        /// <param name="destFileName">文件的新路径和名称</param>
        public virtual void FileMove(string sourceFileName, string destFileName)
        {
            File.Move(sourceFileName, destFileName);
        }

        /// <summary>
        /// 返回目录的绝对路径
        /// </summary>
        /// <param name="paths">路径部分的数组</param>
        /// <returns>目录的绝对路径</returns>
        public virtual string GetAbsolutePath(params string[] paths)
        {
            var allPaths = new List<string>();

            if (paths.Any() && !paths[0].Contains(WebRootPath, StringComparison.InvariantCulture))
                allPaths.Add(WebRootPath);

            allPaths.AddRange(paths);

            return Combine(allPaths.ToArray());
        }

        /// <summary>
        /// 获取System.Security.AccessControl。DirectorySecurity对象，用于封装指定目录的访问控制列表（ACL）条目
        /// </summary>
        /// <param name="path">包含System.Security.AccessControl的目录的路径。描述文件访问控制列表（ACL）信息的DirectorySecurity对象</param>
        /// <returns>封装路径参数所描述文件的访问控制规则的对象</returns>
        [SupportedOSPlatform("windows")]
        public virtual DirectorySecurity GetAccessControl(string path)
        {
            return new DirectoryInfo(path).GetAccessControl();
        }

        /// <summary>
        /// 返回指定文件或目录的创建日期和时间
        /// </summary>
        /// <param name="path">要获取创建日期和时间信息的文件或目录</param>
        /// <returns>
        /// System.DateTime结构设置为指定文件或目录的创建日期和时间。此值以本地时间表示
        /// </returns>
        public virtual DateTime GetCreationTime(string path)
        {
            return File.GetCreationTime(path);
        }

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
        public virtual string[] GetDirectories(string path, string searchPattern = "", bool topDirectoryOnly = true)
        {
            if (string.IsNullOrEmpty(searchPattern))
                searchPattern = "*";

            return Directory.GetDirectories(path, searchPattern,
                topDirectoryOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
        }

        /// <summary>
        /// 返回指定路径字符串的目录信息
        /// </summary>
        /// <param name="path">文件或目录的路径</param>
        /// <returns>
        /// 路径的目录信息，如果路径表示根目录或为空，则为空。返回System.String。如果路径不包含目录信息，则为空
        /// </returns>
        public virtual string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        /// <summary>
        /// 仅返回指定路径字符串的目录名
        /// </summary>
        /// <param name="path">目录的路径</param>
        /// <returns>目录名</returns>
        public virtual string GetDirectoryNameOnly(string path)
        {
            return new DirectoryInfo(path).Name;
        }

        /// <summary>
        /// 返回指定路径字符串的扩展名
        /// </summary>
        /// <param name="filePath">从中获取扩展名的路径字符串</param>
        /// <returns>指定路径的扩展名（包括句点“.”）</returns>
        public virtual string GetFileExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        /// <summary>
        /// 返回指定路径字符串的文件名和扩展名
        /// </summary>
        /// <param name="path">从中获取文件名和扩展名的路径字符串</param>
        /// <returns>路径中最后一个目录字符后的字符</returns>
        public virtual string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        /// <summary>
        /// 返回不带扩展名的指定路径字符串的文件名
        /// </summary>
        /// <param name="filePath">文件的路径</param>
        /// <returns>文件名，减去最后一个句点（.）和其后的所有字符</returns>
        public virtual string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

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
        public virtual string[] GetFiles(string directoryPath, string searchPattern = "", bool topDirectoryOnly = true)
        {
            if (string.IsNullOrEmpty(searchPattern))
                searchPattern = "*.*";

            return Directory.GetFileSystemEntries(directoryPath, searchPattern,
                new EnumerationOptions
                {
                    IgnoreInaccessible = true,
                    MatchCasing = MatchCasing.CaseInsensitive,
                    RecurseSubdirectories = !topDirectoryOnly,

                });
        }

        /// <summary>
        /// 返回上次访问指定文件或目录的日期和时间
        /// </summary>
        /// <param name="path">获取访问日期和时间信息的文件或目录</param>
        /// <returns>System.DateTime结构设置为指定文件的日期和时间</returns>
        public virtual DateTime GetLastAccessTime(string path)
        {
            return File.GetLastAccessTime(path);
        }

        /// <summary>
        /// 返回上次写入指定文件或目录的日期和时间
        /// </summary>
        /// <param name="path">获取写入日期和时间信息的文件或目录</param>
        /// <returns>
        /// System.DateTime结构设置为上次写入指定文件或目录的日期和时间。
        /// 此值以本地时间表示
        /// </returns>
        public virtual DateTime GetLastWriteTime(string path)
        {
            return File.GetLastWriteTime(path);
        }

        /// <summary>
        /// 返回指定文件或目录上次写入的日期和时间，单位为协调世界时（UTC）
        /// </summary>
        /// <param name="path">获取写入日期和时间信息的文件或目录</param>
        /// <returns>
        /// System.DateTime结构设置为上次写入指定文件或目录的日期和时间。
        /// 此值以UTC时间表示
        /// </returns>
        public virtual DateTime GetLastWriteTimeUtc(string path)
        {
            return File.GetLastWriteTimeUtc(path);
        }

        /// <summary>
        /// 在指定的路径上创建或打开文件以进行读/写访问
        /// </summary>
        /// <param name="path">要创建的文件的路径和名称</param>
        /// <returns><see cref="FileStream"/> 提供对 <paramref name="path"/>中指定的文件的读/写访问</returns>
        public FileStream GetOrCreateFile(string path)
        {
            if (FileExists(path))
                return File.Open(path, FileMode.Open, FileAccess.ReadWrite);

            var fileInfo = new FileInfo(path);
            CreateDirectory(fileInfo.DirectoryName);

            return File.Create(path);
        }

        /// <summary>
        /// 检索指定路径的父目录
        /// </summary>
        /// <param name="directoryPath">检索父目录的路径</param>
        /// <returns>父目录，如果路径是根目录，则为null，包括UNC服务器或共享名的根目录</returns>
        public virtual string GetParentDirectory(string directoryPath)
        {
            return Directory.GetParent(directoryPath).FullName;
        }

        /// <summary>
        /// 从物理磁盘路径获取虚拟路径。
        /// </summary>
        /// <param name="path">物理磁盘路径</param>
        /// <returns>虚拟路径。例如："~/bin"</returns>
        public virtual string GetVirtualPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            if (!IsDirectory(path) && FileExists(path))
                path = new FileInfo(path).DirectoryName;

            path = path?.Replace(WebRootPath, string.Empty).Replace('\\', '/').Trim('/').TrimStart('~', '/');

            return $"~/{path ?? string.Empty}";
        }

        /// <summary>
        /// 检查路径是否为目录
        /// </summary>
        /// <param name="path">检查路径</param>
        /// <returns>如果路径是目录，则为True，否则为false</returns>
        public virtual bool IsDirectory(string path)
        {
            return DirectoryExists(path);
        }

        /// <summary>
        /// 将虚拟路径映射到物理磁盘路径。
        /// </summary>
        /// <param name="path">要映射的路径。 例如： "~/bin"</param>
        /// <returns>物理路径。例如："c:\inetpub\wwwroot\bin"</returns>
        public virtual string MapPath(string path)
        {
            path = path.Replace("~/", string.Empty).TrimStart('/');

            //if virtual path has slash on the end, it should be after transform the virtual path to physical path too
            var pathEnd = path.EndsWith('/') ? Path.DirectorySeparatorChar.ToString() : string.Empty;

            return Combine(Root ?? string.Empty, path) + pathEnd;
        }

        /// <summary>
        /// 将文件内容读入字节数组
        /// </summary>
        /// <param name="filePath">用于读取的文件</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含包含文件内容的字节数组
        /// </returns>
        public virtual async Task<byte[]> ReadAllBytesAsync(string filePath)
        {
            return File.Exists(filePath) ? await File.ReadAllBytesAsync(filePath) : Array.Empty<byte>();
        }

        /// <summary>
        /// 打开文件，用指定的编码读取文件的所有行，然后关闭文件。
        /// </summary>
        /// <param name="path">要打开以进行读取的文件</param>
        /// <param name="encoding">应用于文件内容的编码</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含包含文件所有行的字符串
        /// </returns>
        public virtual async Task<string> ReadAllTextAsync(string path, Encoding encoding)
        {
            await using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var streamReader = new StreamReader(fileStream, encoding);

            return await streamReader.ReadToEndAsync();
        }

        /// <summary>
        /// 打开文件，用指定的编码读取文件的所有行，然后关闭文件。
        /// </summary>
        /// <param name="path">要打开以进行读取的文件</param>
        /// <param name="encoding">应用于文件内容的编码</param>
        /// <returns>包含文件所有行的字符串</returns>
        public virtual string ReadAllText(string path, Encoding encoding)
        {
            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var streamReader = new StreamReader(fileStream, encoding);

            return streamReader.ReadToEnd();
        }

        /// <summary>
        /// 将指定的字节数组写入文件
        /// </summary>
        /// <param name="filePath">要写入的文件</param>
        /// <param name="bytes">要写入文件的字节</param>
        /// <returns>表示异步操作的任务</returns>
        public virtual async Task WriteAllBytesAsync(string filePath, byte[] bytes)
        {
            await File.WriteAllBytesAsync(filePath, bytes);
        }

        /// <summary>
        /// 创建新文件，使用指定的编码将指定的字符串写入文件，然后关闭文件。如果目标文件已经存在，则会覆盖它。
        /// </summary>
        /// <param name="path">要写入的文件</param>
        /// <param name="contents">要写入文件的字符串</param>
        /// <param name="encoding">要应用于字符串的编码</param>
        /// <returns>表示异步操作的任务</returns>
        public virtual async Task WriteAllTextAsync(string path, string contents, Encoding encoding)
        {
            await File.WriteAllTextAsync(path, contents, encoding);
        }

        /// <summary>
        /// 创建新文件，使用指定的编码将指定的字符串写入文件，然后关闭文件。如果目标文件已经存在，则会覆盖它。
        /// </summary>
        /// <param name="path">要写入的文件</param>
        /// <param name="contents">要写入文件的字符串</param>
        /// <param name="encoding">要应用于字符串的编码</param>
        public virtual void WriteAllText(string path, string contents, Encoding encoding)
        {
            File.WriteAllText(path, contents, encoding);
        }

        /// <summary>在给定路径找到文件。</summary>
        /// <param name="subpath">标识文件的相对路径。</param>
        /// <returns>文件信息。调用方必须检查Exists属性。</returns>
        public virtual new IFileInfo GetFileInfo(string subpath)
        {
            subpath = subpath.Replace(Root, string.Empty);

            return base.GetFileInfo(subpath);
        }

        /// <summary>
        /// 设置最后一次写入指定文件的日期和时间(以协调世界时(UTC))
        /// </summary>
        /// <param name="path">要为其设置日期和时间信息的文件</param>
        /// <param name="lastWriteTimeUtc">
        /// 一个System.DateTime，其中包含要为路径的最后写入日期和时间设置的值。
        /// 该值以UTC时间表示该值以UTC时间表示
        /// </param>
        public virtual void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
        {
            File.SetLastWriteTimeUtc(path, lastWriteTimeUtc);
        }

        #endregion

        protected string WebRootPath { get; }
    }
}
