﻿using EasyNotice.Models;

namespace EasyNotice;

public interface IEasyNotice {
    /// <summary>
    /// 推送异常消息
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="ex">异常</param>
    /// <returns></returns>
    Task<EasyNoticeSendResponse> SendAsync(string title, Exception ex);

    /// <summary>
    /// 推送正常消息
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="message">消息内容</param>
    /// <returns></returns>
    Task<EasyNoticeSendResponse> SendAsync(string title, string message);
}