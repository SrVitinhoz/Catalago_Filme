namespace CatalogoFilmes.Models.Tmdb;

public class TmdbMovieDetails
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Original_Title { get; set; } = "";
    public string Overview { get; set; } = "";
    public string Original_Language { get; set; } = "";
    public string? Release_Date { get; set; }
    public string? Poster_Path { get; set; }
    public double Vote_Average { get; set; }
    public int Runtime { get; set; }

    public List<TmdbGenre> Genres { get; set; } = new();
    public TmdbCredits Credits { get; set; } = new();
}

public class TmdbGenre
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

public class TmdbCredits
{
    public List<TmdbCast> Cast { get; set; } = new();
}

public class TmdbCast
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Character { get; set; } = "";
    public string? Profile_Path { get; set; }
}