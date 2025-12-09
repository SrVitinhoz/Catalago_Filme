/*using CatalogoFilmes.Models;
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
        _logger.LogInformation("Clima por coordenadas: lat={Lat}, lon={Lon}", lat, lon);

        var result = await _api.GetWeatherAsync(lat, lon);
        if (result == null || result.Current == null)
            return null;

        return new WeatherInfo
        {
            Latitude = result.Latitude,
            Longitude = result.Longitude,
            Temperature = result.Current.Temperature,
            WindSpeed = result.Current.WindSpeed,
            WeatherCode = result.Current.WeatherCode,
            Time = result.Current.Time
        };
    }

    public async Task<WeatherInfo?> GetWeatherByCityAsync(string city)
    {
        _logger.LogInformation("Clima por cidade: {City}", city);

        // usa o geocoder do próprio Open-Meteo
        var geocode = await _api.SearchCityAsync(city);
        if (geocode == null)
            return null;

        return await GetWeatherByCoordinatesAsync(geocode.Latitude, geocode.Longitude);
    }
}*/