using CatalogoFilmes.Models.Tmdb;
using CatalogoFilmes.Services.TMDb;
using Microsoft.Extensions.Logging;

namespace CatalogoFilmes.Services.Tmdb;

public class TmdbService : ITmdbService
{
    private readonly ITmdbApiService _api;
    private readonly ILogger<TmdbService> _logger;

    public TmdbService(ITmdbApiService api, ILogger<TmdbService> logger)
    {
        _api = api;
        _logger = logger;
    }

    public async Task<TmdbSearchResult?> SearchMoviesAsync(string query)
    {
        _logger.LogInformation("Buscando filmes no TMDb com termo: {Query}", query);
        return await _api.SearchMoviesAsync(query);
    }

    public async Task<TmdbMovieDetails?> GetMovieDetailsAsync(int tmdbId)
    {
        _logger.LogInformation("Buscando detalhes do filme TMDb ID {Id}", tmdbId);
        return await _api.GetMovieDetailsAsync(tmdbId);
    }
}