namespace VeioACalhar.Models;

public record Venda : Transacao
{
    public Venda()
    {
        Clientes = Enumerable.Empty<Cliente>();
        Funcionarios = Enumerable.Empty<Funcionario>();
    }

    public DateOnly? PrevisaoInicio { get; init; }

    public DateOnly? PrevisaoEntrega { get; init; }

    public IEnumerable<Cliente> Clientes { get; init; }

    public IEnumerable<Funcionario> Funcionarios { get; init; }

    public decimal ValorTotal => Produtos.Sum(p => p.ValorTotal);
}
