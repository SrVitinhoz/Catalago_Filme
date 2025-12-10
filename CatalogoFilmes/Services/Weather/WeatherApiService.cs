using CatalogoFilmes.Config;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using CatalogoFilmes.Models.Weather;

namespace CatalogoFilmes.Services.Weather;

public class WeatherApiService : IWeatherApiService
{
    private readonly HttpClient _http;
    private readonly WeatherOptions _options;

    public WeatherApiService(HttpClient http, IOptions<WeatherOptions> options)
    {
        _http = http;
        _options = options.Value;
    }

    public async Task<WeatherResult?> GetWeatherAsync(double latitude, double longitude)
    {
        var url = $"{_options.BaseUrl}forecast?latitude={latitude}&longitude={longitude}&current_weather=true";
        return await _http.GetFromJsonAsync<WeatherResult>(url);
    }

    public async Task<GeoResult?> SearchCityAsync(string city)
    {
        var url = $"{_options.GeoUrl}search?name={city}&count=1&language=pt&format=json";
        return await _http.GetFromJsonAsync<GeoResult>(url);
    }
}