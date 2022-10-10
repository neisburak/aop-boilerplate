using System.Text.RegularExpressions;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft;

public class MemoryCacheManager : ICacheManager
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheManager()
    {
        var memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(IMemoryCache));
    }

    public void Add(string key, object data, int duration) => _memoryCache.Set(key, data, TimeSpan.FromMinutes(duration));

    public T Get<T>(string key) => _memoryCache.Get<T>(key);

    public object Get(string key) => _memoryCache.Get(key);

    public bool IsAdded(string key) => _memoryCache.TryGetValue(key, out _);

    public void Remove(string key) => _memoryCache.Remove(key);

    public void RemoveByPattern(string pattern)
    {
        var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        var cacheEntriesCollection = cacheEntriesCollectionDefinition?.GetValue(_memoryCache) as dynamic;

        var cacheCollectionValues = new List<ICacheEntry>();

        foreach (var cacheItem in cacheEntriesCollection!)
        {

            var cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
            cacheCollectionValues.Add(cacheItemValue);
        }

        var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString()!)).Select(d => d.Key).ToList();

        foreach (var key in keysToRemove)
        {
            _memoryCache.Remove(key);
        }
    }
}