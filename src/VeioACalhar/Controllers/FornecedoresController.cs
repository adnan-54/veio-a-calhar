using Microsoft.AspNetCore.Mvc;
using VeioACalhar.Models;
using VeioACalhar.Repositories;

namespace VeioACalhar.Controllers;

public class FornecedoresController : Controller
{
    private readonly IFornecedorRepository fornecedorRepository;

    public FornecedoresController(IFornecedorRepository fornecedorRepository)
    {
        this.fornecedorRepository = fornecedorRepository;
    }

    public IActionResult Index(string? search)
    {
        var fornecedores = fornecedorRepository.GetAll();

        if (!string.IsNullOrEmpty(search))
            fornecedores = fornecedores.Where(x => x.Nome.Contains(search, StringComparison.CurrentCultureIgnoreCase)).ToList();

        return View(fornecedores);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(IFormCollection collection)
    {
        try
        {
            var fornecedor = new Fornecedor
            {
                Nome = collection["Nome"],
                NomeFantasia = collection["NomeFantasia"],
                InscricaoEstadual = collection["InscricaoEstadual"],
                Cnpj = collection["Cnpj"],
                Pix = collection["Pix"],
                Email = collection["Email"],
                Observacoes = collection["Observacoes"],
                Telefone = collection["Telefone"],
                Endereco = collection["Endereco"]
            };

            fornecedorRepository.Create(fornecedor);

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public IActionResult Details(int id)
    {
        var fornecedor = fornecedorRepository.Get(id);
        if (fornecedor.Id > 0)
            return View(fornecedor);
        return View("NotFound");
    }

    public IActionResult Edit(int id)
    {
        var fornecedor = fornecedorRepository.Get(id);
        if (fornecedor.Id > 0)
            return View(fornecedor);
        return View("NotFound");
    }

    [HttpPost]
    public IActionResult Edit(int id, IFormCollection collection)
    {

        var fornecedor = new Fornecedor
        {
            Id = id,
            Nome = collection["Nome"],
            NomeFantasia = collection["NomeFantasia"],
            InscricaoEstadual = collection["InscricaoEstadual"],
            Cnpj = collection["Cnpj"],
            Pix = collection["Pix"],
            Email = collection["Email"],
            Observacoes = collection["Observacoes"],
            Telefone = collection["Telefone"],
            Endereco = collection["Endereco"]
        };

        fornecedorRepository.Update(fornecedor);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var fornecedor = fornecedorRepository.Get(id);
        if (fornecedor.Id > 0)
            return View(fornecedor);
        return View("NotFound");
    }

    [HttpPost]
    public IActionResult Delete(int id, IFormCollection collection)
    {
        var fornecedor = new Fornecedor
        {
            Id = id
        };

        fornecedorRepository.Delete(fornecedor);

        return RedirectToAction(nameof(Index));
    }
}
