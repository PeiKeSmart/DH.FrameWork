using DH.Core.Infrastructure;

using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Primitives;

namespace DH.Services.Media.RoxyFileman
{
    /// <summary>
    /// 表示roxyFileman images文件夹的文件提供程序
    /// </summary>
    public partial class RoxyFilemanProvider : IFileProvider
    {
        #region Fields

        private readonly PhysicalFileProvider _physicalFileProvider;

        #endregion

        #region Ctor

        public RoxyFilemanProvider(string root)
        {
            _physicalFileProvider = new PhysicalFileProvider(root);
        }

        public RoxyFilemanProvider(string root, ExclusionFilters filters)
        {
            _physicalFileProvider = new PhysicalFileProvider(root, filters);
        }

        #endregion

        #region Mehods

        /// <summary>枚举给定路径上的目录（如果有）。</summary>
        /// <param name="subpath">标识目录的相对路径。</param>
        /// <returns>返回目录的内容。</returns>
        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return _physicalFileProvider.GetDirectoryContents(subpath);
        }

        /// <summary>在给定路径中找到文件。</summary>
        /// <param name="subpath">标识文件的相对路径。</param>
        /// <returns>文件信息。调用方必须检查Exists属性。</returns>
        public IFileInfo GetFileInfo(string subpath)
        {
            var pictureService = EngineContext.Current.Resolve<IPictureService>();

            if (_physicalFileProvider.GetFileInfo(subpath).Exists || !pictureService.IsStoreInDb())
                return _physicalFileProvider.GetFileInfo(subpath);

            var fileProvider = EngineContext.Current.Resolve<IDHFileProvider>();
            var roxyFilemanService = EngineContext.Current.Resolve<IRoxyFilemanService>();
            var virtualPath = fileProvider?.GetVirtualPath(fileProvider.GetDirectoryName(_physicalFileProvider.GetFileInfo(subpath).PhysicalPath));
            roxyFilemanService.FlushImagesOnDiskAsync(virtualPath).Wait();

            return _physicalFileProvider.GetFileInfo(subpath);
        }

        /// <summary>
        /// 为指定的<paramref name="filter" />创建<see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> 
        /// </summary>
        /// <param name="filter">用于确定要监视哪些文件或文件夹的筛选器字符串。示例：**/*.cs，*.*，subFolder/**/*.cshtml.</param>
        /// <returns>当添加、修改或删除与 <paramref name="filter" />匹配的文件时，会通知<see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /></returns>
        public IChangeToken Watch(string filter)
        {
            return _physicalFileProvider.Watch(filter);
        }

        #endregion
    }
}
