namespace DH.Extensions;

/// <summary>
/// 文件信息(<see cref="FileInfo"/>) 扩展
/// </summary>
public static class FileInfoExtensions
{
    #region CompareTo(比较文件)

    /// <summary>
    /// 比较文件
    /// </summary>
    /// <param name="file1">文件1</param>
    /// <param name="file2">文件2</param>
    /// <returns></returns>
    public static bool CompareTo(this FileInfo file1, FileInfo file2)
    {
        if (file1 == null || !file1.Exists)
        {
            throw new ArgumentNullException(nameof(file1));
        }

        if (file2 == null || !file2.Exists)
        {
            throw new ArgumentNullException(nameof(file2));
        }

        if (file1.Length != file2.Length)
        {
            return false;
        }

        return file1.Read().Equals(file2.Read());
    }

    #endregion

    #region Read(读取文件并转换为字符串)

    /// <summary>
    /// 读取文件并转换为字符串
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="share">是否允许其他进程读取或写入该文件</param>
    /// <returns></returns>
    public static string Read(this FileInfo file, Boolean share = false)
    {
        if (file == null)
        {
            throw new ArgumentNullException(nameof(file));
        }

        if (!file.Exists)
        {
            return string.Empty;
        }

        if (!share)
        {
            using var reader = file.OpenText();
            return reader.ReadToEnd();
        }
        else
        {
            using var fileStream = file.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(fileStream);

            string content = reader.ReadToEnd();

            return content;
        }
    }

    /// <summary>
    /// 读取文件并转换为字符串,适合大的文件
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="share">是否允许其他进程读取或写入该文件</param>
    /// <returns></returns>
    public static IEnumerable<String> ReadString(this FileInfo file, Boolean share = false)
    {
        if (file == null)
        {
            throw new ArgumentNullException(nameof(file));
        }

        if (!file.Exists)
        {
            yield return String.Empty;
        }

        if (!share)
        {
            using var reader = file.OpenText();
            yield return reader.ReadToEnd();
        }
        else
        {
            using var fileStream = file.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(fileStream);

            String? line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }

    #endregion

    #region ReadBinary(读取文件并转换为二进制数组)

    /// <summary>
    /// 读取文件并转换为二进制数组
    /// </summary>
    /// <param name="file">文件</param>
    /// <param name="share">是否允许其他进程读取或写入该文件</param>
    /// <returns></returns>
    public static byte[] ReadBinary(this FileInfo file, Boolean share = false)
    {
        if (file == null)
        {
            throw new ArgumentNullException(nameof(file));
        }

        if (file.Exists == false)
        {
            return new byte[0];
        }

        if (!share)
        {
            using var reader = file.OpenRead();
            return reader.ReadAllBytes();
        }
        else
        {
            using var fileStream = file.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return fileStream.ReadAllBytes();
        }
        
    }

    #endregion
}