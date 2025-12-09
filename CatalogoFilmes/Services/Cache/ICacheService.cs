using Microsoft.Extensions.Caching.Memory;

namespace CatalogoFilmes.Services.Cache;

public interface ICacheService
{
    T? Get<T>(string key);
    void Set<T>(string key, T value, TimeSpan? expiration = null);
    void Remove(string key);
}