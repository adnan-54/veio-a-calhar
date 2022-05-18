using Microsoft.AspNetCore.Mvc;

namespace VeioACalhar.Controllers;

public class RegisterController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}