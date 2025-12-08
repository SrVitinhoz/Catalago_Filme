using CatalogoFilmes.Models;

namespace CatalogoFilmes.Repository;

public class IFilmeRepository
{
    Task<IEnumerable<Filme>> ListAsync();
    Task<Filme?> GetByIdAsync(int id);
    Task CreateAsync(Filme filme);
    Task UpdateAsync(Filme filme);
    Task DeleteAsync(int id);
}