using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VeioACalhar.Models;
using VeioACalhar.Repositories;
using VeioACalhar.Services;
using VeioACalhar.ViewModels;

namespace VeioACalhar.Controllers;

public class VendasController : Controller
{
    private readonly IVendaRepository vendaRepository;
    private readonly IClienteRepository clienteRepository;
    private readonly IFuncionarioRepository funcionarioRepository;
    private readonly IProdutoRepository produtoRepository;
    private readonly IFormaPagamentoRepository formaPagamentoRepository;
    private readonly IPagamentoRepository pagamentoRepository;
    private readonly IUserService userService;

    public VendasController(IVendaRepository vendaRepository, IClienteRepository clienteRepository, IFuncionarioRepository funcionarioRepository, IProdutoRepository produtoRepository, IFormaPagamentoRepository formaPagamentoRepository, IPagamentoRepository pagamentoRepository, IUserService userService)
    {
        this.vendaRepository = vendaRepository;
        this.clienteRepository = clienteRepository;
        this.funcionarioRepository = funcionarioRepository;
        this.produtoRepository = produtoRepository;
        this.formaPagamentoRepository = formaPagamentoRepository;
        this.pagamentoRepository = pagamentoRepository;
        this.userService = userService;
    }

    public IActionResult Index(string? search = null, string? status = null)
    {
        IEnumerable<Venda> vendas = vendaRepository.GetAll().OrderBy(v => v.Status.Id);

        if (!string.IsNullOrEmpty(search))
            vendas = vendas.Where(v => v.ToString().Contains(search, StringComparison.InvariantCultureIgnoreCase));

        if (!string.IsNullOrEmpty(status))
        {
            vendas = status.ToLower() switch
            {
                "orcamento" => vendas.Where(v => v.Status.Id == 1),
                "emaberto" => vendas.Where(v => v.Status.Id == 2),
                "finalizada" => vendas.Where(v => v.Status.Id == 3),
                "cancelada" => vendas.Where(v => v.Status.Id == 4),
                _ => vendas
            };
        }

        return View(vendas);
    }

    public IActionResult Create()
    {
        var venda = vendaRepository.Create(new());
        return RedirectToAction(nameof(Edit), new { id = venda.Id });
    }

    public IActionResult Edit(int id)
    {
        var venda = vendaRepository.Get(id);

        if (venda.Id == 0)
            return NotFound();

        return View(venda);
    }

    [HttpPost]
    public IActionResult Edit(IFormCollection form)
    {
        var venda = vendaRepository.Get(int.Parse(form["Id"]));
        if (venda.Id == 0)
            return NotFound();

        venda = venda with
        {
            Observacoes = form["Observacoes"]
        };

        if (DateOnly.TryParse(form["PrevisaoInicio"], out var date))
            venda = venda with { PrevisaoInicio = date };
        if (DateOnly.TryParse(form["PrevisaoEntrega"], out date))
            venda = venda with { PrevisaoEntrega = date };

        vendaRepository.Update(venda);

        return RedirectToAction(nameof(Edit), new { id = venda.Id });
    }

    public IActionResult Delete(int id)
    {
        vendaRepository.Delete(new() { Id = id });
        return RedirectToAction(nameof(Index));
    }

    public IActionResult AddCliente(int id, string? search = null)
    {
        var venda = vendaRepository.Get(id);

        if (venda.Id == 0)
            return NotFound();

        var clientes = clienteRepository.GetAll().Except(venda.Clientes);

        if (!string.IsNullOrEmpty(search))
            clientes = clientes.Where(c => c.Nome.Contains(search, StringComparison.CurrentCultureIgnoreCase));

        ViewBag.Venda = venda;

        return View(clientes);
    }

    public IActionResult AddClienteTo(int idVenda, int idCliente)
    {
        var venda = vendaRepository.Get(idVenda);
        if (venda.Id == 0)
            return NotFound();

        var cliente = clienteRepository.Get(idCliente);
        if (cliente.Id == 0)
            return NotFound();

        var clientes = new List<Cliente>(venda.Clientes)
        {
            cliente
        };

        venda = venda with { Clientes = clientes };

        vendaRepository.Update(venda);

        return RedirectToAction(nameof(Edit), new { id = venda.Id });
    }

    public IActionResult RemoveClienteFrom(int idVenda, int idCliente)
    {
        var venda = vendaRepository.Get(idVenda);
        if (venda.Id == 0)
            return NotFound();

        var cliente = clienteRepository.Get(idCliente);
        if (cliente.Id == 0)
            return NotFound();

        var clientes = venda.Clientes.Where(c => c.Id != cliente.Id);

        venda = venda with { Clientes = clientes };

        vendaRepository.Update(venda);

        return RedirectToAction(nameof(Edit), new { id = venda.Id });
    }

    public IActionResult AddFuncionario(int id, string? search = null)
    {
        var venda = vendaRepository.Get(id);

        if (venda.Id == 0)
            return NotFound();

        var funcionarios = funcionarioRepository.GetAll().Except(venda.Funcionarios);

        if (!string.IsNullOrEmpty(search))
            funcionarios = funcionarios.Where(c => c.Nome.Contains(search, StringComparison.CurrentCultureIgnoreCase));

        ViewBag.Venda = venda;

        return View(funcionarios);
    }

    public IActionResult AddFuncionarioTo(int idVenda, int idFuncionario)
    {
        var venda = vendaRepository.Get(idVenda);
        if (venda.Id == 0)
            return NotFound();

        var funcionario = funcionarioRepository.Get(idFuncionario);
        if (funcionario.Id == 0)
            return NotFound();

        var funcioarios = new List<Funcionario>(venda.Funcionarios)
        {
            funcionario
        };

        venda = venda with { Funcionarios = funcioarios };

        vendaRepository.Update(venda);

        return RedirectToAction(nameof(Edit), new { id = venda.Id });
    }

    public IActionResult RemoveFuncionarioFrom(int idVenda, int idFuncionario)
    {
        var venda = vendaRepository.Get(idVenda);
        if (venda.Id == 0)
            return NotFound();

        var funcionario = funcionarioRepository.Get(idFuncionario);
        if (funcionario.Id == 0)
            return NotFound();

        var funcioarios = venda.Funcionarios.Where(f => f.Id != funcionario.Id);

        venda = venda with { Funcionarios = funcioarios };

        vendaRepository.Update(venda);

        return RedirectToAction(nameof(Edit), new { id = venda.Id });
    }

    public IActionResult AddProduto(int id, string? search = null)
    {
        var venda = vendaRepository.Get(id);

        if (venda.Id == 0)
            return NotFound();

        var produtos = produtoRepository.GetAll().Except(venda.Produtos.Select(p => p.Produto));

        if (!string.IsNullOrEmpty(search))
            produtos = produtos.Where(c => c.Nome.Contains(search, StringComparison.CurrentCultureIgnoreCase));

        ViewBag.Venda = venda;

        return View(produtos);
    }

    public IActionResult AddProdutoTo(int idVenda, int idProduto)
    {
        var venda = vendaRepository.Get(idVenda);
        if (venda.Id == 0)
            return NotFound();

        var produto = produtoRepository.Get(idProduto);
        if (produto.Id == 0)
            return NotFound();

        var produtoTransacao = new ProdutoTransacao
        {
            Produto = produto,
            ValorUnitario = produto.PrecoVenda
        };

        ViewBag.Venda = venda;

        return View(nameof(EditProduto), produtoTransacao);
    }

    public IActionResult EditProduto(int idVenda, int idProduto)
    {
        var venda = vendaRepository.Get(idVenda);
        if (venda.Id == 0)
            return NotFound();

        var produto = venda.Produtos.Single(p => p.Id == idProduto);

        ViewBag.Venda = venda;

        return View(produto);
    }

    [HttpPost]
    public IActionResult EditProduto(IFormCollection values)
    {
        var venda = vendaRepository.Get(int.Parse(values["IdVenda"]));
        if (venda.Id == 0)
            return NotFound();

        var produto = produtoRepository.Get(int.Parse(values["IdProduto"]));
        if (produto.Id == 0)
            return NotFound();

        var produtoTrasacao = new ProdutoTransacao
        {
            Id = int.Parse(values["Id"]),
            Produto = produto,
            DescontoUnitario = int.Parse(values["DescontoUnitario"]),
            Quantidade = decimal.Parse(values["Quantidade"]),
            ValorUnitario = decimal.Parse(values["ValorUnitario"])
        };

        var produtos = new List<ProdutoTransacao>(venda.Produtos);

        if (produtoTrasacao.Id != 0)
        {
            var target = produtos.Single(p => p.Id == produtoTrasacao.Id);
            produtos.Remove(target);
        }

        produtos.Add(produtoTrasacao);

        venda = venda with { Produtos = produtos };

        vendaRepository.Update(venda);

        return RedirectToAction(nameof(Edit), new { id = venda.Id });
    }

    public IActionResult RemoveProdutoFrom(int idVenda, int idProduto)
    {
        var venda = vendaRepository.Get(idVenda);
        if (venda.Id == 0)
            return NotFound();

        var produtos = venda.Produtos.Where(p => p.Id != idProduto);

        venda = venda with { Produtos = produtos };

        vendaRepository.Update(venda);

        return RedirectToAction(nameof(Edit), new { id = venda.Id });
    }

    public IActionResult AbrirOrdem(int id)
    {
        var venda = vendaRepository.Get(id);
        if (venda.Id == 0)
            return NotFound();

        venda = venda with { Status = new() { Id = 2 } };

        vendaRepository.Update(venda);

        foreach (var produto in venda.Produtos)
        {
            var produtoBase = produto.Produto;
            var quantidade = produtoBase.Quantidade - produto.Quantidade;
            produtoBase = produtoBase with { Quantidade = quantidade };

            produtoRepository.Update(produtoBase);
        }

        return RedirectToAction(nameof(Edit), new { id });
    }

    public IActionResult FinalizarOrdem(int id)
    {
        var venda = vendaRepository.Get(id);
        if (venda.Id == 0)
            return NotFound();

        ViewBag.Venda = venda;

        var viewModel = new FinalizarVendaViewModel
        {
            Pagadores = venda.Clientes.Select(c => new SelectListItem(c.Nome, c.Id.ToString())).ToList(),
            FormasPagamento = formaPagamentoRepository.GetAll().Select(p => new SelectListItem(p.Nome, p.Id.ToString())).ToList(),
            ValorRecebido = venda.ValorTotal
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult FinalizarOrdem(IFormCollection form)
    {
        var venda = vendaRepository.Get(int.Parse(form["idVenda"]));
        if (venda.Id == 0)
            return NotFound();
        if (venda.Status.Id == 3)
            throw new InvalidOperationException("Uma venda finalizada não pode ser finalizada novamente");

        var today = DateOnly.FromDateTime(DateTime.Today);
        var favorecido = funcionarioRepository.GetAll().First(f => f.Usuario == userService.CurrentUser);
        var pagador = clienteRepository.Get(int.Parse(form["Pagador"]));
        var formaPagamento = new FormaPagamento { Id = int.Parse(form["FormaPagamento"]) };
        var valorRecebido = decimal.Parse(form["ValorRecebido"]);

        venda = venda with { DataFechamento = today, Status = new() { Id = 3 } };

        vendaRepository.Update(venda);

        var parcela = new Parcela
        {
            Numero = 1,
            Valor = valorRecebido,
            PorcentagemDesconto = 0,
            ValorPago = valorRecebido,
            DataVencimento = today,
            DataPagamento = today
        };

        var pagamento = new Pagamento
        {
            Transacao = venda,
            Pagador = pagador,
            Favorecido = favorecido,
            FormaPagamento = formaPagamento,
            Parcelas = new List<Parcela>() { parcela }
        };

        pagamentoRepository.Create(pagamento);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult CancelarOrdem(int id)
    {
        var venda = vendaRepository.Get(id);
        if (venda.Id == 0)
            return NotFound();

        if (venda.Status.Id == 2)
        {
            foreach (var produto in venda.Produtos)
            {
                var produtoBase = produto.Produto;
                var quantidade = produtoBase.Quantidade + produto.Quantidade;
                produtoBase = produtoBase with { Quantidade = quantidade };

                produtoRepository.Update(produtoBase);
            }
        }

        venda = venda with { Status = new() { Id = 4 } };
        vendaRepository.Update(venda);

        return RedirectToAction(nameof(Edit), new { id });
    }
}
