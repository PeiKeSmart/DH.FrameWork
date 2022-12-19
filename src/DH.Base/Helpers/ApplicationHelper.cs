﻿using System.Diagnostics;
using System.Reflection;

namespace DH.Helpers;

public static class ApplicationHelper
{
    /// <summary>
    /// ApplicationName
    /// </summary>
    public static string ApplicationName => Assembly.GetEntryAssembly()?.GetName().Name ?? AppDomain.CurrentDomain.FriendlyName;

    /// <summary>
    /// 应用根目录
    /// </summary>
    public static readonly string AppRoot = AppDomain.CurrentDomain.BaseDirectory;

    /// <summary>
    /// 将虚拟路径转换为物理路径，相对路径转换为绝对路径
    /// </summary>
    /// <param name="virtualPath">虚拟路径</param>
    /// <returns>虚拟路径对应的物理路径</returns>
    public static string MapPath(string virtualPath) => AppRoot + virtualPath.TrimStart('~');

    /// <summary>
    /// 是否IIS
    /// </summary>
    public static Boolean IsIIS
    {
        get
        {
            var processName = Process.GetCurrentProcess().ProcessName.ToLower();
            if (processName.Contains("iis") || processName.Contains("w3wp"))
                return true;

            return false;
        }
    }
}