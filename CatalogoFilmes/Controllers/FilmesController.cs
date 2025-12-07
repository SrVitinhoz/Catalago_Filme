using Microsoft.AspNetCore.Mvc;

namespace CatalogoFilmes.Controllers;

public class FilmesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}