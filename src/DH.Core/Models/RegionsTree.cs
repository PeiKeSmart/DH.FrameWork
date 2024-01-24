namespace DH.Models;

/// <summary>
/// 区域树型对象
/// </summary>
public class RegionsTree : TreeBase<RegionsTree, Int64> {
    /// <summary>
    /// 是否父级
    /// </summary>
    public bool isParent { get; set; } = true;

    public RegionsTree()
    {
        open = false;
    }
}