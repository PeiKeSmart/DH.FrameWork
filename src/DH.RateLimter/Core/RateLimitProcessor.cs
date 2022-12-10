using DH.RateLimter.Store;

using NewLife;

namespace DH.RateLimter;

public class RateLimitProcessor
{
    private readonly IRateLimitStore<RateLimitCounter> _counterStore;

    public RateLimitProcessor(IRateLimitStore<RateLimitCounter> counterStore)
    {
        _counterStore = counterStore;
    }

    /// 用于限制请求的键锁。
    private static readonly AsyncKeyLock AsyncLock = new AsyncKeyLock();

    public virtual async Task<RateLimitCounter> ProcessRequestAsync(String api, String policyValue, Valve valve, CancellationToken cancellationToken = default)
    {
        var counter = new RateLimitCounter
        {
            Timestamp = DateTime.UtcNow,
            Count = 1
        };

        var counterId = BuildCounterKey(api, valve.Policy, valve.PolicyKey, policyValue);

        // 串行读写同一密钥
        using (await AsyncLock.WriterLockAsync(counterId).ConfigureAwait(false))
        {
            RateLimitCounter? entry = await _counterStore.GetAsync(counterId, cancellationToken);

            if (valve is RateValve rateValve)
            {
                if (entry != null)
                {
                    // entry没有过期
                    if (entry.Value.Timestamp.AddSeconds(rateValve.Duration) >= DateTime.UtcNow)
                    {
                        // increment request count
                        var totalCount = entry.Value.Count + 1;

                        // deep copy
                        counter = new RateLimitCounter
                        {
                            Timestamp = entry.Value.Timestamp,
                            Count = totalCount
                        };
                    }
                }
                // stores: id (string) - timestamp (datetime) - total_requests (long)
                await _counterStore.SetAsync(counterId, counter, TimeSpan.FromSeconds(rateValve.Duration), cancellationToken);
            }
        }

        return counter;
    }

    protected virtual string BuildCounterKey(String api, Policy policy, String policyKey, String policyValue)
    {
        var key = $"{UtilSetting.Current.CacheKeyPrefix}:record:{policy.ToString().ToLower()}";
        if (!policyKey.IsNullOrWhiteSpace())
        {
            key += ":" + Common.EncryptMD5Short(policyKey);
        }
        if (!policyValue.IsNullOrWhiteSpace())
        {
            key += ":" + Common.EncryptMD5Short(policyValue);
        }
        key += ":" + api.ToLower();
        return key;
    }
}
