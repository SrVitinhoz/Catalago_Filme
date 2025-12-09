using CatalogoFilmes.Services.Cache;
using CatalogoFilmes.Services.Tmdb;
using CatalogoFilmes.Services.TMDb;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoFilmes.Controllers;

public class TmdbController : Controller
{
    private readonly ITmdbService _tmdbService;
    private readonly ICacheService _cache;
    private readonly ILogger<TmdbController> _logger;

    public TmdbController(
        ITmdbService tmdbService,
        ICacheService cache,
        ILogger<TmdbController> logger)
    {
        _tmdbService = tmdbService;
        _cache = cache;
        _logger = logger;
    }

    // /tmdb/search?query=matrix
    [HttpGet]
    public async Task<IActionResult> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return View("Search", null);

        var cacheKey = $"tmdb_search_{query.ToLower()}";
        var cached = _cache.Get<object>(cacheKey);

        if (cached != null)
        {
            _logger.LogInformation("TMDb search HIT cache para query: {Query}", query);
            return View("Search", cached);
        }

        _logger.LogInformation("TMDb search MISS cache para query: {Query}", query);
        var result = await _tmdbService.SearchMoviesAsync(query);

        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(10));

        return View("Search", result);
    }

    // /tmdb/details/123
    [HttpGet("tmdb/details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var cacheKey = $"tmdb_movie_{id}";
        var cached = _cache.Get<object>(cacheKey);

        if (cached != null)
        {
            _logger.LogInformation("TMDb details HIT cache para Id={Id}", id);
            return View("Details", cached);
        }

        _logger.LogInformation("TMDb details MISS cache para Id={Id}", id);
        var movie = await _tmdbService.GetMovieDetailsAsync(id);

        if (movie == null)
            return NotFound();

        _cache.Set(cacheKey, movie, TimeSpan.FromMinutes(60));

        return View("Details", movie);
    }
}
