#if WINDOWS

using Microsoft.Win32;

using System.Diagnostics;

namespace DH.Platform;

/// <summary>
/// 压缩解压类
/// </summary>
public class WinRARHelper {
    /// <summary>
    /// 利用 WinRAR 进行压缩
    /// </summary>
    /// <param name="path">将要被压缩的文件夹（绝对路径）</param>
    /// <param name="rarPath">压缩后的 .rar 的存放目录（绝对路径）</param>
    /// <param name="rarName">压缩文件的名称（包括后缀）</param>
    /// <returns>true 或 false。压缩成功返回 true，反之，false。</returns>
    public static bool RAR(string path, string rarPath, string rarName)
    {
        bool flag = false;
        string rarexe;       //WinRAR.exe 的完整路径
        RegistryKey regkey;  //注册表键
        Object regvalue;     //键值
        string cmd;          //WinRAR 命令参数
        ProcessStartInfo startinfo;
        Process process;
        try
        {
            regkey = Registry.ClassesRoot.OpenSubKey(@"Applications\WinRAR.exe\shell\open\command");
            regvalue = regkey.GetValue("WinRAR");  // 键值为 "d:\Program Files\WinRAR\WinRAR.exe" "%1"
            rarexe = regvalue.ToString();
            regkey.Close();
            rarexe = rarexe.Substring(1, rarexe.Length - 7);  // d:\Program Files\WinRAR\WinRAR.exe

            Directory.CreateDirectory(path);
            //压缩命令，相当于在要压缩的文件夹(path)上点右键->WinRAR->添加到压缩文件->输入压缩文件名(rarName)
            cmd = string.Format("a {0} {1} -r",
                                rarName,
                                path);
            startinfo = new ProcessStartInfo();
            startinfo.FileName = rarexe;
            startinfo.Arguments = cmd;                          //设置命令参数
            startinfo.WindowStyle = ProcessWindowStyle.Hidden;  //隐藏 WinRAR 窗口

            startinfo.WorkingDirectory = rarPath;
            process = new Process();
            process.StartInfo = startinfo;
            process.Start();
            process.WaitForExit(); //无限期等待进程 winrar.exe 退出
            if (process.HasExited)
            {
                flag = true;
            }
            process.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        return flag;
    }


    /// <summary>
    /// 利用 WinRAR 进行解压缩
    /// </summary>
    /// <param name="path">文件解压路径（绝对）</param>
    /// <param name="rarPath">将要解压缩的 .rar 文件的存放目录（绝对路径）</param>
    /// <param name="rarName">将要解压缩的 .rar 文件名（包括后缀）</param>
    /// <returns>true 或 false。解压缩成功返回 true，反之，false。</returns>
    public static bool UnRAR(string path, string rarPath, string rarName)
    {
        bool flag = false;
        string rarexe;
        RegistryKey regkey;
        Object regvalue;
        string cmd;
        ProcessStartInfo startinfo;
        Process process;
        try
        {
            regkey = Registry.ClassesRoot.OpenSubKey(@"Applications\WinRAR.exe\shell\open\command");
            regvalue = regkey.GetValue("");
            rarexe = regvalue.ToString();
            regkey.Close();
            rarexe = rarexe.Substring(1, rarexe.Length - 7);

            Directory.CreateDirectory(path);
            //解压缩命令，相当于在要压缩文件(rarName)上点右键->WinRAR->解压到当前文件夹
            cmd = string.Format("x {0} {1} -y",
                                rarName,
                                path);
            startinfo = new ProcessStartInfo();
            startinfo.FileName = rarexe;
            startinfo.Arguments = cmd;
            startinfo.WindowStyle = ProcessWindowStyle.Hidden;

            startinfo.WorkingDirectory = rarPath;
            process = new Process();
            process.StartInfo = startinfo;
            process.Start();
            process.WaitForExit();
            if (process.HasExited)
            {
                flag = true;
            }
            process.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        return flag;
    }
}
#endif