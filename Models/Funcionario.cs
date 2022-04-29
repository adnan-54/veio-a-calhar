namespace VeioACalhar.Models;

public class Funcionario : PessoaFisica
{
    public Cargo? Cargo { get; init; }

    public long Salario { get; init; }
}