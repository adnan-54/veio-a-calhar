using Microsoft.AspNetCore.Mvc;

namespace VeioACalhar.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
