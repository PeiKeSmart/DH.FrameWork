using System;

namespace DH.RateLimter
{
    /// <summary>
    /// 阀门
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class Valve : Attribute
    {
        /// <summary>
        /// 策略
        /// </summary>
        public Policy Policy { set; get; } = Policy.Ip;

        /// <summary>
        /// 策略Key
        /// </summary>
        /// <remarks>
        /// Policy == Policy.Header是，PolicyKey指定为对应Header的key
        /// Policy == Policy.Query是，PolicyKey指定为对应Query的key
        /// </remarks>
        public string PolicyKey { set; get; }

        /// <summary>
        /// 当识别值为空时处理方式
        /// </summary>
        public WhenNull WhenNull { set; get; } = WhenNull.Pass;

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { set; get; }

        /// <summary>
        /// 返回数据格式
        /// </summary>
        public ReturnType ReturnType { get; set; } = ReturnType.Json_DResult;
    }

    /// <summary>
    /// 频率阀门
    /// </summary>
    public class RateValve : Valve
    {
        /// <summary>
        /// 限制次数
        /// </summary>
        public int Limit { set; get; } = 1;

        /// <summary>
        /// 计时间隔(单位：秒)
        /// </summary>
        public int Duration { set; get; } = 60;

    }

    /// <summary>
    /// 名册阀门
    /// </summary>
    public abstract class RosterValve : Valve
    {
    }

    /// <summary>
    /// 黑名单阀门
    /// </summary>
    public class BlackListValve : RosterValve
    {

    }

    /// <summary>
    /// 白名单阀门
    /// </summary>
    public class WhiteListValve : RosterValve
    {

    }
}
