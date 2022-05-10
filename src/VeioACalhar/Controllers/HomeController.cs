using Microsoft.AspNetCore.Mvc;
using VeioACalhar.Services;

namespace VeioACalhar.Controllers;

public class HomeController : Controller
{
    private readonly ICargoRepository cargoRepository;

    public HomeController(ICargoRepository cargoRepository)
    {
        this.cargoRepository = cargoRepository;
    }

    public IActionResult Index()
    {
        var cargo = cargoRepository.Create(new() { Nome = "Teste" });

        return View();
    }
}