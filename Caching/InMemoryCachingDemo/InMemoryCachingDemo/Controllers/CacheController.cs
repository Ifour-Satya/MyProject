using InMemoryCachingDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCachingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        // Service to manage cache keys and operations.
        private readonly CacheManager _cacheManager;
        // Direct reference to IMemoryCache for retrieving cached values.
        private readonly IMemoryCache _memoryCache;
        // Constructor with dependency injection.
        public CacheController(CacheManager cacheManager, IMemoryCache memoryCache)
        {
            _cacheManager = cacheManager;
            _memoryCache = memoryCache;
        }
        // Retrieves all active cache entries with their keys and values.
        // GET /api/cache/all
        [HttpGet("All")]
        public IActionResult GetAllCacheEntries()
        {
            var cacheEntries = new List<object>();
            // Get all tracked cache keys.
            var keys = _cacheManager.GetAllKeys();
            // Iterate through each key to retrieve its cached value.
            foreach (var key in keys)
            {
                if (_memoryCache.TryGetValue(key, out object? value))
                {
                    // Add the key-value pair to the result list.
                    cacheEntries.Add(new { Key = key, Value = value });
                }
            }
            return Ok(cacheEntries);
        }
        // Retrieves a specific key's value
        // GET /api/cache/{key}
        [HttpGet("{key}")]
        public IActionResult GetCacheEntry(string key)
        {
            // Attempt to get the value from IMemoryCache
            if (_memoryCache.TryGetValue(key, out object? value))
            {
                return Ok(new { Key = key, Value = value });
            }
            return NotFound(new { Message = $"Cache key '{key}' not found." });
        }
        // Clears ALL cache entries
        // DELETE /api/cache/clearall
        [HttpDelete("ClearAll")]
        public IActionResult ClearAllCaches()
        {
            _cacheManager.Clear();
            return Ok(new { Message = "All cache entries have been cleared." });
        }
        // Clears a specific cache entry
        // DELETE /api/cache/{key}
        [HttpDelete("{key}")]
        public IActionResult ClearCacheByKey(string key)
        {
            // Check if the key is being tracked
            if (_cacheManager.GetAllKeys().Contains(key))
            {
                _cacheManager.Remove(key);
                return Ok(new { Message = $"Cache entry '{key}' has been cleared." });
            }
            else
            {
                return NotFound(new { Message = $"Cache key '{key}' not found." });
            }
        }
    }
}
