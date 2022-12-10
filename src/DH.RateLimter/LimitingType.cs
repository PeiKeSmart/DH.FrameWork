namespace DH.RateLimter
{
    public enum LimitingType
    {
        /// <summary>
        /// 令牌桶
        /// </summary>
        TokenBucket,
        /// <summary>
        /// 漏桶
        /// </summary>
        LeakageBucket
    }
}
