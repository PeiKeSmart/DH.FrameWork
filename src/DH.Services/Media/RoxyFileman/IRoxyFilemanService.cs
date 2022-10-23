namespace DH.Services.Media.RoxyFileman
{
    /// <summary>
    /// RoxyFileman服务接口
    /// </summary>
    public partial interface IRoxyFilemanService
    {
        #region Configuration

        /// <summary>
        /// 初始服务配置
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task ConfigureAsync();

        /// <summary>
        /// 获取配置文件路径
        /// </summary>
        string GetConfigurationFilePath();

        /// <summary>
        /// 为RoxyFileman创建配置文件
        /// </summary>
        /// <returns>表示异步操作的任务</returns>
        Task CreateConfigurationAsync();

        #endregion

        #region Directories

        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="sourcePath">源目录的路径</param>
        /// <param name="destinationPath">目标目录的路径</param>
        /// <returns>表示异步操作的任务</returns>
        Task CopyDirectoryAsync(string sourcePath, string destinationPath);

        /// <summary>
        /// 创建新目录
        /// </summary>
        /// <param name="parentDirectoryPath">父目录的路径</param>
        /// <param name="name">新目录的名称</param>
        /// <returns>表示异步操作的任务</returns>
        Task CreateDirectoryAsync(string parentDirectoryPath, string name);

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="path">目录的路径</param>
        /// <returns>表示异步操作的任务</returns>
        Task DeleteDirectoryAsync(string path);

        /// <summary>
        /// 从服务器下载目录作为zip存档
        /// </summary>
        /// <param name="path">目录的路径</param>
        /// <returns>表示异步操作的任务</returns>
        Task DownloadDirectoryAsync(string path);

        /// <summary>
        /// 获取所有可用目录作为目录树
        /// </summary>
        /// <param name="type">文件的类型</param>
        /// <returns>表示异步操作的任务</returns>
        Task GetDirectoriesAsync(string type);

        /// <summary>
        /// 移动目录
        /// </summary>
        /// <param name="sourcePath">源目录的路径</param>
        /// <param name="destinationPath">目标目录的路径</param>
        /// <returns>表示异步操作的任务</returns>
        Task MoveDirectoryAsync(string sourcePath, string destinationPath);

        /// <summary>
        /// 重命名目录
        /// </summary>
        /// <param name="sourcePath">源目录的路径</param>
        /// <param name="newName">目录的新名称</param>
        /// <returns>表示异步操作的任务</returns>
        Task RenameDirectoryAsync(string sourcePath, string newName);

        #endregion

        #region Files

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sourcePath">源文件的路径</param>
        /// <param name="destinationPath">目标文件的路径</param>
        /// <returns>表示异步操作的任务</returns>
        Task CopyFileAsync(string sourcePath, string destinationPath);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件的路径</param>
        /// <returns>表示异步操作的任务</returns>
        Task DeleteFileAsync(string path);

        /// <summary>
        /// 从服务器下载文件
        /// </summary>
        /// <param name="path">文件的路径</param>
        /// <returns>表示异步操作的任务</returns>
        Task DownloadFileAsync(string path);

        /// <summary>
        /// 获取传递目录中的文件
        /// </summary>
        /// <param name="directoryPath">文件目录的路径</param>
        /// <param name="type">文件的类型</param>
        /// <returns>表示异步操作的任务</returns>
        Task GetFilesAsync(string directoryPath, string type);

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sourcePath">源文件的路径</param>
        /// <param name="destinationPath">目标文件的路径</param>
        /// <returns>表示异步操作的任务</returns>
        Task MoveFileAsync(string sourcePath, string destinationPath);

        /// <summary>
        /// Rename the file
        /// </summary>
        /// <param name="sourcePath">源文件的路径</param>
        /// <param name="newName">文件的新名称</param>
        /// <returns>表示异步操作的任务</returns>
        Task RenameFileAsync(string sourcePath, string newName);

        /// <summary>
        /// 将文件上载到传递路径上的目录
        /// </summary>
        /// <param name="directoryPath">上载文件的目录路径</param>
        /// <returns>表示异步操作的任务</returns>
        Task UploadFilesAsync(string directoryPath);

        #endregion

        #region Images

        /// <summary>
        /// 创建图像的缩略图并将其写入响应
        /// </summary>
        /// <param name="path">图像的路径</param>
        /// <returns>表示异步操作的任务</returns>
        Task CreateImageThumbnailAsync(string path);

        /// <summary>
        /// 刷新磁盘上的所有图像
        /// </summary>
        /// <param name="removeOriginal">指定是否删除原始图像</param>
        /// <returns>表示异步操作的任务</returns>
        Task FlushAllImagesOnDiskAsync(bool removeOriginal = true);

        /// <summary>
        /// 刷新磁盘上的图像
        /// </summary>
        /// <param name="directoryPath">刷新图像的目录路径</param>
        /// <returns>表示异步操作的任务</returns>
        Task FlushImagesOnDiskAsync(string directoryPath);

        #endregion

        #region Others

        /// <summary>
        /// 获取字符串以写入错误响应
        /// </summary>
        /// <param name="message">附加信息</param>
        /// <returns>要写入响应的字符串</returns>
        string GetErrorResponse(string message = null);

        /// <summary>
        /// 获取语言资源值
        /// </summary>
        /// <param name="key">语言资源键</param>
        /// <returns>
        /// 表示异步操作的任务
        /// 任务结果包含语言资源值
        /// </returns>
        Task<string> GetLanguageResourceAsync(string key);

        /// <summary>
        /// 是否使用ajax发出请求
        /// </summary>
        /// <returns>正确或错误</returns>
        bool IsAjaxRequest();

        #endregion
    }
}
