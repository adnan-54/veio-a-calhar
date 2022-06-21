using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public record Venda : Transacao
{
    public Venda()
    {
        Clientes = Enumerable.Empty<Cliente>();
        Funcionarios = Enumerable.Empty<Funcionario>();
    }

    [DataType(DataType.Date)]
    [Display(Name = "Previsão de Inicio")]
    public DateOnly? PrevisaoInicio { get; init; }

    [DataType(DataType.Date)]
    [Display(Name = "Previsão de Entrega")]
    public DateOnly? PrevisaoEntrega { get; init; }

    [Display(Name = "Clientes")]
    public IEnumerable<Cliente> Clientes { get; init; }

    [Display(Name = "Funcionarios")]
    public IEnumerable<Funcionario> Funcionarios { get; init; }
}
