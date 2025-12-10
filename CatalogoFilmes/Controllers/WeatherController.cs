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
    
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }


    public WeatherController(
        IWeatherService weatherService,
        ICacheService cache,
        ILogger<WeatherController> logger)
    {
        _weatherService = weatherService;
        _cache = cache;
        _logger = logger;
    }

    // ============================================================
    // GET /weather/city?city=porto velho
    // Busca previsão do tempo por nome da cidade
    // ============================================================
    [HttpGet("city")]
    public async Task<IActionResult> GetByCity([FromQuery] string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            return BadRequest(new { error = "Cidade inválida." });

        var cacheKey = $"weather_city_{city.ToLower()}";

        // Tenta recuperar do cache
        var cached = _cache.Get<object>(cacheKey);
        if (cached != null)
        {
            _logger.LogInformation("Weather HIT — cidade: {City}", city);
            return Ok(cached);
        }

        _logger.LogInformation("Weather MISS — cidade: {City}", city);

        var result = await _weatherService.GetWeatherByCityAsync(city);

        if (result == null)
            return NotFound(new { error = $"Não foi possível obter o clima para '{city}'." });

        // Salvar no cache por 30 min
        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(30));

        return Ok(result);
    }

    // ============================================================
    // GET /weather/coords?lat=-10.90&lon=-61.90
    // Busca previsão do tempo por coordenadas
    // ============================================================
    [HttpGet("coords")]
    public async Task<IActionResult> GetByCoordinates(
        [FromQuery] double lat,
        [FromQuery] double lon)
    {
        if (lat == 0 || lon == 0)
            return BadRequest(new { error = "Latitude e longitude são obrigatórias." });

        var cacheKey = $"weather_coords_{lat}_{lon}";

        // Cache HIT
        var cached = _cache.Get<object>(cacheKey);
        if (cached != null)
        {
            _logger.LogInformation("Weather HIT — coords: {Lat}, {Lon}", lat, lon);
            return Ok(cached);
        }

        _logger.LogInformation("Weather MISS — coords: {Lat}, {Lon}", lat, lon);

        var result = await _weatherService.GetWeatherByCoordinatesAsync(lat, lon);

        if (result == null)
            return NotFound(new { error = "Não foi possível obter o clima para as coordenadas informadas." });

        // Salvar no cache por 30 min
        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(30));

        return Ok(result);
    }
}
