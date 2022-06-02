using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeioACalhar.Models;
using VeioACalhar.Repositories;
using VeioACalhar.Services;

namespace VeioACalhar.Controllers;

public class FuncionariosController : Controller
{
    private readonly IFuncionarioRepository funcionarioRepository;
    private readonly ICargoRepository cargoRepository;
    private readonly IUserService userService;

    public FuncionariosController(IFuncionarioRepository funcionarioRepository, ICargoRepository cargoRepository, IUserService userService)
    {
        this.funcionarioRepository = funcionarioRepository;
        this.cargoRepository = cargoRepository;
        this.userService = userService;
    }

    public IActionResult Index(string? search)
    {
        var funcionarios = funcionarioRepository.GetAll();

        if (!string.IsNullOrEmpty(search))
            funcionarios = funcionarios.Where(funcionario => funcionario.Nome.Contains(search, StringComparison.CurrentCultureIgnoreCase)).ToList();
        return View(funcionarios);
    }

    public IActionResult Create()
    {
        ViewBag.Cargos = cargoRepository.GetAll().Select(cargo => new SelectListItem { Text = cargo.Nome, Value = cargo.Id.ToString() }).ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Create(IFormCollection collection)
    {
        var cargo = cargoRepository.Get(int.Parse(collection["Cargo"]));

        var funcionario = new Funcionario
        {
            Nome = collection["Nome"],
            Cpf = collection["Cpf"],
            Rg = collection["Rg"],
            Pix = collection["Pix"],
            Email = collection["Email"],
            Telefone = collection["Telefone"],
            Endereco = collection["Endereco"],
            Observacoes = collection["Observacoes"],
            Salario = decimal.Parse(collection["Salario"]),
            Cargo = cargo,
            Usuario = userService.CurrentUser
        };

        funcionarioRepository.Create(funcionario);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int id)
    {
        var funcionario = funcionarioRepository.Get(id);
        if (funcionario.Id > 0)
            return View(funcionario);
        return NotFound();
    }


    public IActionResult Edit(int id)
    {
        ViewBag.Cargos = cargoRepository.GetAll().Select(cargo => new SelectListItem { Text = cargo.Nome, Value = cargo.Id.ToString() }).ToList();

        var funcionario = funcionarioRepository.Get(id);
        if (funcionario.Id > 0)
            return View(funcionario);
        return NotFound();
    }

    [HttpPost]
    public IActionResult Edit(int id, IFormCollection collection)
    {
        var cargo = cargoRepository.Get(int.Parse(collection["Cargo"]));

        var funcionario = new Funcionario
        {
            Id = id,
            Nome = collection["Nome"],
            Cpf = collection["Cpf"],
            Rg = collection["Rg"],
            Pix = collection["Pix"],
            Email = collection["Email"],
            Telefone = collection["Telefone"],
            Endereco = collection["Endereco"],
            Observacoes = collection["Observacoes"],
            Salario = decimal.Parse(collection["Salario"]),
            Cargo = cargo,
            Usuario = userService.CurrentUser
        };

        funcionarioRepository.Update(funcionario);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var funcionario = funcionarioRepository.Get(id);
        if (funcionario.Id > 0)
            return View(funcionario);
        return NotFound();
    }

    [HttpPost]
    public IActionResult Delete(int id, IFormCollection collection)
    {
        var funcionario = new Funcionario
        {
            Id = id,
        };

        funcionarioRepository.Delete(funcionario);
        return RedirectToAction(nameof(Index));
    }
}
