using Microsoft.AspNetCore.Mvc;

namespace VeioACalhar.Controllers;

public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}