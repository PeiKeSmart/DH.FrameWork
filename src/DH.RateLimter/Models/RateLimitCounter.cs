using System;

namespace DH.RateLimter
{
    /// <summary>
    /// 存储初始访问时间和从该点开始的呼叫次数
    /// </summary>
    public struct RateLimitCounter
    {
        public DateTime Timestamp { get; set; }

        public double Count { get; set; }
    }
}
