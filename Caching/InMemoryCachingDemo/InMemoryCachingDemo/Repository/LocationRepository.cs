using InMemoryCachingDemo.Data;
using InMemoryCachingDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCachingDemo.Repository
{
    public class LocationRepository
    {
        // ApplicationDbContext instance for interacting with the database.
        private readonly ApplicationDbContext _context;
        // IMemoryCache instance for implementing in-memory caching.
        private readonly IMemoryCache _cache;
        // Cache expiration time set to 30 minutes.
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(30);
        // Constructor to initialize the ApplicationDbContext and IMemoryCache via DI.
        public LocationRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }





        // Retrieves the list of countries, with caching.
        public async Task<List<Country>> GetCountriesAsync()
        {
            // Defines a unique key for caching the countries data.
            var cacheKey = "Countries";
            // Tries to get the value of Countries key from cache.
            if (!_cache.TryGetValue(cacheKey, out List<Country>? countries))
            {
                // If not found in cache, fetch from the database.
                countries = await _context.Countries
                                          .AsNoTracking() // Improves performance for read-only queries
                                          .ToListAsync();
                // Stores the fetched countries in the cache with a 30-minute expiration.
                _cache.Set(cacheKey, countries, _cacheExpiration);
            }
            // Returns the cached or fresh data.
            return countries ?? new List<Country>();
        }
        // Retrieves the list of states for a specific country, with caching.
        public async Task<List<State>> GetStatesAsync(int countryId)
        {
            // Unique cache key for states based on country ID.
            string cacheKey = $"States_{countryId}";
            if (!_cache.TryGetValue(cacheKey, out List<State>? states))
            {
                // Fetch from DB if not cached
                states = await _context.States
                                       .Where(s => s.CountryId == countryId)
                                       .AsNoTracking()
                                       .ToListAsync();
                // Cache the result with a 30-minute expiration.
                _cache.Set(cacheKey, states, _cacheExpiration);
            }
            return states ?? new List<State>();
        }
        // Retrieves the list of cities for a specific state, with caching.
        public async Task<List<City>> GetCitiesAsync(int stateId)
        {
            // Unique cache key for cities based on state ID.
            string cacheKey = $"Cities_{stateId}";
            if (!_cache.TryGetValue(cacheKey, out List<City>? cities))
            {
                // Fetch from DB if not cached
                cities = await _context.Cities
                                       .Where(c => c.StateId == stateId)
                                       .AsNoTracking()
                                       .ToListAsync();
                // Cache the result with a 30-minute expiration.
                _cache.Set(cacheKey, cities, _cacheExpiration);
            }
            return cities ?? new List<City>();
        }

    }
}
