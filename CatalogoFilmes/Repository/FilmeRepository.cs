using CatalogoFilmes.Models;
using Dapper;
using CatalogoFilmes.Data;
using CatalogoFilmes.Repository;

namespace CatalogoFilmes;

public class FilmeRepository : IFilmeRepository
{
    private readonly DapperContext _context;

    public FilmeRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Filme>> ListAsync()
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<Filme>("SELECT * FROM Filmes ORDER BY Id DESC");
    }

    public async Task<Filme?> GetByIdAsync(int id)
    {
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Filme>(
            "SELECT * FROM Filmes WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> CreateAsync(Filme filme)
    {
        var sql = @"
INSERT INTO Filmes (
    TmdbId, Titulo, TituloOriginal, Sinopse, DataLancamento, Genero,
    PosterPath, Lingua, Duracao, NotaMedia, ElencoPrincipal,
    CidadeReferencia, Latitude, Longitude, DataCriacao, DataAtualizacao
)
VALUES (
    @TmdbId, @Titulo, @TituloOriginal, @Sinopse, @DataLancamento, @Genero,
    @PosterPath, @Lingua, @Duracao, @NotaMedia, @ElencoPrincipal,
    @CidadeReferencia, @Latitude, @Longitude, @DataCriacao, @DataAtualizacao
);
SELECT last_insert_rowid();
";

        using var conn = _context.CreateConnection();
        return await conn.ExecuteScalarAsync<int>(sql, filme);
    }

    public async Task<bool> UpdateAsync(Filme filme)
    {
        filme.DataAtualizacao = DateTime.UtcNow;

        var sql = @"
UPDATE Filmes SET
    Titulo = @Titulo,
    TituloOriginal = @TituloOriginal,
    Sinopse = @Sinopse,
    DataLancamento = @DataLancamento,
    Genero = @Genero,
    PosterPath = @PosterPath,
    Lingua = @Lingua,
    Duracao = @Duracao,
    NotaMedia = @NotaMedia,
    ElencoPrincipal = @ElencoPrincipal,
    CidadeReferencia = @CidadeReferencia,
    Latitude = @Latitude,
    Longitude = @Longitude,
    DataAtualizacao = @DataAtualizacao
WHERE Id = @Id
";

        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(sql, filme) > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var conn = _context.CreateConnection();
        return await conn.ExecuteAsync(
            "DELETE FROM Filmes WHERE Id = @Id", new { Id = id }) > 0;
    }
}
