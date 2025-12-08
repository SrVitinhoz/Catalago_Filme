using CatalogoFilmes.Models;
using Dapper;
using CatalogoFilmes.Data;
using CatalogoFilmes.Models;
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
        var query = @"SELECT * FROM Filmes ORDER BY Id DESC";
        using var conn = _context.CreateConnection();
        return await conn.QueryAsync<Filme>(query);
    }

    public async Task<Filme?> GetByIdAsync(int id)
    {
        var query = @"SELECT * FROM Filmes WHERE Id = @Id";
        using var conn = _context.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Filme>(query, new { Id = id });
    }

    public async Task CreateAsync(Filme filme)
    {
        var query = @"
            INSERT INTO Filmes (
                TmdbId, Titulo, TituloOriginal, Sinopse, DataLancamento,
                Genero, PosterPath, Lingua, Duracao, NotaMedia, ElencoPrincipal,
                CidadeReferencia, Latitude, Longitude, DataCriacao, DataAtualizacao
            ) VALUES (
                @TmdbId, @Titulo, @TituloOriginal, @Sinopse, @DataLancamento,
                @Genero, @PosterPath, @Lingua, @Duracao, @NotaMedia, @ElencoPrincipal,
                @CidadeReferencia, @Latitude, @Longitude, @DataCriacao, @DataAtualizacao
            );
        ";

        filme.DataCriacao = DateTime.UtcNow;
        filme.DataAtualizacao = DateTime.UtcNow;

        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(query, filme);
    }

    public async Task UpdateAsync(Filme filme)
    {
        var query = @"
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
            WHERE Id = @Id;
        ";

        filme.DataAtualizacao = DateTime.UtcNow;

        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(query, filme);
    }

    public async Task DeleteAsync(int id)
    {
        var query = @"DELETE FROM Filmes WHERE Id = @Id";
        using var conn = _context.CreateConnection();
        await conn.ExecuteAsync(query, new { Id = id });
    }
}