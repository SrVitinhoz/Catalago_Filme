using CatalogoFilmes.Models;
using CatalogoFilmes.Models.Weather;

namespace CatalogoFilmes.Services.Weather
{
    public interface IWeatherService
    {
        Task<WeatherInfo?> GetWeatherByCityAsync(string city);
        Task<WeatherInfo?> GetWeatherByCoordinatesAsync(double lat, double lon);
    }
}