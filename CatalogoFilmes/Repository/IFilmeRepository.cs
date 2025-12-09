using CatalogoFilmes.Models;

namespace CatalogoFilmes.Repository;

public interface IFilmeRepository
{
    Task<IEnumerable<Filme>> ListAsync();
    Task<Filme?> GetByIdAsync(int id);
    Task<int> CreateAsync(Filme filme);
    Task<bool> UpdateAsync(Filme filme);
    Task<bool> DeleteAsync(int id);
}