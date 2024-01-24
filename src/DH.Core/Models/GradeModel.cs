namespace DH.Models;

/// <summary>
/// 会员等级实体
/// </summary>
public class GradeModel
{
    /// <summary>
    /// 等级标识
    /// </summary>
    public Int32 level { get; set; }

    /// <summary>
    /// 等级名称
    /// </summary>
    public String level_name { get; set; }

    /// <summary>
    /// 等级经验值
    /// </summary>
    public Int32 exppoints { get; set; }
}