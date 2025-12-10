namespace CatalogoFilmes.Models.Weather;

public class WeatherResult
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public CurrentWeather? Current_Weather { get; set; }
}

public class CurrentWeather
{
    public double Temperature { get; set; }
    public double Windspeed { get; set; }
    public double Winddirection { get; set; }
    public int Weathercode { get; set; }
    public DateTime Time { get; set; }
}