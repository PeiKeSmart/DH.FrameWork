namespace DH.Model;

public class LocationVM {
    /// <summary>
    /// 链接
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 是否最后一个
    /// </summary>
    public bool IsLast { get; set; } = false;

    /// <summary>
    /// 是否第一个
    /// </summary>
    public Boolean IsFirst { get; set; } = false;
}