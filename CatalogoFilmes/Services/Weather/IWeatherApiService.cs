using CatalogoFilmes.Models.Weather;

namespace CatalogoFilmes.Services.Weather;

public interface IWeatherApiService
{
    Task<WeatherResult?> GetWeatherAsync(double latitude, double longitude);
}