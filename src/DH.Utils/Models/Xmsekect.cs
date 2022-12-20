namespace DH.Models;

/// <summary>
/// 下拉选项
/// </summary>
public class Xmsekect<TModel>
{
    public string name { get; set; }

    public TModel value { get; set; }

    public bool disabled { get; set; } = false;
}