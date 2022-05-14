using Microsoft.AspNetCore.Mvc;
using VeioACalhar.Models;
using VeioACalhar.Repositories;

namespace VeioACalhar.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}