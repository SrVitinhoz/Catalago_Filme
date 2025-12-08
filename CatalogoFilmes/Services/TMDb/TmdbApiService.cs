using CatalogoFilmes.Config;
using CatalogoFilmes.Models.Tmdb;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace CatalogoFilmes.Services.TMDb;

public class TmdbApiService : ITmdbApiService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    public TmdbApiService(IOptions<TmdbOptions> options)
    {
        _apiKey = options.Value.ApiKey;

        _http = new HttpClient
        {
            BaseAddress = new Uri("https://api.themoviedb.org/3/")
        };
    }

    public async Task<TmdbSearchResult> SearchMoviesAsync(string query, int page = 1)
    {
        var result = await _http.GetFromJsonAsync<TmdbSearchResult>(
            $"search/movie?api_key={_apiKey}&query={query}&page={page}&include_adult=false"
        );

        return result ?? new TmdbSearchResult();
    }

    public async Task<TmdbMovieDetails> GetMovieDetailsAsync(int tmdbId)
    {
        var result = await _http.GetFromJsonAsync<TmdbMovieDetails>(
            $"movie/{tmdbId}?api_key={_apiKey}&append_to_response=images,credits"
        );

        return result ?? new TmdbMovieDetails();
    }

    public async Task<TmdbConfiguration> GetConfigurationAsync()
    {
        var result = await _http.GetFromJsonAsync<TmdbConfiguration>(
            $"configuration?api_key={_apiKey}"
        );

        return result ?? new TmdbConfiguration();
    }
}