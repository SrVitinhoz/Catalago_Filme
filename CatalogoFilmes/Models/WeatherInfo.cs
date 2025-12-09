namespace CatalogoFilmes.Models;

public class WeatherInfo
{
    public string? City { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public double Humidity { get; set; }

    public DateTime RetrievedAt { get; set; } = DateTime.UtcNow;
}