namespace VeioACalhar.Models;

public class Venda : Transacao
{
    public DateOnly PrevisaoInicio { get; set; }

    public DateOnly PrevisaoEntrega { get; set; }

    public IEnumerable<Pessoa>? Clientes { get; set; }

    public IEnumerable<Funcionario>? Funcionarios { get; set; }
}
