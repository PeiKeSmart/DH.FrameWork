﻿using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Primitives;
using NewLife;
using NewLife.Http;
using NewLife.IO;
using NewLife.Log;
using NewLife.Web;

namespace Stardust.Extensions.Caches;

/// <summary>文件缓存提供者。本地文件不存在时，从上级拉取</summary>
class CacheFileProvider : IFileProvider
{
    #region 属性
    private static readonly Char[] _pathSeparators = new Char[2]
    {
        Path.DirectorySeparatorChar,
        Path.AltDirectorySeparatorChar
    };

    private readonly ExclusionFilters _filters;

    /// <summary>根目录</summary>
    public String Root { get; }

    /// <summary>服务端地址。本地文件不存在时，将从这里下载</summary>
    public String Server { get; set; }

    /// <summary>索引信息文件。列出扩展显示的文件内容</summary>
    public String IndexInfoFile { get; set; }
    #endregion

    //public CacheFileProvider(String root) : this(root, ExclusionFilters.Sensitive) { }

    /// <summary>
    /// 实例化
    /// </summary>
    /// <param name="root"></param>
    /// <param name="server"></param>
    /// <param name="filters"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public CacheFileProvider(String root, String server, ExclusionFilters filters = ExclusionFilters.Sensitive)
    {
        if (!Path.IsPathRooted(root)) throw new ArgumentException("The path must be absolute.", nameof(root));

        Root = Path.GetFullPath(root).EnsureEnd(Path.DirectorySeparatorChar + "");
        if (!Directory.Exists(Root)) throw new DirectoryNotFoundException(Root);

        Server = server.TrimEnd('/');
        _filters = filters;
    }

    private String GetFullPath(String path)
    {
        if (PathNavigatesAboveRoot(path)) return null;

        String fullPath;
        try
        {
            fullPath = Path.GetFullPath(Path.Combine(Root, path));
        }
        catch
        {
            return null;
        }

        return !fullPath.StartsWithIgnoreCase(Root) ? null : fullPath;
    }

    /// <summary>
    /// 获取文件信息
    /// </summary>
    /// <param name="subpath"></param>
    /// <returns></returns>
    public IFileInfo GetFileInfo(String subpath)
    {
        if (String.IsNullOrEmpty(subpath) || HasInvalidPathChars(subpath)) return new NotFoundFileInfo(subpath);

        subpath = subpath.TrimStart(_pathSeparators);
        if (Path.IsPathRooted(subpath)) return new NotFoundFileInfo(subpath);

        var fullPath = GetFullPath(subpath);
        if (fullPath == null) return new NotFoundFileInfo(subpath);

        // 本地不存在时，从服务器下载
        var fi = fullPath.AsFile();
        //if ((!fi.Exists || fi.LastWriteTime.AddDays(1) < DateTime.Now) && Path.GetFileName(fullPath).Contains('.'))
        if (!fi.Exists && Path.GetFileName(fullPath).Contains('.'))
        {
            var url = subpath.Replace("\\", "/");
            url = Server.Contains("{0}") ? Server.Replace("{0}", url) : Server + url.EnsureStart("/");

            XTrace.WriteLine("下载：{0}", url);

            // 先下载到临时目录，避免出现下载半截的情况
            var tmp = Path.GetTempFileName();
            using var fs = new FileStream(tmp, FileMode.OpenOrCreate);

            using var client = new HttpClient();
            using var rs = client.GetStreamAsync(url).Result;
            rs.CopyTo(fs);
            fs.Flush();
            fs.SetLength(fs.Position);
            fs.Dispose();

            // 移动临时文件到最终目录
            fullPath.EnsureDirectory(true);
            File.Move(tmp, fullPath);

            XTrace.WriteLine("下载完成：{0}", fullPath);
        }

        var fileInfo = new FileInfo(fullPath);
        return IsExcluded(fileInfo, _filters) ? new NotFoundFileInfo(subpath) : new PhysicalFileInfo(fileInfo);
    }

    /// <summary>
    /// 是否存在
    /// </summary>
    /// <param name="fileInfo"></param>
    /// <param name="filters"></param>
    /// <returns></returns>
    public static Boolean IsExcluded(FileSystemInfo fileInfo, ExclusionFilters filters)
    {
        if (filters == ExclusionFilters.None) return false;

        return fileInfo.Name.StartsWith(".", StringComparison.Ordinal) && (filters & ExclusionFilters.DotPrefixed) != 0 ||
            fileInfo.Exists && (

                (fileInfo.Attributes & FileAttributes.Hidden) != 0 && (filters & ExclusionFilters.Hidden) != 0 ||
                (fileInfo.Attributes & FileAttributes.System) != 0 && (filters & ExclusionFilters.System) != 0
            );
    }

    /// <summary>
    /// 获取文件内容
    /// </summary>
    /// <param name="subpath"></param>
    /// <returns></returns>
    public IDirectoryContents GetDirectoryContents(String subpath)
    {
        try
        {
            if (subpath == null || HasInvalidPathChars(subpath)) return NotFoundDirectoryContents.Singleton;

            subpath = subpath.TrimStart(_pathSeparators);
            if (Path.IsPathRooted(subpath)) return NotFoundDirectoryContents.Singleton;

            var fullPath = GetFullPath(subpath);

            // 下载信息文件
            if (!IndexInfoFile.IsNullOrEmpty() && !Server.IsNullOrEmpty())
            {
                var fi = fullPath.CombinePath(IndexInfoFile).GetBasePath().AsFile();
                if (!fi.Exists || fi.LastWriteTime.AddDays(1) < DateTime.Now)
                {
                    try
                    {
                        var url = subpath.Replace("\\", "/");
                        url = Server.Contains("{0}") ? Server.Replace("{0}", url) : Server + url.EnsureStart("/");

                        using var client = new HttpClient();
                        var html = client.GetString(url);

                        var links = Link.Parse(html, url);
                        var list = links.Select(e => new FileInfoModel
                        {
                            Name = e.FullName,
                            LastModified = e.Time.Year > 2000 ? e.Time : DateTime.Now,
                            Exists = true,
                            IsDirectory = false,
                        }).ToList();

                        var csv = new CsvDb<FileInfoModel> { FileName = fi.FullName };
                        csv.Write(list, false);
                    }
                    catch (Exception ex)
                    {
                        XTrace.Log?.Debug("下载目录信息出错：{0}", ex.Message);
                    }
                }
            }

            return fullPath == null || !Directory.Exists(fullPath)
                      ? NotFoundDirectoryContents.Singleton
                      : new CacheDirectoryContents(fullPath, _filters) { IndexInfoFile = IndexInfoFile };
        }
        catch (DirectoryNotFoundException) { }
        catch (IOException) { }

        return NotFoundDirectoryContents.Singleton;
    }

    /// <summary>
    /// 监控文件改变
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IChangeToken Watch(String filter) => NullChangeToken.Singleton;

    internal static Boolean HasInvalidPathChars(String path) => path.IndexOfAny(_invalidFileNameChars) != -1;

    private static readonly Char[] _invalidFileNameChars = (from c in Path.GetInvalidFileNameChars()
                                                            where c != Path.DirectorySeparatorChar && c != Path.AltDirectorySeparatorChar
                                                            select c).ToArray();

    internal static Boolean PathNavigatesAboveRoot(String path)
    {
        var stringTokenizer = new StringTokenizer(path, _pathSeparators);
        var num = 0;
        foreach (var item in stringTokenizer)
        {
            if (item.Equals(".") || item.Equals("")) continue;
            if (item.Equals(".."))
            {
                num--;
                if (num == -1) return true;
            }
            else
                num++;
        }
        return false;
    }
}