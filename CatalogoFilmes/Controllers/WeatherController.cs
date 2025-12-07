using Microsoft.AspNetCore.Mvc;

namespace CatalogoFilmes.Controllers;

public class WeatherController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}