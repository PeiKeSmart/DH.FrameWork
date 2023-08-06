namespace DH.Model;

/// <summary>
/// 树型列表基类
/// </summary>
public class TreeBase<T, TIdValue> {
    /// <summary>
    /// Id编号
    /// </summary>
    public TIdValue id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public String name { get; set; }

    /// <summary>
    /// 树是否默认打开
    /// </summary>
    public Boolean open { get; set; }

    /// <summary>
    /// 父编号
    /// </summary>
    public TIdValue pId { get; set; }

    /// <summary>
    /// 是否禁用checkbox
    /// </summary>
    public Boolean chkDisabled { get; set; }
}