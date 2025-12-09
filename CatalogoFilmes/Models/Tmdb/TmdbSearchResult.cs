namespace CatalogoFilmes.Models.Tmdb;

public class TmdbSearchResult
{
    public int Page { get; set; }
    public List<TmdbSearchMovieItem> Results { get; set; } = new();
    public int TotalPages { get; set; }
    public int TotalResults { get; set; }
}

public class TmdbSearchMovieItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Original_Title { get; set; } = "";
    public string Overview { get; set; } = "";
    public string? Release_Date { get; set; }
    public string? Poster_Path { get; set; }
    public double Vote_Average { get; set; }
    public string Original_Language { get; set; } = "";
}