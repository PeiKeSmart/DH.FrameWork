using DH.SLazyCaptcha.Storage.Caches;

namespace DH.SLazyCaptcha.Storage;

public class MemeoryStorage : IStorage {
    private MemoryCache Cache;
    public string StoreageKeyPrefix { get; set; } = string.Empty;

    public MemeoryStorage()
    {
        Cache = MemoryCache.Default;
    }

    private string WrapKey(string key)
    {
        return $"{StoreageKeyPrefix}{key}";
    }

    public string Get(string key)
    {
        return Cache.Get(WrapKey(key));
    }

    public void Remove(string key)
    {
        Cache.Remove(WrapKey(key));
    }

    public void Set(string key, string value, DateTimeOffset absoluteExpiration)
    {
        Cache.Set(WrapKey(key), value, absoluteExpiration);
    }
}