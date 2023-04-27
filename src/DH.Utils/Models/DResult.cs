namespace DH.Models;

public class DResult
{
    public bool success { get; set; }

    public string msg { get; set; }

    /// <summary>
    /// 状态
    /// <para>1表成功</para>
    /// </summary>
    public int status { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    public object data { get; set; }

    /// <summary>
    /// 附加数据
    /// </summary>
    public object extdata { get; set; }

    public int code { get; set; }

    /// <summary>
    /// 网址路径
    /// </summary>
    public string locate { get; set; }
}

public class DResult<T> : DResult
{
    public T TData { get; set; }
}