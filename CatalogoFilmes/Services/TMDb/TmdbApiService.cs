using CatalogoFilmes.Config;
using CatalogoFilmes.Models.Tmdb;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace CatalogoFilmes.Services.TMDb;

public class TmdbApiService : ITmdbApiService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;
    private readonly string _baseUrl;

    public TmdbApiService(IOptions<TmdbOptions> options)
    {
        var cfg = options.Value;

        _apiKey = cfg.ApiKey;
        _baseUrl = string.IsNullOrWhiteSpace(cfg.BaseUrl)
            ? "https://api.themoviedb.org/3/"
            : cfg.BaseUrl;

        _http = new HttpClient
        {
            BaseAddress = new Uri(_baseUrl)
        };
    }

    public async Task<TmdbSearchResult> SearchMoviesAsync(string query, int page = 1)
    {
        return await _http.GetFromJsonAsync<TmdbSearchResult>(
            $"search/movie?api_key={_apiKey}&query={query}&page={page}&include_adult=false"
        ) ?? new TmdbSearchResult();
    }

    public async Task<TmdbMovieDetails> GetMovieDetailsAsync(int tmdbId)
    {
        return await _http.GetFromJsonAsync<TmdbMovieDetails>(
            $"movie/{tmdbId}?api_key={_apiKey}&append_to_response=images,credits"
        ) ?? new TmdbMovieDetails();
    }

    public async Task<TmdbConfiguration> GetConfigurationAsync()
    {
        return await _http.GetFromJsonAsync<TmdbConfiguration>(
            $"configuration?api_key={_apiKey}"
        ) ?? new TmdbConfiguration();
    }
}