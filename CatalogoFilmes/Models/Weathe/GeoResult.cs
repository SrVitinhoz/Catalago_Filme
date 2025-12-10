namespace CatalogoFilmes.Models.Weather;

public class GeoResult
{
    public GeoResultItem[]? Results { get; set; }
}

public class GeoResultItem
{
    public string? Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}