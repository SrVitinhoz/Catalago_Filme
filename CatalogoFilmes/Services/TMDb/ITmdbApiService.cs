using CatalogoFilmes.Models.Tmdb;

namespace CatalogoFilmes.Services.TMDb;

public interface ITmdbApiService
{
    Task<TmdbSearchResult> SearchMoviesAsync(string query, int page = 1);
    Task<TmdbMovieDetails> GetMovieDetailsAsync(int tmdbId);
    Task<TmdbConfiguration> GetConfigurationAsync();
}