namespace VeioACalhar.Models;

public class Funcionario : PessoaFisica
{
    public Cargo? Cargo { get; init; }

    public Usuario? Usuario { get; init; }

    public decimal Salario { get; init; }
}