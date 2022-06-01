using Microsoft.AspNetCore.Mvc;
using VeioACalhar.Models;
using VeioACalhar.Repositories;

namespace VeioACalhar.Controllers;

public class ClientesController : Controller
{
    private readonly IClienteRepository clienteRepository;

    public ClientesController(IClienteRepository clienteRepository)
    {
        this.clienteRepository = clienteRepository;
    }

    public IActionResult Index(string? search)
    {
        var clientes = clienteRepository.GetAll();

        if (!string.IsNullOrEmpty(search))
            clientes = clientes.Where(cliente => cliente.Nome.Contains(search, StringComparison.CurrentCultureIgnoreCase)).ToList();
        return View(clientes);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(IFormCollection collection)
    {
        var cliente = new Cliente
        {
            Nome = collection["Nome"],
            Cpf = collection["Cpf"],
            Rg = collection["Rg"],
            Pix = collection["Pix"],
            Email = collection["Email"],
            Telefone = collection["Telefone"],
            Endereco = collection["Endereco"],
            Observacoes = collection["Observacoes"]
        };

        clienteRepository.Create(cliente);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int id)
    {
        var cliente = clienteRepository.Get(id);
        if (cliente.Id > 0)
            return View(cliente);
        return NotFound();
    }


    public IActionResult Edit(int id)
    {
        var cliente = clienteRepository.Get(id);
        if (cliente.Id > 0)
            return View(cliente);
        return NotFound();
    }

    [HttpPost]
    public IActionResult Edit(int id, IFormCollection collection)
    {
        var cliente = new Cliente
        {
            Id = id,
            Nome = collection["Nome"],
            Cpf = collection["Cpf"],
            Rg = collection["Rg"],
            Pix = collection["Pix"],
            Email = collection["Email"],
            Telefone = collection["Telefone"],
            Endereco = collection["Endereco"],
            Observacoes = collection["Observacoes"]
        };

        clienteRepository.Update(cliente);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var cliente = clienteRepository.Get(id);
        if (cliente.Id > 0)
            return View(cliente);
        return NotFound();
    }

    [HttpPost]
    public IActionResult Delete(int id, IFormCollection collection)
    {
        var cliente = new Cliente
        {
            Id = id,
        };

        clienteRepository.Delete(cliente);
        return RedirectToAction(nameof(Index));
    }
}
