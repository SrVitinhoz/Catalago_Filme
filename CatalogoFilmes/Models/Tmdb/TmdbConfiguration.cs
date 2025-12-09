namespace CatalogoFilmes.Models.Tmdb;

public class TmdbConfiguration
{
    public TmdbImagesConfig Images { get; set; } = new();
}

public class TmdbImagesConfig
{
    public string Base_Url { get; set; } = "";
    public string Secure_Base_Url { get; set; } = "";
    public List<string> Poster_Sizes { get; set; } = new();
}