namespace DH.Models;

public class ResultMsg
{
    /// <summary>
    /// HTTP 状态代码
    /// </summary>
    public int? Status { get; set; } = HttpState.OK;
    /// <summary>
    /// 问题类型的简短、可读的摘要
    /// </summary>
    public string Title { get; set; } = "操作完成";
    /// <summary>
    /// 标识问题类型的 URI 引用
    /// </summary>
    public string Uri { get; set; } = "";

    public string TraceId { get; set; } = Guid.NewGuid().ToString();
    /// <summary>
    /// 此问题特定的可读说明。
    /// </summary>
    public string Message { get; set; } = "";
    /// <summary>
    /// 获取与此实例关联的验证错误
    /// </summary>
    public IDictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    /// <summary>
    /// 当状态不为200时是否提示Title
    /// </summary>
    public bool IsTips { get; set; } = true;
}

public class ResultMsg<T> : ResultMsg
{
    public T Data { get; set; }
}

public static class HttpState
{
    /// <summary>
    /// 请求成功
    /// </summary>
    public const int OK = 200;
    /// <summary>
    /// 请求失败
    /// </summary>
    public const int Error = 406;
    /// <summary>
    /// 重定向
    /// </summary>
    public const int Redirect = 302;
    /// <summary>
    /// 未授权，需要登录
    /// </summary>
    public const int Unauthorized = 401;
    /// <summary>
    /// 拒绝访问，需要登录
    /// </summary>
    public const int Forbidden = 403;
    /// <summary>
    /// 无页面
    /// </summary>
    public const int NotFound = 404;
    /// <summary>
    /// 服务器内部错误
    /// </summary>
    public const int InternaError = 500;

    /// <summary>
    /// 刷新令牌不存在或已过期
    /// </summary>
    public const int RefreshTokenErr = 998;

    /// <summary>
    /// 校验失败
    /// </summary>
    public const int CheckFail = 999;
}