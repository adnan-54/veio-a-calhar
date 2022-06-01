using Microsoft.AspNetCore.Mvc;

namespace VeioACalhar.Controllers;

public class NotFoundController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}