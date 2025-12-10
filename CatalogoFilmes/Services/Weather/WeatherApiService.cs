using CatalogoFilmes.Config;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using CatalogoFilmes.Models.Weather;

namespace CatalogoFilmes.Services.Weather;

public class WeatherApiService : IWeatherApiService
{
    private readonly HttpClient _http;

    public WeatherApiService(HttpClient http, IOptions<WeatherOptions> options)
    {
        http.BaseAddress = new Uri(options.Value.BaseUrl);
        _http = http;
    }

    public async Task<WeatherResult?> GetWeatherAsync(double latitude, double longitude)
    {
        return await _http.GetFromJsonAsync<WeatherResult>(
            $"forecast?latitude={latitude}&longitude={longitude}&current_weather=true"
        );
    }

    public async Task<GeoResult?> SearchCityAsync(string city)
    {
        return await _http.GetFromJsonAsync<GeoResult>(
            $"search?name={city}&count=1&language=pt&format=json"
        );
    }
}