using CatalogoFilmes.Models;
using CatalogoFilmes.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoFilmes.Controllers;

public class FilmesController : Controller
{
    private readonly IFilmeRepository _repo;

    public FilmesController(IFilmeRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        var filmes = await _repo.ListAsync();
        return View(filmes);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Filme filme)
    {
        if (!ModelState.IsValid)
            return View(filme);

        await _repo.CreateAsync(filme);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var filme = await _repo.GetByIdAsync(id);
        if (filme == null) return NotFound();

        return View(filme);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var filme = await _repo.GetByIdAsync(id);
        if (filme == null) return NotFound();

        return View(filme);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Filme filme)
    {
        if (!ModelState.IsValid)
            return View(filme);

        await _repo.UpdateAsync(filme);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var filme = await _repo.GetByIdAsync(id);
        if (filme == null) return NotFound();

        return View(filme);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repo.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}