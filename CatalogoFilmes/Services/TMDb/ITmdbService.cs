using CatalogoFilmes.Models.Tmdb;

namespace CatalogoFilmes.Services.Tmdb;

public interface ITmdbService
{
    Task<TmdbSearchResult?> SearchMoviesAsync(string query);
    Task<TmdbMovieDetails?> GetMovieDetailsAsync(int tmdbId);
}