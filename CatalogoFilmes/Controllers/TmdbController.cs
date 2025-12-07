using Microsoft.AspNetCore.Mvc;

namespace CatalogoFilmes.Controllers;

public class TmdbController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}