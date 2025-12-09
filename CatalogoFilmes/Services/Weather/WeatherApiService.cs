using CatalogoFilmes.Config;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using CatalogoFilmes.Models.Weather;

namespace CatalogoFilmes.Services.Weather;

public class WeatherApiService : IWeatherApiService
{
    private readonly HttpClient _http;
    private readonly string _baseUrl;

    public WeatherApiService(IOptions<WeatherOptions> options)
    {
        var cfg = options.Value;
        _baseUrl = cfg.BaseUrl ?? "https://api.open-meteo.com/v1/";

        _http = new HttpClient
        {
            BaseAddress = new Uri(_baseUrl)
        };
    }

    public async Task<WeatherResult?> GetWeatherAsync(double latitude, double longitude)
    {
        return await _http.GetFromJsonAsync<WeatherResult>(
            $"forecast?latitude={latitude}&longitude={longitude}&current_weather=true"
        );
    }
}