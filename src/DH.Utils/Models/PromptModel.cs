namespace DH.Models;

/// <summary>
/// 提示模型类
/// </summary>
public class PromptModel
{
    /// <summary>
    /// 返回地址
    /// </summary>
    public String BackUrl { get; set; } = "";

    /// <summary>
    /// 提示信息
    /// </summary>
    public String Message { get; set; } = "";

    /// <summary>
    /// 倒计时时间
    /// </summary>
    public Int32 CountdownTime { get; set; } = 3;

    /// <summary>
    /// 是否显示返回地址
    /// </summary>
    public Boolean IsShowBackLink { get; set; } = false;

    /// <summary>
    /// 是否自动返回
    /// </summary>
    public Boolean IsAutoBack { get; set; } = true;

    /// <summary>
    /// 网页标题
    /// </summary>
    public String Title { get; set; } = "";

    /// <summary>
    /// 网站名称
    /// </summary>
    public String WebName { get; set; } = "";

    /// <summary>
    /// 是否成功
    /// </summary>
    public Boolean IsOk { get; set; }
}