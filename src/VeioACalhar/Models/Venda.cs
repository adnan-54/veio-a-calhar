namespace VeioACalhar.Models;

public record Venda : Transacao
{
    public Venda()
    {
        Clientes = Enumerable.Empty<Pessoa>();
        Funcionarios = Enumerable.Empty<Funcionario>();
    }

    public DateOnly PrevisaoInicio { get; init; }

    public DateOnly PrevisaoEntrega { get; init; }

    public IEnumerable<Pessoa> Clientes { get; init; }

    public IEnumerable<Funcionario> Funcionarios { get; init; }
}
