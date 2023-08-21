using System.Globalization;
using System.Text;

namespace DH.Files;

/// <summary>
/// 文件操作对象类型
/// </summary>
public enum FsoMethod {
    /// <summary>
    /// 文件夹
    /// </summary>
    Folder,
    /// <summary>
    /// 文件
    /// </summary>
    File,
    /// <summary>
    /// 文件夹和文件均包括
    /// </summary>
    All
}

/// <summary>
/// 文件管理类
/// </summary>
public abstract class FileSystemObject {

    public static string ConvertEncoding(string content, Encoding srcEncoding, Encoding targetEncoding)
    {
        if ((srcEncoding != targetEncoding) && !string.IsNullOrEmpty(content))
        {
            Byte[] bytes = srcEncoding.GetBytes(content);
            bytes = Encoding.Convert(srcEncoding, targetEncoding, bytes);
            Char[] chars = new Char[targetEncoding.GetCharCount(bytes, 0, bytes.Length)];
            targetEncoding.GetChars(bytes, 0, bytes.Length, chars, 0);
            content = new string(chars);
        }
        return content;
    }

    public static string ConvertSizeToShow(long fileSize)
    {
        Int64 num = fileSize / 0x400L;
        if (num < 1L)
        {
            return (fileSize.ToString(CultureInfo.CurrentCulture) + "<span style='color:red'>&nbsp;&nbsp;B</span>");
        }
        if (num < 0x400L)
        {
            return (num.ToString(CultureInfo.CurrentCulture) + "<span style='color:red'>&nbsp;KB</span>");
        }
        Int64 num2 = num / 0x400L;
        if (num2 < 1L)
        {
            return (num.ToString(CultureInfo.CurrentCulture) + "<span style='color:red'>&nbsp;KB</span>");
        }
        if (num2 >= 0x400L)
        {
            num2 /= 0x400L;
            return (num2.ToString(CultureInfo.CurrentCulture) + "<span style='color:red'>&nbsp;GB</span>");
        }
        return (num2.ToString(CultureInfo.CurrentCulture) + "<span style='color:red'>&nbsp;MB</span>");
    }

    /// <summary>
    /// 复制目录
    /// </summary>
    /// <param name="oldDir"></param>
    /// <param name="newDir"></param>
    public static void CopyDirectory(string oldDir, string newDir)
    {
        DirectoryInfo od = new DirectoryInfo(oldDir);
        CopyDirInfo(od, oldDir, newDir);
    }

    private static void CopyDirInfo(DirectoryInfo od, string oldDir, string newDir)
    {
        if (!IsExist(newDir, FsoMethod.Folder))
        {
            Create(newDir, FsoMethod.Folder);
        }
        foreach (var info in od.GetDirectories())
        {
            CopyDirInfo(info, info.FullName, newDir + info.FullName.Replace(oldDir, string.Empty));
        }
        foreach (var info2 in od.GetFiles())
        {
            CopyFile(info2.FullName, newDir + info2.FullName.Replace(oldDir, string.Empty));
        }
    }

    public static List<DirectoryAllInfo> CopyDT(List<DirectoryAllInfo> parent, List<DirectoryAllInfo> child)
    {
        foreach (var row in child)
        {
            parent.Add(row);
        }
        return parent;
    }

    public static void CopyFile(string oldFile, string newFile)
    {
        System.IO.File.Copy(oldFile, newFile, true);
    }

    public static bool CopyFileStream(string oldPath, string newPath)
    {
        try
        {
            FileStream input = new FileStream(oldPath, FileMode.Open, FileAccess.Read);
            FileStream output = new FileStream(newPath, FileMode.Create, FileAccess.Write);
            BinaryReader reader = new BinaryReader(input);
            BinaryWriter writer = new BinaryWriter(output);
            reader.BaseStream.Seek(0L, SeekOrigin.Begin);
            reader.BaseStream.Seek(0L, SeekOrigin.End);
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                writer.Write(reader.ReadByte());
            }
            reader.Dispose();
            writer.Dispose();
            input.Flush();
            input.Dispose();
            output.Flush();
            output.Dispose();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static void Create(string file, FsoMethod method)
    {
        try
        {
            if (method == FsoMethod.File)
            {
                WriteFile(file, string.Empty);
            }
            else if (method == FsoMethod.Folder)
            {
                Directory.CreateDirectory(file);
            }
        }
        catch
        {
            throw new UnauthorizedAccessException("没有权限！");
        }
    }

    //public static string CreateFileFolder(string folderName)
    //{
    //    if (string.IsNullOrEmpty(folderName))
    //    {
    //        throw new ArgumentNullException("folderName", "folderName为空！");
    //    }
    //    string path = Path.Combine(DHWeb.RootPath, folderName);
    //    if (!Directory.Exists(path))
    //    {
    //        Directory.CreateDirectory(path);
    //    }
    //    return path;
    //}

    public static void Delete(string file, FsoMethod method)
    {
        if ((method == FsoMethod.File) && System.IO.File.Exists(file))
        {
            System.IO.File.Delete(file);
        }
        if ((method == FsoMethod.Folder) && Directory.Exists(file))
        {
            Directory.Delete(file, true);
        }
    }

    private static long[] DirInfo(DirectoryInfo d)
    {
        long[] numArray = new long[3];
        long num = 0L;
        long num2 = 0L;
        long num3 = 0L;
        var files = d.GetFiles();
        num3 += files.Length;
        foreach (var info in files)
        {
            num += info.Length;
        }
        DirectoryInfo[] directories = d.GetDirectories();
        num2 += directories.Length;
        foreach (DirectoryInfo info2 in directories)
        {
            num += DirInfo(info2)[0];
            num2 += DirInfo(info2)[1];
            num3 += DirInfo(info2)[2];
        }
        numArray[0] = num;
        numArray[1] = num2;
        numArray[2] = num3;
        return numArray;
    }

    private static List<DirectoryAllInfo> GetDirectoryAllInfo(DirectoryInfo d, FsoMethod method)
    {
        var list = new List<DirectoryAllInfo>();
        foreach (DirectoryInfo info in d.GetDirectories())
        {
            if (method == FsoMethod.File)
            {
                list = CopyDT(list, GetDirectoryAllInfo(info, method));
            }
            else
            {
                var model = new DirectoryAllInfo();
                model.name = info.Name;
                model.rname = info.FullName;
                model.content_type = string.Empty;
                model.type = 1;
                model.path = info.FullName.Replace(info.Name, string.Empty);
                model.creatime = info.CreationTime;
                model.lastWriteTime = info.LastWriteTime;
                model.size = 0;
                list.Add(model);
                list = CopyDT(list, GetDirectoryAllInfo(info, method));
            }
        }
        if (method != FsoMethod.Folder)
        {
            foreach (var info2 in d.GetFiles())
            {
                var model = new DirectoryAllInfo();
                model.name = info2.Name;
                model.rname = info2.FullName;
                model.content_type = info2.Extension.Replace(".", string.Empty);
                model.type = 2;
                model.path = info2.DirectoryName + @"\";
                model.creatime = info2.CreationTime;
                model.lastWriteTime = info2.LastWriteTime;
                model.size = info2.Length;
                list.Add(model);
            }
        }
        return list;
    }

    public static List<DirectoryAllInfo> GetDirectoryAllInfos(string dir, FsoMethod method)
    {
        List<DirectoryAllInfo> directoryAllInfo;
        try
        {
            DirectoryInfo d = new DirectoryInfo(dir);
            directoryAllInfo = GetDirectoryAllInfo(d, method);
        }
        catch (Exception exception)
        {
            throw new FileNotFoundException(exception.ToString());
        }
        return directoryAllInfo;
    }

    public static List<DirectoryInfos> GetDirectoryInfos(string dir, FsoMethod method)
    {
        var list = new List<DirectoryInfos>();
        dir = dir.GetFullPath();
        if (method != FsoMethod.File)
        {
            for (int i = 0; i < Directory.GetDirectories(dir).Length; i++)
            {
                var model = new DirectoryInfos();
                DirectoryInfo d = new DirectoryInfo(Directory.GetDirectories(dir)[i]);
                Int64[] numArray = DirInfo(d);
                model.name = d.Name;
                model.type = 1;
                model.size = numArray[0];
                model.content_type = string.Empty;
                model.createTime = d.CreationTime;
                model.lastWriteTime = d.LastWriteTime;
                model.path = d.Name;
                model.Id = i + 1;
                list.Add(model);
            }
        }
        if (method != FsoMethod.Folder)
        {
            for (int j = 0; j < Directory.GetFiles(dir).Length; j++)
            {
                var model = new DirectoryInfos();
                var info2 = new System.IO.FileInfo(Directory.GetFiles(dir)[j]);
                model.name = info2.Name;
                model.type = 2;
                model.size = info2.Length;
                model.content_type = info2.Extension.Replace(".", string.Empty);
                model.createTime = info2.CreationTime;
                model.lastWriteTime = info2.LastWriteTime;
                model.path = info2.Name;
                model.Id = j + 1;
                list.Add(model);
            }
        }
        return list;
    }

    public static long[] GetDirInfos(string dir)
    {
        Int64[] numArray = new Int64[3];
        DirectoryInfo d = new DirectoryInfo(dir);
        return DirInfo(d);
    }

    public static Encoding GetEncoding(FileStream stream)
    {
        Encoding bigEndianUnicode = Encoding.UTF8;
        if ((stream != null) && (stream.Length >= 2L))
        {
            byte num = 0;
            byte num2 = 0;
            byte num3 = 0;
            long offset = stream.Seek(0L, SeekOrigin.Begin);
            stream.Seek(0L, SeekOrigin.Begin);
            num = System.Convert.ToByte(stream.ReadByte());
            num2 = System.Convert.ToByte(stream.ReadByte());
            if (stream.Length >= 3L)
            {
                num3 = System.Convert.ToByte(stream.ReadByte());
            }
            if (stream.Length >= 4L)
            {
                System.Convert.ToByte(stream.ReadByte());
            }
            if ((num == 0xfe) && (num2 == 0xff))
            {
                bigEndianUnicode = Encoding.BigEndianUnicode;
            }
            if (((num == 0xff) && (num2 == 0xfe)) && (num3 != 0xff))
            {
                bigEndianUnicode = Encoding.Unicode;
            }
            if (((num == 0xef) && (num2 == 0xbb)) && (num3 == 0xbf))
            {
                bigEndianUnicode = Encoding.UTF8;
            }
            stream.Seek(offset, SeekOrigin.Begin);
        }
        stream.Dispose();
        return bigEndianUnicode;
    }

    /// <summary>
    /// 获取指定文件大小
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string GetFileSize(string filePath)
    {
        var info = new System.IO.FileInfo(filePath);
        float num = info.Length / 0x400L;
        return (num.ToString(CultureInfo.CurrentCulture) + "KB");
    }

    /// <summary>
    /// 获取文件修改时间
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static DateTime GetFileUpdateTime(string filePath)
    {
        var info = new System.IO.FileInfo(filePath);
        DateTime dt = info.LastWriteTime;
        return dt;
    }

    /// <summary>
    /// 判断是否存在
    /// </summary>
    /// <param name="file">物理路径</param>
    /// <param name="method"></param>
    /// <returns></returns>
    public static Boolean IsExist(string file, FsoMethod method)
    {
        if (method == FsoMethod.File)
        {
            return System.IO.File.Exists(file);
        }
        return ((method == FsoMethod.Folder) && Directory.Exists(file));
    }

    //public static Boolean IsExistCategoryDirAndCreate(string categorDir)
    //{
    //    string file = Path.Combine(DHWeb.RootPath, categorDir);
    //    if (IsExist(file, FsoMethod.Folder))
    //    {
    //        return true;
    //    }
    //    Create(file, FsoMethod.Folder);
    //    return false;
    //}

    public static void Move(string oldFile, string newFile, FsoMethod method)
    {
        if (method == FsoMethod.File)
        {
            System.IO.File.Move(oldFile, newFile);
        }
        if (method == FsoMethod.Folder)
        {
            Directory.Move(oldFile, newFile);
        }
    }

    public static string ReadFile(string filePath)
    {
        string content = string.Empty;
        if (!System.IO.File.Exists(filePath))
        {
            return content;
        }
        using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            Encoding encoding = GetEncoding(stream);
            StreamReader reader = new StreamReader(System.IO.File.OpenRead(filePath), encoding, true, 0x400);
            content = reader.ReadToEnd();
            reader.Dispose();
            if (encoding != Encoding.UTF8)
            {
                content = ConvertEncoding(content, encoding, Encoding.UTF8);
            }
            return content;
        }
    }

    /// <summary>
    /// 使用gbk编码读取
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="gbk"></param>
    /// <returns></returns>
    public static string ReadFile(string filePath, string gbk)
    {
        string content = string.Empty;
        if (!System.IO.File.Exists(filePath))
        {
            return content;
        }
        using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            Encoding encoding = Encoding.GetEncoding(936);
            StreamReader reader = new StreamReader(System.IO.File.OpenRead(filePath), encoding, true, 0x400);
            content = reader.ReadToEnd();
            reader.Dispose();
            if (encoding != Encoding.UTF8)
            {
                content = ConvertEncoding(content, encoding, Encoding.UTF8);
            }
            return content;
        }
    }

    public static void ReplaceFileContent(string dir, string originalContent, string newContent)
    {
        if (!string.IsNullOrEmpty(originalContent))
        {
            DirectoryInfo info = new DirectoryInfo(dir);
            foreach (var info2 in info.GetFiles("*.*", SearchOption.AllDirectories))
            {
                StreamReader reader = info2.OpenText();
                string str = reader.ReadToEnd();
                reader.Dispose();
                if (str.Contains(originalContent))
                {
                    str = str.Replace(originalContent, newContent);
                    StreamWriter writer = new StreamWriter(System.IO.File.OpenWrite(info2.FullName));
                    writer.Write(str);
                    writer.Dispose();
                }
            }
        }
    }

    public static List<DirectoryInfos> SearchFileContent(string dir, string searchPattern, string searchKeyword)
    {
        var list = new List<DirectoryInfos>();
        DirectoryInfo info = new DirectoryInfo(dir);
        foreach (var info2 in info.GetFiles(searchPattern, SearchOption.AllDirectories))
        {
            var model = new DirectoryInfos();
            StreamReader reader = info2.OpenText();
            string str = reader.ReadToEnd();
            reader.Dispose();
            if (str.Contains(searchKeyword))
            {
                model.name = info2.FullName.Remove(0, info.FullName.Length);
                model.type = 2;
                model.size = info2.Length;
                model.content_type = info2.Extension.Replace(".", string.Empty);
                model.createTime = info2.CreationTime;
                model.lastWriteTime = info2.LastWriteTime;
                list.Add(model);
            }
        }
        return list;
    }

    public static List<DirectoryInfos> SearchFiles(string dir, string searchPattern)
    {
        var list = new List<DirectoryInfos>();
        DirectoryInfo info = new DirectoryInfo(dir);
        foreach (var info2 in info.GetFiles(searchPattern, SearchOption.AllDirectories))
        {
            var model = new DirectoryInfos();
            model.name = info2.FullName.Remove(0, info.FullName.Length);
            model.type = 2;
            model.size = info2.Length;
            model.content_type = info2.Extension.Replace(".", string.Empty);
            model.createTime = info2.CreationTime;
            model.lastWriteTime = info2.LastWriteTime;
            list.Add(model);
        }
        return list;
    }

    public static List<DirectoryInfos> SearchTemplateFiles(string dir, string searchPattern)
    {
        var list = new List<DirectoryInfos>();
        DirectoryInfo info = new DirectoryInfo(dir);
        string str = searchPattern;
        string str2 = searchPattern.ToLower(CultureInfo.CurrentCulture);
        int length = searchPattern.Length;
        if (length < 4)
        {
            str = "*" + str + "*.html";
        }
        else if ((str2.Substring(length - 4, 4) != ".html") || (str2.Substring(length - 3, 3) != ".htm"))
        {
            str = "*" + str + "*.html";
        }
        try
        {
            foreach (var info2 in info.GetFiles(str, SearchOption.AllDirectories))
            {
                var model = new DirectoryInfos();
                model.name = info2.FullName.Remove(0, info.FullName.Length).Replace("/", "\"");
                model.type = 2;
                model.size = info2.Length;
                model.content_type = info2.Extension.Replace(".", string.Empty);
                model.createTime = info2.CreationTime;
                model.lastWriteTime = info2.LastWriteTime;
                list.Add(model);
            }
        }
        catch (ArgumentException)
        {
        }
        return list;
    }

    public static string WriteAppend(string file, string fileContent)
    {
        string str;
        var info = new System.IO.FileInfo(file);
        if (!Directory.Exists(info.DirectoryName))
        {
            Directory.CreateDirectory(info.DirectoryName);
        }
        FileStream stream = new FileStream(file, FileMode.Append, FileAccess.Write);
        StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
        try
        {
            writer.Write(fileContent);
            str = fileContent;
        }
        catch (Exception exception)
        {
            throw new FileNotFoundException(exception.ToString());
        }
        finally
        {
            writer.Flush();
            stream.Flush();
            writer.Dispose();
            stream.Dispose();
        }
        return str;
    }

    public static string WriteFile(string file, string fileContent)
    {
        string str;
        var info = new System.IO.FileInfo(file);
        if (!Directory.Exists(info.DirectoryName))
        {
            Directory.CreateDirectory(info.DirectoryName);
        }
        FileStream stream = new FileStream(file, FileMode.Create, FileAccess.Write);
        StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(false));
        try
        {
            writer.Write(fileContent);
            str = fileContent;
        }
        catch (Exception exception)
        {
            throw new FileNotFoundException(exception.ToString());
        }
        finally
        {
            writer.Flush();
            stream.Flush();
            writer.Dispose();
            stream.Dispose();
        }
        return str;
    }

    /// <summary>
    /// 将二进制文件读入byte[]（如图片等）
    /// </summary>
    /// <param name="fileName">文件名与路径</param>
    /// <returns>byte[]类型数据</returns>
    public static Byte[] ReadFile1(string fileName)
    {
        FileStream pFileStream = null;
        byte[] pReadByte = new byte[0];

        try
        {
            pFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(pFileStream);
            r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开
            pReadByte = r.ReadBytes((int)r.BaseStream.Length);
            return pReadByte;
        }
        catch
        {
            return pReadByte;
        }
        finally
        {
            if (pFileStream != null)
                pFileStream.Dispose();
        }
    }

    /// <summary>
    /// 写byte[]数据到文件（如图片等二进制数据）
    /// </summary>
    /// <param name="pReadByte">二进制数</param>
    /// <param name="fileName">文件名</param>
    /// <returns>成功返回True 否返回False</returns>
    public static bool WriteFile(byte[] pReadByte, string fileName)
    {
        FileStream pFileStream = null;

        try
        {
            pFileStream = new FileStream(fileName, FileMode.OpenOrCreate);
            pFileStream.Write(pReadByte, 0, pReadByte.Length);
        }
        catch
        {
            return false;
        }
        finally
        {
            if (pFileStream != null)
                pFileStream.Dispose();
        }
        return true;
    }

}

/// <summary>
/// 文件夹所有信息
/// </summary>
public class DirectoryAllInfo {
    /// <summary>
    /// 文件名
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// 文件整个路径
    /// </summary>
    public string rname { get; set; }

    /// <summary>
    /// 文件后缀
    /// </summary>
    public string content_type { get; set; }

    /// <summary>
    /// 2为文件，1为文件夹
    /// </summary>
    public int type { get; set; }

    /// <summary>
    /// 文件夹路径
    /// </summary>
    public string path { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime creatime { get; set; }

    /// <summary>
    /// 最后的编辑时间
    /// </summary>
    public DateTime lastWriteTime { get; set; }

    /// <summary>
    /// 文件大小
    /// </summary>
    public long size { get; set; }

}

/// <summary>
/// 文件夹信息
/// </summary>
public class DirectoryInfos {
    public string name { get; set; }

    /// <summary>
    /// 1为文件夹
    /// </summary>
    public int type { get; set; }

    public long size { get; set; }

    public string content_type { get; set; }

    public DateTime createTime { get; set; }

    public DateTime lastWriteTime { get; set; }

    public string path { get; set; }

    public int Id { get; set; }
}