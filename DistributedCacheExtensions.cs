using Microsoft.Extensions.Caching.Distributed;

namespace weatherbot
{
    public static class DistributedCacheExtensions
    {
        public static async Task<string> GetOrCreateAsync(this IDistributedCache cache, string key, Func<string> callback, CancellationToken cancellationToken = default)
        {
            var value = callback?.Invoke();
            await cache.SetStringAsync(key, value, cancellationToken);
            return value;
        }
    }
}