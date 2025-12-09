namespace CatalogoFilmes.DTOs;

public class ImportFilmeDto
{
    public int TmdbId { get; set; }
    public string Titulo { get; set; } = "";
    public string? Sinopse { get; set; }
    public string? PosterPath { get; set; }
    public string? DataLancamento { get; set; }
    public string? Generos { get; set; }
    public string? Lingua { get; set; }
    public int? Duracao { get; set; }
    public double? NotaMedia { get; set; }
    public string? ElencoPrincipal { get; set; }
}