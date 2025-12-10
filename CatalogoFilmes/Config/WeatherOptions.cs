namespace CatalogoFilmes.Config;

public class WeatherOptions
{ 
    public string BaseUrl { get; set; } = "https://api.open-meteo.com/v1/";
    public string GeoUrl { get; set; } = "https://geocoding-api.open-meteo.com/v1/";
}