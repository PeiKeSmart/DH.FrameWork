using DH.Extensions.Collections;

namespace DH.Helpers.Internal;

/// <summary>
/// 状态码
/// </summary>
public enum StateCode
{
    /// <summary>
    /// 成功
    /// </summary>
    [Text("成功")]
    Ok = 1,

    /// <summary>
    /// 失败
    /// </summary>
    [Text("失败")]
    Fail = 2,

    /// <summary>
    /// 限流繁忙
    /// </summary>
    [Text("限流繁忙")]
    Busy = 99,

    /// <summary>
    /// 请求(或处理)成功
    /// </summary>
    [Text("请求(或处理)成功")]
    Status = 200,  // 请求(或处理)成功

    /// <summary>
    /// 内部请求出错
    /// </summary>
    [Text("内部请求出错")]
    Error = 500,  // 内部请求出错

    /// <summary>
    /// 未授权标识
    /// </summary>
    [Text("未授权标识")]
    Unauthorized = 401, // 未授权标识

    /// <summary>
    /// 请求参数不完整或不正确
    /// </summary>
    [Text("请求参数不完整或不正确")]
    ParameterError = 400, // 请求参数不完整或不正确

    /// <summary>
    /// 请求TOKEN失效
    /// </summary>
    [Text("请求TOKEN失效")]
    TokenInvalid = 403, // 请求TOKEN失效

    /// <summary>
    /// HTTP请求类型不合法
    /// </summary>
    [Text("HTTP请求类型不合法")]
    HttpMehtodError = 405, // HTTP请求类型不合法

    /// <summary>
    /// HTTP请求不合法,请求参数可能被篡改
    /// </summary>
    [Text("HTTP请求不合法,请求参数可能被篡改")]
    HttpRequestError = 406, // HTTP请求不合法

    /// <summary>
    /// 该URL已经失效
    /// </summary>
    [Text("该URL已经失效")]
    URLExpireError = 407, // 该URL已经失效

    /// <summary>
    /// 部分出错
    /// </summary>
    [Text("部分出错")]
    PartialError = 999, // 部分出错
}