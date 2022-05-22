using Microsoft.AspNetCore.Mvc;
using VeioACalhar.Models;
using VeioACalhar.Repositories;

namespace VeioACalhar.Controllers;

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

    [HttpGet]
    public IActionResult Update(int id)
    {
        var cliente = clienteRepository.Get(id);
        return View(cliente);
    }

    [HttpPut]
    public IActionResult Update(Pessoa cliente)
    {
        clienteRepository.Update(cliente);
        return RedirectToAction("Index");
    }

    [HttpDelete]
    public IActionResult Delete(Pessoa cliente)
    {
        clienteRepository.Delete(cliente);
        return RedirectToAction("Index");
    }
}