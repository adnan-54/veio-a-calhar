using Microsoft.AspNetCore.Mvc;

namespace VeioACalhar.Controllers;

public class LoginController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SignIn(string userName, string password)
    {
        return View();
    }
}