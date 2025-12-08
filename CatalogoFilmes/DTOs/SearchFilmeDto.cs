namespace CatalogoFilmes.DTOs;

public class SearchFilmeDto
{
    public string Query { get; set; } = "";
    public int Page { get; set; } = 1;
}