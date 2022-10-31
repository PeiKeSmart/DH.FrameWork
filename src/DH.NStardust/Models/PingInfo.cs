﻿using System;

namespace Stardust.Models
{
    /// <summary>心跳信息</summary>
    public class PingInfo
    {
        #region 属性
        /// <summary>可用内存大小</summary>
        public UInt64 AvailableMemory { get; set; }

        /// <summary>磁盘可用空间。应用所在盘</summary>
        public UInt64 AvailableFreeSpace { get; set; }

        /// <summary>主频</summary>
        public Single CpuRate { get; set; }

        /// <summary>温度</summary>
        public Double Temperature { get; set; }

        /// <summary>电量</summary>
        public Double Battery { get; set; }

        /// <summary>上行速度。网络发送速度，字节每秒</summary>
        public UInt64 UplinkSpeed { get; set; }

        /// <summary>下行速度。网络接收速度，字节每秒</summary>
        public UInt64 DownlinkSpeed { get; set; }

        /// <summary>MAC地址</summary>
        public String Macs { get; set; }

        ///// <summary>串口</summary>
        //public String COMs { get; set; }

        /// <summary>本地IP地址。随着网卡变动，可能改变</summary>
        public String IP { get; set; }

        /// <summary>进程列表</summary>
        public String Processes { get; set; }

        /// <summary>进程个数</summary>
        public Int32 ProcessCount { get; set; }

        /// <summary>正在传输的Tcp连接数</summary>
        public Int32 TcpConnections { get; set; }

        /// <summary>主动关闭的Tcp连接数</summary>
        public Int32 TcpTimeWait { get; set; }

        /// <summary>被动关闭的Tcp连接数</summary>
        public Int32 TcpCloseWait { get; set; }

        /// <summary>开机时间，单位s</summary>
        public Int32 Uptime { get; set; }

        /// <summary>本地UTC时间。ms毫秒</summary>
        public Int64 Time { get; set; }

        /// <summary>延迟</summary>
        public Int32 Delay { get; set; }
        #endregion
    }

    /// <summary>心跳响应</summary>
    public class PingResponse
    {
        /// <summary>本地时间。ms毫秒</summary>
        public Int64 Time { get; set; }

        /// <summary>服务器时间</summary>
        public DateTime ServerTime { get; set; }

        /// <summary>采样周期。单位秒</summary>
        public Int32 Period { get; set; }

        /// <summary>令牌。现有令牌即将过期时，颁发新的令牌</summary>
        public String Token { get; set; }

        ///// <summary>下发命令</summary>
        //public CommandModel[] Commands { get; set; }

        ///// <summary>下发应用服务</summary>
        //public ServiceInfo[] Services { get; set; }
    }
}