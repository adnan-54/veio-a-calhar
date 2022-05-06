namespace VeioACalhar.Models;

public class Venda : Transacao
{
    public DateOnly PrevisaoInicio { get; init; }

    public DateOnly PrevisaoEntrega { get; init; }

    public IEnumerable<Pessoa>? Clientes { get; init; }

    public IEnumerable<Funcionario>? Funcionarios { get; init; }
}
