using CatalogoFilmes.Services.Cache;
using CatalogoFilmes.Services.Weather;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoFilmes.Controllers;

[Route("weather")]
public class WeatherController : Controller
{
    private readonly IWeatherService _weatherService;
    private readonly ICacheService _cache;
    private readonly ILogger<WeatherController> _logger;

    public WeatherController(
        IWeatherService weatherService,
        ICacheService cache,
        ILogger<WeatherController> logger)
    {
        _weatherService = weatherService;
        _cache = cache;
        _logger = logger;
    }

    /// <summary>
    /// GET /weather/city?city=porto velho
    /// Busca previsão do tempo por nome da cidade
    /// </summary>
    [HttpGet("city")]
    public async Task<IActionResult> GetByCity([FromQuery] string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            return BadRequest("Cidade inválida.");

        var cacheKey = $"weather_city_{city.ToLower()}";

        // Tenta pegar do cache
        var cached = _cache.Get<object>(cacheKey);
        if (cached != null)
        {
            _logger.LogInformation("Weather HIT cache para cidade: {City}", city);
            return Ok(cached);
        }

        // MISS
        _logger.LogInformation("Weather MISS cache para cidade: {City}", city);
        var result = await _weatherService.GetWeatherByCityAsync(city);

        if (result == null)
            return NotFound($"Não foi possível obter o clima para {city}");

        // Salva no cache (30 minutos)
        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(30));

        return Ok(result);
    }

    /// <summary>
    /// GET /weather/coords?lat=-10.90&lon=-61.90
    /// Busca previsão por coordenadas
    /// </summary>
    [HttpGet("coords")]
    public async Task<IActionResult> GetByCoordinates([FromQuery] double lat, [FromQuery] double lon)
    {
        if (lat == 0 || lon == 0)
            return BadRequest("Latitude e longitude são obrigatórias.");

        var cacheKey = $"weather_coords_{lat}_{lon}";

        // Tenta pegar do cache
        var cached = _cache.Get<object>(cacheKey);
        if (cached != null)
        {
            _logger.LogInformation("Weather HIT cache para coords: {Lat}, {Lon}", lat, lon);
            return Ok(cached);
        }

        // MISS
        _logger.LogInformation("Weather MISS cache para coords: {Lat}, {Lon}", lat, lon);
        var result = await _weatherService.GetWeatherByCoordinatesAsync(lat, lon);

        if (result == null)
            return NotFound("Não foi possível obter o clima para essas coordenadas.");

        // Salva no cache
        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(30));

        return Ok(result);
    }
}
