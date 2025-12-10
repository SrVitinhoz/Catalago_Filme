namespace CatalogoFilmes.DTOs;

public class WeatherDto
{
    public string? City { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public double WindDirection { get; set; }
    public int WeatherCode { get; set; }
    public DateTime Time { get; set; }
}