using Microsoft.AspNetCore.Mvc;
using VeioACalhar.Services;

namespace VeioACalhar.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}