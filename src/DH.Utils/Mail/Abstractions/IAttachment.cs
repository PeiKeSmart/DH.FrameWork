﻿namespace DH.Mail.Abstractions;

/// <summary>
/// 附件
/// </summary>
public interface IAttachment : IDisposable
{
    /// <summary>
    /// 获取文件流
    /// </summary>
    /// <returns></returns>
    Stream GetFileStream();

    /// <summary>
    /// 获取文件名称
    /// </summary>
    /// <returns></returns>
    string GetName();
}