using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using CatalogoFilmes.Config;

namespace CatalogoFilmes.Services.Cache;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private readonly CacheOptions _options;

    public MemoryCacheService(IMemoryCache cache, IOptions<CacheOptions> options)
    {
        _cache = cache;
        _options = options.Value;
    }

    public T? Get<T>(string key)
    {
        if (!_options.Enabled) return default;

        key = _options.KeyPrefix + key;

        return _cache.TryGetValue(key, out T? value) ? value : default;
    }

    public void Set<T>(string key, T value, TimeSpan? expiration = null)
    {
        if (!_options.Enabled) return;

        var expirationToUse = expiration ??
                              TimeSpan.FromSeconds(_options.DefaultExpirationSeconds);

        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expirationToUse
        };

        key = _options.KeyPrefix + key;

        _cache.Set(key, value, options);
    }

    public void Remove(string key)
    {
        if (!_options.Enabled) return;

        key = _options.KeyPrefix + key;

        _cache.Remove(key);
    }
}