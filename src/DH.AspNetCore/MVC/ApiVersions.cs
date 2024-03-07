namespace DH.Swagger;

/// <summary>
/// 接口版本号
/// </summary>
public class ApiVersions : CacheObject
{
    static ApiVersions()
    {
        Init();
    }

    private static void Init()
    {
        var list = cdb.FindAll<ApiVersions>();
        if (list.Count == 0)
        {
            var model = new ApiVersions();
            model.Name = "V1";
            cdb.Insert(model);
        }
    }

    public static List<ApiVersions> GetAll()
    {
        return cdb.FindAll<ApiVersions>();
    }

    /// <summary>
    /// 网关地址
    /// </summary>
    public string Version { get; set; }
}
