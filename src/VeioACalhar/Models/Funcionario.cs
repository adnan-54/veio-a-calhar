using System.ComponentModel.DataAnnotations;

namespace VeioACalhar.Models;

public record Funcionario : PessoaFisica
{
    public Funcionario()
    {
        Cargo = new();
        Usuario = new();
    }

    public Cargo Cargo { get; init; }

    public Usuario Usuario { get; init; }

    [DataType(DataType.Currency)]
    [Display(Name = "Salário")]
    public decimal Salario { get; init; }
}
