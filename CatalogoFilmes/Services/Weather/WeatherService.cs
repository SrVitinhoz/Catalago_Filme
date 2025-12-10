using CatalogoFilmes.Models;
using CatalogoFilmes.Models.Weather;
using Microsoft.Extensions.Logging;

namespace CatalogoFilmes.Services.Weather;

public class WeatherService : IWeatherService
{
    private readonly IWeatherApiService _api;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(IWeatherApiService api, ILogger<WeatherService> logger)
    {
        _api = api;
        _logger = logger;
    }

    public async Task<WeatherInfo?> GetWeatherByCoordinatesAsync(double lat, double lon)
    {
        _logger.LogInformation("Consultando clima por coordenadas: {lat}, {lon}", lat, lon);

        var result = await _api.GetWeatherAsync(lat, lon);
        if (result == null || result.Current_Weather == null)
            return null;

        return new WeatherInfo
        {
            Latitude = result.Latitude,
            Longitude = result.Longitude,
            Temperature = result.Current_Weather.Temperature,
            WindSpeed = result.Current_Weather.Windspeed,
            Humidity = 0, // API gratuita não oferece
            RetrievedAt = DateTime.UtcNow
        };
    }

    public async Task<WeatherInfo?> GetWeatherByCityAsync(string city)
    {
        _logger.LogInformation("Consultando clima para cidade: {city}", city);

        var geo = await _api.SearchCityAsync(city);
        var item = geo?.Results?.FirstOrDefault();
        if (item == null)
            return null;

        return await GetWeatherByCoordinatesAsync(item.Latitude, item.Longitude);
    }
}