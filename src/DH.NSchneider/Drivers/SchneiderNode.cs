﻿using NewLife.IoT;
using NewLife.IoT.Drivers;

namespace NewLife.Schneider.Drivers;

/// <summary>
/// 节点
/// </summary>
public class SchneiderNode : INode
{
    /// <summary>主机地址</summary>
    public String Address { get; set; }

    /// <summary>通道</summary>
    public IDriver Driver { get; set; }

    /// <summary>设备</summary>
    public IDevice Device { get; set; }

    /// <summary>参数</summary>
    public IDriverParameter Parameter { get; set; }
}