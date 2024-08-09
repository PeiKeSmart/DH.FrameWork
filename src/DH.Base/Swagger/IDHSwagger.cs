using DH.AspNetCore;

namespace DH.Swagger;

/// <summary>
/// 表示基于Swagger的接口
/// </summary>
public interface IDHSwagger {
    /// <summary>
    /// 接口版本
    /// </summary>
    ApiVersions ApiVersions { get; set; }

    /// <summary>
    /// 版本说明
    /// </summary>
    String Description { get; set; }
}
