using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeioACalhar.Models;
using VeioACalhar.Repositories;

namespace VeioACalhar.Controllers;

public class ProdutosController : Controller
{
    private readonly IProdutoRepository produtoRepository;
    private readonly IFornecedorRepository fornecedorRepository;
    private readonly IUnidadeRepository unidadeRepository;

    public ProdutosController(IProdutoRepository produtoRepository, IFornecedorRepository fornecedorRepository, IUnidadeRepository unidadeRepository)
    {
        this.produtoRepository = produtoRepository;
        this.fornecedorRepository = fornecedorRepository;
        this.unidadeRepository = unidadeRepository;
    }

    public IActionResult Index(string? search)
    {
        var produtos = produtoRepository.GetAll();

        if (!string.IsNullOrEmpty(search))
            produtos = produtos.Where(produto => produto.Nome.Contains(search, StringComparison.CurrentCultureIgnoreCase)).ToList();
        return View(produtos);
    }

    public IActionResult Create()
    {
        ViewBag.Unidades = unidadeRepository.GetAll().Select(unidade => new SelectListItem { Text = unidade.Nome, Value = unidade.Id.ToString() }).ToList();
        ViewBag.Fornecedores = fornecedorRepository.GetAll().Select(fornecedor => new SelectListItem { Text = fornecedor.Nome, Value = fornecedor.Id.ToString() }).ToList();

        return View();
    }

    [HttpPost]
    public IActionResult Create(IFormCollection collection)
    {
        var produto = new Produto
        {
            Nome = collection["Nome"],
            Descricao = collection["Descricao"],
            PrecoCusto = decimal.Parse(collection["PrecoCusto"]),
            PrecoVenda = decimal.Parse(collection["PrecoVenda"]),
            Quantidade = decimal.Parse(collection["Quantidade"]),
            Unidade = unidadeRepository.Get(int.Parse(collection["Unidade"])),
            Fornecedor = fornecedorRepository.Get(int.Parse(collection["Fornecedor"]))
        };

        produtoRepository.Create(produto);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Details(int id)
    {
        var produto = produtoRepository.Get(id);
        if (produto.Id > 0)
            return View(produto);
        return NotFound();
    }


    public IActionResult Edit(int id)
    {
        ViewBag.Unidades = unidadeRepository.GetAll().Select(unidade => new SelectListItem { Text = unidade.Nome, Value = unidade.Id.ToString() }).ToList();
        ViewBag.Fornecedores = fornecedorRepository.GetAll().Select(fornecedor => new SelectListItem { Text = fornecedor.Nome, Value = fornecedor.Id.ToString() }).ToList();

        var produto = produtoRepository.Get(id);
        if (produto.Id > 0)
            return View(produto);
        return NotFound();
    }

    [HttpPost]
    public IActionResult Edit(int id, IFormCollection collection)
    {
        var produto = new Produto
        {
            Id = id,
            Nome = collection["Nome"],
            Descricao = collection["Descricao"],
            PrecoCusto = decimal.Parse(collection["PrecoCusto"]),
            PrecoVenda = decimal.Parse(collection["PrecoVenda"]),
            Quantidade = decimal.Parse(collection["Quantidade"]),
            Unidade = unidadeRepository.Get(int.Parse(collection["Unidade"])),
            Fornecedor = fornecedorRepository.Get(int.Parse(collection["Fornecedor"]))
        };

        produtoRepository.Update(produto);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var produto = produtoRepository.Get(id);
        if (produto.Id > 0)
            return View(produto);
        return NotFound();
    }

    [HttpPost]
    public IActionResult Delete(int id, IFormCollection collection)
    {
        var produto = new Produto
        {
            Id = id,
        };

        produtoRepository.Delete(produto);
        return RedirectToAction(nameof(Index));
    }
}
