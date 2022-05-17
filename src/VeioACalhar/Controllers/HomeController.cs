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

public class ClienteController : Controller
{
    private readonly IClienteRepository clienteRepository;

    public ClienteController(IClienteRepository clienteRepository)
    {
        this.clienteRepository = clienteRepository;
    }

    public IActionResult Index()
    {
        var clientes = clienteRepository.GetAll();
            return View(clientes);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Pessoa cliente)
    {
        clienteRepository.Create(cliente);
        return RedirectToAction("Index");
    }

}