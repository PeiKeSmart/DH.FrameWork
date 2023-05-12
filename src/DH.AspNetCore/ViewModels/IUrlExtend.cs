using NewLife.Data;

namespace DH.ViewModels;

/// <summary>Url扩展</summary>
public interface IUrlExtend {
    /// <summary>解析Url地址</summary>
    /// <param name="field"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    String Resolve(DataField field, IModel data);
}